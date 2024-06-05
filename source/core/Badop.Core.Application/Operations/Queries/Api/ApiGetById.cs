using Badop.Core.Application.Operations.Queries;
using Microsoft.Extensions.Logging;

namespace Badop.Core.Application.Operations.Commands.Api;

public record ApiGetByIdQuery(string Id):IQuery;

public class ApiGetByIdQueryHandler(
    BadopContext dbContext) : IQueryHandler<ApiGetByIdQuery,Domain.Models.Api?>
{
    public async Task<Domain.Models.Api?> Handle(ApiGetByIdQuery query)
    {
        return await dbContext.Apis.FindAsync(query.Id);
    }
}