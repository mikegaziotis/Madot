namespace Madot.Interface.WebAPI.DTOs.Requests;

public record AppCommonPageInsertCommand
{
    public required string Title { get; init; }
    public required string Data { get; init; }
    public int OrderId { get; init; }
}


public record AppCommonPageUpdateCommand
{
    public int Id { get; init; }
    public required string Data { get; init; }
    public int OrderId { get; init; }
    public bool IsDeleted { get; init; }
}