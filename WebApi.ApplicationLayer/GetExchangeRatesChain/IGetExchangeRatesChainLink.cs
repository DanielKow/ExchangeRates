using ExchangeRatesSource.DomainLayer;

namespace WebApi.ApplicationLayer.GetExchangeRatesChain;

public interface IGetExchangeRatesChainLink
{
    Task<ExchangeRate[]> GetExchangeRates();
    void SetNext(IGetExchangeRatesChainLink nextChainLink);
}