using System.Collections.Immutable;

namespace WebApi.DomainLayer.Config;

public interface IInternalSourcesConfig
{
    IImmutableList<string> InternalSourcesUrls { get; }
}