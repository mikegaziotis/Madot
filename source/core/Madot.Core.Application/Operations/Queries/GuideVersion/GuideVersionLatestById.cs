using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries.GuideVersion;

public record GuideVersionLatestGetByIdQuery(string GuideId):IQuery;

public class GuideVersionLatestByIdQueryHandler(MadotDbContext dbContext): IQueryHandler<GuideVersionLatestGetByIdQuery,Domain.Models.GuideVersion?>
{
    public override async Task<Domain.Models.GuideVersion?> Handle(GuideVersionLatestGetByIdQuery query)
    {
        var guide = await SafeDbExecuteAsync(async ()=> await dbContext.Guides
            .Include(x => x.GuideVersions)
            .Where(x => x.Id == query.GuideId)
            .SingleOrDefaultAsync());

        if (guide is null)
            return null;

        return guide.GuideVersions.Where(x => !x.IsDeleted).MaxBy(x => x.Iteration);
    }
}