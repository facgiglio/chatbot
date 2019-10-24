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
            var cliente = new Models.Cliente();
            var cliMapper = new Mapper<Models.Cliente>();

            //Primero inserto la frase para luego relacionarla.
            mapper.Insert(palabra);

            cliente.IdCliente = Session.User.IdCliente;
            cliente.Palabras.Add(palabra);

            cliMapper.InsertRelation(cliente);
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
