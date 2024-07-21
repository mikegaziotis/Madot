using Madot.Interface.API;
using Microsoft.AspNetCore.Components;

namespace Madot.Interface.BlazorWebUI.Components.Pages;

public partial class CommonPage : ComponentBase
{
    [Parameter] public int StaticPageId { get; set; }
    
    [Inject] public required IAppCommonPagesApi CommonPageClient { get; set; }

    private string? _data;

    protected override async Task OnParametersSetAsync()
    {
        _data = (await CommonPageClient.AppCommonPageGetByIdAsync(StaticPageId)).Data;
    }
}