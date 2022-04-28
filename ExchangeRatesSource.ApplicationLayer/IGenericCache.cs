namespace ExchangeRatesSource.ApplicationLayer;

public interface IGenericCache<T>
{
    Task SaveManyAsync(IEnumerable<T> data);
    Task<IEnumerable<T>> GetAllAsync();
}