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
        Mapper<Entities.Palabra> mapper = new Mapper<Entities.Palabra>();

        #region Insertar
        public void Insertar(Entities.Palabra palabra)
        {
            mapper.Insert(palabra);
        }
        #endregion

        #region Modificar
        public void Modificar(Entities.Palabra palabra)
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
        
        public Entities.Palabra ObtenerPorId(int Id)
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

        public List<Entities.Palabra> ObtenerListadoEntity()
        {
            return mapper.GetListEntity(null);
        }

        #endregion

        public Entities.Palabra AnalizarPalabras(string texto)
        {

            return null;
        }

        public List<Entities.Palabra> ConsultarmatrixEngine(Entities.Palabra palabra)
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
