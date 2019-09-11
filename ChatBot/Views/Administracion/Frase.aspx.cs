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
    public partial class Frase : Page
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        const string _page = "Frase.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized(_page, Constantes.Action.Listado))
                Response.Redirect(Page.ResolveClientUrl("~/Views/LogIn.aspx"));
            */
            XmlConfigurator.Configure();

            this.log.Info("Inicio de la página");

            grdFrase.AddColumn(MultiLanguage.GetTranslate(_page, "grdId"), ColumnType.Data, "IdFrase", "", true, false);
            grdFrase.AddColumn(MultiLanguage.GetTranslate(_page, "lblDescripcion"), ColumnType.Data, "Descripcion", "", false, true);
            grdFrase.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate(_page, "cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdFrase.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate(_page, "cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdFrase.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate(_page, "cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdFrase.DataSource = new Rules.Frase().ObtenerListado();

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
            lblDescripcion.InnerHtml = MultiLanguage.GetTranslate(_page, "lblDescripcion");
            lblRespuesta.InnerHtml = MultiLanguage.GetTranslate(_page, "lblRespuesta");
            lblActivo.InnerHtml = MultiLanguage.GetTranslate(_page, "lblActivo");
        }

        [WebMethod]
        public static void Insertar(Entities.Frase frase)
        {
            var br = new Rules.Frase();
            br.Insertar(frase);
        }

        [WebMethod]
        public static void Modificar(Entities.Frase frase)
        {
            try
            {
                var br = new Rules.Frase();
                br.Modificar(frase);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod]
        public static void Eliminar(Entities.Frase frase)
        {
            var br = new Rules.Frase();
            br.Eliminar(frase.IdFrase);
        }

        [WebMethod]
        public static Entities.Frase Obtener(int Id)
        {
            try
            {
                var br = new Rules.Frase();
                return br.ObtenerPorId(Id);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}