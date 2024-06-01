CREATE PROCEDURE [dbo].[APIVersion_Get]
    @Id VARCHAR(72)
AS
BEGIN
    SELECT
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
    WHERE [Id] = @Id
END