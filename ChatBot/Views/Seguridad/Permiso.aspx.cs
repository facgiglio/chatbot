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
    public partial class Permiso : Page
    {
        private readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        const string _page = "Permiso.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized("Permiso.aspx", Constantes.Action.Listado))
                Response.Redirect(Page.ResolveClientUrl("~/Views/LogIn.aspx"));
            */
            var descripcion = "";
            if (IsPostBack)
            {
                descripcion = txtPermiso.Value;
            }

            grdPermiso.AddColumn(MultiLanguage.GetTranslate(_page, "grdId"), ColumnType.Data, "IdPermiso", "", true, false);
            grdPermiso.AddColumn(MultiLanguage.GetTranslate(_page, "grdDescripcion"), ColumnType.Data, "Descripcion", "", true, true);
            grdPermiso.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate("Generico", "cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdPermiso.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate("Generico", "cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdPermiso.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate("Generico", "cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdPermiso.DataSource = new Rules.Permiso().GetList(descripcion);

            SetLanguage();
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_page, "tituloPrincipal");
            lblNuevo.InnerHtml = MultiLanguage.GetTranslate("lblNuevo");
            lblFiltrar.InnerHtml = MultiLanguage.GetTranslate("lblFiltrar");
            btnCancelar.InnerHtml = MultiLanguage.GetTranslate("btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate("btnGuardar");
            lblPermiso.InnerHtml = MultiLanguage.GetTranslate(_page, "lblPermiso");
            lblDescripcion.InnerHtml = MultiLanguage.GetTranslate(_page, "lblDescripcion");
        }

        [WebMethod]
        public static void Insertar(Framework.Models.Permiso permiso)
        {
            try
            {
                var br = new Rules.Permiso();
                br.Insertar(permiso);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod]
        public static void Modificar(Framework.Models.Permiso permiso)
        {
            try
            {
                var br = new Rules.Permiso();
                br.Modificar(permiso);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod]
        public static void Eliminar(Framework.Models.Permiso permiso)
        {
            try
            {
                var br = new Rules.Permiso();
                br.Eliminar(permiso.IdPermiso);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod]
        public static Framework.Models.Permiso Obtener(int Id)
        {
            try
            {
                var br = new Rules.Permiso();
                return br.GetById(Id);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}