using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.GuideVersion;

public record GuideVersionLatestGetByIdQuery(string GuideId):IQuery;

public class GuideVersionLatestByIdQueryHandler(BadopDbContext dbContext): IQueryHandler<GuideVersionLatestGetByIdQuery,Domain.Models.GuideVersion?>
{
    public async Task<Domain.Models.GuideVersion?> Handle(GuideVersionLatestGetByIdQuery query)
    {
        var guide = await dbContext.Guides
            .Include(x => x.GuideVersions)
            .Where(x => x.Id == query.GuideId)
            .SingleOrDefaultAsync();

        if (guide is null)
            return null;

        return guide.GuideVersions.Where(x => !x.IsDeleted).MaxBy(x => x.Iteration);
    }
}