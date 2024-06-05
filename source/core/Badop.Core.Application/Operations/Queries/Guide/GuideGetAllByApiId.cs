using Microsoft.EntityFrameworkCore;

namespace Badop.Core.Application.Operations.Queries.Guide;

public record GuideGetAllByApiIdQuery(string ApiId, bool IncludeDeleted=false):IQuery;

public class GuideGetAllByApiIdQueryHandler(
    BadopDbContext dbDbContext):IQueryHandler<GuideGetAllByApiIdQuery,IEnumerable<Domain.Models.Guide>>
{
    public async Task<IEnumerable<Domain.Models.Guide>> Handle(GuideGetAllByApiIdQuery query)
    {
        return await dbDbContext.Guides
            .Where(x => x.ApiId == query.ApiId)
            .Where(x=>!x.IsDeleted || query.IncludeDeleted)
            .ToListAsync();
    }
}