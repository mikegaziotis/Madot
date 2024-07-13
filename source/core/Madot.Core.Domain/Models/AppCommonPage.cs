namespace Madot.Core.Domain.Models;

public record AppCommonPage:IModel
{
    public int Id { get; init; }
    public required string Title { get; init; }
    public required string Data { get; init; }
    public int OrderId { get; init; }
    public bool IsDeleted { get; init; }
    public required string CreatedBy { get; init; }
    public DateTime CreatedDate { get; init; }
    public required string LastModifiedBy { get; init; }
    public DateTime LastModifiedDate { get; init; }
}