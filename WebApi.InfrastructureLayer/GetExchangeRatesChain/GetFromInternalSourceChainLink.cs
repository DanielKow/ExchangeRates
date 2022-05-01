using System.Collections.Immutable;
using System.Text.Json;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Logging;
using WebApi.DomainLayer.Config;

namespace WebApi.InfrastructureLayer.GetExchangeRatesChain;

public class GetFromInternalSourceChainLink : AbstractGetExchangeRatesChainLink
{
    private readonly HttpClient _httpClient;
    private readonly IInternalSourcesConfig _internalSourcesConfig;

    public GetFromInternalSourceChainLink(
        HttpClient httpClient,
        IInternalSourcesConfig internalSourcesConfig,
        ILogger logger) : base(logger)
    {
        _httpClient = httpClient;
        _internalSourcesConfig = internalSourcesConfig;
    }

    protected override async Task<IImmutableList<ExchangeRate>> ConcreteGetExchangeRate()
    {
        Logger.Log(LogLevel.Information, "Getting exchange rates from internal sources started");
        
        var tasks = new List<Task<ExchangeRate[]>>();

        foreach (var sourceUrl in _internalSourcesConfig.InternalSourcesUrls)
        {
            tasks.Add(GetExchangeRatesFromSource(sourceUrl));
        }

        ExchangeRate[][] results = await Task.WhenAll(tasks);

        var result = new ExchangeRate[results.Sum(row => row.Length)];
        var offset = 0;
        foreach (var array in results)
        {
            array.CopyTo(result, offset);
            offset += array.Length;
        }
        
        return ImmutableList.Create(result);
    }

    private async Task<ExchangeRate[]> GetExchangeRatesFromSource(string sourceUrl)
    {
        var response = await _httpClient.GetAsync(sourceUrl);

        if (!response.IsSuccessStatusCode)
        {
            Logger.Log(LogLevel.Warning, "Cannot get exchange rates from internal source under url: {Url}", sourceUrl);
            return Array.Empty<ExchangeRate>();
        }

        string contentString = await response.Content.ReadAsStringAsync();
        var content = JsonSerializer.Deserialize<ExchangeRate[]>(contentString);

        if (content != null)
        {
            return content;
        }

        Logger.Log(LogLevel.Warning, "There is no exchange rates in internal source under url: {Url}", sourceUrl);
        return Array.Empty<ExchangeRate>();
    }
}