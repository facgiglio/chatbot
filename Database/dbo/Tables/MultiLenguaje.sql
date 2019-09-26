CREATE TABLE [dbo].[MultiLenguaje] (
    [IdMultiLenguaje] INT          IDENTITY (1, 1) NOT NULL,
    [IdSeccion]       INT          NOT NULL,
    [Descripcion]     VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_MultiIdioma] PRIMARY KEY CLUSTERED ([IdMultiLenguaje] ASC),
    CONSTRAINT [FK_MultiIdioma_Seccion] FOREIGN KEY ([IdSeccion]) REFERENCES [dbo].[Seccion] ([IdSeccion])
);

