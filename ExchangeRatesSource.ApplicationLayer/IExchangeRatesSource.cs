using System.Collections.Immutable;
using ExchangeRatesSource.DomainLayer;

namespace ExchangeRatesSource.ApplicationLayer;

public interface IExchangeRatesSource
{
    Task<ImmutableArray<ExchangeRate>> GetExchangeRatesAsync();
}