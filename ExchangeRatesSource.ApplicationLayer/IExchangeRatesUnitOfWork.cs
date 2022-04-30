using ExchangeRatesSource.DomainLayer;

namespace ExchangeRatesSource.ApplicationLayer;

public interface IExchangeRatesUnitOfWork
{
    IGenericRepository<ExchangeRate> ExchangeRate { get; }
    Task SaveAsync();
}