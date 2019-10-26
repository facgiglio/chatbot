using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Cliente
    {
        Mapper<Models.Cliente> mapper = new Mapper<Models.Cliente>();

        #region Insertar
        public void Insertar(Models.Cliente cliente)
        {
            var logMessage = "Insertar " + this.GetType().Name + " - Id: " + cliente.IdCliente.ToString();

            try
            {
                //Inserto la entidad.
                mapper.Insert(cliente);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, this.GetType().Name, cliente.IdCliente, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, this.GetType().Name, cliente.IdCliente, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Modificar
        public void Modificar(Models.Cliente cliente)
        {
            try
            {
                //Actualizo el usuario
                mapper.Update(cliente);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, this.GetType().Name, cliente.IdCliente, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, this.GetType().Name, cliente.IdCliente, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Eliminar
        public void Eliminar(int Id)
        {
            var logMessage = "Eliminar " + this.GetType().Name + " - Id: " + Id.ToString();

            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>() {
                    new SqlParameter("@IdCliente", Id)
                };

                mapper.Delete(parameters.ToArray());

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Eliminar, this.GetType().Name, Id, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Eliminar, this.GetType().Name, Id, Logger.LogType.Exception, ex.Message);

                //Arrojo la excepción a la controladora..
                throw (ex);
            }

            
        }
        #endregion

        #region Obtener
        
        public Models.Cliente ObtenerPorId(int Id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>() {
                    new SqlParameter("@IdCliente", Id)
                };

                return mapper.GetById(Id);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        
        public List<object> ObtenerListado()
        {
            return mapper.GetList(null);
        }
        
        public Models.Cliente ObtenerPoryRazonSocial(string RazonSocial)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@RazonSocial", RazonSocial)
            };

            return mapper.GetByWhere(parameters.ToArray());
        }
        
        #endregion

    }
}
