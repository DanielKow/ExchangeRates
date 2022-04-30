using System;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.NextWorkingDay;

[TestFixture]
[Category("unit")]
internal class NextWorkingDayStrategyTests : NextWorkingDayStrategyTestsCases
{
    [TestCaseSource(nameof(DateTimesToNextWorkingDay))]
    public void CalculateDelay_should_return_number_of_milliseconds_to_next_working_day_when_called(DateTime actualTime, DateOnly expectedDate)
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