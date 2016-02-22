using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SJDealStore.Pages.Items
{
    public partial class AddNewItem : System.Web.UI.Page
    {

        private DALayer _layer = new DALayer();
        SquareSjApi Api = new SquareSjApi();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlFileSelect.DataSource = _layer.SelectImportMaster();
                ddlFileSelect.DataTextField = "FileName";
                ddlFileSelect.DataValueField = "Sno";
                ddlFileSelect.DataBind();
            }
        }

    
        protected void btnAdd_Click(object sender, EventArgs e)
        {


            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            var result = _layer.AddNewItems(txtUPC.Text, txtDesc.Text, double.Parse(txtMSRP.Text).ToString("C", nfi).ToString(), double.Parse(txtSJRetail.Text).ToString("C", nfi).ToString(),
                ddlFileSelect.SelectedValue);
            if (result != null)
            {
                if (result.Rows.Count > 0)
                {
                    lblWarning.Text = "Added Successful !!!";
                    txtDesc.Text = "";
                    txtMSRP.Text = "";
                    txtSJRetail.Text = "";
                    txtUPC.Text = "";
                }
            }
            else
            {
                lblWarning.Text = "Error in the selection or text";
            }



        }

      
     
    }
}