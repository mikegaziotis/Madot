using Badop.Shell.API.DTO;
using Badop.Shell.API.Requests;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Badop.Shell.API.Handlers;

public class ApiPatchHandler(ILogger<ApiPatchHandler> logger): IHandler<ApiPatchRequest, IResult>
{
    public async Task<IResult> Handle(ApiPatchRequest request)
    {
        var apiKey = request.ApiKey;
        var api = new ApiDTO
        {
            Name = request.ApiName,
            DisplayName = request.ApiName
        };
        /*if (!context.Request.HasJsonContentType())
        {
            throw new BadHttpRequestException(
                "Request content type was not a recognized JSON content type.",
                StatusCodes.Status415UnsupportedMediaType);
        }*/
        var str = JsonSerializer.Serialize(request.JsonPatch);
        var result = JsonConvert.DeserializeObject<JsonPatchDocument>(str);

        try
        {
            result?.ApplyTo(api);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString(),e);
        }
        
        await Task.Delay(1);
        return Results.Ok();
    }
}