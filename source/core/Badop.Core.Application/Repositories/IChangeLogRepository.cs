using Badop.Core.Domain.Entities;

namespace Badop.Core.Application.Repositories;

public interface IChangeLogRepository
{
    Task<string?> Insert(ChangeLog log);
    Task<ChangeLog?> Get(string id);
    Task<IEnumerable<ChangeLog>> GetCollectionForVersions(string apiName, IEnumerable<string> versions);
}