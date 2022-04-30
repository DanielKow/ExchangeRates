using System.Globalization;
using ExchangeRatesSource.ApplicationLayer;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace ExchangeRatesSource.InfrastructureLayer.Cache;

public class LastUpdateDateRedisCache : ILastUpdateDateCache
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger<LastUpdateDateRedisCache> _logger;
    private const string DateFormat = "dd.MM.yyyy";
    private const string Key = "LastUpdateDate";

    public LastUpdateDateRedisCache(IConnectionMultiplexer redis, ILogger<LastUpdateDateRedisCache> logger)
    {
        _redis = redis;
        _logger = logger;
    }

    public async Task SaveAsync(DateOnly lastUpdateDate)
    {
        var db = _redis.GetDatabase();

        string dateString = lastUpdateDate.ToString(DateFormat, CultureInfo.InvariantCulture);
        await db.StringSetAsync(Key, dateString);
    }

    public async Task<DateOnly?> GetAsync()
    {
        var db = _redis.GetDatabase();
        string data = await db.StringGetAsync(Key);

        if (DateOnly.TryParseExact(data, DateFormat, out var date))
        {
            return date;
        }
        
        _logger.Log(LogLevel.Warning, "Cannot get last update date from cache");
        return null;
    }
}