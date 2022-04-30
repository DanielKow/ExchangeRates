using System;
using ExchangeRatesSource.InfrastructureLayer.CalculateDelay;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.Factory;

[TestFixture]
[Category("unit")]
internal class NbpCalculateDelayStrategyFactoryTests : NbpCalculateDelayStrategyFactoryTestsBase
{
    [Test]
    public void GetStrategyForType_should_return_NextWorkingDayStrategy_when_given_type_was_A()
    {
        // Arrange
        var factory = GetMockedFactory();

        // Act
        var result = factory.GetStrategyForType("A");

        // Assert
        Assert.That(result, Is.TypeOf<NextWorkingDayStrategy>());
    }
    
    [Test]
    public void GetStrategyForType_should_return_AtLeastNextWorkingWednesdayStrategy_when_given_type_was_B()
    {
        // Arrange
        var factory = GetMockedFactory();

        // Act
        var result = factory.GetStrategyForType("B");

        // Assert
        Assert.That(result, Is.TypeOf<AtLeastNextWorkingWednesdayStrategy>());
    }

    [TestCase("C")]
    [TestCase("D")]
    [TestCase("AX")]
    [TestCase("XD")]
    [TestCase("test")]
    public void GetStrategyForType_should_throw_ArgumentOutOfRangeException_when_given_was_not_A_nor_B(string type)
    {
        // Arrange
        var factory = GetMockedFactory();

        // Act
        // Assert
        Assert.That(() => factory.GetStrategyForType(type), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
    }
}