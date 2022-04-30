using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.NextWorkingDay;

[Category("test_cases")]
internal class NextWorkingDayStrategyTestsCases : NextWorkingDayStrategyTestsBase
{
    protected static IEnumerable<TestCaseData> DateTimesToNextWorkingDay => new[]
    {
        new TestCaseData(new DateTime(2022, 1, 1, 23, 1, 1), new DateOnly(2022, 1, 3)),
        new TestCaseData(new DateTime(2022, 1, 31, 12, 13, 14), new DateOnly(2022, 2, 1)),
        new TestCaseData(new DateTime(2021, 12, 31, 23, 59, 59), new DateOnly(2022, 1, 3)),
        new TestCaseData(new DateTime(2022, 3, 23, 0, 0, 1), new DateOnly(2022, 3, 24)),
        new TestCaseData(new DateTime(2022, 1, 18, 16, 20,0), new DateOnly(2022, 1, 19)),
        new TestCaseData(new DateTime(2022, 4, 15, 16, 20,0), new DateOnly(2022, 4, 19)),
        new TestCaseData(new DateTime(2022, 10, 31), new DateOnly(2022, 11, 2)),
        new TestCaseData(new DateTime(2022, 11, 1), new DateOnly(2022, 11, 2)),
        new TestCaseData(new DateTime(2022, 11, 10), new DateOnly(2022, 11, 14))
    };
}