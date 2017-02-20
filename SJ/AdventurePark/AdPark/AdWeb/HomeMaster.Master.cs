using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AdBsnsLayer;

namespace AdWeb
{
    public partial class HomeMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginUser ssns = (LoginUser) Session["User"];
            if (ssns != null)
            {

                HtmlAnchor DisplayUserName = (HtmlAnchor) this.FindControl("DisplayUserName");
                DisplayUserName.InnerHtml =
                    "<span class=\"glyphicon glyphicon-user\"></span>" +" Welcome "+ ssns.Name;


                HtmlAnchor LogOutbtn = (HtmlAnchor) this.FindControl("LogOutbtn");
                LogOutbtn.InnerHtml =
                    "<span class=\"glyphicon glyphicon-log-in\"></span> Logout";
                LogOutbtn.HRef = "Logout.aspx";
            }
        }
    }
}