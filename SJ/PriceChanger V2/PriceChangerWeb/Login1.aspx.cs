using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using  BusinessLogic;
namespace PriceChangerWeb
{
    public partial class Login : System.Web.UI.Page
    {
        BusinessLogic.BusinessLayer bl = new BusinessLayer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlSearchCriteria.DataSource = bl.GetTableSearchCriteria();
                ddlSearchCriteria.DataValueField = "ColumnName";
                ddlSearchCriteria.DataTextField = "AliasColumnName";
                ddlSearchCriteria.DataBind();
                ddlSearchCriteria.Items.Insert(0, new ListItem("All", ""));

            }
        
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var pr = new List<Product>();
            if (ddlSearchCriteria.SelectedValue != "")
            {
                pr = bl.GetProductSearchResult(ddlSearchCriteria.SelectedValue, srchTextBox.Text);
            }
            Label1.Text = "Total Records found : " + pr.Count ;

            foreach (var  i in  pr.AsEnumerable().Select(product => new List<int> {product.StoreID})
                .Distinct().ToList())
            {
                if (i != null)
                {
                    string StoreID = i[0].ToString();
                      GridView gvResultView = new GridView();
                         gvResultView.AutoGenerateColumns = false;
                    
                    BoundField bndUPC = new BoundField();
                    bndUPC.DataField = "UPC";
                    bndUPC.HeaderText = "UPC";
                    gvResultView.Columns.Add(bndUPC);

                    BoundField bndSKU = new BoundField();
                    bndSKU.DataField = "SKU";
                    bndSKU.HeaderText = "SKU";
                    gvResultView.Columns.Add(bndSKU);

                    BoundField bndDesc = new BoundField();
                    bndDesc.DataField = "Desc";
                    bndDesc.HeaderText = "Description";
                    gvResultView.Columns.Add(bndDesc);

                    BoundField bndCost = new BoundField();
                    bndCost.DataField = "Custom1";
                    bndCost.HeaderText = "Cost";
                    gvResultView.Columns.Add(bndCost);

                    BoundField bndPrice = new BoundField();
                    bndPrice.DataField = "Price";
                    bndPrice.HeaderText = "Price";
                    gvResultView.Columns.Add(bndPrice);

                    gvResultView.AllowPaging = true;
                    gvResultView.PageSize = 10;
                      gvResultView.DataSource = pr.AsEnumerable().Where(product => product.StoreID.ToString() == StoreID);
                       gvResultView.DataBind();
                       pnlGridView.Controls.Add(new LiteralControl("<br />"));
                    pnlGridView.Controls.Add(gvResultView);

                }
            }
          
        }
    }
}