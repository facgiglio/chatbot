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
    public partial class Rol : Page
    {
        const string _seccion = "Rol";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized((int)Constantes.Roles.Roles))
                Response.Redirect(Page.ResolveClientUrl("~/Default.aspx"));

            var descripcion = "";

            if (IsPostBack)
            {
                descripcion = txtRol.Value;
            }

            grdRol.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdId"), ColumnType.Data, "IdRol", "", true, false);
            grdRol.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdDescripcion"), ColumnType.Data, "Descripcion", "", true, true);
            grdRol.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate("cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdRol.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate("cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdRol.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate("cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdRol.DataSource = new Rules.Rol().GetList(descripcion);

            SetLanguage();
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_seccion, "tituloPrincipal");
            lblRol.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblRol");
            lblDescripcion.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblDescripcion");

            lblNuevo.InnerHtml = MultiLanguage.GetTranslate("lblNuevo");
            lblFiltrar.InnerHtml = MultiLanguage.GetTranslate("lblFiltrar");
            btnCancelar.InnerHtml = MultiLanguage.GetTranslate("btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate("btnGuardar");
        }

        [WebMethod]
        public static void Insertar(Framework.Models.Rol rol)
        {
            try
            {
                var br = new Rules.Rol();
                br.Insertar(rol);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod]
        public static void Modificar(Framework.Models.Rol rol)
        {
            try
            {
                var br = new Rules.Rol();
                br.Modificar(rol);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod]
        public static void Eliminar(Framework.Models.Rol rol)
        {
            try
            {
                var br = new Rules.Rol();
                br.Eliminar(rol.IdRol);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod]
        public static Framework.Models.Rol Obtener(int Id)
        {
            try
            {
                var br = new Rules.Rol();
                return br.GetById(Id);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}