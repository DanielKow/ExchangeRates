using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.ApplicationLayer.CalculateDelay;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExchangeRatesSource.InfrastructureLayer.Services;

public class UpdateExchangeRatesService : BackgroundService
{
    private readonly IExchangeRatesSource _exchangeRatesSource;
    private readonly ILastUpdateDateCache _cache;
    private readonly IExchangeRatesUnitOfWork _unitOfWork;
    private readonly ICalculateDelayStrategy _calculateDelayStrategy;
    private readonly ILogger<UpdateExchangeRatesService> _logger;
    private readonly string _type;
    private const int OneHourDelay = 60 * 60 * 1_000;

    public UpdateExchangeRatesService(
        IExchangeRatesSource exchangeRatesSource,
        ILastUpdateDateCache cache,
        IExchangeRatesUnitOfWork unitOfWork,
        ICalculateDelayStrategyFactory calculateDelayStrategyFactory,
        IConfiguration configuration, 
        ILogger<UpdateExchangeRatesService> logger)
    {
        _exchangeRatesSource = exchangeRatesSource;
        _cache = cache;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _type = configuration["ExchangeRateType"];
        _calculateDelayStrategy = calculateDelayStrategyFactory.GetStrategyForType(_type);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Log(LogLevel.Debug, "Updating exchange rates with type {Type} started", _type);
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

        if (!_calculateDelayStrategy.CheckIfActual(lastUpdateDate.Value))
        {
            return await TryUpdateCacheIfNewer(lastUpdateDate.Value);
        }
        
        _logger.Log(LogLevel.Debug, "Exchange rates already actual for type {Type}", _type);
        return true;

    }

    private async Task<bool> TryUpdateCacheNow()
    {
        GettingExchangeRatesResult result = await _exchangeRatesSource.GetExchangeRatesAsync(_type);

        if (result.Successfully == false)
        {
            _logger.Log(LogLevel.Debug, "Cannot get exchange rates from NBP for type {Type}", _type);
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
            _logger.Log(LogLevel.Debug, "Cannot get exchange rates from NBP for type {Type}", _type);
            return false;
        }

        if (result.LastUpdateDate <= lastUpdateDate)
        {
            _logger.Log(LogLevel.Debug, "There is no new exchange rates from NBP for type {Type}", _type);
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
        _logger.Log(LogLevel.Debug, "Exchange rates updated for type {Type}", _type);
    }
}