using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Permiso
    {
        Mapper<Framework.Models.Permiso> mapper = new Mapper<Framework.Models.Permiso>();
        private string _section
        {
            get { return this.GetType().Name + ".asxp"; }
        }

        #region Insertar
        public void Insertar(Framework.Models.Permiso permiso)
        {
            try
            {
                Validar(permiso);
                mapper.Insert(permiso);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
        }
        #endregion

        #region Modificar
        public void Modificar(Framework.Models.Permiso permiso)
        {
            try
            {
                //Validaciones genéricas del permiso.
                Validar(permiso);
                //Actualizo el permiso
                mapper.Update(permiso);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            
        }
        #endregion

        #region Eliminar
        public void Eliminar(int id)
        {
            mapper.Delete(id);
        }
        #endregion

        #region Get Permiso
        public Framework.Models.Permiso GetById(int id)
        {
            try
            {
                return mapper.GetById(id);
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
        public Framework.Models.Permiso GetByPermiso(string descripcion)
        {
            SqlParameter[] parameters = { new SqlParameter("@Descripcion", descripcion) };

            return mapper.GetByWhere(parameters);
        }
        #endregion

        #region Validaciones
        private void Validar(Framework.Models.Permiso permiso)
        {
            var mensaje = "";

            if (permiso.Descripcion == "")
            {
                mensaje += MultiLanguage.GetTranslate(_section, "lblDescripcion") + ": ";
                mensaje += MultiLanguage.GetTranslate("errorVacioString");
            }

            if(mensaje != "")
                throw new Exception(mensaje);

        }
        #endregion
    }

}
