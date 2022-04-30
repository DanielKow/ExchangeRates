using System.Collections.Immutable;
using ExchangeRatesSource.DomainLayer;

namespace ExchangeRatesSource.ApplicationLayer.Services;

public interface IGetExchangeRatesService
{
    Task<ImmutableList<ExchangeRate>> GetAll();
}