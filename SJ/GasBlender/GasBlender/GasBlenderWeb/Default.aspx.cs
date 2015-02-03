using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GasBlenderWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                Label l1 = (Label)Master.FindControl("LV1");
                l1.Text = "Receive Truck";
                Label l2 = (Label)Master.FindControl("LV2");
                l2.Text = "Truck";
                Label l3 = (Label)Master.FindControl("LV3");
                l3.Text = "Receive Truck";
            }
        }
    }
}