using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Hosting;

namespace ExchangeRatesSource.InfrastructureLayer.Services;

public class UpdateExchangeRatesCacheService : BackgroundService
{
    private readonly IExchangeRatesSource _exchangeRatesSource;
    private readonly ILastUpdateDateCache _cache;
    private readonly IExchangeRatesUnitOfWork _unitOfWork;
    private const int OneHourDelay = 60 * 60 * 1_000;

    public UpdateExchangeRatesCacheService(
        IExchangeRatesSource exchangeRatesSource,
        ILastUpdateDateCache cache,
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
            await Task.Delay(OneHourDelay, stoppingToken);
        }
    }

}