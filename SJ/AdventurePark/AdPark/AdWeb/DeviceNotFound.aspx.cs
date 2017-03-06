using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdBsnsLayer;
namespace AdWeb
{
    public partial class DeviceNotFound : System.Web.UI.Page
    {
        AdBsnsLayer.User User = new User();
        TagDetail Tag = new TagDetail();
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session["User"];

            if (!IsPostBack)
            {
                if (user == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
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

        protected void btnDevices_Click(object sender, EventArgs e)
        {

        }

        protected void btnTubing_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("JustCount.aspx?DeviceID={0}",9));
        }
    }
}