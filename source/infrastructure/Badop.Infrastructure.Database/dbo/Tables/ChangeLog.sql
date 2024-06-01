CREATE TABLE [dbo].[ChangeLog] (
        [Id]                UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_ChangeLog_Id] DEFAULT ((NEWID())),
        [ApiName]           VARCHAR(50)      NOT NULL,
        [FileTypeExtension] VARCHAR(5)          NOT NULL,   
        [Data]              VARCHAR(max)     NOT NULL,
        [CreatedBy]         VARCHAR(100)  NULL,
        [CreatedDate]       DATETIME2(7)  CONSTRAINT [DF_ChangeLog_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
        CONSTRAINT [FK_ChangeLog_Api] FOREIGN KEY ([ApiName]) REFERENCES dbo.[Api]([Name]),
        CONSTRAINT [CC_ChangeLog_FileTypeExtension] CHECK ([FileTypeExtension] = 'txt' OR [FileTypeExtension] = 'md' OR [FileTypeExtension] = 'mdoc'),
        CONSTRAINT [PK_ChangeLog] PRIMARY KEY CLUSTERED ([Id]) 
)
                            