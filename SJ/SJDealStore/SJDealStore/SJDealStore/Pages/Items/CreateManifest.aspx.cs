using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SJDealStore.Pages.Items
{
    public partial class CreateManifest : System.Web.UI.Page
    {
        DALayer _layer= new DALayer();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
            }

        }

        protected void btnCreateManifest_Click(object sender, EventArgs e)
        {
            var dt = _layer.SJDeals_CreateManifest(txtName.Text, txtDesc.Text);

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Response.Redirect("ManageManifest.aspx?ManifestID="+dt.Rows[0][0].ToString());
                }
            }
        }
    }
}