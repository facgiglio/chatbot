
create PROCEDURE [dbo].[BuscarPalabra]
	@Palabras AS VARCHAR(1000),
	@IdCliente AS INT
AS

SELECT * 
FROM ClientePalabra cp
INNER JOIN Palabra p on p.IdPalabra = cp.IdPalabra
INNER JOIN STRING_SPLIT(@Palabras, '|') palabra ON palabra.value = p.Palabra1
WHERE cp.IdCliente = @IdCliente