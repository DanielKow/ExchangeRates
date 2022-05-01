using System.Collections.Immutable;
using Microsoft.Extensions.Configuration;

namespace WebApi.DomainLayer.Config;

public class InternalSourcesConfig : IInternalSourcesConfig
{
    public IImmutableList<string> InternalSourcesUrls { get; }

    public InternalSourcesConfig(IConfiguration configuration)
    {
        string internalSourcesString = configuration["InternalSources"];

        InternalSourcesUrls = ImmutableList.Create(internalSourcesString.Split(","));
    }
}