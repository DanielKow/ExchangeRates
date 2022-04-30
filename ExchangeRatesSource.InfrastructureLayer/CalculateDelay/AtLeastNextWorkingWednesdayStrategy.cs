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
            toNextWednesday = 7 + toNextWednesday;
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
        throw new NotImplementedException();
    }
}