using Badop.Core.Domain.ShortTypes;
using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.GuideVersion;

public record GuideVersionGetByApiVersionIdQuery(string ApiVersionId):IQuery;

public class GuideVersionGetByApiVersionIdQueryHandler(
    BadopDbContext dbContext):IQueryHandler<GuideVersionGetByApiVersionIdQuery,IEnumerable<GuideVersionShort>>
{
    public async Task<IEnumerable<GuideVersionShort>> Handle(GuideVersionGetByApiVersionIdQuery query)
    {
        return await dbContext.ApiVersionGuideVersions
            .Include(x => x.GuideVersion)
            .Include(x=>x.GuideVersion.Version)
            .Where(x => x.ApiVersionId == query.ApiVersionId)
            .Select(x => new GuideVersionShort(x.GuideVersion!))
            .ToListAsync();
    }
}