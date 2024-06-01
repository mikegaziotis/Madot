CREATE TABLE [dbo].[ApiVersion] (
    [Id]                        VARCHAR(72)      NOT NULL,
    [ApiName]                   VARCHAR(50)      NOT NULL,
    [MajorVersion]              INT              NOT NULL,
    [MinorVersion]              INT              NOT NULL,
    [BuildOrReleaseReference]   VARCHAR(20)         NULL, 
    [OpenApiSpecId]             VARCHAR(82)      NOT NULL,
    [DocumentationId]           VARCHAR(82)      NOT NULL,
    [ChangeLogId]               UNIQUEIDENTIFIER NULL,
    [IsPreview]                 BIT              NOT NULL CONSTRAINT [DF_ApiVersion_IsPreview] DEFAULT((1)),
    [IsAvailable]               BIT              NOT NULL,
    [CreatedBy]                 VARCHAR(100)     NULL,
    [CreatedDate]               DATETIME2(7)     CONSTRAINT [DF_ApiVersion_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
    [LastModifiedBy]            VARCHAR(100)     NULL,
    [LastModifiedDate]          DATETIME2(7)     NULL,
    [ValidFrom]                 DATETIME2(7) GENERATED ALWAYS AS ROW START NOT NULL,
    [ValidTo]                   DATETIME2(7) GENERATED ALWAYS AS ROW END NOT NULL,
    CONSTRAINT [CK_ApiVersion_Major] CHECK ([MajorVersion]>(0)),
    CONSTRAINT [CK_ApiVersion_Minor] CHECK ([MinorVersion]>=(0)),
    CONSTRAINT [CK_ApiVersion_NameMajorMinor] UNIQUE NONCLUSTERED ([ApiName],[MajorVersion],[MinorVersion]),
    CONSTRAINT [FK_ApiVersion_Api] FOREIGN KEY ([ApiName]) REFERENCES [dbo].[Api] ([Name]),
    CONSTRAINT [FK_ApiVersion_ChangeLog] FOREIGN KEY ([ChangeLogId]) REFERENCES [dbo].[ChangeLog] ([Id]),
    CONSTRAINT [FK_ApiVersion_OpenApiSpec] FOREIGN KEY ([OpenApiSpecId]) REFERENCES [dbo].[VersionedDocument] ([Id]),
    CONSTRAINT [FK_ApiVersion_Documentation] FOREIGN KEY ([OpenApiSpecId]) REFERENCES [dbo].[VersionedDocument] ([Id]),
    CONSTRAINT [PK_ApiVersion] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [CK_ApiVersion_Id] CHECK ([Id]=([ApiName]+':'+CONVERT([varchar](10),[MajorVersion])+'.'+CONVERT([varchar](10),[MinorVersion]))),
    PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
)
WITH
(
    SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[ApiVersion_History])
);










