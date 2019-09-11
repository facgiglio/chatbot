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
    public partial class MultiIdioma : Page
    {
        const string _page = "MultiIdioma.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            int seccion = 0;

            if (IsPostBack)
            {
                seccion = Convert.ToInt32(ddlSeccionFilter.SelectedValue);
            }

            grdMultiIdioma.AddColumn("#", ColumnType.Data, "IdMultiLenguaje", "", true, false);
            grdMultiIdioma.AddColumn(MultiLanguage.GetTranslate(_page, "grdSeccion"), ColumnType.Data, "Seccion", "", true, true);
            grdMultiIdioma.AddColumn(MultiLanguage.GetTranslate(_page, "grdMultiIdioma"), ColumnType.Data, "Descripcion", "", true, true);
            grdMultiIdioma.AddColumn("es", ColumnType.TextBox, "es", "IdEs", true, true);
            grdMultiIdioma.AddColumn("en", ColumnType.TextBox, "en", "IdEn", true, true);

            grdMultiIdioma.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate(_page, "cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdMultiIdioma.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate(_page, "cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdMultiIdioma.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate(_page, "cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");

            grdMultiIdioma.DataSource = new Framework.Rules.MultiLenguaje().GetMultiLanguageList(seccion);

            var idiomas = new Rules.Idioma().GetList();
            var sectionList = new Rules.Seccion().GetList();

            ddlSeccion.DataSource = sectionList;
            ddlSeccion.DataTextField = "Descripcion";
            ddlSeccion.DataValueField = "IdSeccion";
            ddlSeccion.DataBind();

            sectionList.Add(new { IdSeccion = 0, Descripcion = "Select" });
            ddlSeccionFilter.SelectedValue = seccion.ToString();
            ddlSeccionFilter.DataSource = sectionList;
            ddlSeccionFilter.DataTextField = "Descripcion";
            ddlSeccionFilter.DataValueField = "IdSeccion";
            ddlSeccionFilter.DataBind();

            /*
            grdRoles.AddColumn(MultiLanguage.GetTranslate("Usuario.aspx", "grdId"), ColumnType.Data, "IdRol", "", true, false);
            grdRoles.AddColumn(MultiLanguage.GetTranslate("Usuario.aspx", "grdId"), ColumnType.CheckBox, "Descripcion", "IdRol", false, true);
            grdRoles.AddColumn(MultiLanguage.GetTranslate("Usuario.aspx", "grdNombre"), ColumnType.Data, "Descripcion", "", false, true);
            grdRoles.Config.Condense = true;
            grdRoles.DataSource = new Rules.Rol().GetList();

            ddlIdioma.DataSource = new Rules.Idioma().GetList();
            ddlIdioma.DataTextField = "Descripcion";
            ddlIdioma.DataValueField = "Id";
            ddlIdioma.DataBind();
            */

            SetLanguage();
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_page, "tituloPrincipal");
            tituloSeccion.InnerHtml = MultiLanguage.GetTranslate(_page, "tituloSeccion");

            lblSeccionFiltrar.InnerHtml = MultiLanguage.GetTranslate(_page, "lblSeccion") + ":";
            lblSeccion.InnerHtml = MultiLanguage.GetTranslate(_page, "lblSeccion") + ":";
            lblDescripcion.InnerHtml = MultiLanguage.GetTranslate(_page, "lblDescripcion") + ":";

            btnCancelar.InnerHtml = MultiLanguage.GetTranslate(_page, "btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate(_page, "btnGuardar");
            
            lblFiltrar.InnerHtml = MultiLanguage.GetTranslate(_page, "lblFiltrar");
            lblNuevo.InnerHtml = MultiLanguage.GetTranslate(_page, "lblNuevo");
            lblRefrescarCache.InnerHtml = MultiLanguage.GetTranslate(_page, "lblRefrescarCache");
        }

        [WebMethod]
        public static void Insertar(Framework.Models.MultiLenguaje multiLanguage)
        {
            var br = new Framework.Rules.MultiLenguaje();
            br.Insertar(multiLanguage);
        }

        [WebMethod]
        public static Int32 InsertarTraduccion(Framework.Models.Traduccion traduccion)
        {
            var br = new Rules.Traduccion();
            br.Insertar(ref traduccion);

            return traduccion.IdTraduccion;
        }

        [WebMethod]
        public static void Modificar(Framework.Models.MultiLenguaje multiLanguage)
        {
            var br = new Framework.Rules.MultiLenguaje();
            br.Modificar(multiLanguage);
        }

        [WebMethod]
        public static void ModificarTraduccion(Framework.Models.Traduccion traduccion)
        {
            var br = new Rules.Traduccion();
            br.Modificar(traduccion);
        }

        [WebMethod]
        public static void Eliminar(int Id)
        {
            var br = new Framework.Rules.MultiLenguaje();
            br.Eliminar(Id);
        }

        [WebMethod]
        public static Framework.Models.MultiLenguaje Obtener(int IdMultiLenguaje)
        {
            var br = new Framework.Rules.MultiLenguaje();
            return br.GetById(IdMultiLenguaje);
        }

        [WebMethod]
        public static void RefreshCache()
        {
            Framework.MultiLanguage.RefreshCache();
        }
    }
}