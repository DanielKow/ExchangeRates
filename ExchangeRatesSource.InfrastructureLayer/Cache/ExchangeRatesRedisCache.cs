using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer;

namespace ExchangeRatesSource.InfrastructureLayer.Cache;

public class ExchangeRatesRedisCache : IGenericCache<ExchangeRate>
{
    public Task SaveManyAsync(IEnumerable<ExchangeRate> data)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ExchangeRate>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}