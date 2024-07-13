using System;
using System.Collections.Generic;

namespace Madot.Core.Domain.Models;

public record ApiVersionGuideVersion:IModel
{
    public required string ApiVersionId { get; init; }

    public required string GuideVersionId { get; init; }
    
    public int OrderId { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public virtual ApiVersion ApiVersion { get; init; } = null!;

    public virtual GuideVersion GuideVersion { get; init; } = null!;
}
