using Madot.Interface.API;
using Microsoft.AspNetCore.Components;

namespace Madot.Interface.BlazorWebUI.Components.Layout;

public partial class ApiList : ComponentBase
{
    [Inject] 
    public required IAPIApi ApiClient { get; set; }
    private ICollection<Api>? _apis;

    protected override async Task OnInitializedAsync()
    {
        _apis = await ApiClient.ApiGetAllAsync(true);
    }
    private async Task ApiFilterChange(ChangeEventArgs changeEventArgs)
    {
        var textValue = changeEventArgs.Value?.ToString();
        if (string.IsNullOrEmpty(textValue)|| textValue.Length<2)
        {
            _apis = await ApiClient.ApiGetAllAsync(true);
            return;
        }
        
        _apis = await ApiClient.ApiSearchByNameAsync(textValue, SearchMethod.Contains);
    }
}