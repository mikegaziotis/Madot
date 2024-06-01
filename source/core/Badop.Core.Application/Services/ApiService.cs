using Badop.Core.Application.Repositories;
using Badop.Core.Domain.Services;
using Badop.Core.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Badop.Core.Application.StaticHelpers;

namespace Badop.Core.Application.Services;

public class ApiService(IApiRepository repository, ILogger logger) : IApiService
{
    private readonly IApiRepository _repository = repository;

    public async Task<Api?> Get(string name)
    {
        try
        {
            return await _repository.Get(name);
        }
        catch (Exception ex)
        {
            logger.LogError($"Failed fetching Api with name {name}", ex);
            throw;
        }
    }

    public async Task<(string Name, string Key)?> Insert(Api api)
    {
        var key = Guid.NewGuid();
        var hashedKey = Sha256Hasher.Hash(key.ToString());
        var result = await _repository.Insert(api, hashedKey);
        if (!result)
            return null;
        return new ValueTuple<string, string>(api.Name, key.ToString());
    }

    public async Task<bool> Update(Api api, string key)
    {
        var hashedKey = Sha256Hasher.Hash(key);
        return await _repository.Update(api, hashedKey);
    }

    public async Task<IEnumerable<Api>?> GetAll(bool? includeInternal, bool? includeUnavailable)
    {
        return await _repository.GetAll(includeInternal, includeUnavailable);
    }

    public async Task<IEnumerable<string>?> GetAllNames(bool? includeInternal, bool? includeUnavailable)
    {
        return await _repository.GetAllNames(includeInternal, includeUnavailable);
    }

    public Task<(bool Success, string Key)> GenerateNewKey(string name, string oldKey, bool? force)
    {
        throw new NotImplementedException();
    }
}