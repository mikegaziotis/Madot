CREATE TABLE [dbo].[ApiVersion] 
(
    [Id]                        VARCHAR(10)         NOT NULL,
    [ApiId]                     VARCHAR(30)         NOT NULL,
    [MajorVersion]              INT                 NOT NULL,
    [MinorVersion]              INT                 NOT NULL,
    [BuildOrReleaseTag]         VARCHAR(20)         NULL, 
    [OpenApiSpecId]             VARCHAR(10)         NOT NULL,
    [HomepageId]                VARCHAR(10)         NOT NULL,
    [ChangelogId]               VARCHAR(10)         NULL,
    [IsBeta]                    BIT                 NOT NULL CONSTRAINT [DF_ApiVersion_IsBeta] DEFAULT((0)),
    [IsHidden]                  BIT                 NOT NULL CONSTRAINT [DF_ApiVersion_IsHidden] DEFAULT((0)),
    [CreatedBy]                 VARCHAR(100)        NOT NULL,
    [CreatedDate]               DATETIME2(7)        CONSTRAINT [DF_ApiVersion_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
    [LastModifiedBy]            VARCHAR(100)        NOT NULL,
    [LastModifiedDate]          DATETIME2(7)        CONSTRAINT [DF_ApiVersion_LastModifiedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
    [ValidFrom]                 DATETIME2(7)        GENERATED ALWAYS AS ROW START NOT NULL,
    [ValidTo]                   DATETIME2(7)        GENERATED ALWAYS AS ROW END NOT NULL,
    CONSTRAINT [CK_ApiVersion_Major] CHECK ([MajorVersion]>(0)),
    CONSTRAINT [CK_ApiVersion_Minor] CHECK ([MinorVersion]>=(0)),
    CONSTRAINT [CK_ApiVersion_NameMajorMinor] UNIQUE ([ApiId],[MajorVersion],[MinorVersion]),
    CONSTRAINT [FK_ApiVersion_Api] FOREIGN KEY ([ApiId]) REFERENCES [dbo].[Api] ([Id]),
    CONSTRAINT [FK_ApiVersion_ChangeLog] FOREIGN KEY ([ChangelogId]) REFERENCES [dbo].[VersionedDocument] ([Id]),
    CONSTRAINT [FK_ApiVersion_OpenApiSpec] FOREIGN KEY ([OpenApiSpecId]) REFERENCES [dbo].[VersionedDocument] ([Id]),
    CONSTRAINT [FK_ApiVersion_Documentation] FOREIGN KEY ([HomepageId]) REFERENCES [dbo].[VersionedDocument] ([Id]),
    CONSTRAINT [PK_ApiVersion] PRIMARY KEY CLUSTERED ([Id]),
    PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
)
WITH
(
    SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[ApiVersion_History])
);










