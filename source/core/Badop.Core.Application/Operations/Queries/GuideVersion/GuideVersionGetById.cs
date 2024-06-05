namespace Badop.Core.Application.Operations.Queries.GuideVersion;

public record GuideVersionGetSpecificQuery(string Id) : IQuery;

public class GuideVersionGetSpecificQueryHandler(
    BadopContext dbContext): IQueryHandler<GuideVersionGetSpecificQuery,Domain.Models.GuideVersion?>
{
    public async Task<Domain.Models.GuideVersion?> Handle(GuideVersionGetSpecificQuery query)
    {
        return await dbContext.GuideVersions.FindAsync(query.Id);
    }
}

