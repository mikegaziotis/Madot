using Badop.Shell.API.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Handlers;

public class ApiGetHandler(ILogger<ApiGetHandler> logger): IHandler<ApiGetRequest,IResult> 
{
    public async Task<IResult> Handle(ApiGetRequest request)
    {
        await Task.Delay(1);
        logger.LogInformation($"Requested: {request.ApiName}");
        return Results.Ok($"Returns: {request.ApiName}");
    }
}