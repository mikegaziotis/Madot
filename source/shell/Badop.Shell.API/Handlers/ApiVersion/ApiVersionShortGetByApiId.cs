using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.ApiVersion;
using Badop.Core.Domain.ShortTypes;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record ApiVersionShortGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId, [FromQuery(Name="include_hidden")] bool IncludeHidden=false):IRequest;

public class ApiVersionShortGetByApiIdHandler(
    IQueryHandler<ApiVersionShortGetByApiIdQuery, IEnumerable<ApiVersionShort>> handler): IHandler<ApiVersionShortGetByApiIdRequest, IResult>
{

    public async Task<IResult> Handle(ApiVersionShortGetByApiIdRequest request)
    {
        var result = await handler.Handle(new ApiVersionShortGetByApiIdQuery(request.ApiId, request.IncludeHidden));
        return Results.Ok(result);
    }
}