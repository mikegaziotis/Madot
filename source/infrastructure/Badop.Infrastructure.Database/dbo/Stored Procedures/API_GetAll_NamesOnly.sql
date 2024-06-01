CREATE PROCEDURE [dbo].[API_GetAll_NamesOnly]
@IncludeInternal bit = 0,
@IncludeUnavailable bit = 0
AS
BEGIN
	SELECT [Name]
	FROM [dbo].[Api]
	WHERE
    IsInternal = IIF(@IncludeInternal = 0, 0, IsInternal)
	AND IsAvailable = IIF(@IncludeUnavailable = 0, 1, IsAvailable)
	ORDER BY OrderId, [Name]
END