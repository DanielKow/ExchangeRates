using System;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.EveryDay;

[TestFixture]
[Category("unit")]
internal class EverydayStrategyTests : EverydayStrategyTestsCases
{
    [TestCaseSource(nameof(DateTimesToExpectedDay))]
    public void CalculateDelay_should_return_number_of_milliseconds_to_next_day_when_called(DateTime actualTime, DateOnly expectedDate)
    {
        // Arrange
        SetUpActualTime(actualTime);
        var strategy = GetMockedStrategy();
        
        // Act
        var delay = strategy.CalculateDelay();

        // Assert
        var result = actualTime.AddMilliseconds(delay);
        Assert.That(DateOnly.FromDateTime(result), Is.EqualTo(expectedDate));
    }
}