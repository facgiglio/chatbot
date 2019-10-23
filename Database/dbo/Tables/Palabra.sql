CREATE TABLE [dbo].[Palabra] (
    [IdPalabra] INT          IDENTITY (1, 1) NOT NULL,
    [Palabra1]  VARCHAR (50) NULL,
    [Palabra2]  VARCHAR (50) NULL,
    [Palabra3]  VARCHAR (50) NULL,
    [Respuesta] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Palabra] PRIMARY KEY CLUSTERED ([IdPalabra] ASC)
);

