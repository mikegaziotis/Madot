CREATE TABLE [dbo].[FileLink]
(
    [FileId]              VARCHAR(10)     NOT NULL,
    [OperatingSystem]     VARCHAR(10)     NOT NULL,
    [ChipArchitecture]    VARCHAR(5)      NOT NULL,
    [DownloadURL]         VARCHAR(MAX)    NOT NULL,
    [CreatedBy]           VARCHAR(100)    NOT NULL,
    [CreatedDate]         DATETIME2(7)    CONSTRAINT [DF_FileLink_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
    [LastModifiedBy]      VARCHAR(100)    NOT NULL,
    [LastModifiedDate]    DATETIME2(7)    CONSTRAINT [DF_FileLink_LastModifiedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
    [ValidFrom]           DATETIME2(7)    GENERATED ALWAYS AS ROW START NOT NULL,
    [ValidTo]             DATETIME2(7)    GENERATED ALWAYS AS ROW END NOT NULL,
    CONSTRAINT [PK_FileLink] PRIMARY KEY CLUSTERED ([FileId] ASC,[OperatingSystem] ASC, [ChipArchitecture] ASC),
    CONSTRAINT [FK_FileLink_File] FOREIGN KEY ([FileId]) REFERENCES [dbo].[File]([Id]),
    CONSTRAINT [CK_FileLink_OS] CHECK ([OperatingSystem] IN ('Windows','Linux','MacOS','Any')),
    CONSTRAINT [CK_FileLink_Chip] CHECK ([ChipArchitecture] IN ('X64','Arm64','Any')),
    PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
) 
WITH
(
  SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[FileLink_History])
);

