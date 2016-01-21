using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SJDealStore.Pages.Items
{
    public partial class SearchString : System.Web.UI.Page
    {
        private DALayer _layer = new DALayer();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtString.Focus();
                ddlFileSelect.DataSource = _layer.SelectImportMaster();
                ddlFileSelect.DataTextField = "FileName";
                ddlFileSelect.DataValueField = "Sno";
                ddlFileSelect.DataBind();
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            SearchStringFunc();
        }

        public void SearchStringFunc()
        {
            string searchstring = txtString.Text;
            if (rblSelect.SelectedValue == "1")
            {
                grdResult.DataSource = _layer.SearchString(searchstring, int.Parse(ddlFileSelect.SelectedValue));
                grdResult.DataBind();
                lblWarning.Text = grdResult.Rows.Count.ToString() + " Rows Found";
            }
            else
            {
                searchstring = searchstring.Trim();
                searchstring = searchstring.TrimStart('0');
                if (searchstring.Count() > 5)
                {
                    searchstring = searchstring.Substring(2);
                    searchstring = searchstring.Substring(0, searchstring.Count() - 2);
                }
                grdResult.DataSource = _layer.SearchString(searchstring, int.Parse(ddlFileSelect.SelectedValue));
                grdResult.DataBind();
                lblWarning.Text = grdResult.Rows.Count.ToString() + " Rows Found";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchStringFunc();
        }

        protected void grdResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Received")
            {
                // Retrieve the row index stored in the 
                // CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = grdResult.Rows[index];
                _layer.IncrementReceived(row.Cells[34].Text);
                // Add code here to add the item to the shopping cart.
            }
            if (e.CommandName == "Damaged")
            {
                // Retrieve the row index stored in the 
                // CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = grdResult.Rows[index];
                _layer.IncrementDamaged(row.Cells[34].Text);

                // Add code here to add the item to the shopping cart.
            }
            SearchStringFunc();
           

        }
    }
}