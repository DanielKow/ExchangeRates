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

    protected static IEnumerable<TestCaseData> ActualDateTimeAndLastUpdateDateForActualData => new[]
    {
        new TestCaseData(new DateTime(2022, 4, 26, 23, 59, 59), new DateOnly(2022, 4, 20)),
        new TestCaseData(new DateTime(2022, 4, 25), new DateOnly(2022, 4, 20)),
        new TestCaseData(new DateTime(2022, 4, 21, 0, 0, 1), new DateOnly(2022, 4, 20)),
        new TestCaseData(new DateTime(2022, 4, 23), new DateOnly(2022, 4, 20)),
        new TestCaseData(new DateTime(2016, 1, 18, 17, 7, 0), new DateOnly(2016, 1, 18)),
        new TestCaseData(new DateTime(2017, 11, 1), new DateOnly(2017, 10, 25)),
        new TestCaseData(new DateTime(2015, 11, 11), new DateOnly(2015, 11, 4)),
        new TestCaseData(new DateTime(2015, 11, 17), new DateOnly(2015, 11, 12))
    };

    protected static IEnumerable<TestCaseData> ActualDateTimeAndLastUpdateDateForNotActualData => new[]
    {
        new TestCaseData(new DateTime(2022, 4, 27), new DateOnly(2022, 4, 20)),
        new TestCaseData(new DateTime(2022, 4, 28), new DateOnly(2022, 4, 20)),
        new TestCaseData(new DateTime(2022, 5, 12), new DateOnly(2022, 4, 20)),
        new TestCaseData(new DateTime(2017, 11, 2), new DateOnly(2017, 10, 25)),
        new TestCaseData(new DateTime(2022, 10, 29), new DateOnly(2017, 10, 29)),
        new TestCaseData(new DateTime(2022, 2, 15), new DateOnly(2022, 2, 2)),
        new TestCaseData(new DateTime(2021, 11, 11), new DateOnly(2021, 11, 3)),
        new TestCaseData(new DateTime(2021, 9, 18), new DateOnly(2021, 9, 8)),
        new TestCaseData(new DateTime(2015, 11, 12), new DateOnly(2015, 11, 4)),
        new TestCaseData(new DateTime(2015, 11, 18), new DateOnly(2015, 11, 12)),
        new TestCaseData(new DateTime(2015, 11, 20), new DateOnly(2015, 11, 12)),
    };
}