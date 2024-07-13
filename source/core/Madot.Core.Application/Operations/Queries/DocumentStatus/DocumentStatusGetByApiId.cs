using Madot.Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries.DocumentStatus;

public record DocumentStatusGetByApiIdQuery(string ApiId) : IQuery;

public class DocumentStatusGetByApiIdQueryHandler(
    MadotDbContext dbDbContext): IQueryHandler<DocumentStatusGetByApiIdQuery,Domain.OtherTypes.DocumentStatus?>
{
    public override async Task<Domain.OtherTypes.DocumentStatus?> Handle(DocumentStatusGetByApiIdQuery query)
    {
        var files = await dbDbContext.Files.Where(x => !x.IsDeleted && x.ApiId == query.ApiId).AnyAsync();
        var changelog= await dbDbContext.VersionedDocuments.Where(x => x.DocumentType == VersionedDocumentType.Changelog && x.ApiId == query.ApiId).AnyAsync();
        var guide = await dbDbContext.Guides.Where(x => x.ApiId == query.ApiId && !x.IsDeleted).AnyAsync();

        return new Domain.OtherTypes.DocumentStatus(HasGuides:guide, HasChangelog: changelog, HasDownloadFiles: files);
    }
}