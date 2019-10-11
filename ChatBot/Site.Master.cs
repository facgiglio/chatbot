using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework;

namespace ChatBot
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(Request.QueryString["logout"]))
            {
                Framework.Helpers.Session.LogOut();
            }

            MenuSecurity();

            if (!Page.IsPostBack)
            {
                var html = "";
                var flag = "";
                var li = "<li class=\"nav-item text-nowrap\">#CONTENT#</li>";

                if (Framework.Helpers.Session.SessionUser != null)
                {
                    switch (Framework.Helpers.Session.SessionUser.IdIdioma)
                    {
                        case 1:
                            flag = "es";
                            break;
                        case 2:
                            flag = "en";
                            break;
                        case 3:
                            flag = "pt";
                            break;
                    }

                    html += li.Replace("#CONTENT#", "<a class=\"nav-link\" href=\"" + Page.ResolveClientUrl("~/Content/Img/") + "\"><i class=\"fas fa-user\"></i>&nbsp;&nbsp;" + Framework.Helpers.Session.SessionUser.Nombre + ", " + Framework.Helpers.Session.SessionUser.Apellido + "</a>");
                    html += "<li><a class=\"nav-link\"><img src=\"" + Page.ResolveClientUrl("~/Content/Img/") + "" + flag + ".png\" /></a></li>";
                    html += li.Replace("#CONTENT#", "<a class=\"nav-link\" href=\"" + Page.ResolveClientUrl("~/Default.aspx?logout=true") + "\">Sign out</a>");

                    navUser.InnerHtml = html;
                    navLogin.Style.Add("display", "none");

                }
            }
        }

        protected void MenuSecurity()
        {
            var menuSeguridad = new List<System.Web.UI.HtmlControls.HtmlGenericControl> {
                mnuUsuario,
                mnuRol,
                mnuMultiidioma
            };
            var menuAdministracion = new List<System.Web.UI.HtmlControls.HtmlGenericControl>{
                mnuMultiidioma,
                mnuFrase,
                mnuPalabra,
                mnuCliente
            };

            mnuSeguridad.Visible = false;
            mnuAdministracion.Visible = false;

            foreach (var menu in menuSeguridad) {
                if (Framework.Security.IsAuthorized(Convert.ToInt32(menu.Attributes["rol"])))
                {
                    menu.Visible = true;
                    mnuSeguridad.Visible = true;
                }
                else
                {
                    menu.Visible = false;
                }
            }

            foreach (var menu in menuAdministracion)
            {
                if (Framework.Security.IsAuthorized(Convert.ToInt32(menu.Attributes["rol"])))
                {
                    menu.Visible = true;
                    mnuAdministracion.Visible = true;
                }
                else
                {
                    menu.Visible = false;
                }
            }
        }
    }
}