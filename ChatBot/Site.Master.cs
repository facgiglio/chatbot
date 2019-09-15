using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChatBot
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    html += li.Replace("#CONTENT#", "<a class=\"nav-link\" href=\"" + Page.ResolveClientUrl("~/Home.aspx") + "\">Sign out</a>");

                    navUser.InnerHtml = html;
                    navLogin.Style.Add("display", "none");
                }
            }
        }
    }
}