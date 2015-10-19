using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SimplifiedPOBusiness;
using Telerik.Web.UI;

namespace SimplifiedPOWeb
{
    public partial class SearchItems : System.Web.UI.Page
    {
        public static string StrCompanyId;
        SPOBL _spobl = new SPOBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Sno"] == null)
                {
                   StrCompanyId = "H";
                    lblCompanyName.Text="on Company : " +StrCompanyId;

                    
                }
                else
                {
                    var poMaster = _spobl.GetTempPoDetails(Request.QueryString["Sno"].ToString());
                    if (poMaster.Rows.Count > 0)
                    {
                        StrCompanyId = poMaster.Rows[0]["SupplierEntity"].ToString();
                    }
                    lblCompanyName.Text = "on Company : " + StrCompanyId;
                }


            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            rgSearchItems.DataSource = _spobl.SearchProducts(txtSearch.Text, "H");
            rgSearchItems.DataBind();
        }

        protected void rgSearchItems_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            rgSearchItems.DataSource = _spobl.SearchProducts(txtSearch.Text, "H");
        }

        protected void btnAddItems_Click(object sender, EventArgs e)
        {
            foreach (GridDataItem item in rgSearchItems.SelectedItems)
            {
                
                _spobl.AddItemsTempPo(item["StockCode"].Text.Trim().ToString(), Request.QueryString["Sno"],
                    txtTotalQuantity.Text);
                // CustomerID is the uniquename of column 
                //    string custoId = (string) item.GetDataKeyValue("StockCode"); // Works if you set the DataKeyValue as CustomerID 
            }
            lblMessage.Text = rgSearchItems.SelectedItems.Count.ToString() + " Items added to PO Number : " +
                              Request.QueryString["Sno"].ToString();
            lblMessage.ForeColor = Color.ForestGreen;
            
        }

     
    }
}