using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer.Time;

namespace ExchangeRatesSource.InfrastructureLayer.CalculateDelay;

public class EveryWednesdayStrategy : ICalculateDelayStrategy
{
    private readonly ITimeProvider _timeProvider;
    private const int OneDayDelay = 24 * 60 * 60 * 1_000;

    public EveryWednesdayStrategy(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public int CalculateDelay()
    {
        throw new NotImplementedException();
    }
}