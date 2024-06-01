using Badop.Core.Domain.Entities;

namespace Badop.Core.Domain.Services;

public interface IApiVersionService
{
    public Task<bool> Insert(string apiName, string key, ApiVersion version);
    public Task<bool> Update(string apiName, string key, ApiVersion version);
    public Task<ApiVersion?> Get(string apiName, string version);
    public Task<ApiVersion?> Get(string apiVersionId);
    public Task<ApiVersion?> GetLatest(string apiName, bool includePreview = false);
    public Task<IEnumerable<ApiVersionShort>> GetAllVersions(string apiName, bool? includePreview, bool? includeUnavailable);
}