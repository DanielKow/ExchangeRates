using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Hosting;

namespace ExchangeRatesSource.InfrastructureLayer.Services;

public class UpdateExchangeRatesCacheService : BackgroundService
{
    private readonly IExchangeRatesSource _exchangeRatesSource;
    private readonly IGenericCache<ExchangeRate> _cache;
    private readonly IExchangeRatesUnitOfWork _unitOfWork;
    private const int Delay = 60 * 60 * 1_000;

    public UpdateExchangeRatesCacheService(
        IExchangeRatesSource exchangeRatesSource,
        IGenericCache<ExchangeRate> cache,
        IExchangeRatesUnitOfWork unitOfWork)
    {
        _exchangeRatesSource = exchangeRatesSource;
        _cache = cache;
        _unitOfWork = unitOfWork;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var type = await _unitOfWork.ExchangeRateType.GetAsync("A");
            
            if (type == null)
            {
                await UpdateCache();
                continue;
            }

            var lastUpdateTime = type.LastUpdateDate;

            await Task.Delay(Delay, stoppingToken);
        }
    }

    private async Task UpdateCache()
    {
        var exchangeRates = await _exchangeRatesSource.GetExchangeRatesAsync("A");
        await _cache.SaveManyAsync(exchangeRates.ExchangeRates);
        await _unitOfWork.ExchangeRateType.UpsertAsync(new ExchangeRateType
        {
            Name = "A",
            LastUpdateDate = exchangeRates.LastUpdateDate
        });
        await _unitOfWork.SaveAsync();
    }
}