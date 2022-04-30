using System.Globalization;
using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace ExchangeRatesSource.InfrastructureLayer.Cache;

public class ExchangeRatesTypeRedisCache : IExchangeRatesTypeCache
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger<ExchangeRatesTypeRedisCache> _logger;
    private const string DateFormat = "dd.MM.yyyy";

    public ExchangeRatesTypeRedisCache(IConnectionMultiplexer redis, ILogger<ExchangeRatesTypeRedisCache> logger)
    {
        _redis = redis;
        _logger = logger;
    }

    public async Task SaveManyAsync(IEnumerable<ExchangeRateType> data)
    {
        var db = _redis.GetDatabase();

        foreach (var exchangeRateType in data)
        {
            string dateString = exchangeRateType.LastUpdateDate.ToString(DateFormat, CultureInfo.InvariantCulture);
            await db.StringSetAsync(exchangeRateType.Name, dateString);
        }
    }

    public async Task<DateOnly?> GetLastUpdateTimeAsync(string key)
    {
        var db = _redis.GetDatabase();
        string data = await db.StringGetAsync(key);

        if (DateOnly.TryParseExact(data, DateFormat, out var date))
        {
            return date;
        }
        
        _logger.Log(LogLevel.Warning, "Cannot get last update time for exchange rates type: {Type}", key);
        return null;
    }
    
}