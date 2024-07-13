CREATE TABLE [dbo].[File]
(
    [Id]                  VARCHAR(10)     NOT NULL,
    [ApiId]               VARCHAR(30)     NOT NULL,
    [DisplayName]         VARCHAR(50)     NOT NULL,
    [Description]         VARCHAR(max)    NULL,
    [ImageURL]            VARCHAR(max)    NULL, 
    [IsDeleted]           BIT             CONSTRAINT [DF_File_IsDeleted] DEFAULT ((0)) NOT NULL,      
    [CreatedBy]           VARCHAR(100)    NOT NULL,
    [CreatedDate]         DATETIME2(7)    CONSTRAINT [DF_File_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
    [LastModifiedBy]      VARCHAR(100)    NOT NULL,
    [LastModifiedDate]    DATETIME2(7)    CONSTRAINT [DF_File_LastModifiedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
    [ValidFrom]           DATETIME2(7)    GENERATED ALWAYS AS ROW START NOT NULL,
    [ValidTo]             DATETIME2(7)    GENERATED ALWAYS AS ROW END NOT NULL,
    CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_File_Api] FOREIGN KEY ([ApiId]) REFERENCES [dbo].[Api]([Id]),
    PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
) 
WITH
(
  SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[File_History])
);