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
        Mapper<Entities.Aprender> mapper = new Mapper<Entities.Aprender>();

        #region Insertar
        public void Insertar(Entities.Aprender aprender)
        {
            mapper.Insert(aprender);
        }
        #endregion

        #region Modificar
        public void Modificar(Entities.Aprender aprender)
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
        public Entities.Aprender ObtenerPorId(int Id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>() {
                    new SqlParameter("@IdAprender", Id)
                };

                return mapper.GetById(Id); ;
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
        #endregion
    }
    
}
