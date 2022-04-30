using System;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.NextWorkingDay;

[TestFixture]
[Category("unit")]
internal class NextWorkingDayStrategyTests : NextWorkingDayStrategyTestsCases
{
    [TestCaseSource(nameof(ActualDateTimeToNextWorkingDay))]
    public void CalculateDelay_should_return_number_of_milliseconds_to_next_working_day_when_called(DateTime actualDateTime, DateOnly expectedDate)
    {
        // Arrange
        SetUpActualDateTime(actualDateTime);
        var strategy = GetMockedStrategy();
        
        // Act
        var delay = strategy.CalculateDelay();

        // Assert
        var result = actualDateTime.AddMilliseconds(delay);
        Assert.That(DateOnly.FromDateTime(result), Is.EqualTo(expectedDate));
    }

    [TestCaseSource(nameof(ActualDateTimeAndLastUpdateDateForNotActualData))]
    public void CheckIfActual_should_return_false_when_there_was_update_of_exchange_rates_between_last_update_date_and_today(DateTime actualDateTime, DateOnly lastUpdateDate)
    {
        // Arrange
        SetUpActualDateTime(actualDateTime);
        var strategy = GetMockedStrategy();
        
        // Act
        var result = strategy.CheckIfActual(lastUpdateDate);

        // Assert
        Assert.That(result, Is.False);
    }
    
    [TestCaseSource(nameof(ActualDateTimeAndLastUpdateDateForActualData))]
    public void CheckIfActual_should_return_true_when_there_was_not_update_of_exchange_rates_between_last_update_date_and_today(DateTime actualDateTime, DateOnly lastUpdateDate)
    {
        // Arrange
        SetUpActualDateTime(actualDateTime);
        var strategy = GetMockedStrategy();
        
        // Act
        var result = strategy.CheckIfActual(lastUpdateDate);

        // Assert
        Assert.That(result, Is.True);
    }
}