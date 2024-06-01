using Badop.Core.Domain.Aggregates;

namespace Badop.Core.Domain.Services;

public interface IApiVersionAggregateService
{
    Task<ApiVersionAggregate?> Load(string id);
    Task<ApiVersionAggregate?> Load(string apiName, string version);
    Task<ApiVersionAggregate?> Load(string apiName, int majorVersion, int minorVersion);
}