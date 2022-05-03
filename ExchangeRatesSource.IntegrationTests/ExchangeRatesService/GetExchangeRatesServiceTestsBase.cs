using ExchangeRatesSource.ApplicationLayer.Services;
using ExchangeRatesSource.DomainLayer;
using ExchangeRatesSource.InfrastructureLayer.Db;
using ExchangeRatesSource.InfrastructureLayer.Services;
using NUnit.Framework;

namespace ExchangeRatesSource.IntegrationTests.ExchangeRatesService;

[Category("integration_base")]
internal class GetExchangeRatesServiceTestsBase
{
    protected IGetExchangeRatesService PrepareService(ExchangeRateContext ctx)
    {
        var repository = new GenericRepository<ExchangeRate>(ctx);
        var unitOfWork = new ExchangeRatesUnitOfWork(ctx, repository);
        var service = new GetExchangeRatesService(unitOfWork);

        return service;
    }
}