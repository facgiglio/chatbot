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
            SqlParameter[] parameters = { new SqlParameter("@RazonSocial", RazonSocial) };

            return mapper.GetByWhere(parameters);
        }
        #endregion

        public List<string> AnalizarFrase(string mensaje)
        {
            var frases = new List<string>();

            //Elimino los signos de puntuación para evitar interpretaciones incorrectas.
            mensaje = mensaje.Replace("?", "").Replace(",","");

            //Reemplazo los acentos por letras comunes.
            mensaje = mensaje.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u");

            //Split del mensaje por punto.
            frases.AddRange(mensaje.Split('.'));

            return frases;
        }

        public string BuscarFrase(List<string> frases)
        {
            SqlParameter[] paramExacta = {new SqlParameter("@Frases", String.Join("|", frases))};
            //Obtengo primero por la frase exacta.
            var respuestas = mapper.GetListEntity("BuscarFraseExacta", paramExacta);

            if (respuestas.Count == 0)
            {
                SqlParameter[] paramAprox = { new SqlParameter("@Frases", String.Join("|", frases)) };
                //No no tengo la frase exacta, obtengo por un aproximado.
                respuestas = mapper.GetListEntity("BuscarFraseAproximada", paramAprox);

                if (respuestas.Count == 0)
                {
                    //Aca tengo que guardar el tema de aprender.
                    return "No entendí una mierda, podes escribir mejor carajo. No cuesta una mierda ser claro, no?";
                }
            }

            //Retorno la primer respuesta que encuentro.
            return respuestas[0].Respuesta;
        }
    }
}
