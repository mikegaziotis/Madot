CREATE TABLE [dbo].[File](
    FileId          int             NOT NULL,
    FileURL         varchar(100)    NOT NULL,
    FileProvider    varchar(10)     NOT NULL,
    CONSTRAINT [PK_File] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_FileUrl] UNIQUE NONCLUSTERED (FileURL),
    CONSTRAINT [UQ_FileUrl] UNIQUE NONCLUSTERED (FileURL),
    CONSTRAINT [CC_File_Provider] CHECK ([FileProvider]='Share'),
)
