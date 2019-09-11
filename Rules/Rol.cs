using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Rol
    {
        Mapper<Framework.Models.Rol> mapper = new Mapper<Framework.Models.Rol>();

        #region Insertar
        public void Insertar(Framework.Models.Rol rol)
        {
            mapper.Insert(rol);
        }
        #endregion

        #region Modificar
        public void Modificar(Framework.Models.Rol rol)
        {
            //Actualizo el rol
            mapper.Update(rol);
        }
        #endregion

        #region Eliminar
        public void Eliminar(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@IdRol", Id));

            mapper.Delete(parameters.ToArray());
        }
        #endregion

        #region Get Rol
        public Framework.Models.Rol GetById(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlHelper sqlHelper = new SqlHelper();
            Framework.Models.Rol rol = new Framework.Models.Rol();

            if (Id != 0) parameters.Add(new SqlParameter("@Id", Id));

            DataSet data = sqlHelper.ExecuteDataSet("ListadoRol", parameters.ToArray());
            DataRow rowRol = data.Tables[0].Rows[0];

            rol.IdRol = Convert.ToInt32(rowRol["IdRol"]);
            rol.Descripcion = Convert.ToString(rowRol["Descripcion"]);
            rol.Permisos = new List<Framework.Models.Permiso>();

            foreach (DataRow row in data.Tables[1].Rows)
            {
                rol.Permisos.Add(new Framework.Models.Permiso
                {
                    IdPermiso = Convert.ToInt32(row["IdPermiso"]),
                    Descripcion = Convert.ToString(row["Descripcion"])
                });
            }

            return rol;
        }
        public List<object> GetList()
        {
            return mapper.GetList(null);
        }
        public Framework.Models.Rol GetByRol(string Rol)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@Email", Rol)
            };

            return mapper.GetByWhere(parameters.ToArray());
        }
        #endregion
    }
    
}
