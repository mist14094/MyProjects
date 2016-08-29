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
    public partial class EditItemWithPrint : System.Web.UI.Page
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
              //  Barcode=29285&SKUNumber=910002645&Desc=FRAM Ultra Synthetic Oil Filter, XG7317&Price=$2.99&MSRP=910002645&FileNumber=82
                try { lblUniqueNumber.Text = Request.QueryString["Barcode"]; }
                catch (Exception ex) { }
                try {  txtUPC.Text  = Request.QueryString["SKUNumber"]; }catch(Exception ex) { }
                try { txtDesc.Text = Request.QueryString["Desc"]; }
                catch (Exception ex) { }
                try { txtSJRetail.Text = Request.QueryString["Price"]; }
                catch (Exception ex) { }
                try {  txtMSRP.Text  = Request.QueryString["MSRP"]; }
                catch (Exception ex) { }
                try { ddlFileSelect.SelectedValue = Request.QueryString["FileNumber"]; }
                catch (Exception ex) { }
                txtNoOfUnits.Text = "1";

            //    HyperLink hlEdit = new HyperLink();
               

            }
        }

    
        protected void btnAdd_Click(object sender, EventArgs e)
        {


            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            var result = _layer.EditItemsWithReceiving(txtUPC.Text, txtDesc.Text, double.Parse(txtMSRP.Text).ToString("C", nfi).ToString(), double.Parse(txtSJRetail.Text).ToString("C", nfi).ToString(),
              ddlFileSelect.SelectedValue, int.Parse(txtNoOfUnits.Text), int.Parse(lblUniqueNumber.Text));
            if (result != null)
            {
                if (result.Rows.Count > 0)
                {

                    for (int i = 1; i <= int.Parse(txtNoOfUnits.Text); i++)
                    {
                        PrintTags(result.Rows[0][0].ToString(), txtUPC.Text, txtDesc.Text,
                            "SJ PRICE : $" + txtSJRetail.Text,
                            "MSRP : $" + txtMSRP.Text, DateTime.Now.ToString("yyyyMMd"), 1,
                            " ", "$" + txtMSRP.Text, "$" + txtSJRetail.Text, "Received",i);

                    }
                    lblWarning.Text = "Update Successful !!!";
                    txtDesc.Text = "";
                    txtMSRP.Text = "";
                    txtSJRetail.Text = "";
                    txtUPC.Text = "";
                    txtNoOfUnits.Text = "1";

                }
            }
            else
            {
                lblWarning.Text = "Error in the selection or text";
            }



        }

        public void PrintTags(string barcode, string skulotNumber, string stockDesc, string price, string otherprice, string stockCode, int NoOfCopies, string TestTag, string MsrPrice, string SjPrice, string stickerType,int hwa)
        {

            if (!stickerType.Contains("-"))
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa"+hwa.ToString()
                    ,
                    "confirmDelete('" + HttpContext.Current.Request.Url.Scheme + ":\\\\\\\\" +
                    HttpContext.Current.Request.Url.Authority + "\\\\" + "OverStock_1.label" + "','" + barcode + "','" +
                    skulotNumber + "','" + stockDesc + "','" + price + "','" +
                    otherprice + "','" + stockCode + "','" + NoOfCopies + "','" + TestTag + "');", true);
            }

            _layer.TagLogger(skulotNumber, stockDesc, otherprice, price, barcode, stockCode, MsrPrice, SjPrice, stickerType);
            //   _dymoAddin.Open(str);


            //if (_dymoAddin.Open(str))
            //{
            //    // this call returns a list of objects on the label
            //    string[] objNames = _dymoLabel.GetObjectNames(false).Split(new Char[] { '|' });
            //    // verify that our text object is on the lab
            //    if (objNames.Contains("BARCODE"))
            //    {
            //        _dymoLabel.SetField("BARCODE", barcode);
            //        _dymoLabel.SetField("UPC", skulotNumber);
            //        _dymoLabel.SetField("Desc", stockDesc);
            //        _dymoLabel.SetField("Price", price);
            //        _dymoLabel.SetField("OrgPrice", otherprice);
            //        _dymoLabel.SetField("ShortLot", stockCode);
            //        _dymoLabel.SetField("TestTag", TestTag);
            //        // take the to-do's list entered by the user
            //        // and put in on the label

            //        // let's print to the first LabelWriter available
            //        // on the pc, if the printer is a TwiTurbo, print
            //        // to the left tray
            //        string[] printers = _dymoAddin.GetDymoPrinters().Split(new char[] { '|' });
            //        if (printers.Count() > 0)
            //        {
            //            if (_dymoAddin.SelectPrinter(printers[0]))
            //            {
            //                if (_dymoAddin.IsTwinTurboPrinter(printers[0]))
            //                {
            //                    _dymoAddin.Print2(NoOfCopies, false, 0);
            //                }
            //                else
            //                {
            //                    _dymoAddin.Print(NoOfCopies, false);
            //                }
            //            }
            //        }
            //    }
            //}

        }

      
     
    }
}