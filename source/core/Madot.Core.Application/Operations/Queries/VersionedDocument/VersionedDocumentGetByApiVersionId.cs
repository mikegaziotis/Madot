using Madot.Core.Domain.Enums;
using Madot.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries.Homepage;

public record VersionedDocumentGetByApiVersionIdQuery(string ApiVersionId, VersionedDocumentType Type): IQuery;

public class VersionedDocumentGetByApiVersionIdQueryHandler(
    MadotDbContext dbDbContext) : IQueryHandler<VersionedDocumentGetByApiVersionIdQuery, VersionedDocument?>
{
    public override async Task<VersionedDocument?> Handle(VersionedDocumentGetByApiVersionIdQuery query)
        => query.Type switch
        {
            VersionedDocumentType.Homepage =>
                await SafeDbExecuteAsync(async ()=> await dbDbContext.ApiVersions
                    .Include(x => x.Homepage)
                    .Where(x => x.Id == query.ApiVersionId)
                    .Select(x => x.Homepage)
                    .FirstOrDefaultAsync()),
            VersionedDocumentType.Changelog =>
                await SafeDbExecuteAsync(async ()=> await dbDbContext.ApiVersions
                    .Include(x => x.Changelog)
                    .Where(x => x.Id == query.ApiVersionId)
                    .Select(x => x.Changelog)
                    .FirstOrDefaultAsync()),
            VersionedDocumentType.OpenApiSpec =>
                await SafeDbExecuteAsync(async ()=> await dbDbContext.ApiVersions
                    .Include(x => x.OpenApiSpec)
                    .Where(x => x.Id == query.ApiVersionId)
                    .Select(x => x.OpenApiSpec)
                    .FirstOrDefaultAsync()),
            _ => throw new ArgumentException("Invalid Versioned Document type")
        };

}