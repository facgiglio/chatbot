CREATE TABLE [dbo].[Seccion] (
    [IdSeccion]   INT           IDENTITY (1, 1) NOT NULL,
    [Descripcion] VARCHAR (100) NOT NULL,
    [IdUsuario]   INT           NOT NULL,
    CONSTRAINT [PK_Seccion] PRIMARY KEY CLUSTERED ([IdSeccion] ASC)
);

