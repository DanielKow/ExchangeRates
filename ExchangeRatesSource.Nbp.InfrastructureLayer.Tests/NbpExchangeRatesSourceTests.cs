using System.Net;
using System.Threading.Tasks;
using ExchangeRatesSource.Nbp.DomainLayer;
using NUnit.Framework;

namespace ExchangeRatesSource.Nbp.InfrastructureLayer.Tests;

[TestFixture]
[Category("unit")]
internal class NbpExchangeRatesSourceTests : NbpExchangeRatesSourceTestsCases
{
    [TestCaseSource(nameof(SomeOfNotOkStatusCodes))]
    public async Task GetExchangeRatesAsync_should_return_empty_GettingExchangeRatesResult_when_request_was_not_successfully(HttpStatusCode code)
    {
        // Arrange
        SetUpNotOkStatusOfGettingExchangeRates(code);
        var source = GetMockedSource();
        
        // Act
        var result = await source.GetExchangeRatesAsync(NbpExchangeRatesTableType.A.Value);

        // Assert
        Assert_that_GettingExchangeRatesResult_is_empty(result);
    }

    [TestCaseSource(nameof(NotAcceptedExchangeRatesTypes))]
    public async Task GetExchangeRatesAsync_should_return_empty_GettingExchangeRatesResult_when_request_was_not_successfully(string type)
    {
        // Arrange
        SetUpResponseWithExchangeRatesTableFromNbp(TestCorrectResponseWithExchangeRatesTable);
        var source = GetMockedSource();
        
        // Act
        var result = await source.GetExchangeRatesAsync(type);

        // Assert
        Assert_that_GettingExchangeRatesResult_is_empty(result);
    }

    [Test]
    public async Task GetExchangeRatesAsync_should_return_empty_GettingExchangeRatesResult_when_content_was_empty()
    {
        // Arrange
        SetUpResponseWithExchangeRatesTableFromNbp(string.Empty);
        var source = GetMockedSource();
        
        // Act
        var result = await source.GetExchangeRatesAsync(NbpExchangeRatesTableType.A.Value);

        // Assert
        Assert_that_GettingExchangeRatesResult_is_empty(result);
    }

    [Test]
    public async Task GetExchangeRatesAsync_should_return_empty_GettingExchangeRatesResult_when_content_was_not_deserializable_to_exchange_rates_result()
    {
        // Arrange
        SetUpResponseWithExchangeRatesTableFromNbp(TestIncorrectResponse);
        var source = GetMockedSource();
        
        // Act
        var result = await source.GetExchangeRatesAsync(NbpExchangeRatesTableType.B.Value);

        // Assert
        Assert_that_GettingExchangeRatesResult_is_empty(result);
    }

    [Test]
    public async Task GetExchangeRatesAsync_should_return_GettingExchangeRatesResult_with_exchange_rates_when_content_was_deserializable_to_exchange_rates_result()
    {
        // Arrange
        SetUpResponseWithExchangeRatesTableFromNbp(TestCorrectResponseWithExchangeRatesTable);
        var source = GetMockedSource();
        
        // Act
        var result = await source.GetExchangeRatesAsync(NbpExchangeRatesTableType.A.Value);

        // Assert
        Assert_that_GettingExchangeRatesResults_have_same_values(result, TestGettingExchangeRatesResult);
    }
}