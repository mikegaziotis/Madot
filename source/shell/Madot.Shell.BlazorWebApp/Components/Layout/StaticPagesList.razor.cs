using Madot.Shell.API;
using Microsoft.AspNetCore.Components;

namespace Madot.Shell.BlazorWebApp.Components.Layout;

public partial class StaticPagesList : ComponentBase
{
    [Inject] 
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required IAppCommonPagesApi StaticPagesClient { get; set; }

    private List<AppCommonPageLite>? _staticPages = null;

    protected override async Task OnInitializedAsync()
    {
        _staticPages = (await StaticPagesClient.AppCommonPagesGetAllAsync(false)).ToList();
    }
}