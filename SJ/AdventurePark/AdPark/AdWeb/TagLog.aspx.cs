using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdBsnsLayer;

namespace AdWeb
{
    public partial class TagLog : System.Web.UI.Page
    {
        AdBsnsLayer.User User = new User();
        AdBsnsLayer.Log Detail = new Log();
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
            // var asd = menu.GetAllTagActivities();

        }

     
        protected void btnScan_Click(object sender, EventArgs e)
        {
            CheckTags(txtTagNumber.Text);
        }
        protected void txtTagNumber_TextChanged(object sender, EventArgs e)
        {
            CheckTags(txtTagNumber.Text);
        }

        public void CheckTags(string TagNumber)
        {
            GridView1.DataSource = Detail.GetLogsforTag(TagNumber);
            GridView1.DataBind();
        }

      
    }
}