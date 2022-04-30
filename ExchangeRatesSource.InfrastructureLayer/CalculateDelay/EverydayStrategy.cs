using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer.Time;

namespace ExchangeRatesSource.InfrastructureLayer.CalculateDelay;

public class EverydayStrategy : ICalculateDelayStrategy
{
    private readonly ITimeProvider _timeProvider;
    private const int OneHourDelay = 60 * 60 * 1_000;

    public EverydayStrategy(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public int CalculateDelay()
    {
        var currentHour = _timeProvider.Now.Hour;
        var hoursToNextDay = 24 - currentHour;
        return OneHourDelay * hoursToNextDay;
    }
}