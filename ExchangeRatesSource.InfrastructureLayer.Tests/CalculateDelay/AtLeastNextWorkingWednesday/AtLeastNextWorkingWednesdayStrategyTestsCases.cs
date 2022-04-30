using System;
using System.Collections.Generic;
using ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.NextWorkingDay;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.AtLeastNextWorkingWednesday;

[Category("test_cases")]
internal class AtLeastNextWorkingWednesdayStrategyTestsCases : AtLeastNextWorkingWednesdayStrategyTestsBase
{
    protected static IEnumerable<TestCaseData> DateTimesToAtLeastNextWorkingWednesday => new[]
    {
        new TestCaseData(new DateTime(2022, 4, 30, 12, 33, 2), new DateOnly(2022, 5, 4)),
        new TestCaseData(new DateTime(2022, 1, 1, 0, 0, 1), new DateOnly(2022, 1, 5)),
        new TestCaseData(new DateTime(2021, 1, 3, 23, 59, 59), new DateOnly(2022, 1, 7)),
        new TestCaseData(new DateTime(2021, 1, 4), new DateOnly(2022, 1, 7)),
        new TestCaseData(new DateTime(2021, 1, 5, 23, 1, 1), new DateOnly(2022, 1, 7)),
        new TestCaseData(new DateTime(2021, 1, 6), new DateOnly(2022, 1, 13)),
        new TestCaseData(new DateTime(2020, 12, 30, 0, 1, 32), new DateOnly(2022, 1, 7)),
        new TestCaseData(new DateTime(2023, 5, 1), new DateOnly(2023, 5, 4)),
    };
}