using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdBsnsLayer;

namespace AdWeb
{
    public partial class SearchTag : System.Web.UI.Page
    {
        AdBsnsLayer.User User= new User();
        AdBsnsLayer.ActivitiesMenu Menu = new ActivitiesMenu();
        AdBsnsLayer.SmartWaiverIntegration  _integration = new SmartWaiverIntegration();
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = Session["User"];

            if (!IsPostBack)
            {
                RadGrid1.ClientSettings.Scrolling.AllowScroll = true;
                RadGrid1.ClientSettings.Scrolling.UseStaticHeaders = true;
                if (user == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }

            if (!IsPostBack)
            {
               
            }
        }

       

        protected void btnClear_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }

        protected void RadGrid1_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            GetData();
        }

        protected void RadGrid1_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            if (txtSearchString.Text.Length > 2)
            {


                    RadGrid1.DataSource = _integration.TagSearch(txtSearchString.Text);
                    RadGrid1.DataBind();


            }
        }

        protected void btnTop20Waiver_Click(object sender, EventArgs e)
        {
            RadGrid1.DataSource = _integration.Top20Tag();
            RadGrid1.DataBind();
        }
    }
}