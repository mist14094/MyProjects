using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SJDealStore.Pages.Items
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Sales.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Returns.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchString.aspx");
        }
        
        
        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("SearchStringPrintJS.aspx");
        }
    }
}