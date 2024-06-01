CREATE PROCEDURE [dbo].[API_Update]
	@Name varchar(50),
	@DisplayName varchar(100),
	@Sha256HashedKey varchar(64),
    @BaseUrlPath varchar(100),
    @IsInternal bit = 0,
	@IsBeta bit = 0,
	@IsAvailable bit = 1,
	@OrderId int = 1,
    @UserId varchar(100) = NULL
AS 
BEGIN

	UPDATE [dbo].[Api]
	SET 
        [DisplayName] = @DisplayName
	    ,[BaseUrlPath] = @BaseUrlPath
        ,[IsInternal] = @IsInternal
        ,[IsBeta] = @IsBeta
        ,[IsAvailable] = @IsAvailable
        ,[OrderId] = @OrderId
	    ,[LastModifiedBy] = @UserId
        ,[LastModifiedDate] = GETUTCDATE()
	WHERE [Name] = @Name AND Sha256HashedKey = @Sha256HashedKey

	SELECT CAST(IIF(@@ROWCOUNT = 1, 1, 0) as bit) Success

END