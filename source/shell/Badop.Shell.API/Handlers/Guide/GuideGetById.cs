namespace Badop.Core.Application.Operations.Queries.Guide;

public record GuideGetByIdQuery(string Id) : IQuery;

public class GuideGetByIdQueryHandler(
    BadopDbContext dbDbContext): IQueryHandler<GuideGetByIdQuery,Domain.Models.Guide?>
{
    public async Task<Domain.Models.Guide?> Handle(GuideGetByIdQuery query)
    {
        return await dbDbContext.Guides.FindAsync(query.Id);
    }
}