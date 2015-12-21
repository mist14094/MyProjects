using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SJDealStore
{
    public partial class Returns : System.Web.UI.Page
    {
        DALayer _layer = new DALayer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TextBox1.Focus();
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            var productDetails = _layer.GetProductDetails(TextBox1.Text);
            if (productDetails.Rows.Count > 0)
            {
                TextBox2.Focus();
                lblUPC.Text = productDetails.Rows[0]["UPC"].ToString();
                lblSKU.Text = productDetails.Rows[0]["SKU"].ToString();
                lblDesc.Text = productDetails.Rows[0]["Desc"].ToString();
                lblPrice.Text = productDetails.Rows[0]["Price"].ToString();
                lblWarning.Text = "";

            }
            else
            {
                TextBox1.Text = "";
                TextBox1.Focus();
                lblUPC.Text = "";
                lblSKU.Text = "";
                lblDesc.Text = "";
                lblPrice.Text = "";
                lblWarning.Text = "* UPC doesn't exist";
            }
         
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            string result = "";
            if (lblUPC.Text != "")
            {
                result = _layer.ReturnProduct(lblUPC.Text, lblSKU.Text, TextBox2.Text);
                if (result == "-1")
                {
                    lblWarning.Text = "* Item already Exist";
                }
                else
                {
                    lblWarning.Text = "* Item associated successfully";
                }
            }
            

            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox1.Focus();
            lblUPC.Text = "";
            lblSKU.Text = "";
            lblDesc.Text = "";
            lblPrice.Text = "";

          

        }
    }
}