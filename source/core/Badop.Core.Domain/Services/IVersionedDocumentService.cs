using Badop.Core.Domain.Entities;

namespace Badop.Core.Domain.Services;

public interface IVersionedDocumentService
{
    Task<VersionedDocument> Get(string id);
    Task<VersionedDocument> GetLatest(string apiName);
    Task<bool> Update(VersionedDocument versionedDocument);
    Task<bool> Insert(VersionedDocument versionedDocument);
}