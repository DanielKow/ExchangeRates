using ExchangeRatesSource.ApplicationLayer;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Logging;
using WebApi.DomainLayer.Config;

namespace WebApi.InfrastructureLayer.GetExchangeRatesChain;

public class GetFromExternalSourceChainLink : AbstractGetExchangeRatesChainLink
{
    private readonly IExchangeRatesSource _exchangeRatesSource;
    private readonly IExternalSourcesConfig _externalSourcesConfig;

    public GetFromExternalSourceChainLink(
        IExchangeRatesSource exchangeRatesSource,
        IExternalSourcesConfig externalSourcesConfig,
        ILogger logger) : base(logger)
    {
        _externalSourcesConfig = externalSourcesConfig;
        _exchangeRatesSource = exchangeRatesSource;
    }

    protected override async Task<ExchangeRate[]> ConcreteGetExchangeRate()
    {
        Logger.Log(LogLevel.Information, "Getting exchange rates from external sources started");

        var tasks = new List<Task<GettingExchangeRatesResult>>();

        foreach (var type in _externalSourcesConfig.ExternalSourcesTypes)
        {
            tasks.Add(GetExchangeRatesFromExternalSource(type));
        }

        GettingExchangeRatesResult[] results = await Task.WhenAll(tasks);
        
        var resultList = new List<ExchangeRate>();
        foreach (var result in results)
        {
            resultList.AddRange(result.ExchangeRates);
        }
        
        return resultList.ToArray();
    }

    private Task<GettingExchangeRatesResult> GetExchangeRatesFromExternalSource(string type)
    {
        return _exchangeRatesSource.GetExchangeRatesAsync(type);
    }
}