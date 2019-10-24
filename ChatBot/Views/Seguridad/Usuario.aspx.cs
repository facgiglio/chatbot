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
        const string _seccion = "Usuario.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized((int)Constantes.Roles.Usuario))
                Response.Redirect(Page.ResolveClientUrl("~/LogIn.aspx"));
            
            grdUsuario.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdId"), ColumnType.Data, "IdUsuario", "", true, false);
            grdUsuario.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdNombre"), ColumnType.Data, "Nombre", "", false, true);
            grdUsuario.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdApellido"), ColumnType.Data, "Apellido", "", false, true);
            grdUsuario.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdEmail"), ColumnType.Data, "Email", "", false, true);
            grdUsuario.AddContextMenu("cmnuNuevo", MultiLanguage.GetTranslate("cmnuNuevo"), "@New", "glyphicon glyphicon-file", "#5cb85c", "exampleModal");
            grdUsuario.AddContextMenu("cmnuModificar", MultiLanguage.GetTranslate("cmnuModificar"), "@Upd", "glyphicon glyphicon-pencil", "#337AB7", "exampleModal");
            grdUsuario.AddContextMenu("cmnuEliminar", MultiLanguage.GetTranslate("cmnuEliminar"), "@Del", "glyphicon glyphicon-remove", "#d9534f", "exampleModal");
            grdUsuario.DataSource = new Rules.Usuario().GetList();

            grdRoles.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdId"), ColumnType.Data, "IdRol", "", true, false);
            grdRoles.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdId"), ColumnType.CheckBox, "Descripcion", "IdRol", false, true);
            grdRoles.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdNombre"), ColumnType.Data, "Descripcion", "", false, true);
            grdRoles.Config.Condense = true;
            grdRoles.DataSource = new Rules.Rol().GetList("");

            ddlIdioma.DataSource = new Rules.Idioma().GetList();
            ddlIdioma.DataTextField = "Descripcion";
            ddlIdioma.DataValueField = "IdIdioma";
            ddlIdioma.DataBind();

            ddlCliente.DataSource = new Rules.Cliente().ObtenerListado();
            ddlCliente.DataTextField = "RazonSocial";
            ddlCliente.DataValueField = "IdCliente";
            ddlCliente.DataBind();

            SetLanguage();
        }

        private void SetLanguage()
        {
            /*
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_page, "tituloPrincipal");
            tituloSecundario.InnerHtml = MultiLanguage.GetTranslate(_page, "tituloSecundario");
            */
            lblCliente.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblCliente");
            btnCancelar.InnerHtml = MultiLanguage.GetTranslate(_seccion, "btnCancelar");
            btnGuardar.InnerHtml = MultiLanguage.GetTranslate(_seccion, "btnGuardar");
            lblEmail.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblEmail");
            lblNombre.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblNombre");
            lblApellido.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblApellido");
            lblIdioma.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblIdioma");
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