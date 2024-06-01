using Badop.Core.Application.Repositories;
using Badop.Core.Domain.Entities;

namespace Badop.Infrastructure.Storage.Repositories;

public class ApiVersionRepository(string connectionString) : BaseRepository(null, false), IApiVersionRepository
{
    protected override string ConnectionString { get; } = connectionString;

    public async Task<bool> Insert(ApiVersion version, string hashedKey)
    {
        return await base.GetResultAsync<bool>("dbo.APIVersion_Insert", new
        {
            version.ApiName, 
            HashedKey = hashedKey, 
            version.MajorVersion,
            version.MinorVersion,
            version.BuildOrReleaseReference,
            version.OpenApiSpecId, 
            version.DocumentationId, 
            version.ChangeLogId, 
            version.IsPreview, 
            version.IsAvailable
        });
    }

    public async Task<bool> Update(ApiVersion version, string hashedKey)
    {
        return await base.GetResultAsync<bool>("dbo.APIVersion_Insert", new
        {
            version.ApiName, 
            HashedKey = hashedKey, 
            version.MajorVersion,
            version.MinorVersion,
            version.BuildOrReleaseReference,
            version.OpenApiSpecId, 
            version.DocumentationId, 
            version.ChangeLogId, 
            version.IsPreview, 
            version.IsAvailable
        });
    }

    public async Task<ApiVersion?> Get(string id)
    {
        return await base.GetResultAsync<ApiVersion?>("dbo.APIVersion_Get", new
        {
            Id = id
        },new QueryOptions(){ConnectionTimeout = 300});
    }

    public async Task<ApiVersion?> GetLatest(string apiName, bool? includePreview)
    {
        return await base.GetResultAsync<ApiVersion?>("dbo.APIVersion_Get_Latest", new
        {
            ApiName = apiName,
            IncludePreview = includePreview
        });
    }

    public async Task<IEnumerable<ApiVersionShort>?> GetAllVersions(string apiName, bool? includePreview = false, bool? includeUnavailable = false)
    {
        return await base.GetResultsAsync<ApiVersionShort>("dbo.APIVersionShort_GetAll", new
        {
            ApiName = apiName, 
            IncludePreview = includePreview, 
            IncludeUnavailable = includeUnavailable
        });
    }
}