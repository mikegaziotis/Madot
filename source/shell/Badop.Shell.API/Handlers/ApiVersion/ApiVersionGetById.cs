using Badop.Core.Application.Operations.Queries;
using Badop.Core.Application.Operations.Queries.ApiVersion;
using Badop.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public record ApiVersionGetByIdRequest([FromRoute(Name="api_version_id")] string Id): IRequest;

public class ApiVersionGetByIdHandler(
    IQueryHandler<ApiVersionGetByIdQuery, ApiVersion?> handler) : IHandler<ApiVersionGetByIdRequest, IResult>
{
    public async Task<IResult> Handle(ApiVersionGetByIdRequest request)
    {
        var result = await handler.Handle(new ApiVersionGetByIdQuery(request.Id));
        if (result is null)
            return Results.NotFound();

        return Results.Ok(result);
    }
}