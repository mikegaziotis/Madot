using Badop.Core.Domain.Entities;

namespace Badop.Core.Application.Repositories;

public interface IApiVersionRepository
{
    public Task<bool> Insert(ApiVersion version, string hashedKey);
    public Task<bool> Update(ApiVersion version, string hashedKey);
    public Task<ApiVersion?> Get(string id);
    
    public Task<ApiVersion?> GetLatest(string apiName, bool? includePreview);
    public Task<IEnumerable<ApiVersionShort>?> GetAllVersions(string apiName, bool? includePreview, bool? includeUnavailable);
    
    
}