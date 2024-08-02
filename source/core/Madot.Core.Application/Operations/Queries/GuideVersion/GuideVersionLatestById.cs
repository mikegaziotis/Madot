using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries;

public record GuideVersionLatestGetByIdQuery(string GuideId):IQuery;

public class GuideVersionLatestByIdQueryHandler(MadotDbContext dbContext): IQueryHandler<GuideVersionLatestGetByIdQuery,Domain.Models.GuideVersion?>
{
    public override async Task<Domain.Models.GuideVersion?> Handle(GuideVersionLatestGetByIdQuery query)
    {
        var guideVersion = await SafeDbExecuteAsync(async ()=> await dbContext.Guides
            .Where(guide => guide.Id == query.GuideId && !guide.IsDeleted)
            .SelectMany(guide => guide.GuideVersions)
            .Where(guideVersion => !guideVersion.IsDeleted)
            .OrderByDescending(guideVersion => guideVersion.Iteration)
            .FirstOrDefaultAsync());

        return guideVersion;
    }
}