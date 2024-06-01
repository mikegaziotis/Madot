

CREATE PROCEDURE [dbo].[API_Insert]
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
	BEGIN TRY
		INSERT INTO [dbo].[Api]
            ([Name]
            ,[DisplayName]
            ,[Sha256HashedKey]
            ,[BaseUrlPath]
            ,[IsInternal]
            ,[IsBeta]
            ,[IsAvailable]
            ,[OrderId]
            ,[CreatedBy])
		VALUES
            (@Name 
            ,@DisplayName 
		    ,@Sha256HashedKey
            ,@BaseUrlPath
            ,@IsInternal 
            ,@IsBeta 
            ,@IsAvailable 
            ,@OrderId
            ,@UserId)
		SELECT CAST(IIF(@@ROWCOUNT = 1, 1, 0) as bit) Success
	END TRY
	BEGIN CATCH
		SELECT CAST(0 as bit) as Success
	END CATCH
END