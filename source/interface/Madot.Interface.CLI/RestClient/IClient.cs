
using Refit;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Madot.Interface.API
{
    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.1.3.0")]
    public partial interface IAPIApi
    {
        [Get("/api/{id}")]
        Task<Api> ApiGetByIdAsync(string id);

        [Get("/api/list")]
        Task<ICollection<Api>> ApiGetAllAsync([Query] bool? visible_only);

        [Get("/api/search-by-name")]
        Task<ICollection<Api>> ApiSearchByNameAsync([Query] string search_term, [Query, AliasAs("SearchMethod")] SearchMethod searchMethod);

        [Post("/api")]
        Task ApiInsertAsync([Body] ApiInsertCommand body);

        [Put("/api")]
        Task ApiUpdateAsync([Body] ApiUpdateCommand body);
    }

    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.1.3.0")]
    public partial interface IAPIVersionApi
    {
        [Get("/api-version/{id}")]
        Task<ApiVersion> ApiVersionGetByIdAsync(string id);

        [Get("/api-version/latest/{api_id}")]
        Task<ApiVersion> ApiVersionGetLatestByApiIdAsync(string api_id, [Query] bool? include_hidden);

        [Get("/api-version/list/{api_id}")]
        Task<ICollection<ApiVersion>> ApiVersionsGetByApiIdAsync(string api_id);

        [Post("/api-version")]
        Task<StringIdCreated> ApiVersionInsertAsync([Body] ApiVersionInsertCommand body);

        [Put("/api-version")]
        Task ApiVersionUpdateAsync([Body] ApiVersionUpdateCommand body);
    }

    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.1.3.0")]
    public partial interface IAppCommonPagesApi
    {
        [Get("/app-common-page/{id}")]
        Task<AppCommonPage> AppCommonPageGetByIdAsync(int id);

        [Get("/app-common-page/list")]
        Task<ICollection<AppCommonPageLite>> AppCommonPagesGetAllAsync([Query] bool? include_deleted);

        [Post("/app-common-page")]
        Task<IntIdCreated> AppCommonPageInsertAsync([Body] AppCommonPageInsertCommand body);

        [Put("/app-common-page")]
        Task AppCommonPageUpdateAsync([Body] AppCommonPageUpdateCommand body);
    }

    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.1.3.0")]
    public partial interface IChangelogApi
    {
        [Get("/changelog/{id}")]
        Task<Changelog> ChangelogGetByIdAsync(string id);

        [Get("/changelog/by-api-version/{api_version_id}")]
        Task<Changelog> ChangelogGetByApiVersionIdAsync(string api_version_id);

        [Get("/changelog/by-api/{api_id}")]
        Task<ICollection<Changelog>> ChangelogGetByApiIdAsync(string api_id);

        [Get("/changelog/latest/{api_id}")]
        Task<Changelog> ChangelogGetLatestByApiIdAsync(string api_id);

        [Post("/changelog")]
        Task<StringIdCreated> ChangelogInsertAsync([Body] VersionedDocumentInsertCommand body);

        [Put("/changelog")]
        Task ChangelogUpdateAsync([Body] VersionedDocumentUpdateCommand body);
    }

    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.1.3.0")]
    public partial interface IDocumentStatusApi
    {
        [Get("/document-status/by-api-version-id/{api_version_id}")]
        Task<DocumentStatus> DocumentStatusGetByApiVersionIdAsync(string api_version_id);

        [Get("/document-status/by-api-id/{api_id}")]
        Task<DocumentStatus> DocumentStatusGetByApiIdAsync(string api_id);
    }

    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.1.3.0")]
    public partial interface IFileApi
    {
        [Get("/file/{id}")]
        Task<File> FileGetByIdAsync(string id);

        [Get("/file/list/by-api/{api_id}")]
        Task<ICollection<File>> FilesGetByApiIdAsync(string api_id);

        [Get("/file/list/by-api-version/{api_version_id}")]
        Task<ICollection<File>> FilesGetByApiVersionIdAsync(string api_version_id, [Query, AliasAs("OperatingSystem")] OperatingSystem? operatingSystem, [Query, AliasAs("ChipArchitecture")] ChipArchitecture? chipArchitecture);

        [Post("/file")]
        Task FileInsertAsync([Body] FileInsertCommand body);

        [Put("/file")]
        Task FileUpdateAsync([Body] FileUpdateCommand body);
    }

    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.1.3.0")]
    public partial interface IGuideApi
    {
        [Get("/guide/{id}")]
        Task<Guide> GuideGetByIdAsync(string id);

        [Get("/guide/list/{api_id}")]
        Task<ICollection<Guide>> GuidesGetByApiIdAsync(string api_id, [Query] bool? include_deleted);

        [Post("/guide")]
        Task<StringIdCreated> GuideInsertAsync([Body] GuideInsertCommand body);

        [Put("/guide")]
        Task GuideUpdateAsync([Body] GuideUpdateCommand body);
    }

    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.1.3.0")]
    public partial interface IGuideVersionApi
    {
        [Get("/guide-version/{id}")]
        Task<GuideVersion> GuideVersionGetByIdAsync(string id);

        [Get("/guide-version/by-api-version/{api_version_id}")]
        Task<ICollection<ApiVersionGuide>> GuideVersionGetByApiVersionIdAsync(string api_version_id);

        [Get("/guide-version/latest/{guide_id}")]
        Task<GuideVersion> GuideVersionLatestGetByGuideIdAsync(string guide_id);

        [Post("/guide-version")]
        Task<StringIdCreated> GuideVersionInsertAsync([Body] GuideVersionInsertCommand body);

        [Put("/guide-version")]
        Task GuideVersionUpdateAsync([Body] GuideVersionUpdateCommand body);
    }

    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.1.3.0")]
    public partial interface IHomepageApi
    {
        [Get("/homepage/{id}")]
        Task<Homepage> HomepageGetByIdAsync(string id);

        [Get("/homepage/by-api-version/{api_version_id}")]
        Task<Homepage> HomepageGetByApiVersionIdAsync(string api_version_id);

        [Get("/homepage/by-api/{api_id}")]
        Task<ICollection<Homepage>> HomepageGetByApiIdAsync(string api_id);

        [Get("/homepage/latest/{api_id}")]
        Task<Homepage> HomepageGetLatestByApiIdAsync(string api_id);

        [Post("/homepage")]
        Task<StringIdCreated> HomepageInsertAsync([Body] VersionedDocumentInsertCommand body);

        [Put("/homepage")]
        Task HomepageUpdateAsync([Body] VersionedDocumentUpdateCommand body);
    }

    [System.CodeDom.Compiler.GeneratedCode("Refitter", "1.1.3.0")]
    public partial interface IOpenAPISpecificationApi
    {
        [Get("/open-api-spec/{id}")]
        Task<OpenApiSpec> OpenApiSpecGetByIdAsync(string id);

        [Get("/open-api-spec/as-html/{id}")]
        Task OpenApiSpecHtmlGetByIdAsync(string id);

        [Get("/open-api-spec/by-api-version/{api_version_id}")]
        Task<OpenApiSpec> OpenApiSpecGetByApiVersionIdAsync(string api_version_id);

        [Get("/open-api-spec/by-api/{api_id}")]
        Task<ICollection<OpenApiSpec>> OpenApiSpecGetByApiIdAsync(string api_id);

        [Get("/open-api-spec/latest/{api_id}")]
        Task<OpenApiSpec> OpenApiSpecGetLatestByApiIdAsync(string api_id);

        [Post("/open-api-spec")]
        Task<StringIdCreated> OpenApiSpecInsertAsync([Body] VersionedDocumentInsertCommand body);

        [Put("/open-api-spec")]
        Task OpenApiSpecUpdateAsync([Body] VersionedDocumentUpdateCommand body);
    }


}


