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
    public partial class Usuario : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized("Usuario.aspx", Constantes.Action.Listado))
                Response.Redirect(Page.ResolveClientUrl("~/Views/LogIn.aspx"));
            */

            grdUsuario.AddColumn(MultiLanguage.GetTranslate("Usuario.aspx", "grdId"), ColumnType.Data, "IdUsuario", "", true, false);
            grdUsuario.AddColumn(MultiLanguage.GetTranslate("Usuario.aspx", "grdNombre"), ColumnType.Data, "Nombre", "", false, true);
            grdUsuario.AddColumn(MultiLanguage.GetTranslate("Usuario.aspx", "grdApellido"), ColumnType.Data, "Apellido", "", false, true);
            grdUsuario.AddColumn(MultiLanguage.GetTranslate("Usuario.aspx", "grdEmail"), ColumnType.Data, "Email", "", false, true);
            grdUsuario.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate("Usuario.aspx", "cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdUsuario.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate("Usuario.aspx", "cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdUsuario.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate("Usuario.aspx", "cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdUsuario.DataSource = new Rules.Usuario().GetList();

            grdRoles.AddColumn(MultiLanguage.GetTranslate("Usuario.aspx", "grdId"), ColumnType.Data, "IdRol", "", true, false);
            grdRoles.AddColumn(MultiLanguage.GetTranslate("Usuario.aspx", "grdId"), ColumnType.CheckBox, "Descripcion", "IdRol", false, true);
            grdRoles.AddColumn(MultiLanguage.GetTranslate("Usuario.aspx", "grdNombre"), ColumnType.Data, "Descripcion", "", false, true);
            grdRoles.Config.Condense = true;
            grdRoles.DataSource = new Rules.Rol().GetList("");

            ddlIdioma.DataSource = new Rules.Idioma().GetList();
            ddlIdioma.DataTextField = "Descripcion";
            ddlIdioma.DataValueField = "IdIdioma";
            ddlIdioma.DataBind();

            SetLanguage();
        }

        private void SetLanguage()
        {
            /*
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate("Usuario.aspx", "tituloPrincipal");
            tituloSecundario.InnerHtml = MultiLanguage.GetTranslate("Usuario.aspx", "tituloSecundario");
            */
            
            btnCancelar.InnerHtml = MultiLanguage.GetTranslate("Usuario.aspx", "btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate("Usuario.aspx", "btnGuardar");
            lblEmail.InnerHtml = MultiLanguage.GetTranslate("Usuario.aspx", "lblEmail");
            lblNombre.InnerHtml = MultiLanguage.GetTranslate("Usuario.aspx", "lblNombre");
            lblApellido.InnerHtml = MultiLanguage.GetTranslate("Usuario.aspx", "lblApellido");
            lblIdioma.InnerHtml = MultiLanguage.GetTranslate("Usuario.aspx", "lblIdioma");
        }

        [WebMethod]
        public static void Insertar(Framework.Models.Usuario usuario)
        {
            var br = new Rules.Usuario();
            br.Insertar(usuario);
        }

        [WebMethod]
        public static void Modificar(Framework.Models.Usuario usuario)
        {
            try
            {
                var br = new Rules.Usuario();
                br.Modificar(usuario);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod]
        public static void Eliminar(Framework.Models.Usuario usuario)
        {
            var br = new Rules.Usuario();
            br.Eliminar(usuario.IdUsuario);
        }

        [WebMethod]
        public static Framework.Models.Usuario Obtener(int Id)
        {
            try
            {
                var br = new Rules.Usuario();
                return br.GetById(Id);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}