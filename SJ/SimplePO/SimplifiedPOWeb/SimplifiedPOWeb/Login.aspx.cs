using System;
using SimplifiedPOBusiness;
using SimplifiedPOConstants;

namespace SimplifiedPOWeb
{
    
    
    public partial class Login : System.Web.UI.Page
    {
        SimplifiedPOBusiness.SPOBL  _spobl = new SPOBL();
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
            User user = new User();
            user = _spobl.Login(txtUserID.Text, txtPassword.Text);
            if (user!=null)
            {
                Session["Login"] = user;
                lblPassword.Text = "";
                Response.Redirect("Home.aspx");
            }
            else
            {
                lblPassword.Text = "Wrong Password, Please Try Again !!!";
            }
        }

       
    }
}