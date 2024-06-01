using Badop.Core.Application.Repositories;
using Badop.Core.Domain.Entities;

namespace Badop.Infrastructure.Storage.Repositories;

public class ApiRepository(string connectionString) : BaseRepository(null, false), IApiRepository
{
    protected override string ConnectionString { get; } = connectionString;

    public async Task<Api?> Get(string name)
    {
        return await base.GetResultAsync<Api>("dbo.API_Get", new { Name = name });
    }

    public async Task<bool> Insert( Api api, string hashedKey)
    {
        return await base.GetResultAsync<bool>("dbo.API_Insert", new {
            api.Name, 
            api.DisplayName, 
            Sha256HashedKey = hashedKey, 
            api.BaseUrlPath,
            api.IsInternal,
            api.IsBeta, 
            api.IsAvailable,
            api.OrderId
        });
    }

    public async Task<bool> Update(Api api, string hashedKey)
    {
        return await base.GetResultAsync<bool>("dbo.API_Update",
            new
            {
                api.Name, 
                api.DisplayName, 
                Sha256HashedKey = hashedKey, 
                api.IsInternal,
                api.IsBeta,
                api.IsAvailable,
                api.OrderId
            });
    }

    public async Task<IEnumerable<Api>?> GetAll(bool? includeInternal = false, bool? includeUnavailable = false)
    {
        return await base.GetResultsAsync<Api>("dbo.Api_GetAll",
            new { IncludeInternal = includeInternal, IncludeUnavailable = includeUnavailable });
    }

    public async Task<IEnumerable<string>?> GetAllNames(bool? includeInternal = false, bool? includeUnavailable = false)
    {
        return await base.GetResultsAsync<string>("dbo.Api_GetAll_NamesOnly", new { IncludeInternal = includeInternal, IncludeUnavailable = includeUnavailable });
    }

    public async Task<bool> ChangeKey(string name, string oldHashedKey, string newHashedKey, bool? force = false)
    {
        return await base.GetResultAsync<bool>("dbo.Api_KeyChange",
            new { Name = name, NewHashedKey = newHashedKey, OldHashedKey = oldHashedKey, Force = force });
    }
}