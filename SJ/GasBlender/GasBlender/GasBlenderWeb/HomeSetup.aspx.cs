using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace GasBlenderWeb
{
    public partial class HomeSetup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                Label l1 = (Label)Master.FindControl("LV1");
                l1.Text = "Setup";
                Label l2 = (Label)Master.FindControl("LV2");
                l2.Text = "Setup";
                Label l3 = (Label)Master.FindControl("LV3");
                l3.Text = "Home";
                HtmlAnchor CurrentMenu;
                CurrentMenu = (HtmlAnchor)Master.FindControl("Tab4");
                CurrentMenu.Attributes.Add("class", "active");
            }
        }

        protected void btnTrailer_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetupTrailer.aspx");
        }

        protected void btnLocation_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetupLocation.aspx");
        }

        protected void btnTractor_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetupTractor.aspx");
        }

        protected void btnDriver_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetupDriver.aspx");
        }

        protected void btnCarrier_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetupCarrier.aspx");
        }
    }
}