CREATE TABLE [dbo].[Usuario] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Nome] VARCHAR (MAX) NULL,
    [Uri]  VARCHAR (MAX) NULL,
    [Foto] IMAGE         NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([Id] ASC)
);

