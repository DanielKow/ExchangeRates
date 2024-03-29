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

    public async Task<IImmutableList<ExchangeRate>> GetExchangeRatesAsync()
    {
        try
        {
            var exchangeRates = await ConcreteGetExchangeRate();

            if (!exchangeRates.Any() && _next != null)
            {
                return await _next.GetExchangeRatesAsync();
            }

            return exchangeRates;
        }
        catch (Exception ex)
        {
            Logger.LogWarning("There was error during getting exchange rates in chain of responsibility: {Error}", ex);

            if (_next != null)
            {
                return await _next.GetExchangeRatesAsync();
            }

            return ImmutableList<ExchangeRate>.Empty;
        }
    }

    public void SetNext(IGetExchangeRatesChainLink nextChainLink)
    {
        _next = nextChainLink;
    }

    protected abstract Task<IImmutableList<ExchangeRate>> ConcreteGetExchangeRate();
}