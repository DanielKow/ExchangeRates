namespace ExchangeRatesSource.DomainLayer.Time;

public class TimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.Now;
}