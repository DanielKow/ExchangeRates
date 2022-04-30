namespace ExchangeRatesSource.DomainLayer;

public class ExchangeRate
{
    public string Currency { get; init; }
    public decimal Value { get; init; }

    public ExchangeRate(string currency, decimal value)
    {
        Currency = currency;
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is ExchangeRate other))
        {
            return false;
        }

        return Currency == other.Currency;
    }

    public override int GetHashCode()
    {
        return Currency.GetHashCode();
    }
}