using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.GuideVersion;

public record GuideVersionGetByApiVersionIdQuery(string ApiVersionId):IQuery;

public class GuideVersionGetByApiVersionIdQueryHandler(
    BadopDbContext dbContext):IQueryHandler<GuideVersionGetByApiVersionIdQuery,IEnumerable<Domain.Models.GuideVersion>>
{
    public async Task<IEnumerable<Domain.Models.GuideVersion>> Handle(GuideVersionGetByApiVersionIdQuery query)
    {
        return await dbContext.ApiVersionGuideVersions
            .Include(x => x.GuideVersion)
            .Include(x=>x.GuideVersion.Guide)
            .Where(x => x.ApiVersionId == query.ApiVersionId)
            .Select(x=>x.GuideVersion)
            .ToListAsync();
    }
}