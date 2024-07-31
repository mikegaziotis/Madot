using Madot.Interface.API;
using Microsoft.AspNetCore.Components;

namespace Madot.Interface.BlazorWebUI.Components.ApiComponents;

public partial class ApiHomepage : ComponentBase
{
    [Inject]
    public required IHomepageApi HomepageClient { get; set; }

    [Parameter] 
    public required string ApiId { get; set; }
    
    [Parameter] 
    public required string HomepageId { get; set; }

    private Homepage? _homepage;
    private bool _parametersSet;

    protected override async Task OnParametersSetAsync()
    {
        if (HomepageId!=AppSettings.PreviewKeyword)
        {
            _homepage = await HomepageClient.HomepageGetByIdAsync(HomepageId);
        }
        else
        {
            _homepage = (await HomepageClient.HomepageGetByApiIdAsync(ApiId)).MaxBy(x => x.Iteration);
        }

        _parametersSet = true;
    }
}