using System;
using System.Collections.Generic;

namespace Scaffold.Entities;

public partial class ApiVersion
{
    public string Id { get; set; } = null!;

    public string ApiId { get; set; } = null!;

    public int MajorVersion { get; set; }

    public int MinorVersion { get; set; }

    public string? BuildOrReleaseTag { get; set; }

    public string OpenApiSpecId { get; set; } = null!;

    public string? HomepageId { get; set; }

    public string? ChangelogId { get; set; }

    public bool IsBeta { get; set; }

    public bool IsHidden { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string LastModifiedBy { get; set; } = null!;

    public DateTime LastModifiedDate { get; set; }

    public virtual Api Api { get; set; } = null!;

    public virtual ICollection<ApiVersionGuideVersion> ApiVersionGuideVersions { get; set; } = new List<ApiVersionGuideVersion>();

    public virtual VersionedDocument? Changelog { get; set; }

    public virtual VersionedDocument? Homepage { get; set; }

    public virtual VersionedDocument OpenApiSpec { get; set; } = null!;
}
