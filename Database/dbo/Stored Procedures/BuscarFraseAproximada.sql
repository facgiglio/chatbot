CREATE PROCEDURE [dbo].[BuscarFraseAproximada]
	@Frases AS VARCHAR(1000),
	@IdCliente AS INT
AS

SELECT * 
FROM ClienteFrase fc
INNER JOIN Frase f on f.IdFrase = fc.IdFrase
INNER JOIN STRING_SPLIT(@Frases, '|') frase ON SOUNDEX(frase.value) = SOUNDEX(f.Descripcion)
WHERE fc.IdCliente = @IdCliente