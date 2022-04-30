namespace ExchangeRatesSource.DomainLayer.Time;

public interface ITimeProvider
{
    DateTime Now { get; }
}