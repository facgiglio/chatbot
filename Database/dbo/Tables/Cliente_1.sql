CREATE TABLE [dbo].[Cliente] (
    [IdCliente]    INT          IDENTITY (1, 1) NOT NULL,
    [RazonSocial]  VARCHAR (50) NULL,
    [Direccion]    VARCHAR (50) NULL,
    [CodigoPostal] VARCHAR (10) NULL,
    [Telefono]     VARCHAR (20) NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([IdCliente] ASC)
);

