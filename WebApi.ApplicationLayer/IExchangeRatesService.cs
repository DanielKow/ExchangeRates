using ExchangeRatesSource.DomainLayer;

namespace WebApi.ApplicationLayer;

public interface IExchangeRatesService
{
    Task<IEnumerable<ExchangeRate>> GetExchangeRates();
}