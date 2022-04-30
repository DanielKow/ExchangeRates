namespace ExchangeRatesSource.DomainLayer;

public class ExchangeRate
{
    public string Currency { get; private set; }
    public decimal Value { get; private set; }
    public string Type { get; private set; }

    public ExchangeRate(string currency, decimal value, string type)
    {
        Currency = currency;
        Value = value;
        Type = type;
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