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
        public string Chat(string input)
        {
            var Chatbot = new Rules.Chatbot();
            var respuesta = Chatbot.Responder(input);

            return respuesta;
        }
    }
}
