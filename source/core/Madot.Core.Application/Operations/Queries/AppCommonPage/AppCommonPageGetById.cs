namespace Madot.Core.Application.Operations.Queries;

public record AppCommonPageGetByIdQuery(int Id): IQuery;

public class AppCommonPageGetByIdQueryHandler(
    MadotDbContext dbDbContext) : IQueryHandler<AppCommonPageGetByIdQuery,Domain.Models.AppCommonPage?>
{
    public override async Task<Domain.Models.AppCommonPage?> Handle(AppCommonPageGetByIdQuery query)
    {
        return await SafeDbExecuteAsync(async ()=> await dbDbContext.AppCommonPages.FindAsync(query.Id));
    }
}