using ExchangeRatesSource.DomainLayer;

namespace ExchangeRatesSource.ApplicationLayer;

public interface IExchangeRatesSource
{
    Task<GettingExchangeRatesResult> GetExchangeRatesAsync(string type);
}