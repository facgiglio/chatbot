CREATE TABLE [dbo].[Idioma] (
    [IdIdioma]    INT           IDENTITY (1, 1) NOT NULL,
    [Iso]         VARCHAR (2)   NOT NULL,
    [Descripcion] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Idioma] PRIMARY KEY CLUSTERED ([IdIdioma] ASC)
);

