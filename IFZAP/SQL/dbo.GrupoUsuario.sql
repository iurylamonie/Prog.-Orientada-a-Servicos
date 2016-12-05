CREATE TABLE [dbo].[GrupoUsuario] (
    [Usuario_id] INT NULL,
    [Grupo_id]   INT NULL,
    CONSTRAINT [FK_GrupoUsuario_Usuario] FOREIGN KEY ([Usuario_id]) REFERENCES [dbo].[Usuario] ([Id]),
    CONSTRAINT [FK_GrupoUsuario_Grupo] FOREIGN KEY ([Grupo_id]) REFERENCES [dbo].[Grupo] ([Id])
);

