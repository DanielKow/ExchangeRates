using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer;

namespace ExchangeRatesSource.InfrastructureLayer.Db;

public class ExchangeRatesUnitOfWork : IExchangeRatesUnitOfWork
{
    public IGenericRepository<ExchangeRate> ExchangeRate { get; }
    private readonly ExchangeRateContext _context;

    public ExchangeRatesUnitOfWork(
        ExchangeRateContext context,
        IGenericRepository<ExchangeRate> exchangeRateRepository)
    {
        _context = context;
        ExchangeRate = exchangeRateRepository;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}