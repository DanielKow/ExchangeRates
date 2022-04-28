using System.Collections.Immutable;

namespace ExchangeRatesSource.DomainLayer;

public class GettingExchangeRatesResult
{
    public DateOnly LastUpdateDate { get; }
    public IImmutableList<ExchangeRate> ExchangeRates { get; }
    public bool Successfully { get; }

    public GettingExchangeRatesResult(
        DateOnly lastUpdateDate,
        IImmutableList<ExchangeRate> exchangeRates,
        bool successfully = true)
    {
        LastUpdateDate = lastUpdateDate;
        ExchangeRates = exchangeRates;
        Successfully = successfully;
    }

    public GettingExchangeRatesResult()
    {
        LastUpdateDate = DateOnly.MinValue;
        ExchangeRates = ImmutableList<ExchangeRate>.Empty;
        Successfully = false;
    }
}