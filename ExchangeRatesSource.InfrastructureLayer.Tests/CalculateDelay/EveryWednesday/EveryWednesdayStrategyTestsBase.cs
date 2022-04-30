using ExchangeRatesSource.InfrastructureLayer.CalculateDelay;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.EveryWednesday;

[Category("base")]
internal class EveryWednesdayStrategyTestsBase : CalculateDelayTestsBase
{
    protected EveryWednesdayStrategy GetMockedStrategy()
    {
        return new EveryWednesdayStrategy(GetTimeProvider());
    }
}