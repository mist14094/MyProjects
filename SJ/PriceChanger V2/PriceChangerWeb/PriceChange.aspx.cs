using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using BusinessLogic;

namespace PriceChangerWeb
{
    public partial class PriceChange : System.Web.UI.Page
    {
        private List<Product> products;
        BusinessLogic.BusinessLayer bl = new BusinessLayer();

        protected void Page_Init(object sender, EventArgs e)
        {
            string UPC = Request.QueryString["UPC"];
            string SKU = Request.QueryString["SKU"];
            //   UPC = "884794716404";
            //   SKU = "230000101204";
            C1ComboBox1.MinLength = 40;
            C1ComboBox1.DataSource = bl.GetAllCategory();
            C1ComboBox1.DataTextField = "Result";
            C1ComboBox1.DataValueField = "sno";
            C1ComboBox1.DataBind();
            if (UPC != null && SKU != null)
            {
                products = bl.GETUPCSKUDetails(UPC, SKU);
                if (products != null && products.Count > 0)
                {
                    C1ComboBox1.SelectedValue = bl.GetCategoryForUPCSKU(UPC, SKU);

                    lblUPC.Text = products[0].UPC;
                    lblSKU.Text = products[0].SKU;
                    txtGDesc.Text = products[0].Desc;
                    txtGCost.Text = products[0].Custom1;
                    txtGPrice.Text = products[0].Price.ToString();
                    foreach (var product in products)
                    {
                        
                        Panel storePanel = new Panel();
                        storePanel.Style.Add("margin","20px;");
                        storePanel.ID = "pnlStore" + product.StoreID;
                        storePanel.GroupingText = "Store " + product.StoreID.ToString(CultureInfo.InvariantCulture);


                        Label lblStorePrice = new Label();
                        lblStorePrice.ID = "lblStorePrice" + product.StoreID;
                        lblStorePrice.Text = "Price ";
                        lblStorePrice.Width = Unit.Percentage(10);
                        lblStorePrice.Style.Add("margin", "5px;");
                        storePanel.Controls.Add(lblStorePrice);

                        TextBox txtpriceBox = new TextBox();
                        txtpriceBox.ID = "txtPriceStore" + product.StoreID;
                        txtpriceBox.Text = product.Price.ToString();
                        txtpriceBox.Width = Unit.Percentage(50);
                        storePanel.Controls.Add(txtpriceBox);

                        storePanel.Controls.Add(new LiteralControl("<br/>"));
                        Label lblStoreCost = new Label();
                        lblStoreCost.ID = "lblStoreCost" + product.StoreID;
                        lblStoreCost.Text = "Cost : ";
                        lblStoreCost.Width = Unit.Percentage(10);
                        lblStoreCost.Style.Add("margin", "5px;");
                        storePanel.Controls.Add(lblStoreCost);

                        TextBox txt1 = new TextBox();
                        txt1.ID = "txtCostStore" + product.StoreID;
                        txt1.Text = product.Custom1.ToString(CultureInfo.InvariantCulture);
                        txt1.Width = Unit.Percentage(50);
                        storePanel.Controls.Add(txt1);
                        storePanel.Controls.Add(new LiteralControl("<br/>"));

                        Label lblStoreDesc = new Label();
                        lblStoreDesc.ID = "lblStoreDesc" + product.StoreID;
                        lblStoreDesc.Text = "Description : ";
                        lblStoreDesc.Width = Unit.Percentage(10);
                        lblStoreDesc.Style.Add("margin", "5px;");
                        storePanel.Controls.Add(lblStoreDesc);

                        TextBox txtDescBox = new TextBox();
                        txtDescBox.ID = "txtDescStore" + product.StoreID;
                        txtDescBox.Text = product.Desc.ToString(CultureInfo.InvariantCulture);
                        txtDescBox.Width = Unit.Percentage(50);
                        storePanel.Controls.Add(txtDescBox);

                        pnlAllStores.Controls.Add(storePanel);
                     
                    }

                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
              


            }
        }

        

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }
        
        
        protected void btnPriceChangeAll_Click(object sender, EventArgs e)
        {           
            UpdateTextBoxValue("txtPriceStore", txtGPrice.Text); 
        }

        protected void btnCostChangeAll_Click(object sender, EventArgs e)
        {
            UpdateTextBoxValue("txtCostStore", txtGCost.Text); 
        }

        protected void btnDescChangeAll_Click(object sender, EventArgs e)
        {
            UpdateTextBoxValue("txtDescStore", txtGDesc.Text);
        }

        private void UpdateTextBoxValue(string textboxLikeValue, string newValue)
        {
            var c = GetAll(this, typeof (TextBox));
            c = c.Where(control => control.ID.Contains(textboxLikeValue));
            foreach (var control in c)
            {
                TextBox TextBoxControl = (TextBox) control;
                TextBoxControl.Text = newValue;
            }
        }

        private string GetTextValue(string textboxLikeValue, string StoreID)
        {
            var c = GetAll(this, typeof(TextBox));
            c = c.Where(control => control.ID.Contains(textboxLikeValue+StoreID));
            foreach (var control in c)
            {
                TextBox TextBoxControl = (TextBox)control;
               return TextBoxControl.Text;
            }
            return "";
        }

        protected void btnChangeAll_Click(object sender, EventArgs e)
        {

            UpdateTextBoxValue("txtPriceStore", txtGPrice.Text); 
            UpdateTextBoxValue("txtCostStore", txtGCost.Text); 
            UpdateTextBoxValue("txtDescStore", txtGDesc.Text);
        }

        protected void btnSaveValue_Click(object sender, EventArgs e)
        {
            //var c = GetAll(this, typeof(Panel));
            //c = c.Where(control => control.ID.Contains("pnlStore"));
            //foreach (var control in c)
            //{
            //    Panel PanelControl = (Panel)control;
            //    string StoreID = PanelControl.GroupingText.Substring(6);

            //}
            if (products != null && products.Count > 0)
            {
                foreach (var product in products)
                {
                    var newprice =
                        bl.UpdatePrice(product.UPC, product.SKU, product.StoreID, product.Price,
                           Convert.ToDecimal(GetTextValue("txtPriceStore", product.StoreID.ToString(CultureInfo.InvariantCulture))),
                            product.Custom1, GetTextValue("txtCostStore", product.StoreID.ToString(CultureInfo.InvariantCulture)), product.Desc,
                            GetTextValue("txtDescStore", product.StoreID.ToString(CultureInfo.InvariantCulture)),1, C1ComboBox1.SelectedValue);
                }
lblMessage.Text = "Save successful!";

            }

            
        }

    }
}
