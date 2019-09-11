using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Framework.Helpers;


namespace Rules
{
    public class Traduccion
    {
        Mapper<Framework.Models.Traduccion> mapper = new Mapper<Framework.Models.Traduccion>();

        #region Insertar
        public void Insertar(ref Framework.Models.Traduccion traduccion)
        {
            traduccion.IdTraduccion = mapper.Insert(traduccion);
        }
        #endregion

        #region Modificar
        public void Modificar(Framework.Models.Traduccion traduccion)
        {
            mapper.Update(traduccion);
        }
        #endregion

        #region Eliminar
        public void Eliminar(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@IdTraduccion", Id)
            };

            mapper.Delete(parameters.ToArray());
        }
        #endregion

        #region Get
        public Framework.Models.Traduccion GetById(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@IdTraduccion", Id)
            };

            return mapper.GetByWhere(parameters.ToArray());
        }
        public List<object> GetList()
        {
            return mapper.GetList(null);
        }
        #endregion
    }

}
