using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Idioma
    {
        Mapper<Framework.Models.Idioma> mapper = new Mapper<Framework.Models.Idioma>();

        #region Get Usuario
        
        public Framework.Models.Idioma GetById(int Id)
        {
            try
            {
                /*
                List<SqlParameter> parameters = new List<SqlParameter>();
                SqlHelper sqlHelper = new SqlHelper();
                Framework.Models.Usuario usuario = new Framework.Models.Usuario();

                if (Id != 0) parameters.Add(new SqlParameter("@IdUsuario", Id));

                DataSet data = sqlHelper.ExecuteDataSet("ListadoUsuario", parameters.ToArray());
                DataRow rowUsuario = data.Tables[0].Rows[0];

                usuario.IdUsuario = Convert.ToInt32(rowUsuario["Id"]);
                usuario.IdIdioma = Convert.ToInt32(rowUsuario["IdIdioma"]);
                usuario.Nombre = Convert.ToString(rowUsuario["Nombre"]);
                usuario.Apellido = Convert.ToString(rowUsuario["Apellido"]);
                usuario.Email = Convert.ToString(rowUsuario["Email"]);
                usuario.Contrasena = Convert.ToString(rowUsuario["Contrasena"]);
                usuario.IntentosFallidos = Convert.ToInt16(rowUsuario["IntentosFallidos"]);
                usuario.FechaAlta = Convert.ToDateTime(rowUsuario["FechaAlta"]);

                usuario.Permisos = new List<Framework.Models.Permiso>();
                usuario.Roles = new List<Framework.Models.Rol>();

                foreach (DataRow row in data.Tables[1].Rows)
                {
                    usuario.Permisos.Add(new Framework.Models.Permiso
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Descripcion = Convert.ToString(row["Descripcion"])
                    });
                }

                foreach (DataRow row in data.Tables[2].Rows)
                {
                    usuario.Roles.Add(new Framework.Models.Rol
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Descripcion = Convert.ToString(row["Descripcion"]),
                        hash = Convert.ToString(row["hash"])
                    });
                }

                */

                List<SqlParameter> parameters = new List<SqlParameter>() {
                    new SqlParameter("@IdUsuario", Id)
                };

                var usuario = mapper.GetById(Id);

                return usuario;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        
        public List<object> GetList()
        {
            return mapper.GetList(null);
        }
         
        #endregion

    }
}
