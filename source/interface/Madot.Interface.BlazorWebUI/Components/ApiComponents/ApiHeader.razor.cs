using Madot.Interface.API;
using Microsoft.AspNetCore.Components;

namespace Madot.Interface.BlazorWebUI.Components.ApiComponents;

public partial class ApiHeader : ComponentBase
{
    [Inject]
    public required IAPIVersionApi ApiClient { get; set; }
    
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Parameter]
    public required string SelectedVersion { get; set; }
    
    [Parameter]
    public required Api Api { get; set; }

    private bool _parametersSet;
    private List<ApiVersion>? _versions = null;

    protected override async Task OnParametersSetAsync()
    {
        if (SelectedVersion != AppSettings.PreviewKeyword)
        {
            _versions = (await ApiClient.ApiVersionsGetByApiIdAsync(Api!.Id)).Where(x=>!x.IsHidden).ToList();    
        }

        _parametersSet = true;
    }
    
    private void VersionChanged(ChangeEventArgs args)
    {
        NavigationManager.NavigateTo($"api/{Api.Id}/{args.Value}");
    }
}