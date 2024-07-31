using Madot.Interface.API;
using Microsoft.AspNetCore.Components;
using File = Madot.Interface.API.File;

namespace Madot.Interface.BlazorWebUI.Components.ApiComponents;

public partial class ApiDownloadFiles : ComponentBase
{
    [Inject]
    public required IFileApi FileClient { get; set; }
    
    [Parameter]
    public required string ApiVersionId { get; set; }
    
    [Parameter]
    public required string ApiId { get; set; }

    private List<File>? _files;

    protected override async Task OnParametersSetAsync()
    {
        if (ApiVersionId == AppSettings.PreviewKeyword)
        {
            _files = (await FileClient.FilesGetByApiIdAsync(ApiId)).Where(x=>!x.IsDeleted).ToList();
        }
        else
        {
            _files = (await FileClient.FilesGetByApiVersionIdAsync(ApiVersionId,null,null)).ToList();    
        }
    }
}