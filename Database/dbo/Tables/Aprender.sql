CREATE TABLE [dbo].[Aprender] (
    [IdAprender] INT            IDENTITY (1, 1) NOT NULL,
    [IdCliente]  INT            NOT NULL,
    [Frase]      VARCHAR (1000) NOT NULL,
    [Aprendido]  BIT            NOT NULL,
    CONSTRAINT [PK_Aprender] PRIMARY KEY CLUSTERED ([IdAprender] ASC)
);

