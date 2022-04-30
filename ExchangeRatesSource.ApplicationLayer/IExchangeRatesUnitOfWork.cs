using ExchangeRatesSource.DomainLayer;

namespace ExchangeRatesSource.ApplicationLayer;

public interface IExchangeRatesUnitOfWork
{
    IGenericRepository<ExchangeRate> ExchangeRateType { get; }
    Task SaveAsync();
}