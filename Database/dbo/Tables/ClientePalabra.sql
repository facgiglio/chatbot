CREATE TABLE [dbo].[ClientePalabra] (
    [IdClientePalabra] INT IDENTITY (1, 1) NOT NULL,
    [IdPalabra]        INT NOT NULL,
    [IdCliente]        INT NOT NULL,
    CONSTRAINT [PK_PalabraCliente] PRIMARY KEY CLUSTERED ([IdClientePalabra] ASC),
    CONSTRAINT [FK_PalabraCliente_Cliente] FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Cliente] ([IdCliente]),
    CONSTRAINT [FK_PalabraCliente_Palabra] FOREIGN KEY ([IdPalabra]) REFERENCES [dbo].[Palabra] ([IdPalabra])
);

