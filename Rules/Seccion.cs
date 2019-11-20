using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Framework.Helpers;


namespace Rules
{
    public class Seccion
    {
        Mapper<Framework.Models.Seccion> mapper = new Mapper<Framework.Models.Seccion>();

        #region Insertar
        public void Insertar(Framework.Models.Seccion seccion)
        {
            mapper.Insert(seccion);
            //(new Integridad()).SaveDV(Integridad.Tablas.Seccion);
        }
        #endregion

        #region Modificar
        public void Modificar(Framework.Models.Seccion seccion)
        {
            mapper.Update(seccion);
            //(new Integridad()).SaveDV(Integridad.Tablas.Consorcio);
        }
        #endregion

        #region Eliminar
        public void Eliminar(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@IdSeccion", Id)
            };

            mapper.Delete(parameters.ToArray());
        }
        #endregion

        #region Get
        public Framework.Models.Seccion GetById(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@IdSeccion", Id)
            };

            return mapper.GetByWhere(parameters.ToArray());
        }
        public List<Framework.Models.Seccion> GetList()
        {
            return mapper.GetListEntity(null);
        }
        #endregion
    }
}
