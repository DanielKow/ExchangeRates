using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer;
using ExchangeRatesSource.DomainLayer.NBP;
using Microsoft.Extensions.Logging;

namespace ExchangeRatesSource.InfrastructureLayer.Nbp;

public class NbpExchangeRatesSource : IExchangeRatesSource
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<NbpExchangeRatesSource> _logger;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public NbpExchangeRatesSource(HttpClient httpClient, ILogger<NbpExchangeRatesSource> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                new DateOnlyJsonConverter(),
                new NbpExchangeRatesTableTypeJsonConverter()
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<GettingExchangeRatesResult> GetExchangeRatesAsync(string type)
    {
        if (type != NbpExchangeRatesTableType.A.Value && type != NbpExchangeRatesTableType.B.Value)
        {
            _logger.Log(LogLevel.Warning, "Type {Type} is not recognized by NBP", type);

            return new GettingExchangeRatesResult();
        }

        var response = await _httpClient.GetAsync($"api/exchangerates/tables/{type}");

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogWarning("Cannot get exchange rates table of type {Type} from NBP", type);
            return new GettingExchangeRatesResult();
        }

        var contentString = await response.Content.ReadAsStringAsync();

        var tables = JsonSerializer.Deserialize<NbpExchangeRatesTable[]>(contentString, _jsonSerializerOptions);
        var table = tables?.FirstOrDefault();

        if (table == null)
        {
            _logger.LogWarning("Cannot read exchange rates table of type {Type} from NBP", type);
            return new GettingExchangeRatesResult();
        }

        var rates = table.Rates.Select(row => new ExchangeRate(row.Currency, row.Value, table.Table.Value));
        return new GettingExchangeRatesResult(table.LastUpdateDate, ImmutableList.Create(rates.ToArray()));
    }
}