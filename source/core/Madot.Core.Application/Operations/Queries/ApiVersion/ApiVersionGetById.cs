namespace Madot.Core.Application.Operations.Queries;

public record ApiVersionGetByIdQuery(string Id) : IQuery;

public class ApiVersionGetByIdQueryHandler(MadotDbContext dbDbContext): IQueryHandler<ApiVersionGetByIdQuery,Domain.Models.ApiVersion?>
{
    public override async Task<Domain.Models.ApiVersion?> Handle(ApiVersionGetByIdQuery query)
    {
        return await SafeDbExecuteAsync(async () => await dbDbContext.ApiVersions.FindAsync(query.Id));
    }
}