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

        #region Insertar
        public void Insertar(Models.Palabra palabra)
        {
            var logMessage = "Insertar " + this.GetType().Name + " - Id: " + palabra.IdPalabra.ToString();

            try
            {
                var cliente = new Models.Cliente();
                var cliMapper = new Mapper<Models.Cliente>();

                //Primero inserto la frase para luego relacionarla.
                mapper.Insert(palabra);

                cliente.IdCliente = Session.User.IdCliente;
                cliente.Palabras.Add(palabra);

                //Inserto la relacción entre cliente y palabra.
                cliMapper.InsertRelation(cliente, "Palabra");
            }
            catch (Exception ex)
            {
                //Logueo la excepción.
                Logger.LogException(logMessage + " - Error: " + ex.Message);
            }
        }
        #endregion

        #region Modificar
        public void Modificar(Models.Palabra palabra)
        {
            //Actualizo el usuario
            mapper.Update(palabra);
        }
        #endregion

        #region Eliminar
        public void Eliminar(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@IdPalabra", Id)
            };

            mapper.Delete(parameters.ToArray());
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

        public Models.Palabra AnalizarPalabras(string texto)
        {

            return null;
        }

        public List<Models.Palabra> ConsultarmatrixEngine(Models.Palabra palabra)
        {

            return null;
        }

        public string ProcesarPalabras(string texto)
        {
            return "";
        }

        public bool VerificarPalabras(string texto)
        {
            return false;
        }


    }
}
