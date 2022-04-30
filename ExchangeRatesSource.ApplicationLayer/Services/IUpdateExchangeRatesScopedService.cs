namespace ExchangeRatesSource.ApplicationLayer.Services;

public interface IUpdateExchangeRatesScopedService
{
    Task DoWork(CancellationToken stoppingToken);
}