using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ExchangeRatesSource.DomainLayer.Tests.Time;

[Category("test_cases")]
internal class DateTimeExtensionMethodsTestsCases
{
    protected static IEnumerable<DateTime> FreeDays => new[]
    {
        new DateTime(2000, 1, 1),
        new DateTime(2012, 1, 6),
        new DateTime(2033, 5, 1),
        new DateTime(2069, 5, 3),
        new DateTime(2100, 8, 15),
        new DateTime(2111, 11, 1),
        new DateTime(2022, 11, 11),
        new DateTime(2420, 12, 25),
        new DateTime(1998, 12, 26),
        new DateTime(2022, 4, 17),
        new DateTime(2022, 4, 18),
        new DateTime(2022, 6, 5),
        new DateTime(2022, 6, 16),
        new DateTime(2030, 4, 21),
        new DateTime(2030, 4, 22),
        new DateTime(2030, 6, 9),
        new DateTime(2030, 6, 20),
        new DateTime(2100, 3, 28),
        new DateTime(2100, 3, 29),
        new DateTime(2100, 5, 16),
        new DateTime(2100, 5, 27),
        new DateTime(2022, 4, 30),
        new DateTime(2022, 4, 30),
        new DateTime(2022, 4, 30),
        new DateTime(2022, 4, 30),
        new DateTime(2023, 10, 7),
    };

    protected static IEnumerable<DateTime> WorkingDays => new[]
    {
        new DateTime(2000, 1, 18),
        new DateTime(1963, 5, 30),
        new DateTime(1968, 1, 8),
        new DateTime(1996, 2, 19),
        new DateTime(2006, 11, 28),
        new DateTime(2020, 7, 6),
        new DateTime(2093, 7, 8),
        new DateTime(2158, 11, 6)
    };
}