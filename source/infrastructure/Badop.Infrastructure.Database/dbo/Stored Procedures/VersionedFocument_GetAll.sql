CREATE PROCEDURE dbo.VersionedDocument_Get_ForVersion
    @ApiName varchar(50),
    @DocumentType varchar(20) = NULL
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
    WHERE [ApiName] = @ApiName
    AND [DocumentType] = IIF(@DocumentType IS NOT NULL, @DocumentType, [DocumentType])
END