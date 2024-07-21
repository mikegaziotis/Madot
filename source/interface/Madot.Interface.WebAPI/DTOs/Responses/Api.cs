namespace Madot.Interface.WebAPI.DTOs.Responses;

public record Api
{
    public required string Id { get; init; }

    public required string DisplayName { get; init; }
 
    public required string BaseUrlPath { get; init; }

    public bool IsInternal { get; init; }

    public bool IsPreview { get; init; }

    public bool IsHidden { get; init; }

    public int OrderId { get; init; }

    public required string CreatedBy { get; init; }

    public DateTime CreatedDate { get; init; }

    public required string LastModifiedBy { get; init; }

    public DateTime LastModifiedDate { get; init; }
}