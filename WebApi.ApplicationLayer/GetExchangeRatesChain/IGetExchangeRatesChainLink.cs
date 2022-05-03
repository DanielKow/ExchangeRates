using System.Collections.Immutable;
using ExchangeRatesSource.DomainLayer;

namespace WebApi.ApplicationLayer.GetExchangeRatesChain;

public interface IGetExchangeRatesChainLink
{
    Task<IImmutableList<ExchangeRate>> GetExchangeRatesAsync();
    void SetNext(IGetExchangeRatesChainLink nextChainLink);
}