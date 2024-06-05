using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Guide;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record GuideGetByApiIdRequest([FromRoute(Name="api_id")] string ApiId, [FromQuery(Name="include_deleted")] bool IncludeDeleted=false):IRequest;

public class GuideGetByApiIdHandler(
    IQueryHandler<GuideGetAllByApiIdQuery,IEnumerable<Guide>> handler):IHandler<GuideGetByApiIdRequest,IResult>
{
    public async Task<IResult> Handle(GuideGetByApiIdRequest request)
    {
        var result = await handler.Handle(new GuideGetAllByApiIdQuery(request.ApiId, request.IncludeDeleted));
        return Results.Ok(result);
    }
}