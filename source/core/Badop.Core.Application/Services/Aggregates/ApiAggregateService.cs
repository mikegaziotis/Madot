using Badop.Core.Domain.Aggregates;
using Badop.Core.Domain.Services;

namespace Badop.Core.Application.Services;

public class ApiAggregateService(IApiService apiService, IApiVersionService apiVersionService, IApiVersionAggregateService apiVersionAggregateService): IApiAggregateService
{

    public async Task<ApiAggregate?> Load(string apiName, bool includePreviewVersions = false, bool includeUnavailableVersions = false)
    {
        var api = await apiService.Get(apiName);
        if (api == null)
            return null;
        var result = new ApiAggregate(apiVersionService, apiVersionAggregateService)
        {
            Name = apiName,
            DisplayName = api.DisplayName,
            BaseUrlPath = api.BaseUrlPath,
            IsInternal = api.IsInternal,
            IsAvailable = api.IsAvailable,
            IsBeta = api.IsBeta,
            OrderId = api.OrderId,
            AllVersions = await apiVersionService.GetAllVersions(apiName, includePreviewVersions, includeUnavailableVersions)
        };
        return result;
    }
}