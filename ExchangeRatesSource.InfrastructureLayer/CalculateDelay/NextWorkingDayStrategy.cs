using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer.Time;

namespace ExchangeRatesSource.InfrastructureLayer.CalculateDelay;

public class NextWorkingDayStrategy : ICalculateDelayStrategy
{
    private readonly ITimeProvider _timeProvider;
    private const int OneHourDelay = 60 * 60 * 1_000;
    private const int OneDayDelay = 24 * OneHourDelay;

    public NextWorkingDayStrategy(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public int CalculateDelay()
    {
        var actualTime = _timeProvider.Now;
        var currentHour = actualTime.Hour;
        var hoursToNextDay = 24 - currentHour;
        var delay = OneHourDelay * hoursToNextDay;

        var afterDelay = actualTime.AddMilliseconds(delay);

        while (afterDelay.IsFreeDay())
        {
            delay += OneDayDelay;
            afterDelay = actualTime.AddMilliseconds(delay);
        }
        
        return delay;
    }
}