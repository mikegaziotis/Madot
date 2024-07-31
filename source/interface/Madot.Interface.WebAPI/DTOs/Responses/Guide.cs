namespace Madot.Interface.WebAPI.DTOs.Responses;

public record Guide
{
    public required string Id { get; init; }

    public required string ApiId { get; init; }

    public required string Title { get; init; }

    public int ProvisionalOrderId { get; init; }
    
    public bool IsDeleted { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }
}

public record GuideVersion
{
    public required string Id { get; init; }

    public required string GuideId { get; init; }

    public int Iteration { get; init; }

    public required string Data { get; init; }

    public bool IsDeleted { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }

    public virtual Guide? Guide { get; init; }
}

public record ApiVersionGuide
{
    public int OrderId { get; init; }
    public GuideVersion GuideVersion { get; init; }
}