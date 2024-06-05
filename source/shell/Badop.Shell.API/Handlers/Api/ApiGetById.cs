using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Api;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record ApiGetByIdRequest([FromRoute(Name = "api_id")] string Id) : IRequest;

public class ApiGetByIdHandler(IQueryHandler<ApiGetByIdQuery, Api?> handler): IHandler<ApiGetByIdRequest,IResult> 
{
    public async Task<IResult> Handle(ApiGetByIdRequest request)
    {
        var result = await handler.Handle(new ApiGetByIdQuery(request.Id));
        if(result is null)
            return Results.NotFound();

        return Results.Ok(result);
    }
}