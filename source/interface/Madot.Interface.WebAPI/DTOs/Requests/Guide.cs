namespace Madot.Interface.WebAPI.DTOs.Requests;

public record GuideInsertCommand
{
    
    public required string ApiId { get; init; }

    public required string Title { get; init; }

    public int ProvisionalOrderId { get; init; }
    
}

public record GuideUpdateCommand
{
    public required string Id { get; init; }

    public int ProvisionalOrderId { get; init; }
    
    public bool IsDeleted { get; init; }
}