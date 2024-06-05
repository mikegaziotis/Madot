CREATE TABLE [dbo].[VersionedDocument] 
(
       [Id]                 VARCHAR(10)          NOT NULL,
       [ApiId]              VARCHAR(30)          NOT NULL,
       [DocumentType]       VARCHAR(11)          NOT NULL,
       [Iteration]          INT                  NOT NULL,
       [Data]               VARCHAR(MAX)         NOT NULL,
       [CreatedBy]          VARCHAR(100)         NOT NULL,
       [CreatedDate]        DATETIME2(7)         CONSTRAINT [DF_VersionedDocument_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
       [LastModifiedBy]     VARCHAR(100)         NOT NULL,
       [LastModifiedDate]   DATETIME2(7)         CONSTRAINT [DF_VersionedDocument_LastModifiedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
       [ValidFrom]          DATETIME2(7)         GENERATED ALWAYS AS ROW START NOT NULL,
       [ValidTo]            DATETIME2(7)         GENERATED ALWAYS AS ROW END NOT NULL,
       CONSTRAINT [PK_VersionedDocument] PRIMARY KEY CLUSTERED ([Id]),
       CONSTRAINT [FK_VersionedDocument_Api] FOREIGN KEY ([ApiId]) REFERENCES [dbo].[Api] ([Id]),
       CONSTRAINT [UC_VersionedDocument_Iteration] UNIQUE ([ApiId],[DocumentType],[Iteration]),
       CONSTRAINT [CC_VersionedDocument_Type] CHECK ([DocumentType]='OpenApiSpec' OR [DocumentType]='Homepage' OR [DocumentType]='Changelog'),
       PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
) 
WITH
(
  SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[VersionedDocument_History])
);



