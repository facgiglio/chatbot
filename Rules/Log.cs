using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Log
    {
        Mapper<Models.Log> mapper = new Mapper<Models.Log>();
        private string _seccion
        {
            get { return this.GetType().Name; }
        }


        #region Get Rol
        public Models.Log GetById(int Id)
        {
            try
            {
                var log = mapper.GetById(Id);
                
                return log;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public List<object> GetList(string mensaje, Int32 usuario)
        {
            //Armo los parametros a enviar a la consulta.
            var parameters = new List<SqlParameter>();

            if (mensaje != "") parameters.Add(new SqlParameter("@Message", mensaje));
            if (usuario != 0) parameters.Add(new SqlParameter("@IdUser", usuario));

            return mapper.GetList(parameters.ToArray());
        }
        #endregion

        #region Validaciones
        private void Validar(Framework.Models.Rol rol)
        {
            var mensaje = "";

            if (rol.Descripcion == "")
            {
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblDescripcion") + ": ";
                mensaje += MultiLanguage.GetTranslate("errorVacioString");
            }

            if (mensaje != "")
                throw new Exception(mensaje);

        }
        #endregion
    }

}
