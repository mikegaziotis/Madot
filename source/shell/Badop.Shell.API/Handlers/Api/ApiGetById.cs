using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.Api;
using Badop.Core.Domain.Models;
using Badop.Shell.API.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;
public class ApiGetRequest: IRequest
{
    [FromRoute]
    public string Id { get; set; }
}


public class ApiGetHandler(IQueryHandler<ApiGetByIdQuery, Api?> handler): IHandler<ApiGetRequest,IResult> 
{
    public async Task<IResult> Handle(ApiGetRequest request)
    {
        var result = await handler.Handle(new ApiGetByIdQuery(request.Id));
        if(result is not null)
            return Results.Ok($"Returns: {result.DisplayName}");

        return Results.NotFound();
    }
}