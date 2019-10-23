CREATE TABLE [dbo].[Frase] (
    [IdFrase]     INT          IDENTITY (1, 1) NOT NULL,
    [Descripcion] VARCHAR (50) NOT NULL,
    [Respuesta]   VARCHAR (50) NULL,
    [Activo]      BIT          NULL,
    CONSTRAINT [PK_Frase] PRIMARY KEY CLUSTERED ([IdFrase] ASC)
);

