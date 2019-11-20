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
        private string _seccion
        {
            get { return this.GetType().Name; }
        }

        #region Insertar
        public void Insertar(Framework.Models.Rol rol)
        {
            try
            {
                //Valido la entidad antes.
                this.Validar(rol);

                //Inserto el rol validado.
                mapper.Insert(rol);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, _seccion, rol.IdRol, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, _seccion, rol.IdRol, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Modificar
        public void Modificar(Framework.Models.Rol rol)
        {
            try
            {
                //Valido la entidad antes.
                this.Validar(rol);

                //Actualizo el rol
                mapper.Update(rol);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, rol.IdRol, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, rol.IdRol, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Eliminar
        public void Eliminar(int IdRol)
        {
            try
            {
                //Elimino la entidad.                
                mapper.Delete(IdRol);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, IdRol, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, IdRol, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }

           
        }
        #endregion

        #region Get Rol
        public Framework.Models.Rol GetById(int Id)
        {
            try
            {
                var rol = mapper.GetById(Id);
                
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
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblDescripcion") + ": ";
                mensaje += MultiLanguage.GetTranslate("errorVacioString");
            }

            if (mensaje != "")
                throw new Exception(mensaje);

        }
        #endregion
    }

}
