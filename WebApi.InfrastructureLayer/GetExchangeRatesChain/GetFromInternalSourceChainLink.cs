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
        ILogger<GetFromInternalSourceChainLink> logger) : base(logger)
    {
        _httpClient = httpClient;
        _internalSourcesConfig = internalSourcesConfig;
    }

    protected override async Task<IImmutableList<ExchangeRate>> ConcreteGetExchangeRate()
    {
        var tasks = new List<Task<IEnumerable<ExchangeRate>>>();

        foreach (var sourceUrl in _internalSourcesConfig.InternalSourcesUrls)
        {
            tasks.Add(GetExchangeRatesFromSource(sourceUrl));
        }

        IEnumerable<ExchangeRate>[] results = await Task.WhenAll(tasks);

        var resultList = new List<ExchangeRate>();
        foreach (var result in results)
        {
            resultList.AddRange(result);
        }
        
        return ImmutableList.Create(resultList.ToArray());
    }

    private async Task<IEnumerable<ExchangeRate>> GetExchangeRatesFromSource(string sourceUrl)
    {
        var response = await _httpClient.GetAsync(sourceUrl);

        if (!response.IsSuccessStatusCode)
        {
            _logger.Log(LogLevel.Warning, "Cannot get exchange rates from internal source under url: {Url}", sourceUrl);
            return Array.Empty<ExchangeRate>();
        }

        string contentString = await response.Content.ReadAsStringAsync();
        var content = JsonSerializer.Deserialize<ExchangeRate[]>(contentString);

        if (content != null)
        {
            return content;
        }

        _logger.Log(LogLevel.Warning, "There is no exchange rates in internal source under url: {Url}", sourceUrl);
        return Array.Empty<ExchangeRate>();
    }
}