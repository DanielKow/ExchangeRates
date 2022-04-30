using ExchangeRatesSource.DomainLayer;

namespace ExchangeRatesSource.ApplicationLayer;

public interface IExchangeRatesTypeCache
{
    Task SaveManyAsync(IEnumerable<ExchangeRateType> data);
    Task<DateOnly?> GetLastUpdateTimeAsync(string key);
}