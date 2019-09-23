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
    public partial class Rol : Page
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        const string _page = "Rol.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized("Rol.aspx", Constantes.Action.Listado))
                Response.Redirect(Page.ResolveClientUrl("~/Views/LogIn.aspx"));
            */

            var descripcion = "";
            if (IsPostBack)
            {
                descripcion = txtRol.Value;
            }

            grdRol.AddColumn(MultiLanguage.GetTranslate(_page, "grdId"), ColumnType.Data, "IdRol", "", true, false);
            grdRol.AddColumn(MultiLanguage.GetTranslate(_page, "grdDescripcion"), ColumnType.Data, "Descripcion", "", true, true);
            grdRol.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate("cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdRol.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate("cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdRol.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate("cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdRol.DataSource = new Rules.Rol().GetList(descripcion);


            grdPermiso.AddColumn(MultiLanguage.GetTranslate(_page, "grdId"), ColumnType.Data, "IdPermiso", "", true, false);
            grdPermiso.AddColumn(MultiLanguage.GetTranslate(_page, "grdId"), ColumnType.CheckBox, "IdPermiso", "IdPermiso", false, true);
            grdPermiso.AddColumn(MultiLanguage.GetTranslate(_page, "grdDescripcion"), ColumnType.Data, "Descripcion", "", true, true);
            grdPermiso.Config.Condense = true;
            grdPermiso.DataSource = new Rules.Permiso().GetList("");

            SetLanguage();
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_page, "tituloPrincipal");
            lblNuevo.InnerHtml = MultiLanguage.GetTranslate("lblNuevo");
            lblFiltrar.InnerHtml = MultiLanguage.GetTranslate("lblFiltrar");
            btnCancelar.InnerHtml = MultiLanguage.GetTranslate("btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate("btnGuardar");
            lblRol.InnerHtml = MultiLanguage.GetTranslate(_page, "lblRol");
            lblDescripcion.InnerHtml = MultiLanguage.GetTranslate(_page, "lblDescripcion");
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