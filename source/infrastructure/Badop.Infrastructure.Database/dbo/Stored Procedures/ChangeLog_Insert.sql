CREATE PROCEDURE dbo.ChangeLog_Insert
    @ApiName VARCHAR(50),
    @FileTypeExtension VARCHAR(5),
    @Data VARCHAR(MAX),
    @UserId VARCHAR(100) = NULL
AS
BEGIN
    DECLARE @Id UNIQUEIDENTIFIER = (SELECT NEWID())
    INSERT INTO ChangeLog(Id, ApiName, FileTypeExtension, Data, CreatedBy)
    VALUES(@Id, @ApiName, @FileTypeExtension, @Data, @UserId)
    SELECT @Id as [Id]
END