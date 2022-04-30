using ExchangeRatesSource.InfrastructureLayer.CalculateDelay;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.NextDay;

[Category("base")]
internal class NextWorkingDayStrategyTestsBase : CalculateDelayTestsBase
{
    protected NextWorkingDayStrategy GetMockedStrategy()
    {
        return new NextWorkingDayStrategy(GetTimeProvider());
    }
}