CREATE TABLE [dbo].[AppCommonPage]
(
  [Id]                INT           IDENTITY(1,1),
  [Title]             VARCHAR(100)  NOT NULL,
  [Data]              VARCHAR(max)  NOT NULL,
  [OrderId]           INT           NOT NULL,
  [IsDeleted]         BIT           CONSTRAINT [DF_AppCommonPage_IsInternal] DEFAULT ((0)) NOT NULL,
  [CreatedBy]         VARCHAR(100)  NOT NULL,
  [CreatedDate]       DATETIME2(7)  CONSTRAINT [DF_AppCommonPage_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
  [LastModifiedBy]    VARCHAR(100)  NOT NULL,
  [LastModifiedDate]  DATETIME2(7)  CONSTRAINT [DF_AppCommonPage_LastModifiedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
  [ValidFrom]         DATETIME2(7)  GENERATED ALWAYS AS ROW START NOT NULL,
  [ValidTo]           DATETIME2(7)  GENERATED ALWAYS AS ROW END NOT NULL,
  CONSTRAINT [PK_AppCommonPage] PRIMARY KEY CLUSTERED ([Id] ASC),
  CONSTRAINT [CK_AppCommonPage_Title] UNIQUE ([Title]),
  PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
)
WITH
(
  SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[AppCommonPage_History])
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_AppCommonPage_OrderId] 
ON [dbo].[AppCommonPage] ([OrderId])
WHERE [IsDeleted] = 0;
GO
