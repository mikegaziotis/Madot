using Badop.Shell.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Badop.Shell.API.Requests;

public class ApiGetRequest: IRequest
{
    [FromRoute]
    public string ApiName { get; set; }
}

public class ApiPatchRequest: IRequest
{
    [FromRoute] public string ApiName { get; set; }
    [FromHeader] public string ApiKey { get; set; }
    [FromBody] public IEnumerable<JsonPatchOperation> JsonPatch { get; set; }
}

public class ApiPostRequest: IRequest
{
    [FromBody] public ApiDTO Api { get; set; }
}