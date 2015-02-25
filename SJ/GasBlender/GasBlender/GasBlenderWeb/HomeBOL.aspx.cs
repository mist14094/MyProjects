using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GasBlenderWeb
{
    public partial class HomeBOL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                Label l1 = (Label)Master.FindControl("LV1");
                l1.Text = "BOL";
                Label l2 = (Label)Master.FindControl("LV2");
                l2.Text = "BOL";
                Label l3 = (Label)Master.FindControl("LV3");
                l3.Text = "Home";
                HtmlAnchor CurrentMenu;
                CurrentMenu = (HtmlAnchor)Master.FindControl("Tab3");
                CurrentMenu.Attributes.Add("class", "active");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoadBOLHome.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeleteBOLHome.aspx");
        }
    }
}