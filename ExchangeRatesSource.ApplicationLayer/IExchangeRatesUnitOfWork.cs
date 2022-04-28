using ExchangeRatesSource.DomainLayer;

namespace ExchangeRatesSource.ApplicationLayer;

public interface IExchangeRatesUnitOfWork
{
    IGenericRepository<ExchangeRateType> ExchangeRateType { get; }
    Task SaveAsync();
}