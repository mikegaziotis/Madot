using Madot.Interface.API;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.StaticWebAssets;

namespace Madot.Interface.BlazorWebUI.Components.Pages;

public partial class ApiPage : ComponentBase
{
    #region Injected services
    
    [Inject]
    public required IAPIApi ApiClient { get; set; }
    
    [Inject]
    public required IAPIVersionApi ApiVersionClient { get; set; }

    [Inject] public required IDocumentStatusApi DocumentStatusClient { get; set; }

    #endregion
    
    [Parameter]
    public required string ApiId { get; set; }
    
    [Parameter]
    public string? VersionId { get; set; }

    [Parameter] 
    public string? SectionName { get; set; } = "Homepage";

    private bool _parametersSet;
    
    private ApiVersion? _apiVersion;
    private Api? _api;
    private DocumentStatus? _documentStatus;
    private string? _versionId;

    protected override async Task OnParametersSetAsync()
    { 
        _api = await ApiClient.ApiGetByIdAsync(ApiId);
        
        if (VersionId is null)
        {
            _apiVersion = await ApiVersionClient.ApiVersionGetLatestByApiIdAsync(ApiId, false);
            _versionId = _apiVersion.Id;
        }
        else
        {
            if (!string.Equals(VersionId, AppSettings.PreviewKeyword, StringComparison.OrdinalIgnoreCase))
            {
                _apiVersion = await ApiVersionClient.ApiVersionGetByIdAsync(VersionId);
                _versionId = _apiVersion.Id;
            }
            else
            {
                _versionId = AppSettings.PreviewKeyword;
            }
        }

        if (_apiVersion is not null)
        {
            _documentStatus = await DocumentStatusClient.DocumentStatusGetByApiVersionIdAsync(_apiVersion.Id);
        }
        else
        {
            _documentStatus = await DocumentStatusClient.DocumentStatusGetByApiIdAsync(ApiId);
        }

        _parametersSet = true;
    }
}