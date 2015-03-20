using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PriceChangerWeb
{
    public partial class LandingMenu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnPriceChanger_Click(object sender, EventArgs e)
        {
            Response.Redirect("catgSearch.aspx");
        }

        protected void btnProductCatg_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewCatgrySearch.aspx");
        }
    }
}