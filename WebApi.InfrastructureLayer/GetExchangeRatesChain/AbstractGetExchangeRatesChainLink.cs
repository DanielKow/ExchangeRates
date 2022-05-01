using System.Collections.Immutable;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Logging;
using WebApi.ApplicationLayer.GetExchangeRatesChain;

namespace WebApi.InfrastructureLayer.GetExchangeRatesChain;

public abstract class AbstractGetExchangeRatesChainLink : IGetExchangeRatesChainLink
{
    protected readonly ILogger Logger;
    private IGetExchangeRatesChainLink? _next;

    protected AbstractGetExchangeRatesChainLink(ILogger logger)
    {
        Logger = logger;
    }

    public async Task<ExchangeRate[]> GetExchangeRates()
    {
        try
        {
            var exchangeRates = await ConcreteGetExchangeRate();

            if (!exchangeRates.Any() && _next != null)
            {
                return await _next.GetExchangeRates();
            }

            return exchangeRates;
        }
        catch (Exception ex)
        {
            Logger.Log(LogLevel.Warning,
                "There was error during getting exchange rates in chain of responsibility: {Error}", ex);

            if (_next != null)
            {
                return await _next.GetExchangeRates();
            }

            return Array.Empty<ExchangeRate>();
        }
    }

    public void SetNext(IGetExchangeRatesChainLink nextChainLink)
    {
        _next = nextChainLink;
    }

    protected abstract Task<ExchangeRate[]> ConcreteGetExchangeRate();
}