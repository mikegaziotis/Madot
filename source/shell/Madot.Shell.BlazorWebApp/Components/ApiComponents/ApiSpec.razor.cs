using Madot.Shell.API;
using Microsoft.AspNetCore.Components;

namespace Madot.Shell.BlazorWebApp.Components.ApiComponents;

public partial class ApiSpec : ComponentBase
{
    [Inject] 
    public required IOpenAPISpecificationApi OasClient { get; set; }
    
    [Parameter]
    public required string OasId { get; set; }
    
    [Parameter]
    public required string ApiId { get; set; }

    private string? _oasId;

    protected override async Task OnParametersSetAsync()
    {
        if (OasId == AppSettings.PreviewKeyword)
        {
            _oasId = (await OasClient.OpenApiSpecGetByApiIdAsync(ApiId)).MaxBy(x => x.Iteration)?.Id;
        }
        else
        {
            _oasId = OasId;
        }
    }
}