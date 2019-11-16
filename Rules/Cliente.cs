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
        private string _seccion
        {
            get { return this.GetType().Name; }
        }

        #region Insertar
        public void Insertar(Models.Cliente cliente)
        {
            try
            {
                //Valido la entidad antes.
                this.Validar(cliente);

                //Inserto la entidad.
                mapper.Insert(cliente);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, _seccion, cliente.IdCliente, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, _seccion, cliente.IdCliente, Logger.LogType.Exception, ex.Message);

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
                //Valido la entidad antes.
                this.Validar(cliente);

                //Actualizo el usuario
                mapper.Update(cliente);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, cliente.IdCliente, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, cliente.IdCliente, Logger.LogType.Exception, ex.Message);

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
                List<SqlParameter> parameters = new List<SqlParameter>() {
                    new SqlParameter("@IdCliente", Id)
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

        #region HashKey
        public void GenerarHashKey(int Id)
        {
            try
            {
                var cliente = mapper.GetById(Id);

                cliente.HashKey = Framework.Security.Encrypt(cliente.IdCliente.ToString());

                Modificar(cliente);
            }
            catch (Exception ex)
            {
                //Throw the exception to the controller.
                throw (ex);
            }
        }

        public void EliminarHashKey(int Id)
        {
            try
            {
                var cliente = mapper.GetById(Id);

                cliente.HashKey = "";

                Modificar(cliente);
            }
            catch (Exception ex)
            {
                //Throw the exception to the controller.
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

        public List<Models.Cliente> ObtenerClientes()
        {
            return mapper.GetListEntity(null);
        }
        
        public Models.Cliente ObtenerPoryRazonSocial(string RazonSocial)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@RazonSocial", RazonSocial)
            };

            return mapper.GetByWhere(parameters.ToArray());
        }

        #endregion

        #region Validaciones
        private void Validar(Models.Cliente cliente)
        {
            var mensaje = "";

            if (cliente.RazonSocial == "")
            {
                mensaje += (mensaje != "" ? Environment.NewLine : "");
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblRazonSocial") + ": ";
                mensaje += MultiLanguage.GetTranslate("errorVacioString");
            }

            if (cliente.Direccion == "")
            {
                mensaje += (mensaje != "" ? Environment.NewLine : "");
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblDireccion") + ": ";
                mensaje += MultiLanguage.GetTranslate("errorVacioString");
            }

            if (cliente.Telefono == "")
            {
                mensaje += (mensaje != "" ? Environment.NewLine : "");
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblTelefono") + ": ";
                mensaje += MultiLanguage.GetTranslate("errorVacioString");
            }

            if (cliente.CodigoPostal == "")
            {
                mensaje += (mensaje != "" ? Environment.NewLine : "");
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblCodigoPostal") + ": ";
                mensaje += MultiLanguage.GetTranslate("errorVacioString");
            }

            if (cliente.HostName == "")
            {
                mensaje += (mensaje != "" ? Environment.NewLine : "");
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblHostName") + ": ";
                mensaje += MultiLanguage.GetTranslate("errorVacioString");
            }

            if (mensaje != "")
                throw new Exception(mensaje);
        }

        private bool ControlarPalabra(string palabra)
        {
            return palabra.Split(' ').Length > 1;
        }
        #endregion
    }
}
