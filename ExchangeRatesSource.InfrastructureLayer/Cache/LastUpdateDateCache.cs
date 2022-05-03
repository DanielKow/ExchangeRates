using System.Globalization;
using ExchangeRatesSource.ApplicationLayer;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace ExchangeRatesSource.InfrastructureLayer.Cache;

public class LastUpdateDateCache : ILastUpdateDateCache
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<LastUpdateDateCache> _logger;
    private const string DateFormat = "dd.MM.yyyy";
    private const string Key = "LastUpdateDate";

    public LastUpdateDateCache(IDistributedCache cache, ILogger<LastUpdateDateCache> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task SaveAsync(DateOnly lastUpdateDate)
    {
        string dateString = lastUpdateDate.ToString(DateFormat, CultureInfo.InvariantCulture);
        await _cache.SetStringAsync(Key, dateString);
    }

    public async Task<DateOnly?> GetAsync()
    {
        string data = await _cache.GetStringAsync(Key);

        if (DateOnly.TryParseExact(data, DateFormat, out var date))
        {
            return date;
        }
        
        _logger.LogWarning("Cannot get last update date from cache");
        return null;
    }
}