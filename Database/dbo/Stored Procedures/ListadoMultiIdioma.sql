CREATE PROCEDURE [dbo].[ListadoMultiIdioma]
	@IdSeccion AS INT = NULL
AS
SELECT 
	m.IdMultiLenguaje,
	m.Descripcion,
	s.Descripcion AS Seccion,
	MAX(CASE WHEN i.Iso = 'es' THEN CONVERT(VARCHAR(10), t.IdIdioma) ELSE '0' END + '_' + CONVERT(VARCHAR(10), m.IdMultiLenguaje) + '_1') AS 'IdEs',
	MAX(CASE WHEN i.Iso = 'es' THEN t.Texto ELSE '' END) AS 'es',
	MAX(CASE WHEN i.Iso = 'en' THEN CONVERT(VARCHAR(10), t.IdIdioma) ELSE '0'	END + '_' + CONVERT(VARCHAR(10), m.IdMultiLenguaje) + '_2') AS 'IdEn',
	MAX(CASE WHEN i.Iso = 'en' THEN t.Texto ELSE '' END) AS 'en'
FROM MultiLenguaje m
LEFT JOIN Seccion s		ON s.IdSeccion = m.IdSeccion
LEFT JOIN Traduccion t	ON t.IdMultiIdioma = m.IdMultiLenguaje
LEFT JOIN Idioma i		ON i.IdIdioma = t.IdIdioma
WHERE m.IdSeccion = ISNULL(@IdSeccion, m.IdSeccion)
GROUP BY m.IdMultiLenguaje, m.Descripcion, s.Descripcion