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
            if (!Security.IsAuthorized((int)Constantes.Roles.Cliente))
                Response.Redirect(Page.ResolveClientUrl("~/LogIn.aspx"));

            grdCliente.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdId"), ColumnType.Data, "IdCliente", "", true, false);
            grdCliente.AddColumn(MultiLanguage.GetTranslate(_seccion, "lblRazonSocial"), ColumnType.Data, "RazonSocial", "", false, true);
            grdCliente.AddColumn(MultiLanguage.GetTranslate(_seccion, "lblDireccion"), ColumnType.Data, "Direccion", "", false, true);
            grdCliente.AddColumn(MultiLanguage.GetTranslate(_seccion, "lblHashKey"), ColumnType.Data, "HashKey", "", false, true);
            grdCliente.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate("cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdCliente.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate("cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdCliente.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate("cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdCliente.DataSource = new Rules.Cliente().ObtenerListado();

            var clientes = new Rules.Cliente().ObtenerClientes();
            var cliente = clientes.Where(x => x.IdCliente == Framework.Helpers.Session.User.IdCliente).FirstOrDefault();

            ddlCliente.DataSource = clientes.Cast<Object>().ToList();
            ddlCliente.DataTextField = "RazonSocial";
            ddlCliente.DataValueField = "IdCliente";
            ddlCliente.DataBind();
            ddlCliente.SelectedValue = Framework.Helpers.Session.User.IdCliente.ToString();
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

            lblDireccion.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblDireccion");
            lblCodigoPostal.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblCodigoPostal");
            lblTelefono.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblTelefono");
            lblHostName.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblHostName");
            lblHashKey.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblHashKey");
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