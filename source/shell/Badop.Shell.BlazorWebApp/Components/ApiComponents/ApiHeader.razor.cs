using Badop.Shell.API;
using Microsoft.AspNetCore.Components;

namespace Badop.Shell.BlazorWebApp.Components.ApiComponents;

public partial class ApiHeader : ComponentBase
{
    [Inject]
    public required IAPIVersionApi ApiClient { get; set; }
    
    [Inject]
    public required NavigationManager NavigationManager { get; set; }
    
    [Parameter]
    public required string SelectedVersion { get; set; }
    
    [Parameter]
    public Api? Api { get; set; }

    private List<ApiVersion>? _versions = null;
    

    protected override async Task OnParametersSetAsync()
    {
        if(Api is not null)
        {
            _versions = (await ApiClient.ApiVersionsGetByApiIdAsync(Api.Id)).Where(x=>!x.IsHidden).ToList();
        }
    }
    
    private void VersionChanged(ChangeEventArgs args)
    {
        NavigationManager.NavigateTo($"api/{Api.Id}/homepage/{args.Value}");
    }
}