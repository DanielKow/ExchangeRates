using System.Collections.Immutable;
using ExchangeRatesSource.DomainLayer;
using Microsoft.Extensions.Logging;

namespace WebApi.InfrastructureLayer.GetExchangeRatesChain;

public class GetFromExternalSourceChainLink : AbstractGetExchangeRatesChainLink
{
    public GetFromExternalSourceChainLink(ILogger<GetFromExternalSourceChainLink> logger) : base(logger)
    {
    }

    protected override Task<IImmutableList<ExchangeRate>> ConcreteGetExchangeRate()
    {
        throw new NotImplementedException();
    }
}