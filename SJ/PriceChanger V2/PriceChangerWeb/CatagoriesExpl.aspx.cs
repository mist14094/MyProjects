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
    public partial class CatagoriesExpl : System.Web.UI.Page
    {
        BusinessLogic.BusinessLayer bl = new BusinessLayer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filldata();
            }
        }

        protected void Remove(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                bl.DeleteCatagory(Request.QueryString["UPC"], Request.QueryString["SKU"], ((System.Web.UI.WebControls.HiddenField)(row.FindControl("Hype"))).Value);
                filldata();
            }
           
        }

        public void filldata()
        {
            string upc = Request.QueryString["UPC"];
                string sku = Request.QueryString["SKU"];
                if (upc != null && sku != null)
                {
                    lblSKU.Text = sku;
                    lblUPC.Text = upc;
                    gvCatag.DataSource = bl.GetCatagoriesIDforUPCSKU(upc, sku);
                    gvCatag.DataBind();

                    if (gvCatag.Rows.Count > 0)
                    {
                        lblCatg.Text = gvCatag.Rows.Count.ToString(CultureInfo.InvariantCulture) + " Record Found";
                    }
                    else
                    {
                        lblCatg.Text = "No catagory found";
                    }
                }
        }



    }

}