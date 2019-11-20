using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework;
using Framework.WebControls;

namespace ChatBot
{
    public partial class Frase : Page
    {
        const string _seccion = "Frase";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized((int)Constantes.Roles.Frases))
                Response.Redirect(Page.ResolveClientUrl("~/Default.aspx"));
            
            grdFrase.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdId"), ColumnType.Data, "IdFrase", "", true, false);
            grdFrase.AddColumn(MultiLanguage.GetTranslate(_seccion, "lblDescripcion"), ColumnType.Data, "Descripcion", "", false, true);
            grdFrase.AddColumn(MultiLanguage.GetTranslate(_seccion, "lblRespuesta"), ColumnType.Data, "Respuesta", "", false, true);
            grdFrase.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate("cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdFrase.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate("cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdFrase.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate("cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdFrase.DataSource = new Rules.Frase().ObtenerListadoPorCliente();

            SetLanguage();
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_seccion, "tituloPrincipal");
            
            btnCancelar.InnerHtml = MultiLanguage.GetTranslate("btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate("btnGuardar");
            lblNuevo.InnerHtml = MultiLanguage.GetTranslate("lblNuevo");
            lblFiltrar.InnerHtml = MultiLanguage.GetTranslate("lblFiltrar");
            lblDescripcionFiltrar.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblDescripcion");
            lblDescripcion.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblDescripcion");
            lblRespuesta.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblRespuesta");
            lblActivo.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblActivo");
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