using Badop.Core.Domain.Entities;

namespace Badop.Core.Domain.Services;

public interface IChangeLogService
{
    Task<String?> Insert(ChangeLog changeLogService);

    Task<ChangeLog> Get(string id);

    Task<IEnumerable<ChangeLog>> GetForApiVersions(List<string> apiVersions);
}