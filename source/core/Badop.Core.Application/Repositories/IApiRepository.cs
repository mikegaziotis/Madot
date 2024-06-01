using Badop.Core.Domain.Entities;

namespace Badop.Core.Application.Repositories;

public interface IApiRepository
{
    Task<Api?> Get(string name);
    
    Task<bool> Insert(Api api, string hashedKey);

    Task<bool> Update(Api api, string hashedKey);

    Task<IEnumerable<Api>?> GetAll(bool? includeInternal, bool? includeUnavailable);
    
    Task<IEnumerable<string>?> GetAllNames(bool? includeInternal, bool? includeUnavailable);

    Task<bool> ChangeKey(string name, string oldHashedKey, string newHashedKey, bool? force);
}