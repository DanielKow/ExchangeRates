using System;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.AtLeastNextWorkingWednesday;

[TestFixture]
[Category("unit")]
internal class AtLeastNextWorkingWednesdayStrategyTests : AtLeastNextWorkingWednesdayStrategyTestsCases
{
    [TestCaseSource(nameof(DateTimesToAtLeastNextWorkingWednesday))]
    public void CalculateDelay_should_return_number_of_milliseconds_to_at_least_next_working_wednesday_when_called(DateTime actualTime, DateOnly expectedDate)
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