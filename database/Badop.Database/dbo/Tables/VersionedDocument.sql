CREATE TABLE [dbo].[VersionedDocument] 
(
       [Id]                 VARCHAR(10)          NOT NULL,
       [ApiId]              VARCHAR(20)          NOT NULL,
       [DocumentType]       VARCHAR(11)          NOT NULL,
       [Version]            INT                  NOT NULL,
       [Data]               VARCHAR(MAX)         NOT NULL,
       [CreatedDate]        DATETIME2(7)         CONSTRAINT [DF_VersionedDocument_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
       [CreatedBy]          VARCHAR(100)         NULL,
       [LastModifiedBy]     VARCHAR(100)         NULL,
       [LastModifiedDate]   DATETIME2(7)         NULL,
       [ValidFrom]          DATETIME2(7)         GENERATED ALWAYS AS ROW START NOT NULL,
       [ValidTo]            DATETIME2(7)         GENERATED ALWAYS AS ROW END NOT NULL,
       CONSTRAINT [PK_VersionedDocument] PRIMARY KEY CLUSTERED ([Id]),
       CONSTRAINT [FK_VersionedDocument_Api] FOREIGN KEY ([ApiId]) REFERENCES [dbo].[Api] ([Id]),
       CONSTRAINT [UC_VersionedDocument] UNIQUE ([ApiId],[DocumentType],[Version]),
       CONSTRAINT [CC_VersionedDocument_Type] CHECK ([DocumentType]='OpenApiSpec' OR [DocumentType]='Homepage' OR [DocumentType]='Changelog'),
       PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
) 
WITH
(
  SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[VersionedDocument_History])
);



