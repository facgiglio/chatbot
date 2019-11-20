using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Palabra
    {
        Mapper<Models.Palabra> mapper = new Mapper<Models.Palabra>();
        private string _seccion
        {
            get { return this.GetType().Name; }
        }

        #region Insertar
        public void Insertar(Models.Palabra palabra)
        {
            try
            {
                //Valido la entidad antes.
                this.Validar(palabra);

                var cliente = new Models.Cliente();
                var cliMapper = new Mapper<Models.Cliente>();

                //Primero inserto la frase para luego relacionarla.
                mapper.Insert(palabra);

                cliente.IdCliente = Session.User.IdCliente;
                cliente.Palabras.Add(palabra);

                //Inserto la relacción entre cliente y palabra.
                cliMapper.InsertRelation(cliente, "Palabra");

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, _seccion, palabra.IdPalabra,Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Insertar, _seccion, palabra.IdPalabra, Logger.LogType.Exception, ex.Message);

                //Throw the exception to the controller.
                throw (ex);
            }
        }
        #endregion

        #region Modificar
        public void Modificar(Models.Palabra palabra)
        {
            try
            {
                //Valido la entidad antes.
                this.Validar(palabra);

                //Actualizo la entidad.
                mapper.Update(palabra);

                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, palabra.IdPalabra, Logger.LogType.Info, "");
            }
            catch (Exception ex)
            {
                //Logueo la acción ejecutada.
                Logger.Log(Logger.LogAction.Modificar, _seccion, palabra.IdPalabra, Logger.LogType.Exception, ex.Message);

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
                    new SqlParameter("@IdPalabra", Id)
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

        #region Obtener
        
        public Models.Palabra ObtenerPorId(int Id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>() {
                    new SqlParameter("@IdPalabra", Id)
                };

                var palabra = mapper.GetById(Id);

                return palabra;
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

        public List<Models.Palabra> ObtenerListadoEntity()
        {
            return mapper.GetListEntity(null);
        }

        public List<object> ObtenerListadoPorCliente()
        {
            try
            {
                var mapperPalabras = new MapperMany<Models.Cliente, Models.Palabra>();

                return mapperPalabras.GetListObjectMany(Session.User.IdCliente); ;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region Validaciones
        private void Validar(Models.Palabra palabra)
        {
            var mensaje = "";

            //Acomodo la entidad sacando los espacios en blanco.
            palabra.Palabra1 = palabra.Palabra1.Trim();
            palabra.Palabra2 = palabra.Palabra2.Trim();
            palabra.Palabra3 = palabra.Palabra3.Trim();
            palabra.Respuesta = palabra.Respuesta.Trim();

            //Limpio las palabras en blanco y acomodo la entidad.
            if (palabra.Palabra1 == "")
            {
                if (palabra.Palabra2 != "")
                {
                    palabra.Palabra1 = palabra.Palabra2;
                    palabra.Palabra2 = "";
                }
            }
            if (palabra.Palabra2 == "")
            {
                if (palabra.Palabra3 != "")
                {
                    palabra.Palabra2 = palabra.Palabra3;
                    palabra.Palabra3 = "";
                }
            }

            //Controlo que solo guarden una palabra.
            if (ControlarPalabra(palabra.Palabra1))
            {
                mensaje += (mensaje != "" ? Environment.NewLine : "");
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblPalabra") + " 1 : ";
                mensaje += MultiLanguage.GetTranslate("errorCantidadPalabras");
            }

            //Controlo que solo guarden una palabra.
            if (ControlarPalabra(palabra.Palabra2))
            {
                mensaje += (mensaje != "" ? Environment.NewLine : "");
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblPalabra") + " 2 : ";
                mensaje += MultiLanguage.GetTranslate("errorCantidadPalabras");
            }

            //Controlo que solo guarden una palabra.
            if (ControlarPalabra(palabra.Palabra3))
            {
                mensaje += (mensaje != "" ? Environment.NewLine : "");
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblPalabra") + " 3 : ";
                mensaje += MultiLanguage.GetTranslate("errorCantidadPalabras");
            }

            //Controlo que cargue al menos una palabra
            if (palabra.Palabra1 == "" & palabra.Palabra2 == "" & palabra.Palabra3 == "")
            {
                mensaje += (mensaje != "" ? Environment.NewLine : "");
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblPalabra") + ": ";
                mensaje += MultiLanguage.GetTranslate("errorPalabrasVaciasString");
            }

            //Controlo que la respuesta no esté vacía.
            if (palabra.Respuesta == "")
            {
                mensaje += (mensaje != "" ? Environment.NewLine : "");
                mensaje += MultiLanguage.GetTranslate(_seccion, "lblRespuesta") + ": ";
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

        private List<string> AnalizarPalabras(string mensaje)
        {
            var palabras = new List<string>();

            //
            mensaje = mensaje.ToLower();

            //Elimino los signos de puntuación para evitar interpretaciones incorrectas.
            mensaje = mensaje.Replace("?", "").Replace(",", "");

            //Reemplazo los acentos por letras comunes.
            mensaje = mensaje.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");

            //Split del mensaje por punto.
            palabras.AddRange(mensaje.Split(' '));

            return palabras;
        }

        public string ConsultarmatrixEngine(int idCliente, string message)
        {
            SqlParameter[] param = {
                new SqlParameter("@Palabras", String.Join("|", AnalizarPalabras(message))),
                new SqlParameter("@IdCliente", idCliente),
            };

            //Obtengo las palabras guardadas en la base.
            var palabras = mapper.GetListEntity("BuscarPalabra", param);

            //Si no hay respuesta devuelvo vacío para continuar con el proceso.
            if (palabras.Count == 0) return "";

            //Retorno la primer respuesta que encuentro.
            return VerificarPalabras(message, palabras);
        }

        public string VerificarPalabras(string message, List<Models.Palabra> palabras)
        {
            foreach (var palabra in palabras)
            {
                if (message.Contains(palabra.Palabra1))
                {
                    if (message.Contains(palabra.Palabra2))
                    {
                        if (message.Contains(palabra.Palabra3))
                        {
                            return palabra.Respuesta;
                        }
                    }
                }
            }

            return "";
        }
    }
}
