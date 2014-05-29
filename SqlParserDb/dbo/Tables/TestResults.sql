CREATE TABLE [dbo].[TestResults] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Database]   NVARCHAR (MAX) NULL,
    [Server]     NVARCHAR (MAX) NULL,
    [ObjectName] NVARCHAR (MAX) NULL,
    [Errors]     NVARCHAR (MAX) NULL,
    [Exception]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Results] PRIMARY KEY CLUSTERED ([Id] ASC)
);

