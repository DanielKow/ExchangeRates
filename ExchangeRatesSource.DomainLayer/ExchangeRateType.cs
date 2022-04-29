using System.Text.Json.Serialization;

namespace ExchangeRatesSource.DomainLayer;

public class ExchangeRateType
{
    public string Name { get; init; }
    public DateOnly LastUpdateDate { get; init; }

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