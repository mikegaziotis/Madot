CREATE PROCEDURE [dbo].[API_Get]
@Name varchar(50)
AS
BEGIN
	SELECT
        [Name]
        ,[DisplayName]
        ,[Sha256HashedKey]
        ,[BaseUrlPath]
        ,[IsInternal]
        ,[IsBeta]
        ,[IsAvailable]
        ,[OrderId]
        ,[CreatedDate]
        ,[CreatedBy]
        ,[LastModifiedDate]
        ,[LastModifiedBy]
	FROM [dbo].[Api]
	WHERE [Name] = @Name
END