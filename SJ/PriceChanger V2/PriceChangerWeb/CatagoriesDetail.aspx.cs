using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;

namespace PriceChangerWeb
{
    public partial class CatagoriesDetail : System.Web.UI.Page
    {
        BusinessLogic.BusinessLayer bl = new BusinessLayer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filldata();
            }
        }

        
        public void filldata()
        {
            string CatagID = Request.QueryString["CatagID"];
                string sku = Request.QueryString["SKU"];
                if (CatagID != null)
                {
                    lblSKU.Text = bl.CatagoryDetails(CatagID).Rows[0][0].ToString();
                    //lblUPC.Text = upc;
                    gvCatag.DataSource = bl.GetCatagIDUPC(CatagID);
                    gvCatag.DataBind();

                    if (gvCatag.Rows.Count > 0)
                    {
                        lblCatg.Text = gvCatag.Rows.Count.ToString(CultureInfo.InvariantCulture) + " Products Found";
                    }
                    else
                    {
                        lblCatg.Text = "No Products found";
                    }
                }
        }



    }

}