using System;
using System.Collections.Generic;

namespace Scaffold.Entities;

public partial class FileLink
{
    public string FileId { get; set; } = null!;

    public string OperatingSystem { get; set; } = null!;

    public string ChipArchitecture { get; set; } = null!;

    public string DownloadUrl { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string LastModifiedBy { get; set; } = null!;

    public DateTime LastModifiedDate { get; set; }

    public virtual File File { get; set; } = null!;
}
