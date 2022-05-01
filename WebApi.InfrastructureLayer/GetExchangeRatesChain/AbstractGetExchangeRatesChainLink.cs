using System.Collections.Immutable;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Logging;
using WebApi.ApplicationLayer;

namespace WebApi.InfrastructureLayer.GetExchangeRatesChain;

public abstract class AbstractGetExchangeRatesChainLink : IGetExchangeRatesChainLink
{
    private readonly ILogger<AbstractGetExchangeRatesChainLink> _logger;
    private IGetExchangeRatesChainLink? _next;

    protected AbstractGetExchangeRatesChainLink(ILogger<AbstractGetExchangeRatesChainLink> logger)
    {
        _logger = logger;
    }

    public async Task<IImmutableList<ExchangeRate>> GetExchangeRates()
    {
        try
        {
            return await ConcreteGetExchangeRate();
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