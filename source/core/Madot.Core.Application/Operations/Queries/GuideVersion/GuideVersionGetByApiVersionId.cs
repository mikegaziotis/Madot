using Madot.Core.Domain.OtherTypes;
using Microsoft.EntityFrameworkCore;

namespace Madot.Core.Application.Operations.Queries;

public record GuideVersionGetByApiVersionIdQuery(string ApiVersionId):IQuery;

public class GuideVersionGetByApiVersionIdQueryHandler(
    MadotDbContext dbContext):IQueryHandler<GuideVersionGetByApiVersionIdQuery,IEnumerable<ApiVersionGuide>>
{
    
    public override async Task<IEnumerable<ApiVersionGuide>> Handle(GuideVersionGetByApiVersionIdQuery query)
    {
        var result = await SafeDbExecuteAsync( async () => await dbContext.ApiVersionGuideVersions
            .Include(x => x.GuideVersion)
            .Include(x=>x.GuideVersion.Guide)
            .Where(x => x.ApiVersionId == query.ApiVersionId)
            .Select(x=> new ApiVersionGuide(x.OrderId,x.GuideVersion))
            .ToListAsync());
            
        return result.OrderBy(x=>x.OrderId);
    }
}