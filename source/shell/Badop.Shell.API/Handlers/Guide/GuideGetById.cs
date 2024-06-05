using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Guide;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record GuideGetByIdRequest([FromRoute(Name="guide_id")] string Id) : IRequest;

public class GuideGetByIdHandler(
    IQueryHandler<GuideGetByIdQuery,Guide?> handler):IHandler<GuideGetByIdRequest,IResult> 
{
    public async Task<IResult> Handle(GuideGetByIdRequest request)
    {
        var result = await handler.Handle(new GuideGetByIdQuery(request.Id));
        if (result is null)
            return Results.NotFound();
        
        return Results.Ok(result);
    }
}