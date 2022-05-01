using System.Collections.Immutable;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Logging;
using WebApi.ApplicationLayer;

namespace WebApi.InfrastructureLayer.GetExchangeRatesChain;

public abstract class AbstractGetExchangeRatesChainLink : IGetExchangeRatesChainLink
{
    protected readonly ILogger<AbstractGetExchangeRatesChainLink> _logger;
    private IGetExchangeRatesChainLink? _next;

    protected AbstractGetExchangeRatesChainLink(ILogger<AbstractGetExchangeRatesChainLink> logger)
    {
        _logger = logger;
    }

    public async Task<IImmutableList<ExchangeRate>> GetExchangeRates()
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
            _logger.Log(LogLevel.Warning,
                "There was error during getting exchange rates in chain of responsibility: {Error}", ex);

            if (_next != null)
            {
                return await _next.GetExchangeRates();
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