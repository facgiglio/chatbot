using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using Framework;
using Framework.WebControls;
using log4net;
using log4net.Config;

namespace ChatBot
{
    public partial class ChatbotAdm : Page
    {
        const string _seccion = "Chatbot";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized((int)Constantes.Roles.Chatbot))
                Response.Redirect(Page.ResolveClientUrl("~/Default.aspx"));

            grdAprender.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdId"), ColumnType.Data, "IdAprender", "", true, false);
            grdAprender.AddColumn(MultiLanguage.GetTranslate(_seccion, "lblMensaje"), ColumnType.Data, "Frase", "", true, false);
            grdAprender.AddColumn(MultiLanguage.GetTranslate(_seccion, "lblMensaje"), ColumnType.Data, "Frase", "", false, true);
            grdAprender.AddContextMenu("cmnuAprender", MultiLanguage.GetTranslate(_seccion, "cmnuAprender"), "@New", "glyphicon glyphicon-eye-open", "#5cb85c", "exampleModal");

            grdAprender.DataSource = new Rules.Aprender().ObtenerListado();

            var clientes = new Rules.Cliente().ObtenerClientes();
            var cliente = clientes.Where(x => x.IdCliente == Framework.Session.User.IdCliente).FirstOrDefault();

            ddlCliente.DataSource = clientes.Cast<Object>().ToList();
            ddlCliente.DataTextField = "RazonSocial";
            ddlCliente.DataValueField = "IdCliente";
            ddlCliente.DataBind();
            ddlCliente.SelectedValue = Framework.Session.User.IdCliente.ToString();
            ddlCliente.Enabled = false;

            txtChatbotName.Value = cliente.ChatbotName;

            SetLanguage();

            SetChatbotScript(cliente);
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_seccion, "tituloPrincipal");
            
            btnCancelar.InnerHtml = MultiLanguage.GetTranslate("btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate("btnGuardar");
            lblGuardarName.InnerHtml = MultiLanguage.GetTranslate("btnGuardar");
            lblFiltrar.InnerHtml = MultiLanguage.GetTranslate("lblFiltrar");
            lblCliente.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblCliente");
            lblChatbotName.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblChatbotName");
            lblAyuda.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblAyuda").Replace("{0}", "<kbd><kbd>ctrl</kbd> + <kbd>C</kbd></kbd>").Replace("{1}", "<kbd><kbd>ctrl</kbd> + <kbd>V</kbd></kbd>");

            lblRespuesta.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblRespuesta");
            lblFrase.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblFrase");
        }
      
        [WebMethod]
        public static void GuardarChatbotName(int IdCliente, string ChatbotName)
        {
            try
            {
                var br = new Rules.Cliente();
                var cliente = br.ObtenerPorId(IdCliente);

                cliente.ChatbotName = ChatbotName;

                br.Modificar(cliente);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod]
        public static void GuardarFrasePalabra(int IdAprender, List<string> Palabras, string Frase, string Respuesta)
        {
            try
            {
                var aprendido = false;

                //Controlo si completo la frase.
                if (Frase != "")
                {
                    var brf = new Rules.Frase();
                    var frase = new Models.Frase
                    {
                        Descripcion = Frase,
                        Respuesta = Respuesta,
                        Activo = true
                    };

                    //Si completa la frase la inserto en la base.
                    brf.Insertar(frase);
                    aprendido = true;
                }
                
                //Entro si seleccionó mas de una palabra.
                if (Palabras.Count > 2)
                {
                    var brp = new Rules.Palabra();
                    var palabra = new Models.Palabra
                    {
                        Palabra1 = (Palabras.Count >= 1 ? Palabras[0] : ""),
                        Palabra2 = (Palabras.Count >= 2 ? Palabras[1] : ""),
                        Palabra3 = (Palabras.Count >= 3 ? Palabras[2] : ""),
                        Respuesta = Respuesta
                    };

                    //Inserto la entidad en la base.
                    brp.Insertar(palabra);
                    aprendido = true;
                }

                //Solo la marco como aprendida si seleccionó agluna frase o palabra para guardar.
                if (aprendido)
                {
                    var bra = new Rules.Aprender();
                    var aprender = bra.ObtenerPorId(IdAprender);
                    aprender.Aprendido = true;

                    //Inserto ambas entidades.
                    bra.Modificar(aprender);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        protected void SetChatbotScript(Models.Cliente cliente)
        {
            var script = "";

            script += ("<script type='text/javascript' src='http://localhost/Chatbot/Chatbot/chatbot.js'></script>").Replace("<" , "&lt;").Replace(">", "&gt;") + "<br />";
            script += ("<script type='text/javascript'>").Replace("<", "&lt;").Replace(">", "&gt;") + "<br />";
            script += string.Format("    var chatbot = new Chatbot('{0}', '{1}');", cliente.HashKey, cliente.ChatbotName).Replace("<", "&lt;").Replace(">", "&gt;") + "<br />";
            script += ("</script>").Replace("<", "&lt;").Replace(">", "&gt;") + "<br />";

            chatbotCode.InnerHtml = script;
        }


    }
}