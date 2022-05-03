using System.Collections.Immutable;
using System.Reflection;
using System.Threading.Tasks;
using ExchangeRatesSource.DomainLayer;
using ExchangeRatesSource.InfrastructureLayer.Db;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace ExchangeRatesSource.IntegrationTests.ExchangeRatesService;

[TestFixture]
[Category("integration")]
internal class GetExchangeRatesServiceTests : GetExchangeRatesServiceTestsBase
{
    [Test]
    public async Task GetAll_should_return_exchange_rates_from_db_when_there_are_exchange_rates_in_db()
    {
        // Arrange
        DbContextOptionsBuilder<ExchangeRateContext> builder = new();
        builder.UseInMemoryDatabase(MethodBase.GetCurrentMethod()!.Name);

        var expectedExchangeRates = new[]
        {
            new ExchangeRate("EUR", 4.6765m),
            new ExchangeRate("CZK", 0.1892m),
            new ExchangeRate("USD", 4.4404m)
        };

        await using var ctx = new ExchangeRateContext(builder.Options);
        
        foreach (var exchangeRate in expectedExchangeRates)
        {
            ctx.Add(exchangeRate);
        }
        
        await ctx.SaveChangesAsync();

        var expected = ImmutableList.Create(expectedExchangeRates);
        var service = PrepareService(ctx);
        
        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.That(result, Is.EquivalentTo(expected));
    }
    
    [Test]
    public async Task GetAll_should_return_empty_list_when_db_is_empty()
    {
        // Arrange
        DbContextOptionsBuilder<ExchangeRateContext> builder = new();
        builder.UseInMemoryDatabase(MethodBase.GetCurrentMethod()!.Name);
        
        await using var ctx = new ExchangeRateContext(builder.Options);
        var service = PrepareService(ctx);
        
        // Act
        var result = await service.GetAllAsync();

        // Assert
        Assert.That(result, Is.Empty);
    }
}