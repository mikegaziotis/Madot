CREATE PROCEDURE dbo.ChangeLog_Get
@Id VARCHAR(36)
AS
BEGIN
    SELECT 
        cl.[Id]
        ,cl.[ApiName]
        ,[FileTypeExtension]
        ,[Data]             
        ,cl.[CreatedBy]        
        ,cl.[CreatedDate]
    FROM dbo.ChangeLog as cl
    LEFT JOIN dbo.ApiVersion as av 
        ON av.[ApiName] = cl.[ApiName] and cl.[Id]= av.[ChangeLogId]
    WHERE cl.[Id]=@Id
END