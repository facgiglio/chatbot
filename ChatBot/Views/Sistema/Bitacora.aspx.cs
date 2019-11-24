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
    public partial class Bitacora : Page
    {
        const string _seccion = "Bitacora";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Controlo si puede ingresar a la pantalla.
            if (!Security.IsAuthorized((int)Constantes.Roles.Bitacora))
                Response.Redirect(Page.ResolveClientUrl("~/Default.aspx"));

            var mensaje = "";
            var usuario = 0;

            if (IsPostBack)
            {
                mensaje = txtMensaje.Value;
                usuario = Convert.ToInt32(ddlUsuario.SelectedValue);
            }

            grdLog.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdId"), ColumnType.Data, "Id", "", true, false);
            grdLog.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdLevel"), ColumnType.Data, "Level", "", true, true);
            grdLog.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdMensaje"), ColumnType.Data, "Message", "", true, true);
            grdLog.AddColumn(MultiLanguage.GetTranslate(_seccion, "grdFecha"), ColumnType.Datetime, "Date", "", true, true, "col-xs-2");

            grdLog.DataSource = new Rules.Log().GetList(mensaje, usuario);

            var usuarios = new List<Framework.Models.Usuario>();
            usuarios.Add(new Framework.Models.Usuario { IdUsuario = 0, Email = "Seleccione" });
            usuarios.AddRange(new Rules.Usuario().GetEntityList().OrderBy(o => o.Email).ToList());

            ddlUsuario.DataSource = usuarios;
            ddlUsuario.SelectedValue = usuario.ToString();
            ddlUsuario.DataTextField = "Email";
            ddlUsuario.DataValueField = "IdUsuario";
            ddlUsuario.DataBind();

            SetLanguage();
        }

        private void SetLanguage()
        {
            tituloPrincipal.InnerHtml = MultiLanguage.GetTranslate(_seccion, "tituloPrincipal");
            lblMensaje.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblMensaje");
            lblUsuario.InnerHtml = MultiLanguage.GetTranslate(_seccion, "lblUsuario");

            lblNuevo.InnerHtml = MultiLanguage.GetTranslate("lblNuevo");
            lblFiltrar.InnerHtml = MultiLanguage.GetTranslate("lblFiltrar");
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