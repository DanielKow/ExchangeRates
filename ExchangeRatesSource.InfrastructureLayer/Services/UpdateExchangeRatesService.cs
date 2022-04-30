using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.ApplicationLayer.CalculateDelay;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace ExchangeRatesSource.InfrastructureLayer.Services;

public class UpdateExchangeRatesService : BackgroundService
{
    private readonly IExchangeRatesSource _exchangeRatesSource;
    private readonly ILastUpdateDateCache _cache;
    private readonly IExchangeRatesUnitOfWork _unitOfWork;
    private readonly ICalculateDelayStrategy _calculateDelayStrategy;
    private readonly string _type;
    private const int OneHourDelay = 60 * 60 * 1_000;

    public UpdateExchangeRatesService(
        IExchangeRatesSource exchangeRatesSource,
        ILastUpdateDateCache cache,
        IExchangeRatesUnitOfWork unitOfWork,
        ICalculateDelayStrategyFactory calculateDelayStrategyFactory,
        IConfiguration configuration)
    {
        _exchangeRatesSource = exchangeRatesSource;
        _cache = cache;
        _unitOfWork = unitOfWork;
        _type = configuration["ExchangeRateType"];
        _calculateDelayStrategy = calculateDelayStrategyFactory.GetStrategyForType(_type);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            bool cacheUpdated = await TryUpdateCache();
            await Task.Delay(OneHourDelay, stoppingToken);
        }
    }

    private async Task<bool> TryUpdateCache()
    {
        DateOnly? lastUpdateDate = await _cache.GetAsync();

        if (lastUpdateDate == null)
        {
            return await TryUpdateCacheNow();
        }

        return await TryUpdateCacheIfNewer(lastUpdateDate.Value);
    }

    private async Task<bool> TryUpdateCacheIfNewer(DateOnly lastUpdateDate)
    {
        GettingExchangeRatesResult results = await _exchangeRatesSource.GetExchangeRatesAsync(_type);

        if (results.Successfully == false)
        {
            return false;
        }

        return true;
    }

    private async Task<bool> TryUpdateCacheNow()
    {
        GettingExchangeRatesResult results = await _exchangeRatesSource.GetExchangeRatesAsync(_type);

        if (results.Successfully == false)
        {
            return false;
        }

        await _unitOfWork.ExchangeRateType.UpsertManyAsync(results.ExchangeRates);
        await _unitOfWork.SaveAsync();

        await _cache.SaveAsync(results.LastUpdateDate);

        return true;
    }
}