using System;
using ExchangeRatesSource.DomainLayer.Time;
using NUnit.Framework;

namespace ExchangeRatesSource.DomainLayer.Tests.Time;

[TestFixture]
[Category("unit")]
internal class TimeExtensionMethodsTests : TimeExtensionMethodsTestsCases
{
    [TestCaseSource(nameof(FreeDays))]
    public void IsFreeDay_should_return_true_when_it_is_free_day(DateTime dateTime)
    {
        // Arrange
        // Act
        var result = dateTime.IsFreeDay();

        // Assert
        Assert.That(result, Is.True);
    }
    
    [TestCaseSource(nameof(WorkingDays))]
    public void IsFreeDay_should_return_false_when_it_is_working_day(DateTime dateTime)
    {
        // Arrange
        // Act
        var result = dateTime.IsFreeDay();

        // Assert
        Assert.That(result, Is.False);
    }
}