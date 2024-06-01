

CREATE PROCEDURE [dbo].[APIVersion_Insert]
@ApiName varchar(50),
@HashedKey varchar(64),
@MajorVersion int,
@MinorVersion int,
@BuildReleaseReference varchar(20),
@OpenApiSpecId varchar(82),
@DocumentationId varchar(82),
@ChangeLogId uniqueidentifier,
@IsPreview bit = NULL,
@IsAvailable bit = NULL,
@UserId varchar(100) = NULL
AS
BEGIN
	BEGIN TRANSACTION;  
	BEGIN TRY  
		-- Generate a constraint violation error.  
		IF EXISTS (SELECT NULL FROM Api WITH (TABLOCKX) WHERE [Name] = @ApiName AND Sha256HashedKey = @HashedKey)
		AND NOT EXISTS (SELECT NULL FROM ApiVersion WITH (TABLOCKX) WHERE [ApiName]=@ApiName AND MajorVersion=@MajorVersion AND MinorVersion = @MinorVersion)
		BEGIN
            DECLARE @Id VARCHAR(72) = (SELECT @ApiName+':'+CAST(@MajorVersion as [varchar](10))+'.'+CAST(@MinorVersion as [varchar](10)));
			INSERT INTO [dbo].[ApiVersion]
                ([Id]
                ,[ApiName]
                ,[MajorVersion]
                ,[MinorVersion]
                ,[BuildOrReleaseReference]
                ,[OpenApiSpecId]
                ,[DocumentationId]
                ,[ChangeLogId]
                ,[IsPreview]
                ,[IsAvailable]
                ,[CreatedBy])
			VALUES
                (@Id
                ,@ApiName
                ,@MajorVersion
                ,@MinorVersion
                ,@BuildReleaseReference
                ,@OpenApiSpecId
                ,@DocumentationId
                ,@ChangeLogId
                ,@IsPreview
                ,@IsAvailable
                ,@UserId)
			SELECT CAST(IIF(@@ROWCOUNT = 1, 1, 0) as bit) Success
            COMMIT TRANSACTION
			RETURN;
		END
	END TRY  
	BEGIN CATCH  
		IF @@TRANCOUNT > 0  
			ROLLBACK TRANSACTION;
	END CATCH;  
  
	IF @@TRANCOUNT > 0  
		COMMIT TRANSACTION;  
	
	SELECT CAST(0 as bit) Success
END