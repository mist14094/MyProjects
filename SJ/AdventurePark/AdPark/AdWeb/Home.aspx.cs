using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdWeb
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var sesn = Session["User"];

        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddUser.aspx");
        }

        protected void btTagUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("AssociateActv.aspx");
        }

        protected void btnTagLog_Click(object sender, EventArgs e)
        {
            Response.Redirect("TagLog.aspx");
        }
    }
}