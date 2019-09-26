CREATE TABLE [dbo].[PermisoRol] (
    [IdPermisoRol] INT IDENTITY (1, 1) NOT NULL,
    [IdPermiso]    INT NOT NULL,
    [IdRol]        INT NOT NULL,
    CONSTRAINT [PK_PermisoRol] PRIMARY KEY CLUSTERED ([IdPermisoRol] ASC),
    CONSTRAINT [FK_PermisoRol_Permiso] FOREIGN KEY ([IdPermiso]) REFERENCES [dbo].[Permiso] ([IdPermiso]),
    CONSTRAINT [FK_PermisoRol_Rol] FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Rol] ([IdRol]) ON DELETE CASCADE
);

