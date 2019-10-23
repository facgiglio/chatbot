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
        const string _page = "Frase";

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized(_page, Constantes.Action.Listado))
                Response.Redirect(Page.ResolveClientUrl("~/Views/LogIn.aspx"));
            */

            grdFrase.AddColumn(MultiLanguage.GetTranslate(_page, "grdId"), ColumnType.Data, "IdFrase", "", true, false);
            grdFrase.AddColumn(MultiLanguage.GetTranslate(_page, "lblDescripcion"), ColumnType.Data, "Descripcion", "", false, true);
            grdFrase.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate("cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdFrase.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate("cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdFrase.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate("cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdFrase.DataSource = new Rules.Frase().ObtenerListadoPorCliente();

            SetLanguage();
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_page, "tituloPrincipal");
            
            btnCancelar.InnerHtml = MultiLanguage.GetTranslate("btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate("btnGuardar");
            lblNuevo.InnerHtml = MultiLanguage.GetTranslate("lblNuevo");
            lblFiltrar.InnerHtml = MultiLanguage.GetTranslate("lblFiltrar");
            lblDescripcionFiltrar.InnerHtml = MultiLanguage.GetTranslate(_page, "lblDescripcion");
            lblDescripcion.InnerHtml = MultiLanguage.GetTranslate(_page, "lblDescripcion");
            lblRespuesta.InnerHtml = MultiLanguage.GetTranslate(_page, "lblRespuesta");
            lblActivo.InnerHtml = MultiLanguage.GetTranslate(_page, "lblActivo");
        }

        [WebMethod]
        public static void Insertar(Models.Frase frase)
        {
            var br = new Rules.Frase();
            br.Insertar(frase);
        }

        [WebMethod]
        public static void Modificar(Models.Frase frase)
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
        public static void Eliminar(Models.Frase frase)
        {
            var br = new Rules.Frase();
            br.Eliminar(frase.IdFrase);
        }

        [WebMethod]
        public static Models.Frase Obtener(int Id)
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