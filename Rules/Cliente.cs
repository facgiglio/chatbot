using System;
using System.Collections.Generic;
using Framework;
using Framework.Helpers;
using System.Data.SqlClient;
using System.Data;

namespace Rules
{
    public class Cliente
    {
        Mapper<Entities.Cliente> mapper = new Mapper<Entities.Cliente>();

        #region Insertar
        public void Insertar(Entities.Cliente cliente)
        {
            mapper.Insert(cliente);
        }
        #endregion

        #region Modificar
        public void Modificar(Entities.Cliente cliente)
        {
            //Actualizo el usuario
            mapper.Update(cliente);
        }
        #endregion

        #region Eliminar
        public void Eliminar(int Id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@IdCliente", Id)
            };

            mapper.Delete(parameters.ToArray());
        }
        #endregion

        #region Obtener
        
        public Entities.Cliente ObtenerPorId(int Id)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>() {
                    new SqlParameter("@IdCliente", Id)
                };

                var cliente = mapper.GetById(Id);

                return cliente;
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
        
        public Entities.Cliente ObtenerPoryRazonSocial(string RazonSocial)
        {
            List<SqlParameter> parameters = new List<SqlParameter>() {
                new SqlParameter("@RazonSocial", RazonSocial)
            };

            return mapper.GetByWhere(parameters.ToArray());
        }
        
        #endregion

    }
}
