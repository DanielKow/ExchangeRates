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
            int delay = cacheUpdated ? _calculateDelayStrategy.CalculateDelay() : OneHourDelay;
            await Task.Delay(delay, stoppingToken);
        }
    }

    private async Task<bool> TryUpdateCache()
    {
        DateOnly? lastUpdateDate = await _cache.GetAsync();

        if (lastUpdateDate == null)
        {
            return await TryUpdateCacheNow();
        }

        if (_calculateDelayStrategy.CheckIfActual(lastUpdateDate.Value))
        {
            return true;
        }

        return await TryUpdateCacheIfNewer(lastUpdateDate.Value);
    }

    private async Task<bool> TryUpdateCacheNow()
    {
        GettingExchangeRatesResult result = await _exchangeRatesSource.GetExchangeRatesAsync(_type);

        if (result.Successfully == false)
        {
            return false;
        }

        await UpdateCache(result);

        return true;
    }
    
    private async Task<bool> TryUpdateCacheIfNewer(DateOnly lastUpdateDate)
    {
        GettingExchangeRatesResult result = await _exchangeRatesSource.GetExchangeRatesAsync(_type);

        if (result.Successfully == false)
        {
            return false;
        }

        if (result.LastUpdateDate <= lastUpdateDate)
        {
            return false;
        }

        await UpdateCache(result);

        return true;
    }

    private async Task UpdateCache(GettingExchangeRatesResult result)
    {
        await _unitOfWork.ExchangeRateType.UpsertManyAsync(result.ExchangeRates);
        await _unitOfWork.SaveAsync();

        await _cache.SaveAsync(result.LastUpdateDate);
    }
}