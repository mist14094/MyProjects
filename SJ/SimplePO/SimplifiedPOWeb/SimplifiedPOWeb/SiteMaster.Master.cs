using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SimplifiedPOConstants;

namespace SimplifiedPOWeb
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Page_Init(object sender, EventArgs e)
        {
             if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                User user = (User) Session["Login"];
                lblFirstName.Text = user.FirstName;
                lblLasttName.Text = user.LastName;
            }
        }


    }
}