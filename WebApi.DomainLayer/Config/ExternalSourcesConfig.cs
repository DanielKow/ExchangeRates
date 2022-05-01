using System.Collections.Immutable;
using Microsoft.Extensions.Configuration;

namespace WebApi.DomainLayer.Config;

public class ExternalSourcesConfig : IExternalSourcesConfig
{
    public IImmutableList<string> ExternalSourcesTypes { get; }

    public ExternalSourcesConfig(IConfiguration configuration)
    {
        string externalSourcesString = configuration["ExternalSources"];
        
        ExternalSourcesTypes = ImmutableList.Create(externalSourcesString.Split(","));
    }
}