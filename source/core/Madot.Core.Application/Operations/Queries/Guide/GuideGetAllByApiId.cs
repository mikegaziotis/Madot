using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries;

public record GuideGetAllByApiIdQuery(string ApiId, bool IncludeDeleted=false):IQuery;

public class GuideGetAllByApiIdQueryHandler(
    MadotDbContext dbDbContext):IQueryHandler<GuideGetAllByApiIdQuery,IEnumerable<Domain.Models.Guide>>
{
    public override async Task<IEnumerable<Domain.Models.Guide>> Handle(GuideGetAllByApiIdQuery query)
    {
        return await SafeDbExecuteAsync(async () => await dbDbContext.Guides
            .Where(x => x.ApiId == query.ApiId)
            .Where(x=>!x.IsDeleted || query.IncludeDeleted)
            .ToListAsync());
    }
}