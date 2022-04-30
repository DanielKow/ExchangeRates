namespace ExchangeRatesSource.DomainLayer.Time;

public struct MonthAndDay
{
    public int Month { get; }
    public int Day { get; }

    public MonthAndDay(int month, int day)
    {
        Month = month;
        Day = day;
    }

    public static MonthAndDay FromDateTime(DateTime dateTime)
    {
        return new MonthAndDay(dateTime.Month, dateTime.Day);
    }

    public static MonthAndDay FromOnlyDate(DateOnly dateOnly)
    {
        return new MonthAndDay(dateOnly.Month, dateOnly.Day);
    }
}