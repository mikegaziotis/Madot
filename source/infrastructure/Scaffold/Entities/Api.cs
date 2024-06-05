using System;
using System.Collections.Generic;

namespace Scaffold.Entities;

public partial class Api
{
    public string Id { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string BaseUrlPath { get; set; } = null!;

    public bool IsInternal { get; set; }

    public bool IsPreview { get; set; }

    public bool IsHidden { get; set; }

    public int OrderId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string LastModifiedBy { get; set; } = null!;

    public DateTime LastModifiedDate { get; set; }

    public virtual ICollection<ApiVersion> ApiVersions { get; set; } = new List<ApiVersion>();

    public virtual ICollection<File> Files { get; set; } = new List<File>();

    public virtual ICollection<Guide> Guides { get; set; } = new List<Guide>();

    public virtual ICollection<VersionedDocument> VersionedDocuments { get; set; } = new List<VersionedDocument>();
}
