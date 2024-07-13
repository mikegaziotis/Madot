namespace Madot.Core.Application.Operations.Queries.Api;

public record ApiGetByIdQuery(string Id):IQuery;

public class ApiGetByIdQueryHandler(
    MadotDbContext dbDbContext) : IQueryHandler<ApiGetByIdQuery,Domain.Models.Api?>
{
    public override async Task<Domain.Models.Api?> Handle(ApiGetByIdQuery query)
    {
        return await SafeDbExecuteAsync(async () => await dbDbContext.Apis.FindAsync(query.Id));
    }
}