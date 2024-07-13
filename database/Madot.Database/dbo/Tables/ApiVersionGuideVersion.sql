CREATE TABLE [dbo].[ApiVersionGuideVersion]
(
  [ApiVersionId]    VARCHAR(10)   NOT NULL,
  [GuideVersionId]  VARCHAR(10)   NOT NULL,
  [OrderId]         INT           NOT NULL,
  [CreatedBy]       VARCHAR(100)  NOT NULL,
  [CreatedDate]     DATETIME2(7)  CONSTRAINT [DF_ApiVersionGuideVersion_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
  [ValidFrom]       DATETIME2(7)  GENERATED ALWAYS AS ROW START NOT NULL,
  [ValidTo]         DATETIME2(7)  GENERATED ALWAYS AS ROW END NOT NULL,
  CONSTRAINT [PK_ApiVersionGuideVersion] PRIMARY KEY CLUSTERED (ApiVersionId,GuideVersionId),
  CONSTRAINT [FK_ApiVersionGuideVersion_ApiVersion] FOREIGN KEY (ApiVersionId) REFERENCES [dbo].[ApiVersion]([Id]),
  CONSTRAINT [FK_ApiVersionGuideVersion_GuideVersion] FOREIGN KEY (GuideVersionId) REFERENCES [dbo].[GuideVersion]([Id]),
  PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
) 
WITH
(
  SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[ApiVersionGuideVersion_History])
);

