using ExchangeRatesSource.InfrastructureLayer.CalculateDelay;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.NextDay;

[Category("base")]
internal class NextDayStrategyTestsBase : CalculateDelayTestsBase
{
    protected NextWorkingDayStrategy GetMockedStrategy()
    {
        return new NextWorkingDayStrategy(GetTimeProvider());
    }
}