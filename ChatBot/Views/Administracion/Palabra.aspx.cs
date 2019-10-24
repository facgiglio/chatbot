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
        const string _seccion = "Palabra";

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized(_page, Constantes.Action.Listado))
                Response.Redirect(Page.ResolveClientUrl("~/Views/LogIn.aspx"));
            */
            XmlConfigurator.Configure();

            grdPalabra.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdId"), ColumnType.Data, "IdPalabra", "", true, false);
            grdPalabra.AddColumn(MultiLanguage.GetTranslate(_seccion, "lblPalabra"), ColumnType.Data, "Palabra1", "", false, true);
            grdPalabra.AddColumn(MultiLanguage.GetTranslate(_seccion, "lblPalabra"), ColumnType.Data, "Palabra2", "", false, true);
            grdPalabra.AddColumn(MultiLanguage.GetTranslate(_seccion, "lblPalabra"), ColumnType.Data, "Palabra3", "", false, true);
            grdPalabra.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate("cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdPalabra.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate("cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdPalabra.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate("cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdPalabra.DataSource = new Rules.Palabra().ObtenerListado();

            SetLanguage();
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_seccion, "tituloPrincipal");
            
            btnCancelar.InnerHtml = MultiLanguage.GetTranslate("btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate("btnGuardar");
            lblNuevo.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblNuevo");
            lblFiltrar.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblFiltrar");
            lblPalabra.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblPalabra");
            lblPalabra1.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblPalabra");
            lblPalabra2.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblPalabra");
            lblPalabra3.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblPalabra");
            lblRespuesta.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblRespuesta");
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