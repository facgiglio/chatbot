create PROCEDURE [dbo].[BuscarFraseAproximada]
	@Frases as varchar(1000)
AS



SELECT * 
FROM Frase f
INNER JOIN STRING_SPLIT(@Frases, '|') frase ON SOUNDEX(frase.value) = SOUNDEX(f.Descripcion)
--WHERE SOUNDEX