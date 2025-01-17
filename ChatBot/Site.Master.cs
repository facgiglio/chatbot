﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
                Framework.Session.LogOut();
            }

            MenuSecurity();

            if (!Page.IsPostBack)
            {
                var html = "";
                var flag = "";
                var li = "<li class=\"nav-item text-nowrap\">#CONTENT#</li>";

                if (Framework.Session.User != null)
                {
                    switch (Framework.Session.User.IdIdioma)
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

                    html += li.Replace("#CONTENT#", "<a class=\"nav-link\" href=\"" + Page.ResolveClientUrl("~/Content/Img/") + "\"><i class=\"fas fa-user\"></i>&nbsp;&nbsp;" + Framework.Session.User.Nombre + ", " + Framework.Session.User.Apellido + "</a>");

                    var idiomas = (new Rules.Idioma()).GetEntityList();
                    var menuIdioma = "<li class=\"dropdow\">";

                    menuIdioma += "<a class=\"dropdown-toggle\" data-toggle=\"dropdown\" role=\"button\" aria-haspopup=\"true\" aria-expanded=\"false\"><img src=\"" + Page.ResolveClientUrl("~/Content/Img/") + "" + flag + ".png\" /><span class=\"caret\"></span></a>";
                    menuIdioma += "<ul class=\"dropdown-menu\">";

                    //Recorro los idiomas.
                    foreach (var idioma in idiomas)
                    {
                        if (idioma.IdIdioma != Framework.Session.User.IdIdioma)
                            menuIdioma += "<li onclick=\"genericAction('@Idioma', {'IdIdioma':" + idioma.IdIdioma.ToString() + "},'" + Page.ResolveClientUrl("~/Default.aspx/Cambiaridioma") + "')\"><a><img src=\"" + Page.ResolveClientUrl("~/Content/Img/") + idioma.Iso + ".png\" />&nbsp;&nbsp;" + idioma.Descripcion + "</a></li>";
                    }

                    menuIdioma += "</ul></li>";

                    html += menuIdioma.ToString();
                    html += li.Replace("#CONTENT#", "<a class=\"nav-link\" href=\"" + Page.ResolveClientUrl("~/Default.aspx?logout=true") + "\">Sign out</a>");

                    navUser.InnerHtml = html;
                    navLogin.Style.Add("display", "none");
                }
            }
        }

        protected class Menu
        {
            public HtmlGenericControl parentMenu { get; set; }
            public List<HtmlGenericControl> childMenus { get; set; }
        }

        protected void MenuSecurity()
        {
            var menus = new List<Menu> {
                new Menu {
                    parentMenu = mnuSeguridad,
                    childMenus = new List<HtmlGenericControl> {
                        mnuUsuario,
                        mnuRol
                    }
                },
                new Menu {
                    parentMenu = mnuAdministracion,
                    childMenus = new List<HtmlGenericControl>{
                        mnuFrase,
                        mnuPalabra,
                        mnuCliente,
                        mnuChatbot
                    }
                },
                new Menu {
                    parentMenu = mnuSistema,
                    childMenus = new List<HtmlGenericControl>{
                        mnuMultiidioma,
                        mnuBackup,
                        mnuBitacora
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