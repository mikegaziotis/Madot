using Madot.Interface.API;
using Microsoft.AspNetCore.Components;

namespace Madot.Interface.BlazorWebUI.Components.ApiComponents;

public partial class ApiChangelog : ComponentBase
{

    [Inject] 
    public required IChangelogApi ChangelogClient { get; set; }

    [Parameter] 
    public required string ChangelogId { get; set; }
    
    [Parameter] 
    public required string ApiId { get; set; }

    private Changelog? _changelog;

    protected override async Task OnParametersSetAsync()
    {
        if (ChangelogId!=AppSettings.PreviewKeyword)
        {
            _changelog = await ChangelogClient.ChangelogGetByIdAsync(ChangelogId);
        }
        else
        {
            _changelog = (await ChangelogClient.ChangelogGetByApiIdAsync(ApiId)).MaxBy(x => x.Iteration);
        }
    }
}