CREATE procedure sys_Entity
	@Table VARCHAR(50)
as

/*
sys_Entity 'Cliente'
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
	END + ' ' + COLUMN_NAME + '{ get; set; }'
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = @Table