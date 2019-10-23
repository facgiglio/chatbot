CREATE procedure [dbo].[sys_Entity]
	@Table VARCHAR(50)
as

/*
sys_Entity 'Frase'
*/

SELECT
	COLUMN_NAME,
	DATA_TYPE,
	'[Insertable, Updatable] 
	public ' + 
	CASE DATA_TYPE 
		WHEN 'varchar'	THEN 'string'
		WHEN 'int'		THEN 'int'
		when 'datetime' THEN 'datetime'
		when 'bit'		THEN 'bool'
	END + ' ' + COLUMN_NAME + '{ get; set; }'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = @Table