namespace Badop.Core.Application.Operations.Queries.ApiVersion;

public record ApiVersionGetByIdQuery(string Id) : IQuery;

public class ApiVersionGetByIdQueryHandler(BadopDbContext dbDbContext): IQueryHandler<ApiVersionGetByIdQuery,Domain.Models.ApiVersion?>
{
    public async Task<Domain.Models.ApiVersion?> Handle(ApiVersionGetByIdQuery query)
    {
        return await dbDbContext.ApiVersions.FindAsync(query.Id);
    }
}