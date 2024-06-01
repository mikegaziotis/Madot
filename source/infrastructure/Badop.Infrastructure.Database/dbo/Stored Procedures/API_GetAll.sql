CREATE PROCEDURE [dbo].[API_GetAll]
@IncludeInternal bit = 0,
@IncludeUnavailable bit = 0
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
	WHERE
    IsInternal = IIF(@IncludeInternal = 0, 0, IsInternal)
	AND IsAvailable = IIF(@IncludeUnavailable = 0, 1, IsAvailable)
	ORDER BY OrderId, [Name]
END