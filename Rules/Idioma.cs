using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Idioma
    {
        Mapper<Framework.Models.Idioma> mapper = new Mapper<Framework.Models.Idioma>();

        #region Get Usuario
        
        public Framework.Models.Idioma GetById(int Id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>() {
                    new SqlParameter("@IdUsuario", Id)
                };

                var usuario = mapper.GetById(Id);

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

        public List<Framework.Models.Idioma> GetEntityList()
        {
            return mapper.GetListEntity(null);
        }

        #endregion

    }
}
