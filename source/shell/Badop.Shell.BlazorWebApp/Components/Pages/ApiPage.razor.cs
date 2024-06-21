using Badop.Shell.API;
using Microsoft.AspNetCore.Components;

namespace Badop.Shell.BlazorWebApp.Components.Pages;

public partial class ApiPage : ComponentBase
{
    #region Injected services
    
    [Inject]
    public required IAPIApi ApiClient { get; set; }
    
    [Inject]
    public required IAPIVersionApi ApiVersionClient { get; set; }
    
    [Inject]
    public required IOpenAPISpecificationApi OpenApiClient { get; set; }
    
    #endregion
    
    [Parameter]
    public required string ApiId { get; set; }
    
    [Parameter]
    public string? VersionId { get; set; }

    [Parameter] 
    public string? SectionName { get; set; } = "Homepage";

    private ApiVersion? _apiVersion;
    private Api? _api;

    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }

    protected override async Task OnParametersSetAsync()
    { 
        _api = await ApiClient.ApiGetByIdAsync(ApiId);
        
        if (VersionId is null)
        {
            _apiVersion = await ApiVersionClient.ApiVersionGetLatestByApiIdAsync(ApiId, false);    
        }
        else
        {
            _apiVersion = await ApiVersionClient.ApiVersionGetByIdAsync(VersionId);
        }

    }
}