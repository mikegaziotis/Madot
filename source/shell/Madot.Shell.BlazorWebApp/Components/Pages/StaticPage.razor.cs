using Madot.Shell.API;
using Microsoft.AspNetCore.Components;

namespace Madot.Shell.BlazorWebApp.Components.Pages;

public partial class StaticPage : ComponentBase
{
    [Parameter] public int StaticPageId { get; set; }
    
    [Inject] public IAppCommonPagesApi StaticPageClient { get; set; }

    private string? _data;

    protected override async Task OnParametersSetAsync()
    {
        _data = (await StaticPageClient.AppCommonPageGetByIdAsync(StaticPageId)).Data;
    }
}