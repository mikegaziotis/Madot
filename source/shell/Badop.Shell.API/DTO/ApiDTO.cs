namespace Badop.Shell.API.DTO;

public class ApiDTO
{
    public required string Name { get; init; }
    public required string DisplayName { get; init; }
    public string? BaseUrlPath { get; set; }
    public bool IsInternal { get; set; }
    public bool IsBeta { get; set; }
    public bool IsAvailable { get; set; }
    public int OrderId { get; set; }
}