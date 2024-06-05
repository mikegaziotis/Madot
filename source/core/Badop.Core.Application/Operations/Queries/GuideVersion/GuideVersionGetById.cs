using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.GuideVersion;

public record GuideVersionGetbyIdQuery(string Id) : IQuery;

public class GuideVersionGetByIdQueryHandler(
    BadopDbContext dbDbContext): IQueryHandler<GuideVersionGetbyIdQuery,Domain.Models.GuideVersion?>
{
    public async Task<Domain.Models.GuideVersion?> Handle(GuideVersionGetbyIdQuery query)
    {
        return await dbDbContext.GuideVersions
            .FirstOrDefaultAsync(x=>x.Id==query.Id);
    }
}

