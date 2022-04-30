using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.AtLeastNextWorkingWednesday;

[Category("test_cases")]
internal class AtLeastNextWorkingWednesdayStrategyTestsCases : AtLeastNextWorkingWednesdayStrategyTestsBase
{
    protected static IEnumerable<TestCaseData> ActualDateTimeToAtLeastNextWorkingWednesday => new[]
    {
        new TestCaseData(new DateTime(2022, 4, 30, 12, 33, 2), new DateOnly(2022, 5, 4)),
        new TestCaseData(new DateTime(2022, 1, 1, 0, 0, 1), new DateOnly(2022, 1, 5)),
        new TestCaseData(new DateTime(2021, 1, 3, 23, 59, 59), new DateOnly(2021, 1, 7)),
        new TestCaseData(new DateTime(2021, 1, 4), new DateOnly(2021, 1, 7)),
        new TestCaseData(new DateTime(2021, 1, 5, 23, 1, 1), new DateOnly(2021, 1, 7)),
        new TestCaseData(new DateTime(2021, 1, 6), new DateOnly(2021, 1, 13)),
        new TestCaseData(new DateTime(2020, 12, 30, 0, 1, 32), new DateOnly(2021, 1, 7)),
        new TestCaseData(new DateTime(2023, 5, 1), new DateOnly(2023, 5, 4)),
        new TestCaseData(new DateTime(2000, 1, 18), new DateOnly(2000, 1, 19)),
        new TestCaseData(new DateTime(2000, 2, 21), new DateOnly(2000, 2, 23)),
        new TestCaseData(new DateTime(2000, 3, 22), new DateOnly(2000, 3, 29)),
        new TestCaseData(new DateTime(2000, 4, 20), new DateOnly(2000, 4, 26)),
        new TestCaseData(new DateTime(2000, 5, 5), new DateOnly(2000, 5, 10)),
        new TestCaseData(new DateTime(2000, 6, 10), new DateOnly(2000, 6, 14)),
        new TestCaseData(new DateTime(2000, 7, 23), new DateOnly(2000, 7, 26)),
        new TestCaseData(new DateTime(2000, 4, 28), new DateOnly(2000, 5, 4)),
        new TestCaseData(new DateTime(2000, 9, 28), new DateOnly(2000, 10, 4)),
        new TestCaseData(new DateTime(2000, 12, 31), new DateOnly(2001, 1, 3))
    };
}