using Badop.Shell.API;
using Microsoft.AspNetCore.Components;

namespace Badop.Shell.BlazorWebApp.Components.ApiComponents;

public partial class ApiHomepage : ComponentBase
{
    [Inject]
    public required IHopemageApi HomepageClient { get; set; }

    [Parameter] 
    public string? HomepageId { get; set; }

    private Homepage? _homepage;

    protected override async Task OnParametersSetAsync()
    {
        if(HomepageId is not null)
        {
            _homepage = await HomepageClient.HomepageGetByIdAsync(HomepageId);
        }
    }
}