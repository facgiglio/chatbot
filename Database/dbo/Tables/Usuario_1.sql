﻿CREATE TABLE [dbo].[Usuario] (
    [IdUsuario]        INT          IDENTITY (1, 1) NOT NULL,
    [IdIdioma]         INT          NULL,
    [Email]            VARCHAR (50) NOT NULL,
    [Nombre]           VARCHAR (50) NOT NULL,
    [Apellido]         VARCHAR (50) NOT NULL,
    [Contrasena]       VARCHAR (50) NOT NULL,
    [IntentosFallidos] SMALLINT     NULL,
    [FechaAlta]        DATETIME     NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([IdUsuario] ASC)
);

