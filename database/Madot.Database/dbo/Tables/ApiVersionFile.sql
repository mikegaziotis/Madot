CREATE TABLE [dbo].[ApiVersionFile]
(
  [ApiVersionId]    VARCHAR(10)   NOT NULL,
  [FileId]          VARCHAR(10)   NOT NULL,
  [OrderId]         INT           NOT NULL,
  [CreatedBy]       VARCHAR(100)  NOT NULL,
  [CreatedDate]     DATETIME2(7)  CONSTRAINT [DF_ApiVersionFile_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
  [ValidFrom]       DATETIME2(7)  GENERATED ALWAYS AS ROW START NOT NULL,
  [ValidTo]         DATETIME2(7)  GENERATED ALWAYS AS ROW END NOT NULL,
  CONSTRAINT [PK_ApiVersionFile] PRIMARY KEY CLUSTERED (ApiVersionId,FileId),
  CONSTRAINT [FK_ApiVersionFile_ApiVersion] FOREIGN KEY (ApiVersionId) REFERENCES [dbo].[ApiVersion]([Id]),
  CONSTRAINT [FK_ApiVersionFile_File] FOREIGN KEY (FileId) REFERENCES [dbo].[File]([Id]),
  PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
) 
WITH
(
  SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[ApiVersionFile_History])
);

