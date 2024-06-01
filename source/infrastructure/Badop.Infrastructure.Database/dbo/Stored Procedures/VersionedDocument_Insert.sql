CREATE PROCEDURE dbo.VersionedDocument_Insert
    @ApiName VARCHAR(50),
    @DocumentType VARCHAR(20),
    @FileTypeExtension VARCHAR(5),
    @Data VARCHAR(MAX),
    @UserId VARCHAR(100) = NULL
AS
BEGIN
    BEGIN TRANSACTION;
    BEGIN TRY

        DECLARE @Id VARCHAR(72) = (SELECT [Id] FROM VersionedDocument WITH (TABLOCKX) WHERE IsLatest=1 AND DocumentType=@DocumentType AND [ApiName]=@ApiName)
        DECLARE @NewVersion INT = (SELECT isnull(max(Version),1) FROM VersionedDocument WHERE DocumentType=@DocumentType AND [ApiName]=@ApiName)
    
        IF @Id IS NOT NULL
        BEGIN
            IF @FileTypeExtension = (SELECT FileTypeExtension FROM VersionedDocument WHERE [Id]=@Id)
            AND @Data = (SELECT [Data] FROM VersionedDocument WHERE [Id]=@Id)
            BEGIN
                SELECT CAST(NULL as VARCHAR(72)) AS [Id], 'No changes found' as [Message]
                COMMIT TRANSACTION 
                RETURN
            END
            
        END
        INSERT INTO dbo.VersionedDocument(
            [ApiName]
            ,[DocumentType]
            ,[Version]
            ,[FileTypeExtension]
            ,[IsLatest]
            ,[CreatedBy]
        )
        VALUES (
            @ApiName
            ,@DocumentType
            ,@NewVersion
            ,@FileTypeExtension
            ,1
            ,@UserId
        )
        SELECT @ApiName + ':' + @DocumentType + ':' + CAST(@NewVersion as [VARCHAR](10)) AS [Id], '' as [Message]
        COMMIT TRANSACTION 
        RETURN
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
    END CATCH;

    IF @@TRANCOUNT > 0
        COMMIT TRANSACTION;

    SELECT CAST(NULL as VARCHAR(72)) AS [Id], 'Exception. Bad input'
END