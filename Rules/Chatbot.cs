using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rules
{
    public class Chatbot
    {
        //Ver si se puede obtener el id del navegador.
        public string chatId = "";

        public string Responder(string mensaje) {
            var frase = new Rules.Frase();
            var respuesta = "";

            //Enviamos el mensaje recibido para ser procesado.
            var frases = frase.AnalizarFrase(mensaje);
            //Busco la respuesta a la frase, si es que la encuentra.
            respuesta = frase.BuscarFrase(frases);

            return respuesta;
        }

        public bool Aprender() {

            return true;
        }
    }
}
