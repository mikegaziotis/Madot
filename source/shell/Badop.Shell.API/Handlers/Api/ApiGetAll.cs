using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Api;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record ApiGetAllRequest([FromQuery(Name="visible_only")] bool VisibleOnly=false):IRequest;


public class ApiGetAllHandler(IQueryHandler<ApiGetAllQuery, IEnumerable<Api>> handler): IHandler<ApiGetAllRequest,IResult> 
{
    public async Task<IResult> Handle(ApiGetAllRequest request)
    {
        var result = await handler.Handle(new ApiGetAllQuery(request.VisibleOnly));
        return Results.Ok(result);
    }
}