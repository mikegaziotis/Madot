using System.Reflection.Metadata;
using Badop.Core.Application.Repositories;
using Badop.Core.Domain.Entities;
using Badop.Core.Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace Badop.Infrastructure.Storage.Repositories;

public class VersionedDocumentRepository(string connectionString)
    : BaseRepository(null, false), IVersionedDocumentRepository
{
    protected override string ConnectionString { get; } = connectionString;

    public async Task<VersionedDocument?> Get(int id)
    {
        return await GetResultAsync<VersionedDocument>("dbo.VersionedDocument_Get", new
        {
            Id = id
        });
    }

    public async Task<VersionedDocument?> GetLatest(string apiName, VersionedDocumentType type)
    {
        return await GetResultAsync<VersionedDocument>("dbo.VersionedDocument_GetLatest", new
        {
            ApiName = apiName,
            DocumentType = type.ToString()
        });
    }

    public async Task<IEnumerable<VersionedDocument>> GetAll(string apiName, VersionedDocumentType type)
    {
        return await base.GetResultsAsync<VersionedDocument>("dbo.VersionedDocument_GetAll", new
        {
            ApiName = apiName,
            DocumentType = type.ToString()
        });
    }

    public async Task<(int Id,string Message)> Insert(VersionedDocument document)
    {
        return await GetResultAsync< (int Id,string Message)>("dbo.VersionedDocument_GetLatest", new
        {
            document.ApiName,
            DocumentType = document.DocumentType.ToString(),
            FileTypeExtension = document.FileTypeExtension.ToString(),
            document.Data
        });
    }
}