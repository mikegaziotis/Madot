using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.ApiVersion;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record ApiVersionGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId): IRequest;

public class ApiVersionGetByApiIdHandler(
    IQueryHandler<ApiVersionGetByApiIdQuery,IEnumerable<ApiVersion>> handler): IHandler<ApiVersionGetByApiIdRequest,IResult>
{
    public async Task<IResult> Handle(ApiVersionGetByApiIdRequest request)
    {
        var result = await handler.Handle(new ApiVersionGetByApiIdQuery(request.ApiId));
        return Results.Ok(result);
    }
}