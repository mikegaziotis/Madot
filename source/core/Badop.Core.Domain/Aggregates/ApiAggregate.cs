using Badop.Core.Domain.Entities;
using Badop.Core.Domain.Services;

namespace Badop.Core.Domain.Aggregates;

public record ApiAggregate()
{
    public required string Name { get; init; } 
    public string DisplayName { get; set; }
    public string? BaseUrlPath { get; set; }
    public bool IsInternal { get; set; }
    public bool IsBeta { get; set; }
    public bool IsAvailable { get; set; }
    public int OrderId { get; set; }
    public IEnumerable<ApiVersionShort> AllVersions { get; set; }
    public ApiVersionAggregate? SelectedVersion { get; private set; }

    private readonly IApiVersionService _apiVersionService;
    private readonly IApiVersionAggregateService _versionAggregateService;

    public ApiAggregate(IApiVersionService apiVersionService, IApiVersionAggregateService versionAggregateService) : this()
    {
        _apiVersionService = apiVersionService;
        _versionAggregateService = versionAggregateService;
    }

    public async Task LoadLatestVersion(bool includePreviews = false)
    {
        var apiVersion = await _apiVersionService.GetLatest(Name, includePreviews);
        if (apiVersion is { Id: not null })// Pattern, equivalent to: apiVersion != null && apiVersion.Id!=null
        {
            SelectedVersion = await _versionAggregateService.Load(apiVersion.Id);
        }
    }

    public async Task SelectVersion(string apiVersionId)
    {
        SelectedVersion = await _versionAggregateService.Load(apiVersionId);
    }
}