create PROCEDURE [dbo].[BuscarFraseExacta]
	@Frases as varchar(1000)
AS



SELECT * 
FROM Frase f
INNER JOIN STRING_SPLIT(@Frases, '|') frase ON frase.value = f.Descripcion
--WHERE SOUNDEX