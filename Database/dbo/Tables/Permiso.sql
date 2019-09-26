CREATE TABLE [dbo].[Permiso] (
    [IdPermiso]   INT           IDENTITY (1, 1) NOT NULL,
    [Descripcion] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Permiso] PRIMARY KEY CLUSTERED ([IdPermiso] ASC)
);

