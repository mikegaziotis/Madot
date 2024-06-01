CREATE PROCEDURE dbo.VersionedDocument_Get
@Id VARCHAR(72)
AS 
BEGIN 
    SELECT
        [Id]
        ,[ApiName]
        ,[DocumentType]     
        ,[Version]          
        ,[FileTypeExtension]
        ,[IsLatest]
        ,[Data]
        ,[CreatedDate]      
        ,[CreatedBy]        
    FROM [dbo].[VersionedDocument] 
    WHERE [Id]=@Id
END