CREATE PROCEDURE dbo.VersionedDocument_GetLatest
@ApiName varchar(50),
@DocumentType varchar(20)
AS
BEGIN
    SELECT [Id]
    FROM [dbo].[VersionedDocument]
    WHERE [ApiName] = @ApiName
    AND [DocumentType] = @DocumentType 
END