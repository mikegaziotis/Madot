using System;
using System.Collections.Generic;

namespace Scaffold.Entities;

public partial class GuideVersion
{
    public string Id { get; set; } = null!;

    public string GuideId { get; set; } = null!;

    public int Iteration { get; set; }

    public string Data { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string LastModifiedBy { get; set; } = null!;

    public DateTime LastModifiedDate { get; set; }

    public virtual ICollection<ApiVersionGuideVersion> ApiVersionGuideVersions { get; set; } = new List<ApiVersionGuideVersion>();

    public virtual Guide Guide { get; set; } = null!;
}
