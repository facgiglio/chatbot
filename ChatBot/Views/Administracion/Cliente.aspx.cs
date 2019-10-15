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
    public partial class Cliente : Page
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        const string _page = "Cliente.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized(_page, Constantes.Action.Listado))
                Response.Redirect(Page.ResolveClientUrl("~/Views/LogIn.aspx"));
            */
            XmlConfigurator.Configure();

            this.log.Info("Inicio de la página");

            grdCliente.AddColumn(MultiLanguage.GetTranslate(_page, "grdId"), ColumnType.Data, "IdCliente", "", true, false);
            grdCliente.AddColumn(MultiLanguage.GetTranslate(_page, "lblRazonSocial"), ColumnType.Data, "RazonSocial", "", false, true);
            grdCliente.AddColumn(MultiLanguage.GetTranslate(_page, "lblDireccion"), ColumnType.Data, "Direccion", "", false, true);
            grdCliente.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate(_page, "cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdCliente.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate(_page, "cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdCliente.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate(_page, "cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdCliente.DataSource = new Rules.Cliente().ObtenerListado();

            SetLanguage();
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_page, "tituloPrincipal");
            
            btnCancelar.InnerHtml = MultiLanguage.GetTranslate(_page, "btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate(_page, "btnGuardar");
            lblNuevo.InnerHtml = MultiLanguage.GetTranslate(_page, "lblNuevo");
            lblFiltrar.InnerHtml = MultiLanguage.GetTranslate(_page, "lblFiltrar");
            lblRazonSocialFiltrar.InnerHtml = MultiLanguage.GetTranslate(_page, "lblRazonSocial");
            lblRazonSocial.InnerHtml = MultiLanguage.GetTranslate(_page, "lblRazonSocial");
            lblDireccion.InnerHtml = MultiLanguage.GetTranslate(_page, "lblDireccion");
            lblCodigoPostal.InnerHtml = MultiLanguage.GetTranslate(_page, "lblCodigoPostal");
            lblTelefono.InnerHtml = MultiLanguage.GetTranslate(_page, "lblTelefono");
        }

        [WebMethod]
        public static void Insertar(Models.Cliente cliente)
        {
            var br = new Rules.Cliente();
            br.Insertar(cliente);
        }

        [WebMethod]
        public static void Modificar(Models.Cliente cliente)
        {
            try
            {
                var br = new Rules.Cliente();
                br.Modificar(cliente);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod]
        public static void Eliminar(Models.Cliente cliente)
        {
            var br = new Rules.Cliente();
            br.Eliminar(cliente.IdCliente);
        }

        [WebMethod]
        public static Models.Cliente Obtener(int Id)
        {
            try
            {
                var br = new Rules.Cliente();
                return br.ObtenerPorId(Id);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}