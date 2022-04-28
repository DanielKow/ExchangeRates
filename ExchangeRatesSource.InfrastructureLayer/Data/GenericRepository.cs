using ExchangeRatesSource.ApplicationLayer;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesSource.InfrastructureLayer.Data;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly ExchangeRateContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public GenericRepository(ExchangeRateContext context)
    {
        Context = context;
        DbSet = Context.Set<TEntity>();
    }

    public virtual ValueTask<TEntity?> GetAsync(object id)
    {
        return DbSet.FindAsync(id);
    }

    public virtual async Task InsertAsync(TEntity toAdd)
    {
        await DbSet.AddAsync(toAdd);
    }

    public virtual async Task UpsertAsync(TEntity toUpsert)
    {
        if (await DbSet.AsNoTracking().AnyAsync(row => toUpsert.Equals(row)))
        {
            Update(toUpsert);
            return;
        }

        await InsertAsync(toUpsert);
    }

    public virtual async Task UpsertManyAsync(IEnumerable<TEntity> toUpsertList)
    {
        foreach (var toUpsert in toUpsertList)
        {
            await UpsertAsync(toUpsert);
        }
    }

    public virtual void Update(TEntity toUpdate)
    {
        if (Context.Entry(toUpdate).State == EntityState.Detached)
        {
            DbSet.Attach(toUpdate);
        }

        Context.Entry(toUpdate).State = EntityState.Modified;
    }
}