using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries.DocumentStatus;

public record DocumentStatusGetByApiVersionIdQuery(string ApiVersionId) : IQuery;

public class DocumentStatusGetByApiVersionIdQueryHandler(
    MadotDbContext dbDbContext): IQueryHandler<DocumentStatusGetByApiVersionIdQuery,Domain.OtherTypes.DocumentStatus?>
{
    public override async Task<Domain.OtherTypes.DocumentStatus?> Handle(DocumentStatusGetByApiVersionIdQuery query)
    {
        var result = await 
            SafeDbExecuteAsync(async () => await dbDbContext.ApiVersions
            .Include(x=>x.ApiVersionFiles)
            .Include(x=>x.ApiVersionGuideVersions)
            .FirstOrDefaultAsync(x=>x.Id == query.ApiVersionId));

        if (result is null)
            return null;

        return new Domain.OtherTypes.DocumentStatus(
                HasGuides:result.ApiVersionGuideVersions.Count != 0,
                HasChangelog: result.ChangelogId is not null,
                HasDownloadFiles:result.ApiVersionFiles.Count != 0);
    }
}