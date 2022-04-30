using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.ApplicationLayer.CalculateDelay;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExchangeRatesSource.InfrastructureLayer.Services;

public class UpdateExchangeRatesService : BackgroundService
{
    private IServiceProvider Services { get; }
    private IExchangeRatesSource ExchangeRatesSource { get; set; } = null!;
    private ILastUpdateDateCache Cache { get; set; } = null!;
    private IExchangeRatesUnitOfWork UnitOfWork { get; set; } = null!;
    private ICalculateDelayStrategy CalculateDelayStrategy { get; set; } = null!;
    
    private readonly ILogger<UpdateExchangeRatesService> _logger;
    private readonly string _type;
    
    private const int OneHourDelay = 60 * 60 * 1_000;

    public UpdateExchangeRatesService(
        IServiceProvider services,
        IConfiguration configuration, 
        ILogger<UpdateExchangeRatesService> logger)
    {
        Services = services;
        _logger = logger;
        _type = configuration["ExchangeRateType"];
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Log(LogLevel.Debug, "Updating exchange rates with type {Type} started", _type);

        using var scope = Services.CreateScope();
        ICalculateDelayStrategyFactory factory =
            scope.ServiceProvider.GetRequiredService<ICalculateDelayStrategyFactory>();

        CalculateDelayStrategy = factory.GetStrategyForType(_type);
        Cache = scope.ServiceProvider.GetRequiredService<ILastUpdateDateCache>();
        UnitOfWork = scope.ServiceProvider.GetRequiredService<IExchangeRatesUnitOfWork>();
        ExchangeRatesSource = scope.ServiceProvider.GetRequiredService<IExchangeRatesSource>();
        
        while (!stoppingToken.IsCancellationRequested)
        {
            int delay = OneHourDelay;
            
            try
            {
                bool cacheUpdated = await TryUpdateCache();
                delay = cacheUpdated ? CalculateDelayStrategy.CalculateDelay() : OneHourDelay;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Debug, "There was error during updating cache: {Error}", ex);
                delay = OneHourDelay;
            }

            await Task.Delay(delay, stoppingToken);
        }
    }

    private async Task<bool> TryUpdateCache()
    {
        DateOnly? lastUpdateDate = await Cache.GetAsync();

        if (lastUpdateDate == null)
        {
            return await TryUpdateCacheNow();
        }

        if (!CalculateDelayStrategy.CheckIfActual(lastUpdateDate.Value))
        {
            return await TryUpdateCacheIfNewer(lastUpdateDate.Value);
        }
        
        _logger.Log(LogLevel.Debug, "Exchange rates already actual for type {Type}", _type);
        return true;

    }

    private async Task<bool> TryUpdateCacheNow()
    {
        GettingExchangeRatesResult result = await ExchangeRatesSource.GetExchangeRatesAsync(_type);

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
        GettingExchangeRatesResult result = await ExchangeRatesSource.GetExchangeRatesAsync(_type);

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
        await UnitOfWork.ExchangeRate.UpsertManyAsync(result.ExchangeRates);
        await UnitOfWork.SaveAsync();

        await Cache.SaveAsync(result.LastUpdateDate);
        _logger.Log(LogLevel.Debug, "Exchange rates updated for type {Type}", _type);
    }
}