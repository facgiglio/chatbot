CREATE TABLE [dbo].[Frase] (
    [IdFrase]   INT          NOT NULL,
    [Frase]     VARCHAR (50) NOT NULL,
    [Respuesta] VARCHAR (50) NULL,
    [Activa]    BIT          NULL,
    CONSTRAINT [PK_Frase] PRIMARY KEY CLUSTERED ([IdFrase] ASC)
);

