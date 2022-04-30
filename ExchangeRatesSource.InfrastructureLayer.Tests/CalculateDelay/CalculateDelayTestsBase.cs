using System;
using ExchangeRatesSource.DomainLayer.Time;
using Moq;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay;

[Category("base")]
internal class CalculateDelayTestsBase
{
    private Mock<ITimeProvider> _timeProviderMock = null!;

    [SetUp]
    public void SetUp()
    {
        _timeProviderMock = new Mock<ITimeProvider>();
    }

    protected void SetUpActualDateTime(DateTime time)
    {
        _timeProviderMock.Setup(moq => moq.Now).Returns(time);
    }

    protected ITimeProvider GetTimeProvider()
    {
        return _timeProviderMock.Object;
    }
}