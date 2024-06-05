using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.ApiVersion;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record ApiVersionGetLatestByApiIdRequest([FromRoute(Name="api_id")] string ApiId, [FromQuery(Name="include_hidden")] bool IncludeHidden=false):IRequest;

public class ApiVersionGetLatestByApiIdHandler(
    IQueryHandler<ApiVersionGetLatestByApiIdQuery,ApiVersion?> handler): IHandler<ApiVersionGetLatestByApiIdRequest,IResult>
{
    public async Task<IResult> Handle(ApiVersionGetLatestByApiIdRequest request)
    {
        var result = await handler.Handle(new ApiVersionGetLatestByApiIdQuery(request.ApiId, request.IncludeHidden));
        if (result is null)
            return Results.NotFound();

        return Results.Ok(result);
    }
}