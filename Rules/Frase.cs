using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Frase
    {
        Mapper<Entities.Frase> mapper = new Mapper<Entities.Frase>();

        #region Insertar
        public void Insertar(Entities.Frase frase)
        {
            mapper.Insert(frase);
        }
        #endregion

        #region Modificar
        public void Modificar(Entities.Frase frase)
        {
            //Actualizo el usuario
            mapper.Update(frase);
        }
        #endregion

        #region Eliminar
        public void Eliminar(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@IdFrase", Id)
            };

            mapper.Delete(parameters.ToArray());
        }
        #endregion

        #region Obtener
        
        public Entities.Frase ObtenerPorId(int Id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>() {
                    new SqlParameter("@IdFrase", Id)
                };

                var frase = mapper.GetById(Id);

                return frase;
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
        
        public Entities.Frase ObtenerPoryRazonSocial(string RazonSocial)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@RazonSocial", RazonSocial)
            };

            return mapper.GetByWhere(parameters.ToArray());
        }
        
        #endregion

    }
}
