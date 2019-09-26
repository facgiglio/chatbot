CREATE TABLE [dbo].[FraseCliente] (
    [IdFraseCliente] INT NOT NULL,
    [IdFrase]        INT NOT NULL,
    [IdCliente]      INT NOT NULL,
    CONSTRAINT [PK_FraseCliente] PRIMARY KEY CLUSTERED ([IdFraseCliente] ASC),
    CONSTRAINT [FK_FraseCliente_Cliente] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Cliente] ([IdCliente]),
    CONSTRAINT [FK_FraseCliente_Frase] FOREIGN KEY ([IdFrase]) REFERENCES [dbo].[Frase] ([IdFrase])
);

