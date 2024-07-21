using Madot.Interface.API;
using Microsoft.AspNetCore.Components;

namespace Madot.Interface.BlazorWebUI.Components.Pages;

public partial class HomePage : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IAppCommonPagesApi AppCommonPagesClient { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        var firstId = (await AppCommonPagesClient.AppCommonPagesGetAllAsync(false)).MinBy(x => x.OrderId)?.Id;
        if (firstId is not null)
        {
            NavigationManager.NavigateTo($"/static-page/{firstId}");
        }
    }
}