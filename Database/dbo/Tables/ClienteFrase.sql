CREATE TABLE [dbo].[ClienteFrase] (
    [IdClienteFrase] INT IDENTITY (1, 1) NOT NULL,
    [IdFrase]        INT NOT NULL,
    [IdCliente]      INT NOT NULL,
    CONSTRAINT [PK_FraseCliente] PRIMARY KEY CLUSTERED ([IdClienteFrase] ASC),
    CONSTRAINT [FK_FraseCliente_Cliente] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Cliente] ([IdCliente]),
    CONSTRAINT [FK_FraseCliente_Frase] FOREIGN KEY ([IdFrase]) REFERENCES [dbo].[Frase] ([IdFrase])
);

