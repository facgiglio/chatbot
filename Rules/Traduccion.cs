using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Framework;
using Framework.Helpers;


namespace Rules
{
    public class Traduccion
    {
        Mapper<Framework.Models.Traduccion> mapper = new Mapper<Framework.Models.Traduccion>();
        private string _seccion
        {
            get { return this.GetType().Name; }
        }

        #region Insertar
        public void Insertar(ref Framework.Models.Traduccion traduccion)
        {
            try
            {
                //Valido la entidad antes.
                //this.Validar(frase);

                mapper.Insert(traduccion);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, _seccion, traduccion.IdTraduccion, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, _seccion, traduccion.IdTraduccion, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Modificar
        public void Modificar(Framework.Models.Traduccion traduccion)
        {
            try
            {
                //Valido la entidad antes.
                //this.Validar(frase);

                //Actualizo el usuario
                mapper.Update(traduccion);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, traduccion.IdTraduccion, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, traduccion.IdTraduccion, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Eliminar
        public void Eliminar(int Id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@IdTraduccion", Id)
            };

                mapper.Delete(parameters.ToArray());

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Eliminar, _seccion, Id, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Eliminar, _seccion, Id, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }

           
        }
        #endregion

        #region Get
        public Framework.Models.Traduccion GetById(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@IdTraduccion", Id)
            };

            return mapper.GetByWhere(parameters.ToArray());
        }
        public List<object> GetList()
        {
            return mapper.GetList(null);
        }
        #endregion
    }

}
