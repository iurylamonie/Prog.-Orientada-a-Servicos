CREATE TABLE [dbo].[Grupo] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Descricao] VARCHAR (MAX) NULL,
    [IdAdm]     INT           NULL,
    CONSTRAINT [PK_Grupo] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Grupo_Usuario] FOREIGN KEY ([IdAdm]) REFERENCES [dbo].[Usuario] ([Id])
);

