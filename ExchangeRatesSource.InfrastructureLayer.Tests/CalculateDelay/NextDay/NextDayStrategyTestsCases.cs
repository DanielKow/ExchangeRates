using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ExchangeRatesSource.InfrastructureLayer.Tests.CalculateDelay.NextDay;

[Category("test_cases")]
internal class NextDayStrategyTestsCases : NextDayStrategyTestsBase
{
    protected static IEnumerable<TestCaseData> DateTimesToNextDay => new[]
    {
        new TestCaseData(new DateTime(2022, 1, 1, 23, 1, 1), new DateOnly(2022, 1, 2)),
        new TestCaseData(new DateTime(2022, 1, 31, 12, 13, 14), new DateOnly(2022, 2, 1)),
        new TestCaseData(new DateTime(2021, 12, 31), new DateOnly(2022, 1, 2)),
        new TestCaseData(new DateTime(2022, 3, 23), new DateOnly(2022, 3, 24)),
        new TestCaseData(new DateTime(2022, 1, 18, 16, 20,0), new DateOnly(2022, 1, 19))
    };
}