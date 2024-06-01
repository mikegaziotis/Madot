using Badop.Core.Application.Repositories;
using Badop.Core.Domain.Entities;

namespace Badop.Infrastructure.Storage.Repositories;

public class ChangeLogRepository(string connectionString): BaseRepository(null,false),IChangeLogRepository
{
    protected override string ConnectionString { get; } = connectionString;
    
    public async Task<string?> Insert(ChangeLog log)
    {
        return await GetResultAsync<string>("dboChangeLog_Insert", new
        {
            log.ApiName,
            FileTypeExtension = log.FileTypeExtension.ToString(),
            log.Data
        });
    }

    public async Task<ChangeLog?> Get(string id)
    {
        return await GetResultAsync<ChangeLog?>("dboChangeLog_Get", new
        {
            Id = id
        });
    }

    public async Task<IEnumerable<ChangeLog>> GetCollectionForVersions(string apiName, IEnumerable<string> versions)
    {
        return await GetResultsAsync<ChangeLog>("dboChangeLog_Get_ForVersions", new
        {
            ApiName = apiName,
            ApiVersionsFilter = DataTableBuilder.CreateDataTableForSingleColumn("Id",versions)
        });
    }
}