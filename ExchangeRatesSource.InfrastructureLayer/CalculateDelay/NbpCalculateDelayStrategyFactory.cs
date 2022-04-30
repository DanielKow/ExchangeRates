using ExchangeRatesSource.ApplicationLayer.CalculateDelay;
using ExchangeRatesSource.DomainLayer.Time;

namespace ExchangeRatesSource.InfrastructureLayer.CalculateDelay;

public class NbpCalculateDelayStrategyFactory : ICalculateDelayStrategyFactory
{
    private readonly ITimeProvider _timeProvider;

    public NbpCalculateDelayStrategyFactory(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public ICalculateDelayStrategy GetStrategyForType(string type)
    {
        return type switch
        {
            "A" => new NextWorkingDayStrategy(_timeProvider),
            "B" => new AtLeastNextWorkingWednesdayStrategy(_timeProvider),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Not recognized type of exchange rate for NBP.")
        };
    }
}