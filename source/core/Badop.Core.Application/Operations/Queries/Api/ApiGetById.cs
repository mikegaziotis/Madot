namespace Badop.Core.Application.Operations.Queries.Api;

public record ApiGetByIdQuery(string Id):IQuery;

public class ApiGetByIdQueryHandler(
    BadopDbContext dbDbContext) : IQueryHandler<ApiGetByIdQuery,Domain.Models.Api?>
{
    public async Task<Domain.Models.Api?> Handle(ApiGetByIdQuery query)
    {
        return await dbDbContext.Apis.FindAsync(query.Id);
    }
}