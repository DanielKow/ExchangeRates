using System.Collections.Immutable;
using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.ApplicationLayer.Services;
using ExchangeRatesSource.DomainLayer;
using ExchangeRatesSource.InfrastructureLayer.Db;

namespace ExchangeRatesSource.InfrastructureLayer.Services;

public class GetExchangeRatesService : IGetExchangeRatesService
{
    private readonly IExchangeRatesUnitOfWork _unitOfWork;

    public GetExchangeRatesService(IExchangeRatesUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<ImmutableList<ExchangeRate>> GetAllAsync()
    {
        return _unitOfWork.ExchangeRate.GetAllAsync().ToImmutableListAsync();
    }
}