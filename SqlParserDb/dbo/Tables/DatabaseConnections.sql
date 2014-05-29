CREATE TABLE [dbo].[DatabaseConnections] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [DatabaseName] NVARCHAR (MAX) NULL,
    [Host]         NVARCHAR (MAX) NULL,
    [Password]     NVARCHAR (MAX) NULL,
    [UserName]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Conns] PRIMARY KEY CLUSTERED ([Id] ASC)
);

