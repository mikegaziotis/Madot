using System;
using System.Collections.Generic;

namespace Scaffold.Entities;

public partial class Guide
{
    public string Id { get; set; } = null!;

    public string ApiId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string LastModifiedBy { get; set; } = null!;

    public DateTime LastModifiedDate { get; set; }

    public virtual Api Api { get; set; } = null!;

    public virtual ICollection<GuideVersion> GuideVersions { get; set; } = new List<GuideVersion>();
}
