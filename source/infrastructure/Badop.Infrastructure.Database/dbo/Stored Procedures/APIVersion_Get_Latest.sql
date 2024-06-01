CREATE PROCEDURE [dbo].[APIVersion_Get_Latest]
@ApiName VARCHAR(50),
@IncludePreview bit = 0
AS
BEGIN
    
    SELECT TOP 1
        [Id]
         ,[ApiName]
         ,[MajorVersion]
         ,[MinorVersion]
         ,[BuildOrReleaseReference]
         ,[OpenApiSpecId]
         ,[DocumentationId]
         ,[ChangeLogId]
         ,[IsPreview]
         ,[IsAvailable]
         ,[CreatedDate]
         ,[CreatedBy]
         ,[LastModifiedDate]
         ,[LastModifiedBy]
    FROM [dbo].[ApiVersion]
    WHERE [ApiName] = @ApiName
    AND IsPreview=IIF(@IncludePreview=0, 0, [IsPreview])
    ORDER BY MajorVersion desc, MinorVersion desc
END