CREATE PROCEDURE [dbo].[sys_Permissions]
	@Pantalla VARCHAR(50)
as

DECLARE @Permisos TABLE(
	Tipo varchar(20)
)

INSERT INTO @Permisos VALUES('Alta'), ('Baja'), ('Modificación'), ('Listar')
INSERT INTO Permiso SELECT @Pantalla + ' - ' + Tipo FROM  @Permisos