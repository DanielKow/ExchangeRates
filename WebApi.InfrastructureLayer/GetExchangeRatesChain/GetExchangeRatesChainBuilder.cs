using ExchangeRatesSource.ApplicationLayer;
using Microsoft.Extensions.Logging;
using WebApi.ApplicationLayer.GetExchangeRatesChain;
using WebApi.DomainLayer.Config;

namespace WebApi.InfrastructureLayer.GetExchangeRatesChain;

public class GetExchangeRatesChainBuilder : IGetExchangeRatesChainBuilder
{
    private readonly IExternalSourcesConfig _externalSourcesConfig;
    private readonly IInternalSourcesConfig _internalSourcesConfig;
    private readonly IExchangeRatesSource _exchangeRatesSource;
    private readonly HttpClient _httpClient;
    private readonly ILoggerFactory _loggerFactory;

    public GetExchangeRatesChainBuilder(
        IExternalSourcesConfig externalSourcesConfig,
        IInternalSourcesConfig internalSourcesConfig,
        IExchangeRatesSource exchangeRatesSource,
        HttpClient httpClient,
        ILoggerFactory loggerFactory)
    {
        _externalSourcesConfig = externalSourcesConfig;
        _internalSourcesConfig = internalSourcesConfig;
        _exchangeRatesSource = exchangeRatesSource;
        _httpClient = httpClient;
        _loggerFactory = loggerFactory;
    }

    public IGetExchangeRatesChainLink BuildChain()
    {
        var internalSourceChainLink = new GetFromInternalSourceChainLink(_httpClient, _internalSourcesConfig, _loggerFactory);
        var externalSourceChainLink =
            new GetFromExternalSourceChainLink(_exchangeRatesSource, _externalSourcesConfig, _loggerFactory);
        
        internalSourceChainLink.SetNext(externalSourceChainLink);
        return internalSourceChainLink;
    }
}