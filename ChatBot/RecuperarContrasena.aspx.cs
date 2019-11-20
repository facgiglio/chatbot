using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using Framework;
using Framework.Models;

namespace ChatBot
{
    public partial class RecuperarContrasena : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static MessageDTO Recuperar(string usuario)
        {
            var message = (new Rules.Usuario()).RecuperarContrasena(usuario);

            return message;
        }

        [WebMethod]
        public static MessageDTO Cambiar(string usuario, string codigoRecuperacion, string nuevaContrasena, string repetirContrasena)
        {
            var message = (new Rules.Usuario()).CambiarContrasena(usuario, codigoRecuperacion, nuevaContrasena, repetirContrasena);

            return message;
        }
    }
}