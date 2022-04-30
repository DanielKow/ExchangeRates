namespace ExchangeRatesSource.ApplicationLayer;

public interface ILastUpdateDateCache
{
    Task SaveAsync(DateOnly lastUpdateDate);
    Task<DateOnly?> GetAsync();
}