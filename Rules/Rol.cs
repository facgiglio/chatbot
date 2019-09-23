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

        private string _section {
            get { return this.GetType().Name + ".asxp"; }
        }

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
            SqlParameter[] parameters = { new SqlParameter("@IdRol", Id) };
            mapper.Delete(parameters);
        }
        #endregion

        #region Get Rol
        public Framework.Models.Rol GetById(int Id)
        {
            try
            {
                var mapperRol = new MapperMany<Framework.Models.Rol, Framework.Models.Permiso>();
                var rol = mapper.GetById(Id);
                
                rol.Permisos = mapperRol.GetListEntityMany(Id);

                return rol;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public List<object> GetList(string descripcion)
        {
            //Armo los parametros a enviar a la consulta.
            SqlParameter[] parameters = { new SqlParameter("@Descripcion", descripcion) };

            return mapper.GetList(parameters);
        }
        public Framework.Models.Rol GetByRol(string Rol)
        {
            SqlParameter[] parameters = { new SqlParameter("@Email", Rol) };

            return mapper.GetByWhere(parameters);
        }
        #endregion


        #region Validaciones
        private void Validar(Framework.Models.Rol rol)
        {
            var mensaje = "";

            if (rol.Descripcion == "")
            {
                mensaje += MultiLanguage.GetTranslate(_section, "lblDescripcion") + ": ";
                mensaje += MultiLanguage.GetTranslate("errorVacioString");
            }

            if (mensaje != "")
                throw new Exception(mensaje);

        }
        #endregion
    }

}
