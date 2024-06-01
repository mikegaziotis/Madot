
CREATE PROCEDURE [dbo].[APIVersion_Update]
@Id varchar(72),
@HashedKey varchar(64),
@BuildOrReleaseReference varchar(20),
@OpenApiSpecId varchar(82),
@DocumentationId varchar(82),
@ChangeLogId uniqueidentifier,
@IsPreview bit = NULL,
@IsAvailable bit = NULL,
@UserId varchar(100) = NULL
AS
BEGIN
    DECLARE @ApiName varchar(50)
    
	BEGIN TRANSACTION;  
  
	BEGIN TRY  
	    SELECT @ApiName = [ApiName] FROM dbo.ApiVersion WITH (TABLOCKX) WHERE Id = @Id  
		-- Generate a constraint violation error.  
	    IF @ApiName IS NULL
	    BEGIN
            COMMIT TRANSACTION;
            SELECT CAST(0 as bit) Success;
            RETURN;
	    END
		IF EXISTS (SELECT NULL FROM Api WITH (TABLOCKX) WHERE [Name] = @ApiName AND Sha256HashedKey = @HashedKey)
		BEGIN
			UPDATE [dbo].[ApiVersion]
			SET 
			    [BuildOrReleaseReference]=@BuildOrReleaseReference
			    ,[OpenApiSpecId] = @OpenApiSpecId
			    ,[DocumentationId] = @DocumentationId
			    ,[ChangeLogId] = @ChangeLogId
			    ,[IsPreview] = @IsPreview
			    ,[IsAvailable] = @IsAvailable
			    ,[LastModifiedBy]= @UserId
			    ,[LastModifiedDate]=GETUTCDATE()
		    WHERE Id = @Id 
			SELECT CAST(IIF(@@ROWCOUNT = 1, 1, 0) as bit) Success
            COMMIT TRANSACTION;
            RETURN;
		END
	END TRY  
	BEGIN CATCH  
		IF @@TRANCOUNT > 0  
			ROLLBACK TRANSACTION;  
	END CATCH;  
  
	IF @@TRANCOUNT > 0  
		COMMIT TRANSACTION;  

	SELECT CAST(0 as bit) Success;

END