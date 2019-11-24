using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace ChatBot.Chatbot
{
    /// <summary>
    /// Summary description for chatbotWS
    /// </summary>
    [WebService(Namespace = "http://localhost/ChatBot/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class chatbotWS : System.Web.Services.WebService
    {
        [WebMethod]
        public string Chat(string input, string hashKey, string hostName)
        {
            try
            {
                var cliente = new Rules.Cliente().ObtenerPorId(Convert.ToInt32(Framework.Security.Decrypt(hashKey)));

                if (cliente == null)
                {
                    return "El cliente no está registrado.";
                }

                if (cliente.HostName == "" || cliente.HostName is null)
                {
                    return "El HostName no está configurado, por favor comuníquese con FacaxSystem.";
                }

                if (!hostName.ToLower().Contains(cliente.HostName.ToLower()))
                {
                    return "El sitio no está correctamente configurado para ejecutar el chatbot, contactese con Facax System.";
                }

                var Chatbot = new Rules.Chatbot(cliente.IdCliente);
                var respuesta = Chatbot.Responder(input);

                return respuesta;
            }
            catch (Exception Ex)
            {
                return "Ocurrió un error al procesar tu consulta, intentalo nuevamente. " + Ex.Message;
            }
            
        }
    }
}
