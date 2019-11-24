using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Aprender
    {
        Mapper<Models.Aprender> mapper = new Mapper<Models.Aprender>();
        private string _seccion
        {
            get { return this.GetType().Name; }
        }

        #region Insertar
        public void Insertar(Models.Aprender aprender)
        {
            try
            {
                //Actualizo el aprender
                mapper.Insert(aprender);

                Logger.LogInfo("Se inserto el registro: " + aprender.IdAprender);
            }
            catch (Exception ex)
            {

                Logger.LogException("Error al insertar: " + ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Modificar
        public void Modificar(Models.Aprender aprender)
        {
            try
            {
                //Actualizo el aprender
                mapper.Update(aprender);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, aprender.IdAprender, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, aprender.IdAprender, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Eliminar
        public void Eliminar(int IdAprender)
        {
            try
            {
                //Actualizo el aprender
                mapper.Delete(IdAprender);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Eliminar, _seccion, IdAprender, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Eliminar, _seccion, IdAprender, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
            
        }
        #endregion

        #region Get Aprender
        public Models.Aprender ObtenerPorId(int Id)
        {
            try
            {
                return mapper.GetById(Id); ;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
       
        public List<object> ObtenerListado()
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@Aprendido", false),
                new SqlParameter("@IdCliente", Framework.Session.User.IdCliente)
            };

            return mapper.GetList(parameters.ToArray());
        }
        #endregion
    }
    
}
