using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SJFactoryBusiness;
namespace SJFactoryInventory
{
    public partial class AddItemsPrint : System.Web.UI.Page
    {
        private SJFactoryBusiness.Product Product = new Product();
        private  SJFactoryBusiness.Location Location = new Location();
        private SJFactoryBusiness.ToteLabel ToteLabel = new ToteLabel();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

            {
                ddlLocation.DataSource = Location.GetAllLocations();
                ddlLocation.DataTextField = "Name";
                ddlLocation.DataValueField = "Sno";
                ddlLocation.DataBind();

                ddlTobaccoType.DataSource = Product.GetProductsList();
                ddlTobaccoType.DataTextField = "Description";
                ddlTobaccoType.DataValueField = "UPC";
                ddlTobaccoType.DataBind();
                lblMessage.Text = "";
                txtMfgDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtTotalWeight.Focus();

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            txtTotalWeight.Text = "";
            txtMoisture.Text = "";
            txtNewTote.Text = "";
            txtRFID.Text = "";
            lblMessage.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {


                var identity = ToteLabel.CreateNewToteLabel(ddlTobaccoType.SelectedValue, ddlTobaccoType.SelectedItem.Text, DateTime.Parse(txtMfgDate.Text), int.Parse(txtTotalWeight.Text),
                        float.Parse(txtMoisture.Text), (txtNewTote.Text), txtRFID.Text, int.Parse(ddlLocation.SelectedValue),false,1);
                if (identity != null)
                {
                    //PrintTags(ddlTobaccoType.SelectedValue, txtMfgDate.Text, int.Parse(txtTotalWeight.Text),
                    //    float.Parse(txtMoisture.Text), int.Parse(txtNewTote.Text), txtRFID.Text, identity.Rows[0][0].ToString());

                    //--->   
                    PrintTags(ddlTobaccoType.SelectedItem.Text, DateTime.Now.ToString("d"),
                        int.Parse(txtTotalWeight.Text).ToString() + " LBS",
                        txtMoisture.Text + "%", identity.ToString(),

                        //DateTime.Now.ToString("MMddyyyy ") +
                        txtNewTote.Text, txtRFID.Text);
                    lblMessage.Text = "Tag#"+ identity.ToString() + " Printed Successfully!";
                  
                    txtTotalWeight.Text = "";
                    txtMoisture.Text = "";
                    txtNewTote.Text = "";
                    txtRFID.Text = "";

                  //  txtMfgDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtTotalWeight.Focus();
                }
                else
                {
                    lblMessage.Text = "Something went wrong!";
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    lblMessage.Text = "Tag Already Exists";
                }
                else
                {
                    lblMessage.Text = "Error Occured";
                }
               

            }
        }

        private void PrintTags(string TobaccoType, string MfgDate, string Weight, string Moisture, string Barcode,string NewTote, string Rfid)
        {


            ClientScript.RegisterStartupScript(GetType(), "hwa"
                ,
                "confirmDelete('" + TobaccoType + "','" + MfgDate + "','" +
                Weight + "','" + Moisture + "','" + Barcode + "','" + NewTote + "','" + Rfid + "');", true);


        }
    }
}