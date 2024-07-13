CREATE TABLE [dbo].[GuideVersion]
(
  [Id]                VARCHAR(10)   NOT NULL,
  [GuideId]           VARCHAR(10)   NOT NULL,
  [Iteration]         INT           NOT NULL, 
  [Data]              VARCHAR(MAX)  NOT NULL,
  [IsDeleted]         BIT           CONSTRAINT [DF_GuideVersion_IsDeleted] DEFAULT ((0)) NOT NULL,
  [CreatedBy]         VARCHAR(100)  NOT NULL,
  [CreatedDate]       DATETIME2(7)  CONSTRAINT [DF_GuideVersion_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
  [LastModifiedBy]    VARCHAR(100)  NOT NULL,
  [LastModifiedDate]  DATETIME2(7)  CONSTRAINT [DF_GuideVersion_LastModifiedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
  [ValidFrom]         DATETIME2(7)  GENERATED ALWAYS AS ROW START NOT NULL,
  [ValidTo]           DATETIME2(7)  GENERATED ALWAYS AS ROW END NOT NULL,
  CONSTRAINT [PK_GuideVersion] PRIMARY KEY CLUSTERED ([Id] ASC),
  CONSTRAINT [FK_GuideVersion_Guide] FOREIGN KEY ([GuideId]) REFERENCES [dbo].[Guide]([Id]),
  CONSTRAINT [UC_GuideVersion_Iteration] UNIQUE ([GuideId],[Iteration]),
  PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
)
WITH
(
  SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[GuideVersion_History])
);