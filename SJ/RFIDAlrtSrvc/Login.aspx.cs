using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;

public partial class Login : System.Web.UI.Page
{
    string strusername = WebConfigurationManager.AppSettings["username"];
    string strpassword = WebConfigurationManager.AppSettings["password"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["Login"] = null;
            lblPassword.Text = "";
        }

    }
 
    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        if (strusername == txtUserID.Text && strpassword == txtPassword.Text)
        {
            Session["Login"] = "Admin";
            lblPassword.Text = "";
            Response.Redirect("index.aspx");
        }
        else
        {
            lblPassword.Text = "Wrong Password, Please Try Again !!!";
        }
    }
}