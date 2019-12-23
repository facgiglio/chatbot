using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rules
{
    public class Chatbot
    {
        public int IdCliente { get; }

        public Chatbot(int idCliente)
        {
            IdCliente = idCliente;
        }

        public string Responder(string mensaje) {
            var frase = new Rules.Frase();
            var palabra = new Rules.Palabra();
            var respuesta = "";

            //Enviamos el mensaje recibido para ser procesado.
            var frases = frase.AnalizarFrase(mensaje);
            //Busco la respuesta a la frase, si es que la encuentra.
            respuesta = frase.BuscarFrase(IdCliente, frases);

            //Si no encontró respuesta, continuo buscando por palabras
            if (respuesta == "")
            {
                respuesta = palabra.ConsultarmatrixEngine(IdCliente, mensaje);
            }

            //Si no encontré la respuesta, 
            if (respuesta == "")
            {
                var br = new Rules.Aprender();
                var aprender = new Models.Aprender {
                    IdCliente = IdCliente,
                    Frase = mensaje,
                    Aprendido = false
                };

                //Respuesta por defecto.
                respuesta = "No entendí, por favor podrías repetirlo?";

                //Inserto la entidad para poder lugeo configurarlo.
                br.Insertar(aprender);
            }

            return respuesta;
        }
    }
}
