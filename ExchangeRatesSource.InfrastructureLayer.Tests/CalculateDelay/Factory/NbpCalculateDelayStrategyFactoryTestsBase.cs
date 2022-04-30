using ExchangeRatesSource.DomainLayer.Time;
using ExchangeRatesSource.InfrastructureLayer.CalculateDelay;
using Moq;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.Factory;

[Category("base")]
internal class NbpCalculateDelayStrategyFactoryTestsBase
{
    private Mock<ITimeProvider> _timeProviderMock = null!;

    [SetUp]
    public void SetUp()
    {
        _timeProviderMock = new Mock<ITimeProvider>();
    }

    protected NbpCalculateDelayStrategyFactory GetMockedFactory()
    {
        return new NbpCalculateDelayStrategyFactory(_timeProviderMock.Object);
    }
}