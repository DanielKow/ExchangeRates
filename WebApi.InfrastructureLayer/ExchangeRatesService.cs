using ExchangeRatesSource.DomainLayer;
using WebApi.ApplicationLayer;
using WebApi.ApplicationLayer.GetExchangeRatesChain;

namespace WebApi.InfrastructureLayer;

public class ExchangeRatesService : IExchangeRatesService
{
    private readonly IGetExchangeRatesChainLink _chain;

    public ExchangeRatesService(IGetExchangeRatesChainBuilder chainBuilder)
    {
        _chain = chainBuilder.BuildChain();
    }

    public async Task<IEnumerable<ExchangeRate>> GetExchangeRates()
    {
        var exchangeRates = await _chain.GetExchangeRates();
        var orderedExchangeRates = exchangeRates.OrderBy(row => row.Currency);
        return orderedExchangeRates;
    }
}