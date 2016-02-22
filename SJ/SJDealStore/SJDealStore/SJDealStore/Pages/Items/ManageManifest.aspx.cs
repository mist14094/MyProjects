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
    public partial class ManageManifest : System.Web.UI.Page
    {
        DALayer _layer= new DALayer();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpManifestList.DataSource = _layer.GetAllManifest();

                drpManifestList.DataTextField = "Name";
                drpManifestList.DataValueField = "Sno";
                drpManifestList.DataBind();
               var qr= Request.QueryString["ManifestID"];
                if (qr != null)
                {
                    drpManifestList.SelectedValue = qr;
                }
                InitiateValues();
            }

        }

        private void InitiateValues()
        {
            try
            {
                RefreshGrid();

                var dtManifestMaster = _layer.GetManifestMaster(drpManifestList.SelectedValue);
                txtName.Text = dtManifestMaster.Rows[0]["Name"].ToString();
                txtDesc.Text = dtManifestMaster.Rows[0]["Description"].ToString();

            }
            catch (Exception)
            {
                
                
            }
        
        }

        private void RefreshGrid()
        {
            grdManifest.DataSource = _layer.GetManifestDetail(drpManifestList.SelectedValue);
            grdManifest.DataBind();
        }



        protected void btnCreateManifest_Click(object sender, EventArgs e)
        {
         //   var dt = _layer.SJDeals_CreateManifest(txtName.Text, txtDesc.Text);

        }

        protected void drpManifestList_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitiateValues();
        }

        protected void txtScanItems_TextChanged(object sender, EventArgs e)
        {
            InsertRecord(txtScanItems.Text, drpManifestList.SelectedValue);
        }

        private void InsertRecord(string Barcode, string ManifestID)
        {
            lblWarning.Text =Barcode + " - "+ _layer.InsertItemInManifest(txtScanItems.Text, drpManifestList.SelectedValue).Rows[0][0].ToString();
            txtScanItems.Text = "";
            txtScanItems.Focus();
            RefreshGrid();
        }

        protected void grdManifest_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "-")
            {
                // Retrieve the row index stored in the 
                // CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = grdManifest.Rows[index];

                HiddenField hdnststatus = (HiddenField)row.FindControl("hdnStatus");

                _layer.DecManifestQty(hdnststatus.Value);
            }

            if (e.CommandName == "+")
            {
                // Retrieve the row index stored in the 
                // CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = grdManifest.Rows[index];
                HiddenField hdnststatus = (HiddenField)row.FindControl("hdnStatus");
                _layer.IncManifestQty(hdnststatus.Value);
            }
            
            
            if (e.CommandName == "Del")
            {
                // Retrieve the row index stored in the 
                // CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = grdManifest.Rows[index];
                HiddenField hdnststatus = (HiddenField)row.FindControl("hdnStatus");
                _layer.DeleteManifestItem(hdnststatus.Value);
            }

            RefreshGrid();
        }
    }
}