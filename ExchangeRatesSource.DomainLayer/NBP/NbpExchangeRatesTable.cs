using System.Text.Json.Serialization;

namespace ExchangeRatesSource.DomainLayer.NBP;

public class NbpExchangeRatesTable
{
    public NbpExchangeRatesTableType Table { get; }
    [JsonPropertyName("effectiveDate")] public DateOnly LastUpdateDate { get; }
    public IEnumerable<NbpExchangeRate> Rates { get; }

    [JsonConstructor]
    public NbpExchangeRatesTable(NbpExchangeRatesTableType table, DateOnly lastUpdateDate, IEnumerable<NbpExchangeRate> rates)
    {
        Table = table;
        LastUpdateDate = lastUpdateDate;
        Rates = rates;
    }
}