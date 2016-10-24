using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SJFactoryBusiness;
namespace SJFactoryInventory
{
    public partial class Login : System.Web.UI.Page
    {
        SJFactoryBusiness.User User = new User();
        SJFactoryBusiness.Logr Logr = new Logr();
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session["User"];

            if(!IsPostBack)
            { if (user != null)
                {
                    Response.Redirect("Home.aspx");
                }}

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtPassWord.Text = "";
            txtUserName.Text = "";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Logr.InsertLog("User Log", this.GetType().Namespace + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name, Request.Url.AbsoluteUri, 1, System.Net.Dns.GetHostName() + System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"], Request.UserAgent, false, false);
            Session["User"] =   User.CheckUser(txtUserName.Text, txtPassWord.Text);
            Response.Redirect("Home.aspx");
        }
    }
}