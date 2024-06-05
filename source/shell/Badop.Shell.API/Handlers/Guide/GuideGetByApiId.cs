using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Guide;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record GuideGetAllByApiIdRequest([FromRoute] string ApiId, [FromQuery] bool IncludeDeleted=false):IRequest;

public class GuideGetAllByApiIdHandler(
    IQueryHandler<GuideGetAllByApiIdQuery,IEnumerable<Guide>> handler):IHandler<GuideGetAllByApiIdRequest,IResult>
{
    public async Task<IResult> Handle(GuideGetAllByApiIdRequest request)
    {
        var result = await handler.Handle(new GuideGetAllByApiIdQuery(request.ApiId, request.IncludeDeleted));
        return Results.Ok(result);
    }
}