using System.Text.Json;
using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace ExchangeRatesSource.InfrastructureLayer.Cache;

public class ExchangeRatesRedisCache : IGenericCache<ExchangeRate>
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger<ExchangeRatesRedisCache> _logger;
    private const string Key = "exchangeRates";

    public ExchangeRatesRedisCache(IConnectionMultiplexer redis, ILogger<ExchangeRatesRedisCache> logger)
    {
        _redis = redis;
        _logger = logger;
    }

    public async Task SaveManyAsync(IEnumerable<ExchangeRate> data)
    {
        var db = _redis.GetDatabase();

        var serializedData = JsonSerializer.Serialize(data);
        await db.StringSetAsync(Key, serializedData);
    }

    public async Task<IEnumerable<ExchangeRate>> GetAllAsync()
    {
        var db = _redis.GetDatabase();
        var serializedData = await db.StringGetAsync(Key);
        var data = JsonSerializer.Deserialize<IEnumerable<ExchangeRate>>(serializedData);

        if (data != null)
        {
            return data;
        }
        _logger.Log(LogLevel.Warning, "Cannot read exchange rates from cache");
        return Enumerable.Empty<ExchangeRate>();

    }
}