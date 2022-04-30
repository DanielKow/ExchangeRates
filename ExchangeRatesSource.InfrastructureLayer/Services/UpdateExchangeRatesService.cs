using ExchangeRatesSource.ApplicationLayer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ExchangeRatesSource.InfrastructureLayer.Services;

public class UpdateExchangeRatesService : BackgroundService
{
    private readonly IServiceProvider _services;
    
    private readonly ILogger<UpdateExchangeRatesService> _logger;
    private readonly string _type;
    
    private const int OneHourDelay = 60 * 60 * 1_000;
    private const int FiveMinutesDelay = 5 * 60 * 1_000;

    public UpdateExchangeRatesService(
        IServiceProvider services,
        IConfiguration configuration, 
        ILogger<UpdateExchangeRatesService> logger)
    {
        _services = services;
        _logger = logger;
        _type = configuration["ExchangeRateType"];
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Log(LogLevel.Debug, "Updating exchange rates with type {Type} started", _type);

        using var scope = _services.CreateScope();
        IUpdateExchangeRatesScopedService scopedService =
            scope.ServiceProvider.GetRequiredService<IUpdateExchangeRatesScopedService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            await scopedService.DoWork(stoppingToken);
        }
    }
}