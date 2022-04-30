using ExchangeRatesSource.ApplicationLayer.CalculateDelay;
using ExchangeRatesSource.DomainLayer.Time;

namespace ExchangeRatesSource.InfrastructureLayer.CalculateDelay;

public class AtLeastNextWorkingWednesdayStrategy : ICalculateDelayStrategy
{
    private readonly ITimeProvider _timeProvider;
    private const int OneDayDelay = 24 * 60 * 60 * 1_000;

    public AtLeastNextWorkingWednesdayStrategy(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public int CalculateDelay()
    {
        var actualTime = _timeProvider.Now;
        var actualDayOfWeek = actualTime.DayOfWeek;
        var toNextWednesday = DayOfWeek.Wednesday - actualDayOfWeek;

        if (toNextWednesday < 1)
        {
            toNextWednesday += 7;
        }

        var delay = toNextWednesday * OneDayDelay;
        var afterDelay = actualTime.AddMilliseconds(delay);

        while (afterDelay.IsFreeDay())
        {
            delay += OneDayDelay;
            afterDelay = actualTime.AddMilliseconds(delay);
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

        var toFirstWednesdayAfterLastUpdate = DayOfWeek.Wednesday - lastUpdateDate.DayOfWeek;

        if (toFirstWednesdayAfterLastUpdate < 1)
        {
            toFirstWednesdayAfterLastUpdate += 7;
        }

        DateOnly nextUpdateDate = lastUpdateDate.AddDays(toFirstWednesdayAfterLastUpdate);
        while (nextUpdateDate.IsFreeDay())
        {
            nextUpdateDate = nextUpdateDate.AddDays(1);
        }

        return nextUpdateDate > actualDate;
    }
}