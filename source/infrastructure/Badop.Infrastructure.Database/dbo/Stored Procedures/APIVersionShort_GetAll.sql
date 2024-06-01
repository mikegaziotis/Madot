CREATE PROCEDURE [dbo].[APIVersionShort_GetAll]
@ApiName varchar(50),
@IncludePreview bit = 0, 
@IncludeUnavailable bit = 0 
AS
BEGIN
	SELECT 
	    [MajorVersion]
	    ,[MinorVersion]
		,[IsPreview]
		,[IsAvailable]
	FROM [dbo].[ApiVersion]
	WHERE ApiName = @ApiName
	AND IsPreview = IIF(@IncludePreview = 0, 0, IsPreview)
	AND IsAvailable = IIF(@IncludeUnavailable = 0, 1, IsAvailable)
	ORDER BY MajorVersion desc, MinorVersion desc
END