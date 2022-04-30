namespace ExchangeRatesSource.DomainLayer.Time;

public static class TimeExtensionMethods
{
    public static bool IsFreeDay(this DateTime dateTime)
    {
        var date = DateOnly.FromDateTime(dateTime);
        return date.IsFreeDay();
    }

    public static bool IsFreeDay(this DateOnly date)
    {
        if (Weekend.Contains(date.DayOfWeek))
        {
            return true;
        }

        var monthAndDay = MonthAndDay.FromOnlyDate(date);

        if (StaticFreeDays.Contains(monthAndDay))
        {
            return true;
        }

        var easter = GetEasterDate(date.Year);

        var movingFreeDays = new List<DateOnly>
        {
            easter.AddDays(1), // Poniedziałek wielkanocny
            easter.AddDays(60) // Boże ciało
        };

        return movingFreeDays.Contains(date);
    }
    
    private static readonly List<DayOfWeek> Weekend = new()
    {
        DayOfWeek.Saturday,
        DayOfWeek.Sunday
    };

    private static readonly List<MonthAndDay> StaticFreeDays = new()
    {
        new MonthAndDay(1, 1), // Nowy rok
        new MonthAndDay(1, 6), // Trzech Króli
        new MonthAndDay(5, 1), // 1 maja
        new MonthAndDay(5, 3), // 3 maja
        new MonthAndDay(8, 15), // Wniebowzięcie
        new MonthAndDay(11, 1), // Wszystkich świętych
        new MonthAndDay(11, 11), // Święto niepodległości
        new MonthAndDay(12, 25), // Boże Narodzenie
        new MonthAndDay(12, 26) // Drugi dzień Bożego Narodzenia
    };

    private static DateOnly GetEasterDate(int year)
    {
        int month = 3;

        int g = year % 19 + 1;
        int c = year / 100 + 1;
        int x = 3 * c / 4 - 12;
        int y = (8 * c + 5) / 25 - 5;
        int z = 5 * year / 4 - x - 10;
        int e = (11 * g + 20 + y - x) % 30;

        if (e == 24)
        {
            e++;
        }

        if (e == 25 && g > 11)
        {
            e++;
        }

        int n = 44 - e;
        if (n < 21)
        {
            n += 30;
        }

        int day = n + 7 - (z + n) % 7;
        if (day <= 31)
        {
            return new DateOnly(year, month, day);
        }

        day -= 31;
        month = 4;

        return new DateOnly(year, month, day);
    }
}