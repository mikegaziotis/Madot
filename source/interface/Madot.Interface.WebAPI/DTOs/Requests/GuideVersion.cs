namespace Madot.Interface.WebAPI.DTOs.Requests;

public record GuideVersionInsertCommand
{
    public required string GuideId { get; init; }

    public required string Data { get; init; }

}

public record GuideVersionUpdateCommand
{
    public required string Id { get; init; }

    public string Data { get; init; }
    
    public bool IsDeleted { get; init; }
}
