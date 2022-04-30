using ExchangeRatesSource.InfrastructureLayer.CalculateDelay;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.EveryDay;

[Category("base")]
internal class EverydayStrategyTestsBase : CalculateDelayTestsBase
{
    protected EverydayStrategy GetMockedStrategy()
    {
        return new EverydayStrategy(GetTimeProvider());
    }
}