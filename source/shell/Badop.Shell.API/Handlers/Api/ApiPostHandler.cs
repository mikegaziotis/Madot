using Badop.Shell.API.Requests;

namespace Badop.Shell.API.Handlers;

public class ApiPostHandler(ILogger<ApiPostHandler> logger): IHandler<ApiPostRequest,IResult>
{
    public async Task<IResult> Handle(ApiPostRequest request)
    {
        await Task.Delay(1);
        logger.LogInformation($"Adding api {request.Api.Name}");
        return Results.Created( "Success",new { ApiName = request.Api.Name, ApiKey = Guid.NewGuid() });
    }
}