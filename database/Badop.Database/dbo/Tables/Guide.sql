CREATE TABLE [dbo].[Guide]
(
  [Id]                VARCHAR(10)   NOT NULL,
  [ApiId]             VARCHAR(30)   NOT NULL,
  [Title]             VARCHAR(100)  NOT NULL,
  [IsDeleted]         BIT           CONSTRAINT [DF_Guide_IsInternal] DEFAULT ((0)) NOT NULL,
  [CreatedBy]         VARCHAR(100)  NOT NULL,
  [CreatedDate]       DATETIME2(7)  CONSTRAINT [DF_Guide_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
  [LastModifiedBy]    VARCHAR(100)  NOT NULL,
  [LastModifiedDate]  DATETIME2(7)  CONSTRAINT [DF_Guide_LastModifiedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
  [ValidFrom]         DATETIME2(7)  GENERATED ALWAYS AS ROW START NOT NULL,
  [ValidTo]           DATETIME2(7)  GENERATED ALWAYS AS ROW END NOT NULL,
  CONSTRAINT [PK_Guide] PRIMARY KEY CLUSTERED ([Id] ASC),
  CONSTRAINT [FK_Guide_Api] FOREIGN KEY ([ApiId]) REFERENCES [dbo].[Api]([Id]),
  CONSTRAINT [CK_Guide_Title] UNIQUE NONCLUSTERED ([ApiId],[Title]),
  PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
)
WITH
(
  SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[Guide_History])
);