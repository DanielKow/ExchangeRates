using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer;

namespace ExchangeRatesSource.InfrastructureLayer.Db;

public class ExchangeRatesUnitOfWork : IExchangeRatesUnitOfWork
{
    public IGenericRepository<ExchangeRateType> ExchangeRateType { get; }
    private readonly ExchangeRateContext _context;

    public ExchangeRatesUnitOfWork(
        ExchangeRateContext context,
        IGenericRepository<ExchangeRateType> exchangeRateTypeRepository)
    {
        _context = context;
        ExchangeRateType = exchangeRateTypeRepository;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}