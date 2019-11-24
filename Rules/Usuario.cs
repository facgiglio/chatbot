using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Usuario
    {
        Mapper<Framework.Models.Usuario> mapper = new Mapper<Framework.Models.Usuario>();
        private string _seccion
        {
            get { return this.GetType().Name; }
        }

        #region LogIn
        public Framework.Models.MessageDTO LogIn(string user, string pass)
        {
            var usuario = GetByUsuario(user);
            var logMessage = "LogIn [User: " + user + "]";

            //Controlo que el usuario exista.
            if (usuario == null)
            {
                //Logueo la acción ejecutada.
                Logger.LogInfo(logMessage + " - Usuario no encontrado.");

                return new Framework.Models.MessageDTO("Error", "Usuario o contraseña inválidos.");
            }

            try
            {
                //Controlo que la constraseña sea la correcta.
                if (Security.Decrypt(usuario.Contrasena) == pass)
                {
                    //LogIn exitoso, armo el perfil.
                    Session.AddSessionUser(usuario);

                    //Logueo la acción ejecutada.
                    Logger.LogInfo(logMessage + " - Welcome.");

                    return new Framework.Models.MessageDTO("Success", "LogIn exitoso.");
                }
                else
                {
                    //Actualizo la cantidad de intentos.
                    usuario.IntentosFallidos += 1;
                    mapper.Update(usuario);

                    if (usuario.IntentosFallidos > 3)
                    {
                        //Logueo la acción ejecutada.
                        Logger.LogInfo(logMessage + " - Usuario bloqueado.");

                        return new Framework.Models.MessageDTO("Error", "El usuario ha sido bloqueado. Ingrese a recuperar contraseña.");
                    }
                    else
                    {
                        //Logueo la acción ejecutada.
                        Logger.LogInfo(logMessage + " - Password incorrecta.");

                        return new Framework.Models.MessageDTO("Error", "Usuario o contraseña inválidos.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(logMessage + " - " + ex.Message);
                throw ex;
            }
        }

        public Framework.Models.MessageDTO RecuperarContrasena(string user)
        {
            try
            {

                var usuario = GetByUsuario(user);
                var logMessage = "LogIn [User: " + user + "]";

                //Controlo que el usuario exista.
                if (usuario == null)
                {
                    //Logueo la acción ejecutada.
                    Logger.LogInfo(logMessage + " - Usuario no encontrado.");

                    return new Framework.Models.MessageDTO("Error", "El usuario no existe");
                }
                else
                {
                    usuario.CodigoRecuperacion = Security.Encrypt(DateTime.Now.ToString());

                    Modificar(usuario, false);

                    var mail = new Framework.Models.Mail(usuario.Email, usuario.Nombre + " " + usuario.Apellido,"Chatbot-Mail para recuperar contraseña", "Estimado: para blanquear la contraseña ingrese el siguiente código: " + usuario.CodigoRecuperacion);

                    Framework.SendMail.Send(mail);

                    return new Framework.Models.MessageDTO("Message", "El email fue enviado con éxito.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Framework.Models.MessageDTO CambiarContrasena(string user, string codigoRecuperacion, string nuevaContrasena, string repetirContrasena)
        {
            try
            {
                var usuario = GetByUsuario(user);
                var logMessage = "LogIn [User: " + user + "]";

                //Controlo que el usuario exista.
                if (usuario == null)
                {
                    //Logueo la acción ejecutada.
                    Logger.LogInfo(logMessage + " - Usuario no encontrado.");

                    return new Framework.Models.MessageDTO("Error", "El usuario no existe");
                }
                else
                {
                    if (usuario.CodigoRecuperacion == codigoRecuperacion)
                    {
                        if (nuevaContrasena == repetirContrasena)
                        {
                            //Vacío el código de recuperación y guardo la contraseña.
                            usuario.CodigoRecuperacion = "";
                            usuario.IntentosFallidos = 0;
                            usuario.Contrasena = nuevaContrasena;

                            //Modifico el usuario.
                            Modificar(usuario);

                            //Logueo la acción ejecutada.
                            Logger.LogInfo(logMessage + " - La contraseña fue modificada con éxito.");

                            return new Framework.Models.MessageDTO("Message", "La contraseña fue modificada con éxito.");
                        }
                        else
                        {
                            //Logueo la acción ejecutada.
                            Logger.LogInfo(logMessage + " - Las contraseñas no son iguales.");
                            return new Framework.Models.MessageDTO("Error", "Las contraseñas no son iguales");
                        }
                    }
                    else
                    {
                        //Logueo la acción ejecutada.
                        Logger.LogInfo(logMessage + " - El código de recuperación es incorrecto.");

                        return new Framework.Models.MessageDTO("Error", "El código de recuperación es incorrecto.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Insertar
        public void Insertar(Framework.Models.Usuario usuario)
        {
            try
            {
                //Completo los datos de la entidad.
                usuario.FechaAlta = DateTime.Now;
                usuario.Contrasena = Security.Encrypt(usuario.Contrasena);

                //Controlo que el usuario no esté utilizado.
                if (GetByUsuario(usuario.Email) == null)
                {
                    mapper.Insert(usuario);

                    //Logueo la acción ejecutada.
                    Logger.Log(Logger.LogAction.Insertar, _seccion, usuario.IdUsuario, Logger.LogType.Info, "");
                }
                else
                { 
                    throw (new Exception("El usuario que desea utilizar ya está reservado. Ingrese uno nuevo y vuelva a intentar."));
                }
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, _seccion, usuario.IdUsuario, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Modificar
        public void Modificar(Framework.Models.Usuario usuario, bool updatePass = true)
        {
            try
            {
                if (updatePass)
                {
                    //Hasheo la contraseña de la pantalla
                    usuario.Contrasena = Security.Encrypt(usuario.Contrasena);
                }

                //Actualizo el usuario
                mapper.Update(usuario);

                if (Framework.Session.User != null)
                {
                    //Si el usuario modificado, es el mismo de la session, lo actualizo.
                    if (Framework.Session.User.IdUsuario == usuario.IdUsuario)
                    {
                        //Guardo el usuario modificado en la sessión.
                        Framework.Session.AddSessionUser(usuario);
                    }
                }

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, _seccion, usuario.IdUsuario, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Eliminar, _seccion, usuario.IdUsuario, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Eliminar
        public void Eliminar(int IdUsuario)
        {
            try
            {
                mapper.Delete(IdUsuario);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Eliminar, _seccion, IdUsuario, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Eliminar, _seccion, IdUsuario, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Get Usuario
        
        public Framework.Models.Usuario GetById(int Id)
        {
            try
            {
                MapperMany<Framework.Models.Usuario, Framework.Models.Rol> mapperRol = new MapperMany<Framework.Models.Usuario, Framework.Models.Rol>();

                List<SqlParameter> parameters = new List<SqlParameter>() {
                    new SqlParameter("@IdUsuario", Id)
                };

                var usuario = mapper.GetById(Id);
                usuario.Contrasena = Security.Decrypt(usuario.Contrasena); 
                usuario.Roles = mapperRol.GetListEntityMany(Id);

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

        public List<Framework.Models.Usuario> GetEntityList()
        {
            return mapper.GetListEntity(null);
        }

        public Framework.Models.Usuario GetByUsuario(string Usuario)
        {
            MapperMany<Framework.Models.Usuario, Framework.Models.Rol> mapperRol = new MapperMany<Framework.Models.Usuario, Framework.Models.Rol>();

            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@Email*", Usuario)
            };

            var usuario = mapper.GetByWhere(parameters.ToArray());
            
            //Para evitar errores, solo cargo los roles cuando existe el usuario.
            if (usuario != null)
                usuario.Roles = mapperRol.GetListEntityMany(usuario.IdUsuario);

            return usuario;
        }
        
        #endregion

    }
}
