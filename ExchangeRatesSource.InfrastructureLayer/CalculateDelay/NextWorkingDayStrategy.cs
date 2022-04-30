using ExchangeRatesSource.ApplicationLayer.CalculateDelay;
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
        var actualDateTime = _timeProvider.Now;
        var currentHour = actualDateTime.Hour;
        var hoursToNextDay = 24 - currentHour;
        var delay = OneHourDelay * hoursToNextDay;

        var afterDelay = actualDateTime.AddMilliseconds(delay);

        while (afterDelay.IsFreeDay())
        {
            delay += OneDayDelay;
            afterDelay = actualDateTime.AddMilliseconds(delay);
        }
        
        return delay;
    }

    public bool CheckIfActual(DateOnly lastUpdateDate)
    {
        var actualDate = DateOnly.FromDateTime(_timeProvider.Now);

        if (actualDate == lastUpdateDate)
        {
            return true;
        }
        
        while (lastUpdateDate != actualDate)
        {
            lastUpdateDate = lastUpdateDate.AddDays(1);

            if (!lastUpdateDate.IsFreeDay())
            {
                return false;
            }
        }

        return true;
    }
}