using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.GuideVersion;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record GuideVersionGetByIdRequest([FromRoute(Name="guide_version_id")] string Id) : IRequest;

public class GuideVersionGetByIdHandler(
    IQueryHandler<GuideVersionGetbyIdQuery, GuideVersion?> handler) : IHandler<GuideVersionGetByIdRequest,IResult> 
{
    public async Task<IResult> Handle(GuideVersionGetByIdRequest request)
    {
        var result = await handler.Handle(new GuideVersionGetbyIdQuery(request.Id));
        if(result is null)
            return Results.NotFound();
        
        return Results.Ok(result);
    }
}

