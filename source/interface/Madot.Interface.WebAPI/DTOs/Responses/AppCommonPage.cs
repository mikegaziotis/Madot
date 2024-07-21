namespace Madot.Interface.WebAPI.DTOs.Responses;

public record AppCommonPageLite(int Id, string Title, int OrderId);
public record AppCommonPage(int Id, string Title, int OrderId, string Data): AppCommonPageLite(Id,Title,OrderId);