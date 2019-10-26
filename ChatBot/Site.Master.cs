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

                if (Framework.Helpers.Session.User != null)
                {
                    switch (Framework.Helpers.Session.User.IdIdioma)
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

                    html += li.Replace("#CONTENT#", "<a class=\"nav-link\" href=\"" + Page.ResolveClientUrl("~/Content/Img/") + "\"><i class=\"fas fa-user\"></i>&nbsp;&nbsp;" + Framework.Helpers.Session.User.Nombre + ", " + Framework.Helpers.Session.User.Apellido + "</a>");
                    html += "<li><a class=\"nav-link\"><img src=\"" + Page.ResolveClientUrl("~/Content/Img/") + "" + flag + ".png\" /></a></li>";
                    html += li.Replace("#CONTENT#", "<a class=\"nav-link\" href=\"" + Page.ResolveClientUrl("~/Default.aspx?logout=true") + "\">Sign out</a>");

                    navUser.InnerHtml = html;
                    navLogin.Style.Add("display", "none");

                }
            }
        }


        protected class Menu
        {
            public System.Web.UI.HtmlControls.HtmlGenericControl parentMenu { get; set; }
            public List<System.Web.UI.HtmlControls.HtmlGenericControl> childMenus { get; set; }
        }

        protected void MenuSecurity()
        {
            var menus = new List<Menu> {
                new Menu {
                    parentMenu = mnuSeguridad,
                    childMenus = new List<System.Web.UI.HtmlControls.HtmlGenericControl> {
                        mnuUsuario,
                        mnuRol
                    }
                },
                new Menu {
                    parentMenu = mnuAdministracion,
                    childMenus = new List<System.Web.UI.HtmlControls.HtmlGenericControl>{
                        mnuFrase,
                        mnuPalabra,
                        mnuCliente
                    }
                },
                new Menu {
                    parentMenu = mnuSistema,
                    childMenus = new List<System.Web.UI.HtmlControls.HtmlGenericControl>{
                        mnuMultiidioma,
                        mnuBackup
                    }
                }
            };

            //Recorro el menu armado.
            foreach (var item in menus)
            {
                item.parentMenu.Visible = false;

                foreach (var menu in item.childMenus)
                {
                    if (Framework.Security.IsAuthorized(Convert.ToInt32(menu.Attributes["rol"])))
                    {
                        menu.Visible = true;
                        item.parentMenu.Visible = true;
                    }
                    else
                    {
                        menu.Visible = false;
                    }
                }
            }
        }
    }

    
}