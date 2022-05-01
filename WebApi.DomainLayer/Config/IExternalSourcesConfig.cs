using System.Collections.Immutable;

namespace WebApi.DomainLayer.Config;

public interface IExternalSourcesConfig
{
    IImmutableList<string> ExternalSourcesTypes { get; }
}