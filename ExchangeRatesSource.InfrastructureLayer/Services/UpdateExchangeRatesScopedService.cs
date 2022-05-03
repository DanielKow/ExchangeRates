using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.ApplicationLayer.CalculateDelay;
using ExchangeRatesSource.ApplicationLayer.Services;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ExchangeRatesSource.InfrastructureLayer.Services;

public class UpdateExchangeRatesScopedService : IUpdateExchangeRatesScopedService
{
    private readonly IExchangeRatesSource _exchangeRatesSource;
    private readonly ILastUpdateDateCache _cache;
    private readonly IExchangeRatesUnitOfWork _unitOfWork;
    private readonly ICalculateDelayStrategy _calculateDelayStrategy;
    private readonly ILogger<UpdateExchangeRatesScopedService> _logger;
    private readonly string _type;

    private const int OneHourDelay = 60 * 60 * 1_000;
    private const int FiveMinutesDelay = 5 * 60 * 1_000;

    public UpdateExchangeRatesScopedService(
        IExchangeRatesSource exchangeRatesSource,
        ILastUpdateDateCache cache,
        IExchangeRatesUnitOfWork unitOfWork,
        ICalculateDelayStrategyFactory calculateDelayStrategyFactory,
        ILogger<UpdateExchangeRatesScopedService> logger,
        IConfiguration configuration)
    {
        _exchangeRatesSource = exchangeRatesSource;
        _cache = cache;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _type = configuration["ExchangeRateType"];
        _calculateDelayStrategy = calculateDelayStrategyFactory.GetStrategyForType(_type);
    }

    public async Task DoWork(CancellationToken stoppingToken)
    {
        int delay;
            
        try
        {
            bool cacheUpdated = await TryUpdate();
            delay = cacheUpdated ? _calculateDelayStrategy.CalculateDelay() : OneHourDelay;
        }
        catch (Exception ex)
        {
            _logger.Log(LogLevel.Warning, "There was error during updating cache: {Error}", ex);
            delay = FiveMinutesDelay;
        }

        _logger.Log(LogLevel.Debug, "Next update of exchange rates will be performed in {Delay} ms", delay);
        await Task.Delay(delay, stoppingToken);
    }
    
    private async Task<bool> TryUpdate()
    {
        DateOnly? lastUpdateDate = await _cache.GetAsync();

        if (lastUpdateDate == null)
        {
            return await TryUpdateNow();
        }

        if (!_calculateDelayStrategy.CheckIfActual(lastUpdateDate.Value))
        {
            return await TryUpdateIfNewer(lastUpdateDate.Value);
        }
        
        _logger.Log(LogLevel.Debug, "Exchange rates already actual for type {Type}", _type);
        return true;

    }

    private async Task<bool> TryUpdateNow()
    {
        GettingExchangeRatesResult result = await _exchangeRatesSource.GetExchangeRatesAsync(_type);

        if (result.Successfully == false)
        {
            _logger.Log(LogLevel.Warning, "Cannot get exchange rates from NBP for type {Type}", _type);
            return false;
        }

        await Update(result);

        return true;
    }
    
    private async Task<bool> TryUpdateIfNewer(DateOnly lastUpdateDate)
    {
        GettingExchangeRatesResult result = await _exchangeRatesSource.GetExchangeRatesAsync(_type);

        if (result.Successfully == false)
        {
            _logger.Log(LogLevel.Warning, "Cannot get exchange rates from NBP for type {Type}", _type);
            return false;
        }

        if (result.LastUpdateDate <= lastUpdateDate)
        {
            _logger.Log(LogLevel.Warning, "There is no new exchange rates from NBP for type {Type}", _type);
            return false;
        }

        await Update(result);

        _logger.Log(LogLevel.Debug, "Exchange rates updated in internal store for type {Type}", _type);
        return true;
    }

    private async Task Update(GettingExchangeRatesResult result)
    {
        await _unitOfWork.ExchangeRate.UpsertManyAsync(result.ExchangeRates);
        await _unitOfWork.SaveAsync();

        await _cache.SaveAsync(result.LastUpdateDate);
        _logger.Log(LogLevel.Debug, "Exchange rates updated for type {Type}", _type);
    }
}