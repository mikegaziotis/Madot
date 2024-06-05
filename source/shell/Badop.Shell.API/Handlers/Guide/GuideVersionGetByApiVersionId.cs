using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.GuideVersion;
using Badop.Core.Domain.ShortTypes;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record GuideVersionGetByApiVersionIdRequest([FromRoute(Name="api_version_id")] string ApiVersionId):IRequest;

public class GuideVersionGetByApiVersionIdHandler(
    IQueryHandler<GuideVersionGetByApiVersionIdQuery,IEnumerable<GuideVersionShort>> handler):IHandler<GuideVersionGetByApiVersionIdRequest,IResult>
{
    public async Task<IResult> Handle(GuideVersionGetByApiVersionIdRequest request)
    {
        var result = await handler.Handle(new GuideVersionGetByApiVersionIdQuery(request.ApiVersionId));
        return Results.Ok(result);
    }
}