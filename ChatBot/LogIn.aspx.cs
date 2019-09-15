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
    public partial class LogIn : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static MessageDTO Login(string usuario, string contrasena)
        {
            Rules.Usuario usuarioBR = new Rules.Usuario();
            var message = usuarioBR.LogIn(usuario, contrasena);

            return message;
        }

        [WebMethod]
        public static void LogOut()
        {
            Framework.Helpers.Session.ClearSession();
        }
    }
}