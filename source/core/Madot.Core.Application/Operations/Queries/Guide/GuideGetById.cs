namespace Madot.Core.Application.Operations.Queries;

public record GuideGetByIdQuery(string Id) : IQuery;

public class GuideGetByIdQueryHandler(
    MadotDbContext dbDbContext): IQueryHandler<GuideGetByIdQuery,Domain.Models.Guide?>
{
    public override async Task<Domain.Models.Guide?> Handle(GuideGetByIdQuery query)
    {
        return await SafeDbExecuteAsync(async ()=> await dbDbContext.Guides.FindAsync(query.Id));
    }
}