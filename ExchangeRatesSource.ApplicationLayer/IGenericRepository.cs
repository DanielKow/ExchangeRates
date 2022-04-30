using System.Collections.Immutable;

namespace ExchangeRatesSource.ApplicationLayer;

public interface IGenericRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAllAsync();
    Task InsertAsync(TEntity toAdd);
    Task UpsertAsync(TEntity toUpsert);
    Task UpsertManyAsync(IEnumerable<TEntity> toUpsertList);
    void Update(TEntity toUpdate);
}