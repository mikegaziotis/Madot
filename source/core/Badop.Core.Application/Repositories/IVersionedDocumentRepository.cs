using Badop.Core.Domain.Entities;
using Badop.Core.Domain.Enums;

namespace Badop.Core.Application.Repositories;

public interface IVersionedDocumentRepository
{
    Task<VersionedDocument?> Get(int id);
    Task<VersionedDocument?> GetLatest(string apiName, VersionedDocumentType type);
    Task<IEnumerable<VersionedDocument>> GetAll(string apiName, VersionedDocumentType type);
    Task<(int Id,string Message)> Insert(VersionedDocument document);
}