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
            var respuesta = "";

            //Enviamos el mensaje recibido para ser procesado.
            var frases = frase.AnalizarFrase(mensaje);

            //Busco la respuesta a la frase, si es que la encuentra.
            respuesta = frase.BuscarFrase(IdCliente, frases);

            if (respuesta == "")
            {
                //Continuo con el proceso
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

                //
                br.Insertar(aprender);
            }


            return respuesta;
        }

        public bool Aprender() {

            return true;
        }
    }
}
