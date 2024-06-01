CREATE TABLE [dbo].[Api] (
    [Name]              VARCHAR(50)   NOT NULL,
    [DisplayName]       VARCHAR(100)  NOT NULL,
    [Sha256HashedKey]   VARCHAR (64)     NOT NULL,
    [BaseUrlPath]       VARCHAR(100)     NOT NULL,
    [IsInternal]        BIT           CONSTRAINT [DF_Api_IsInternal] DEFAULT ((0)) NOT NULL,
    [IsBeta]            BIT           CONSTRAINT [DF_Api_IsBeta] DEFAULT ((0)) NOT NULL,
    [IsAvailable]       BIT           CONSTRAINT [DF_Api_IsAvailable] DEFAULT ((1)) NOT NULL,
    [OrderId]           INT           CONSTRAINT [DF_Api_OrderId] DEFAULT ((1)) NOT NULL,
    [CreatedBy]         VARCHAR(100)  NULL,
    [CreatedDate]       DATETIME2(7)  CONSTRAINT [DF_Api_CreatedDate] DEFAULT ((GETUTCDATE())) NOT NULL,
    [LastModifiedBy]    VARCHAR(100)  NULL,
    [LastModifiedDate]  DATETIME2(7)  NULL,
    [ValidFrom]         DATETIME2(7)  GENERATED ALWAYS AS ROW START NOT NULL,
    [ValidTo]           DATETIME2(7)  GENERATED ALWAYS AS ROW END NOT NULL,
    CONSTRAINT [PK_Api] PRIMARY KEY CLUSTERED ([Name] ASC),
    PERIOD FOR SYSTEM_TIME ([ValidFrom], [ValidTo])
) 
WITH
(
  SYSTEM_VERSIONING = ON (HISTORY_TABLE = [dbo].[Api_History])
);



