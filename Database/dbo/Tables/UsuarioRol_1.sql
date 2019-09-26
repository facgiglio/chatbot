CREATE TABLE [dbo].[UsuarioRol] (
    [IdUsuarioRol] INT IDENTITY (1, 1) NOT NULL,
    [IdUsuario]    INT NOT NULL,
    [IdRol]        INT NOT NULL,
    CONSTRAINT [PK_UsuarioRol] PRIMARY KEY CLUSTERED ([IdUsuarioRol] ASC),
    CONSTRAINT [FK_UsuarioRol_Rol] FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Rol] ([IdRol]) ON DELETE CASCADE,
    CONSTRAINT [FK_UsuarioRol_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuario] ([IdUsuario]) ON DELETE CASCADE
);

