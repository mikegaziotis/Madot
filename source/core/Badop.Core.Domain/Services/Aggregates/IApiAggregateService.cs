using Badop.Core.Domain.Aggregates;

namespace Badop.Core.Domain.Services;

public interface IApiAggregateService
{
    Task<ApiAggregate?> Load(string apiName, bool includePreviewVersions = false,
        bool includeUnavailableVersions = false);
}