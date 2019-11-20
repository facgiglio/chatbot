using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Aprender
    {
        Mapper<Models.Aprender> mapper = new Mapper<Models.Aprender>();

        #region Insertar
        public void Insertar(Models.Aprender aprender)
        {
            mapper.Insert(aprender);
        }
        #endregion

        #region Modificar
        public void Modificar(Models.Aprender aprender)
        {
            //Actualizo el aprender
            mapper.Update(aprender);
        }
        #endregion

        #region Eliminar
        public void Eliminar(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter("@IdAprender", Id));

            mapper.Delete(parameters.ToArray());
        }
        #endregion

        #region Get Aprender
        public Models.Aprender ObtenerPorId(int Id)
        {
            try
            {
                return mapper.GetById(Id); ;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
       
        public List<object> ObtenerListado()
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@Aprendido", false),
                new SqlParameter("@IdCliente", Framework.Session.User.IdCliente)
            };

            return mapper.GetList(parameters.ToArray());
        }
        #endregion
    }
    
}