//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 612 // Disable "CS0612 '...' is obsolete"
#pragma warning disable 649 // Disable "CS0649 Field is never assigned to, and will always have its default value null"
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"
#pragma warning disable 8604 // Disable "CS8604 Possible null reference argument for parameter"
#pragma warning disable 8625 // Disable "CS8625 Cannot convert null literal to non-nullable reference type"
#pragma warning disable 8765 // Disable "CS8765 Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes)."

namespace Madot.Interface.API
{
    using System = global::System;

    

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Api
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("baseUrlPath")]
        public string BaseUrlPath { get; set; }

        [JsonPropertyName("isInternal")]
        public bool IsInternal { get; set; }

        [JsonPropertyName("isPreview")]
        public bool IsPreview { get; set; }

        [JsonPropertyName("isHidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public System.DateTimeOffset CreatedDate { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonPropertyName("lastModifiedDate")]
        public System.DateTimeOffset LastModifiedDate { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiInsertCommand
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("baseUrlPath")]
        public string BaseUrlPath { get; set; }

        [JsonPropertyName("isInternal")]
        public bool IsInternal { get; set; }

        [JsonPropertyName("isPreview")]
        public bool IsPreview { get; set; }

        [JsonPropertyName("isHidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiUpdateCommand
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("baseUrlPath")]
        public string BaseUrlPath { get; set; }

        [JsonPropertyName("isInternal")]
        public bool IsInternal { get; set; }

        [JsonPropertyName("isPreview")]
        public bool IsPreview { get; set; }

        [JsonPropertyName("isHidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiVersion
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("apiId")]
        public string ApiId { get; set; }

        [JsonPropertyName("majorVersion")]
        public int MajorVersion { get; set; }

        [JsonPropertyName("minorVersion")]
        public int MinorVersion { get; set; }

        [JsonPropertyName("buildOrReleaseTag")]
        public string BuildOrReleaseTag { get; set; }

        [JsonPropertyName("openApiSpecId")]
        public string OpenApiSpecId { get; set; }

        [JsonPropertyName("homepageId")]
        public string HomepageId { get; set; }

        [JsonPropertyName("changelogId")]
        public string ChangelogId { get; set; }

        [JsonPropertyName("isBeta")]
        public bool IsBeta { get; set; }

        [JsonPropertyName("isHidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public System.DateTimeOffset CreatedDate { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonPropertyName("lastModifiedDate")]
        public System.DateTimeOffset LastModifiedDate { get; set; }

        [JsonPropertyName("fileItems")]
        public ICollection<FileItem> FileItems { get; set; }

        [JsonPropertyName("guideVersionItems")]
        public ICollection<GuideVersionItem> GuideVersionItems { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiVersionGuide
    {

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("guideVersion")]
        public GuideVersion GuideVersion { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiVersionInsertCommand
    {

        [JsonPropertyName("apiId")]
        public string ApiId { get; set; }

        [JsonPropertyName("majorVersion")]
        public int MajorVersion { get; set; }

        [JsonPropertyName("minorVersion")]
        public int MinorVersion { get; set; }

        [JsonPropertyName("buildOrReleaseTag")]
        public string? BuildOrReleaseTag { get; set; }

        [JsonPropertyName("openApiSpecId")]
        public string OpenApiSpecId { get; set; }

        [JsonPropertyName("homepageId")]
        public string HomepageId { get; set; }

        [JsonPropertyName("changelogId")]
        public string? ChangelogId { get; set; }

        [JsonPropertyName("isBeta")]
        public bool IsBeta { get; set; }

        [JsonPropertyName("isHidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("guideVersionItems")]
        public ICollection<GuideVersionItem> GuideVersionItems { get; set; }

        [JsonPropertyName("fileItems")]
        public ICollection<FileItem> FileItems { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ApiVersionUpdateCommand
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("buildOrReleaseTag")]
        public string? BuildOrReleaseTag { get; set; }

        [JsonPropertyName("openApiSpecId")]
        public string OpenApiSpecId { get; set; }

        [JsonPropertyName("homepageId")]
        public string HomepageId { get; set; }

        [JsonPropertyName("changelogId")]
        public string? ChangelogId { get; set; }

        [JsonPropertyName("isBeta")]
        public bool IsBeta { get; set; }

        [JsonPropertyName("isHidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("guideVersionItems")]
        public ICollection<GuideVersionItem> GuideVersionItems { get; set; }

        [JsonPropertyName("fileItems")]
        public ICollection<FileItem> FileItems { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class AppCommonPage
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class AppCommonPageInsertCommand
    {

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class AppCommonPageLite
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class AppCommonPageUpdateCommand
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Changelog
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("apiId")]
        public string ApiId { get; set; }

        [JsonPropertyName("iteration")]
        public int Iteration { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public System.DateTimeOffset CreatedDate { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonPropertyName("lastModifiedDate")]
        public System.DateTimeOffset LastModifiedDate { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum ChipArchitecture
    {

        [System.Runtime.Serialization.EnumMember(Value = @"X64")]
        X64 = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"Arm64")]
        Arm64 = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"Any")]
        Any = 2,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class DocumentStatus
    {

        [JsonPropertyName("hasGuides")]
        public bool HasGuides { get; set; }

        [JsonPropertyName("hasChangelog")]
        public bool HasChangelog { get; set; }

        [JsonPropertyName("hasDownloadFiles")]
        public bool HasDownloadFiles { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class File
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("apiId")]
        public string ApiId { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public System.DateTimeOffset CreatedDate { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonPropertyName("lastModifiedDate")]
        public System.DateTimeOffset LastModifiedDate { get; set; }

        [JsonPropertyName("fileLinks")]
        public ICollection<FileLink> FileLinks { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class FileInsertCommand
    {

        [JsonPropertyName("apiId")]
        public string ApiId { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("fileLinks")]
        public ICollection<FileLinkItem> FileLinks { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class FileItem
    {

        [JsonPropertyName("fileId")]
        public string FileId { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class FileLink
    {

        [JsonPropertyName("fileId")]
        public string FileId { get; set; }

        [JsonPropertyName("operatingSystem")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OperatingSystem OperatingSystem { get; set; }

        [JsonPropertyName("chipArchitecture")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ChipArchitecture ChipArchitecture { get; set; }

        [JsonPropertyName("downloadUrl")]
        public string DownloadUrl { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public System.DateTimeOffset CreatedDate { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonPropertyName("lastModifiedDate")]
        public System.DateTimeOffset LastModifiedDate { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class FileLinkItem
    {

        [JsonPropertyName("operatingSystem")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OperatingSystem OperatingSystem { get; set; }

        [JsonPropertyName("chipArchitecture")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ChipArchitecture ChipArchitecture { get; set; }

        [JsonPropertyName("downloadUrl")]
        public string DownloadUrl { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class FileUpdateCommand
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("fileLinks")]
        public ICollection<FileLinkItem> FileLinks { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Guide
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("apiId")]
        public string ApiId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("provisionalOrderId")]
        public int ProvisionalOrderId { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public System.DateTimeOffset CreatedDate { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonPropertyName("lastModifiedDate")]
        public System.DateTimeOffset LastModifiedDate { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class GuideInsertCommand
    {

        [JsonPropertyName("apiId")]
        public string ApiId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("provisionalOrderId")]
        public int ProvisionalOrderId { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class GuideUpdateCommand
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("provisionalOrderId")]
        public int ProvisionalOrderId { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class GuideVersion
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("guideId")]
        public string GuideId { get; set; }

        [JsonPropertyName("iteration")]
        public int Iteration { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public System.DateTimeOffset CreatedDate { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonPropertyName("lastModifiedDate")]
        public System.DateTimeOffset LastModifiedDate { get; set; }

        [JsonPropertyName("guide")]
        public Guide Guide { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class GuideVersionInsertCommand
    {

        [JsonPropertyName("guideId")]
        public string GuideId { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class GuideVersionItem
    {

        [JsonPropertyName("guideVersionId")]
        public string GuideVersionId { get; set; }

        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class GuideVersionUpdateCommand
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool IsDeleted { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class Homepage
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("apiId")]
        public string ApiId { get; set; }

        [JsonPropertyName("iteration")]
        public int Iteration { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public System.DateTimeOffset CreatedDate { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonPropertyName("lastModifiedDate")]
        public System.DateTimeOffset LastModifiedDate { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class IntIdCreated
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class OpenApiSpec
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("apiId")]
        public string ApiId { get; set; }

        [JsonPropertyName("iteration")]
        public int Iteration { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdDate")]
        public System.DateTimeOffset CreatedDate { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonPropertyName("lastModifiedDate")]
        public System.DateTimeOffset LastModifiedDate { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum OperatingSystem
    {

        [System.Runtime.Serialization.EnumMember(Value = @"Windows")]
        Windows = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"Linux")]
        Linux = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"MacOS")]
        MacOS = 2,

        [System.Runtime.Serialization.EnumMember(Value = @"Any")]
        Any = 3,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public enum SearchMethod
    {

        [System.Runtime.Serialization.EnumMember(Value = @"StartsWith")]
        StartsWith = 0,

        [System.Runtime.Serialization.EnumMember(Value = @"Contains")]
        Contains = 1,

        [System.Runtime.Serialization.EnumMember(Value = @"EndsWith")]
        EndsWith = 2,

        [System.Runtime.Serialization.EnumMember(Value = @"Exact")]
        Exact = 3,

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class StringIdCreated
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class VersionedDocumentInsertCommand
    {

        [JsonPropertyName("apiId")]
        public string ApiId { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class VersionedDocumentUpdateCommand
    {

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("data")]
        public string Data { get; set; }

    }


}

#pragma warning restore  108
#pragma warning restore  114
#pragma warning restore  472
#pragma warning restore  612
#pragma warning restore 1573
#pragma warning restore 1591
#pragma warning restore 8073
#pragma warning restore 3016
#pragma warning restore 8603
#pragma warning restore 8604
#pragma warning restore 8625


#nullable enable
namespace Madot.Interface.API
{
    using System;
   using Microsoft.Extensions.DependencyInjection;
   using Polly;
   using Polly.Contrib.WaitAndRetry;
   using Polly.Extensions.Http;

    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureRefitClients(
            this IServiceCollection services, 
            Action<IHttpClientBuilder>? builder = default, 
            RefitSettings? settings = default)
        {
            var clientBuilderIAPIApi = services
                .AddRefitClient<IAPIApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000"));

            clientBuilderIAPIApi
                .AddPolicyHandler(
                    HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(
                            Backoff.DecorrelatedJitterBackoffV2(
                                TimeSpan.FromSeconds(0.5),
                                3)));

            builder?.Invoke(clientBuilderIAPIApi);

            var clientBuilderIAPIVersionApi = services
                .AddRefitClient<IAPIVersionApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000"));

            clientBuilderIAPIVersionApi
                .AddPolicyHandler(
                    HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(
                            Backoff.DecorrelatedJitterBackoffV2(
                                TimeSpan.FromSeconds(0.5),
                                3)));

            builder?.Invoke(clientBuilderIAPIVersionApi);

            var clientBuilderIAppCommonPagesApi = services
                .AddRefitClient<IAppCommonPagesApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000"));

            clientBuilderIAppCommonPagesApi
                .AddPolicyHandler(
                    HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(
                            Backoff.DecorrelatedJitterBackoffV2(
                                TimeSpan.FromSeconds(0.5),
                                3)));

            builder?.Invoke(clientBuilderIAppCommonPagesApi);

            var clientBuilderIChangelogApi = services
                .AddRefitClient<IChangelogApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000"));

            clientBuilderIChangelogApi
                .AddPolicyHandler(
                    HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(
                            Backoff.DecorrelatedJitterBackoffV2(
                                TimeSpan.FromSeconds(0.5),
                                3)));

            builder?.Invoke(clientBuilderIChangelogApi);

            var clientBuilderIDocumentStatusApi = services
                .AddRefitClient<IDocumentStatusApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000"));

            clientBuilderIDocumentStatusApi
                .AddPolicyHandler(
                    HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(
                            Backoff.DecorrelatedJitterBackoffV2(
                                TimeSpan.FromSeconds(0.5),
                                3)));

            builder?.Invoke(clientBuilderIDocumentStatusApi);

            var clientBuilderIFileApi = services
                .AddRefitClient<IFileApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000"));

            clientBuilderIFileApi
                .AddPolicyHandler(
                    HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(
                            Backoff.DecorrelatedJitterBackoffV2(
                                TimeSpan.FromSeconds(0.5),
                                3)));

            builder?.Invoke(clientBuilderIFileApi);

            var clientBuilderIGuideApi = services
                .AddRefitClient<IGuideApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000"));

            clientBuilderIGuideApi
                .AddPolicyHandler(
                    HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(
                            Backoff.DecorrelatedJitterBackoffV2(
                                TimeSpan.FromSeconds(0.5),
                                3)));

            builder?.Invoke(clientBuilderIGuideApi);

            var clientBuilderIGuideVersionApi = services
                .AddRefitClient<IGuideVersionApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000"));

            clientBuilderIGuideVersionApi
                .AddPolicyHandler(
                    HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(
                            Backoff.DecorrelatedJitterBackoffV2(
                                TimeSpan.FromSeconds(0.5),
                                3)));

            builder?.Invoke(clientBuilderIGuideVersionApi);

            var clientBuilderIHomepageApi = services
                .AddRefitClient<IHomepageApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000"));

            clientBuilderIHomepageApi
                .AddPolicyHandler(
                    HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(
                            Backoff.DecorrelatedJitterBackoffV2(
                                TimeSpan.FromSeconds(0.5),
                                3)));

            builder?.Invoke(clientBuilderIHomepageApi);

            var clientBuilderIOpenAPISpecificationApi = services
                .AddRefitClient<IOpenAPISpecificationApi>(settings)
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000"));

            clientBuilderIOpenAPISpecificationApi
                .AddPolicyHandler(
                    HttpPolicyExtensions
                        .HandleTransientHttpError()
                        .WaitAndRetryAsync(
                            Backoff.DecorrelatedJitterBackoffV2(
                                TimeSpan.FromSeconds(0.5),
                                3)));

            builder?.Invoke(clientBuilderIOpenAPISpecificationApi);

            return services;
        }
    }
}