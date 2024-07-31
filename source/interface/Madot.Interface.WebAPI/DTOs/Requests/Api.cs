namespace Madot.Interface.WebAPI.DTOs.Requests;

public record ApiInsertCommand
{
    public required string Id { get; init; }

    public required string DisplayName { get; init; }

    public required string BaseUrlPath { get; init; }

    public bool IsInternal { get; init; }

    public bool IsPreview { get; init; }

    public bool IsHidden { get; init; }

    public int OrderId { get; init; }
}

public record ApiUpdateCommand
{
    public required string Id { get; init; }

    public required string DisplayName { get; init; }
 
    public required string BaseUrlPath { get; init; }

    public bool IsInternal { get; init; }

    public bool IsPreview { get; init; }

    public bool IsHidden { get; init; }

    public int OrderId { get; init; }
}
