using Badop.Core.Domain.Enums;
using Badop.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.Homepage;

public record VersionedDocumentGetByApiVersionIdQuery(string ApiVersionId, VersionedDocumentType Type): IQuery;

public class VersionedDocumentGetByApiVersionIdQueryHandler(
    BadopDbContext dbDbContext) : IQueryHandler<VersionedDocumentGetByApiVersionIdQuery, VersionedDocument?>
{
    public async Task<VersionedDocument?> Handle(VersionedDocumentGetByApiVersionIdQuery query)
        => query.Type switch
        {
            VersionedDocumentType.Homepage =>
                await dbDbContext.ApiVersions
                    .Include(x => x.Homepage)
                    .Where(x => x.Id == query.ApiVersionId)
                    .Select(x => x.Homepage)
                    .FirstOrDefaultAsync(),
            VersionedDocumentType.Changelog =>
                await dbDbContext.ApiVersions
                    .Include(x => x.Changelog)
                    .Where(x => x.Id == query.ApiVersionId)
                    .Select(x => x.Changelog)
                    .FirstOrDefaultAsync(),
            VersionedDocumentType.OpenApiSpec =>
                await dbDbContext.ApiVersions
                    .Include(x => x.OpenApiSpec)
                    .Where(x => x.Id == query.ApiVersionId)
                    .Select(x => x.OpenApiSpec)
                    .FirstOrDefaultAsync(),
            _ => throw new ArgumentException("Invalid Versioned Document type")
        };

}