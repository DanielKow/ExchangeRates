namespace ExchangeRatesSource.DomainLayer;

public readonly struct ExchangeRate
{
    public string Currency { get; }
    public decimal Value { get; }

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