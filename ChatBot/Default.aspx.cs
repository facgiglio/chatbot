using System;
using System.Web.UI;
using System.Web.Services;

namespace ChatBot
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void Cambiaridioma(int IdIdioma)
        {
            try
            {
                var br = new Rules.Usuario();
                var usuario = br.GetById(Framework.Session.User.IdUsuario);

                usuario.IdIdioma = IdIdioma;

                br.Modificar(usuario);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}