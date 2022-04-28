using System.Text.Json.Serialization;

namespace ExchangeRatesSource.DomainLayer.NBP;

public struct NbpExchangeRate
{
    [JsonPropertyName("code")] public string Currency { get; }

    [JsonPropertyName("mid")] public decimal Value { get; }

    [JsonConstructor]
    public NbpExchangeRate(string currency, decimal value)
    {
        Currency = currency;
        Value = value;
    }
}