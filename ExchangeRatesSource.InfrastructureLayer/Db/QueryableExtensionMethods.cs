using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRatesSource.InfrastructureLayer.Db;

public static class QueryableExtensionMethods
{
    public static async Task<ImmutableList<T>> ToImmutableListAsync<T>(
        this IQueryable<T> queryable,
        CancellationToken cancellationToken = default)
    {
        var builder = ImmutableList.CreateBuilder<T>();
        await foreach (var element in queryable.AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            builder.Add(element);
        }

        return builder.ToImmutable();
    }
}