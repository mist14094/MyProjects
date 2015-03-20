using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PriceChangerWeb
{
    public partial class Login1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                Response.Redirect("Search.aspx");
            }
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserID.Text == "admin" && txtPassword.Text == "Smokin2293" )
            {
                Session["Username"] = "admin";
                Response.Redirect("LandingMenu.aspx");
            }
            else
            {
                lblPassword.Text = "Wrong username/password";

            }
        }
    }
}