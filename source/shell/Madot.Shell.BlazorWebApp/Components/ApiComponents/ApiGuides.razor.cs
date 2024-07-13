using Madot.Shell.API;
using Microsoft.AspNetCore.Components;

namespace Madot.Shell.BlazorWebApp.Components.ApiComponents;

public partial class ApiGuides : ComponentBase
{
    [Inject] 
    public required IGuideVersionApi GuideVersionClient { get; set; }
    
    [Inject] 
    public required IGuideApi GuideClient { get; set; }
    
    [Parameter]
    public required string ApiVersionId { get; set; }
    
    [Parameter]
    public required string ApiId { get; set; }

    private List<ApiVersionGuide>? _guides;
    
        
    protected override async Task OnParametersSetAsync()
    {
        if (ApiVersionId == AppSettings.PreviewKeyword)
        {
            var guides = (await GuideClient.GuidesGetByApiIdAsync(ApiId, false)).OrderBy(x=>x.Title).ToList();
            var results = new List<ApiVersionGuide>();
            foreach (var guide in guides)
            {
                var result = new ApiVersionGuide
                {
                    OrderId = guide.ProvisionalOrderId,
                    GuideVersion = await GuideVersionClient.GuideVersionLatestGetByGuideIdAsync(guide.Id)
                };
                results.Add(result);    
            }
            
            _guides = results.OrderBy(x=>x.OrderId).ToList();
        }
        else
        {
            var results = await GuideVersionClient.GuideVersionGetByApiVersionIdAsync(ApiVersionId);
            _guides = results.OrderBy(x=>x.OrderId).ToList();
        }
        
    }
}