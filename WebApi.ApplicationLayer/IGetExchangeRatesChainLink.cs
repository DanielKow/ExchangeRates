using System.Collections.Immutable;
using ExchangeRatesSource.DomainLayer;

namespace WebApi.ApplicationLayer;

public interface IGetExchangeRatesChainLink
{
    Task<IImmutableList<ExchangeRate>> GetExchangeRates();
    void SetNext(IGetExchangeRatesChainLink nextChainLink);
}