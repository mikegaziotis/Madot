namespace Badop.Core.Domain.Entities;

public record Api
{
    public string Name { get; init; }
    public string DisplayName { get; init; }
    public string? BaseUrlPath { get; set; }
    public bool IsInternal { get; set; }
    public bool IsBeta { get; set; }
    public bool IsAvailable { get; set; }
    public int OrderId { get; set; }
};