using Badop.Core.Domain.Aggregates;
using Badop.Core.Domain.Entities;
using Badop.Core.Domain.Services;

namespace Badop.Core.Application.Services;

public class ApiVersionAggregateService(
    IApiVersionService apiVersionService,
    IVersionedDocumentService versionedDocumentService,
    IChangeLogService changeLogService)
    : IApiVersionAggregateService
{
    public async Task<ApiVersionAggregate?> Load(string id)
    {
        var apiVersion = await apiVersionService.Get(id);
        if (apiVersion==null)
        {
            return null;
        }
        var documentationTask = versionedDocumentService.Get(apiVersion.DocumentationId);
        var openApiSpecTask = versionedDocumentService.Get(apiVersion.OpenApiSpecId);
        ChangeLog? changeLog = null;
        if (!string.IsNullOrEmpty(apiVersion.ChangeLogId))
        {
            changeLog = await changeLogService.Get(apiVersion.ChangeLogId);
        }

        return PopulateObject(apiVersion, await openApiSpecTask, await documentationTask, changeLog);
    }

    public async Task<ApiVersionAggregate?> Load(string apiName, string version)
    {
        return await Load($"{apiName}:{version}");
    }

    public async Task<ApiVersionAggregate?> Load(string apiName, int majorVersion, int minorVersion)
    {
        return await Load($"{apiName}:{majorVersion}.{minorVersion}");
    }

    private ApiVersionAggregate PopulateObject(ApiVersion apiVersion, VersionedDocument openApiSpec,
        VersionedDocument documentation, ChangeLog? changeLog)
    {
        return new ApiVersionAggregate()
        {
            ApiName = apiVersion.ApiName,
            BuildOrReleaseReference = apiVersion.BuildOrReleaseReference,
            ChangeLog = changeLog,
            Documentation = documentation,
            Id = apiVersion.Id,
            MajorVersion = apiVersion.MajorVersion,
            MinorVersion = apiVersion.MinorVersion,
            IsAvailable = apiVersion.IsAvailable,
            IsPreview = apiVersion.IsPreview,
            OpenApiSpec = openApiSpec
        };
    }
}