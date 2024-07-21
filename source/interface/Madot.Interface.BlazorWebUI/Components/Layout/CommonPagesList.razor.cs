using Madot.Interface.API;
using Microsoft.AspNetCore.Components;

namespace Madot.Interface.BlazorWebUI.Components.Layout;

public partial class CommonPagesList : ComponentBase
{
    [Inject] 
    public required NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public required IAppCommonPagesApi StaticPagesClient { get; set; }

    private List<AppCommonPageLite>? _commonPages = null;

    protected override async Task OnInitializedAsync()
    {
        _commonPages = (await StaticPagesClient.AppCommonPagesGetAllAsync(false)).ToList();
    }
}