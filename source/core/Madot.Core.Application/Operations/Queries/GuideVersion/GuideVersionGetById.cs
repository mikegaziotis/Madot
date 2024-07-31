using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries;

public record GuideVersionGetbyIdQuery(string Id) : IQuery;

public class GuideVersionGetByIdQueryHandler(
    MadotDbContext dbDbContext): IQueryHandler<GuideVersionGetbyIdQuery,Domain.Models.GuideVersion?>
{
    public override async Task<Domain.Models.GuideVersion?> Handle(GuideVersionGetbyIdQuery query)
    {
        return await SafeDbExecuteAsync(async () => await dbDbContext.GuideVersions
            .Include(x=>x.Guide)
            .FirstOrDefaultAsync(x=>x.Id==query.Id));
        
    }
}

