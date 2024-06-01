CREATE PROCEDURE dbo.ChangeLog_Get_ForVersions
@ApiName varchar(50),
@ApiVersionsFilter [ApiVersionIdList] readonly
AS
BEGIN
    SELECT
        cl.[Id]
        ,cl.[ApiName]
        ,[FileTypeExtension]
        ,[Data]
        ,cl.[CreatedBy]
        ,cl.[CreatedDate]
    FROM dbo.ChangeLog as cl
    JOIN ApiVersion as av 
        ON cl.ApiName = av.ApiName AND cl.Id = av.ChangeLogId
    JOIN @ApiVersionsFilter as avf
        ON av.Id = avf.[Id]
    WHERE cl.[ApiName] = @ApiName
    ORDER BY av.MajorVersion desc, av.MinorVersion desc
END