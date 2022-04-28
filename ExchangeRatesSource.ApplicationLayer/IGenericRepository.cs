namespace ExchangeRatesSource.ApplicationLayer;

public interface IGenericRepository<TEntity> where TEntity : class
{
    ValueTask<TEntity?> GetAsync(object id);
    Task InsertAsync(TEntity toAdd);
    Task UpsertAsync(TEntity toUpsert);
    Task UpsertManyAsync(IEnumerable<TEntity> toUpsertList);
}