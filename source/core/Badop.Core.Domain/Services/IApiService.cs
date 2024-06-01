using Badop.Core.Domain.Entities;

namespace Badop.Core.Domain.Services;

public interface IApiService
{
    Task<Api?> Get(string name);
    
    Task<(string Name,string Key)?> Insert(Api api);

    Task<bool> Update(Api api, string key);

    Task<IEnumerable<Api>?> GetAll(bool? includeInternal, bool? includeUnavailable);
    
    Task<IEnumerable<string>?> GetAllNames(bool? includeInternal, bool? includeUnavailable);

    Task<(bool Success, string Key)> GenerateNewKey(string name, string oldKey, bool? force = false);
}