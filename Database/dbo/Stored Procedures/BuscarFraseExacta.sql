
CREATE PROCEDURE [dbo].BuscarFraseExacta
	@Frases AS VARCHAR(1000),
	@IdCliente AS INT
AS

SELECT * 
FROM ClienteFrase fc
INNER JOIN Frase f on f.IdFrase = fc.IdFrase
INNER JOIN STRING_SPLIT(@Frases, '|') frase ON frase.value = f.Descripcion
WHERE fc.IdCliente = @IdCliente