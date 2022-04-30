using ExchangeRatesSource.InfrastructureLayer.CalculateDelay;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.AtLeastNextWorkingWednesday;

[Category("base")]
internal class AtLeastNextWorkingWednesdayStrategyTestsBase : CalculateDelayTestsBase
{
    protected AtLeastNextWorkingWednesdayStrategy GetMockedStrategy()
    {
        return new AtLeastNextWorkingWednesdayStrategy(GetTimeProvider());
    }
}