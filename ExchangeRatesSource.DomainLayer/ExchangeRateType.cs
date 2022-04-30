using System.Text.Json.Serialization;

namespace ExchangeRatesSource.DomainLayer;

public readonly struct ExchangeRateType
{
    public string Name { get; }
    public DateOnly LastUpdateDate { get; }

    public ExchangeRateType(string name, DateOnly lastUpdateDate)
    {
        Name = name;
        LastUpdateDate = lastUpdateDate;
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is ExchangeRateType other))
        {
            return false;
        }

        return Equals(Name, other.Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}