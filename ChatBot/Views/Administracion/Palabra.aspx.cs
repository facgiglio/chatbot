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
    public partial class Palabra : Page
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        const string _page = "Palabra.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized(_page, Constantes.Action.Listado))
                Response.Redirect(Page.ResolveClientUrl("~/Views/LogIn.aspx"));
            */
            XmlConfigurator.Configure();

            grdPalabra.AddColumn(MultiLanguage.GetTranslate(_page, "grdId"), ColumnType.Data, "IdPalabra", "", true, false);
            grdPalabra.AddColumn(MultiLanguage.GetTranslate(_page, "lblPalabra"), ColumnType.Data, "Palabra1", "", false, true);
            grdPalabra.AddColumn(MultiLanguage.GetTranslate(_page, "lblPalabra"), ColumnType.Data, "Palabra2", "", false, true);
            grdPalabra.AddColumn(MultiLanguage.GetTranslate(_page, "lblPalabra"), ColumnType.Data, "Palabra3", "", false, true);
            grdPalabra.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate(_page, "cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdPalabra.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate(_page, "cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdPalabra.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate(_page, "cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdPalabra.DataSource = new Rules.Palabra().ObtenerListado();

            SetLanguage();
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_page, "tituloPrincipal");
            
            btnCancelar.InnerHtml = MultiLanguage.GetTranslate(_page, "btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate(_page, "btnGuardar");
            lblNuevo.InnerHtml = MultiLanguage.GetTranslate(_page, "lblNuevo");
            lblFiltrar.InnerHtml = MultiLanguage.GetTranslate(_page, "lblFiltrar");
            lblPalabra.InnerHtml = MultiLanguage.GetTranslate(_page, "lblPalabra");
            lblPalabra1.InnerHtml = MultiLanguage.GetTranslate(_page, "lblPalabra");
            lblPalabra2.InnerHtml = MultiLanguage.GetTranslate(_page, "lblPalabra");
            lblPalabra3.InnerHtml = MultiLanguage.GetTranslate(_page, "lblPalabra");
            lblRespuesta.InnerHtml = MultiLanguage.GetTranslate(_page, "lblRespuesta");
        }

        [WebMethod]
        public static void Insertar(Models.Palabra palabra)
        {
            var br = new Rules.Palabra();
            br.Insertar(palabra);
        }

        [WebMethod]
        public static void Modificar(Models.Palabra palabra)
        {
            try
            {
                var br = new Rules.Palabra();
                br.Modificar(palabra);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod]
        public static void Eliminar(Models.Palabra palabra)
        {
            var br = new Rules.Palabra();
            br.Eliminar(palabra.IdPalabra);
        }

        [WebMethod]
        public static Models.Palabra Obtener(int Id)
        {
            try
            {
                var br = new Rules.Palabra();
                return br.ObtenerPorId(Id);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}