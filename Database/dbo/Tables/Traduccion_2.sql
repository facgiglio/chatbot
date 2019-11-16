CREATE TABLE [dbo].[Traduccion] (
    [IdTraduccion]  INT           IDENTITY (1, 1) NOT NULL,
    [IdMultiIdioma] INT           NOT NULL,
    [IdIdioma]      INT           NOT NULL,
    [Texto]         VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_Traduccion] PRIMARY KEY CLUSTERED ([IdTraduccion] ASC),
    CONSTRAINT [FK_Traduccion_Idioma] FOREIGN KEY ([IdIdioma]) REFERENCES [dbo].[Idioma] ([IdIdioma]),
    CONSTRAINT [FK_Traduccion_MultiIdioma] FOREIGN KEY ([IdMultiIdioma]) REFERENCES [dbo].[MultiLenguaje] ([IdMultiLenguaje]) ON DELETE CASCADE
);



