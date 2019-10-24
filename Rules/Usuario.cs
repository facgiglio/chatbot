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
                throw ex;
            }
        }
        #endregion

        #region Insertar
        public void Insertar(Framework.Models.Usuario usuario)
        {
            usuario.FechaAlta = DateTime.Now;
            usuario.Contrasena = Security.Encrypt(usuario.Contrasena);

            //Controlo que el usuario no esté utilizado.
            if (GetByUsuario(usuario.Email) == null)
            {
                mapper.Insert(usuario);
            }
            else
            {
                throw (new Exception("El usuario que desea utilizar ya está reservado. Ingrese uno nuevo y vuelva a intentar."));
            }
        }
        #endregion

        #region Modificar
        public void Modificar(Framework.Models.Usuario usuario)
        {
            //Hasheo la contraseña de la pantalla
            usuario.Contrasena = Security.Encrypt(usuario.Contrasena);

            //Actualizo el usuario
            mapper.Update(usuario);

            /*
            //Guardo el usuario modificado en la sessión.
            Framework.Helpers.Session.AddSessionUser(usuario);
            */
        }
        #endregion

        #region Eliminar
        public void Eliminar(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@IdUsuario", Id)
            };

            mapper.Delete(parameters.ToArray());
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
        
        public Framework.Models.Usuario GetByUsuario(string Usuario)
        {
            MapperMany<Framework.Models.Usuario, Framework.Models.Rol> mapperRol = new MapperMany<Framework.Models.Usuario, Framework.Models.Rol>();

            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@Email", Usuario)
            };

            var usuario = mapper.GetByWhere(parameters.ToArray());
            usuario.Roles = mapperRol.GetListEntityMany(usuario.IdUsuario);

            return usuario;
        }
        
        #endregion

    }
}
