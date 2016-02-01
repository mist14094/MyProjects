using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SJDealStore.Pages.Items
{
    public partial class SearchStringPrintJS : System.Web.UI.Page
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
            
                for (int i = 4; i <= 34; i++)
                {
                    ddlBarcode.Items.Add(i.ToString());
                    ddlOrginal.Items.Add(i.ToString());
                    ddlDesc.Items.Add(i.ToString());
                    ddlPrice.Items.Add(i.ToString());
                    OrgPrice.Items.Add(i.ToString());
                    
                }

                IntitilaizeValues();
                
             
            }
        }

        private void IntitilaizeValues()
        {
            var Settings = _layer.GetSettings(ddlFileSelect.SelectedValue);
            if (Settings.Rows.Count > 0)
            {
                chkPrintTest.Checked = bool.Parse(Settings.Rows[0]["TestTags"].ToString());
                txtPrefixPrice.Text = Settings.Rows[0]["PrefixPrice"].ToString();
                txtPrefixOrgnlPrice.Text = Settings.Rows[0]["PrefixOriginalPrice"].ToString();
                ddlBarcode.SelectedValue = Settings.Rows[0]["Barcode"].ToString();
                ddlOrginal.SelectedValue = Settings.Rows[0]["OriginalSKUNumber"].ToString();
                ddlDesc.SelectedValue = Settings.Rows[0]["Desc"].ToString();
                ddlPrice.Text = Settings.Rows[0]["Price"].ToString();
                OrgPrice.SelectedValue = Settings.Rows[0]["OrgPrice"].ToString();
                txtCode.Text = DateTime.Now.ToString("yyyyMMd");

            }
            else
            {
                ddlBarcode.SelectedValue = "34";
                txtCode.Text = DateTime.Now.ToString("yyyyMMd");
            }
            
            txtString.Focus();
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
            try
            {
                if (e.CommandName == "Received")
                {
                    // Retrieve the row index stored in the 
                    // CommandArgument property.
                    int index = Convert.ToInt32(e.CommandArgument);

                    // Retrieve the row that contains the button 
                    // from the Rows collection.
                    GridViewRow row = grdResult.Rows[index];
                    // _layer.IncrementReceived(row.Cells[34].Text);
                    if (chkPrintTest.Checked)
                    {
                        PrintTags(row.Cells[int.Parse(ddlBarcode.SelectedValue)].Text,
                        row.Cells[int.Parse(ddlOrginal.SelectedValue)].Text,
                        row.Cells[int.Parse(ddlDesc.SelectedValue)].Text,
                        txtPrefixPrice.Text + row.Cells[int.Parse(ddlPrice.SelectedValue)].Text,
                        txtPrefixOrgnlPrice.Text + row.Cells[int.Parse(OrgPrice.SelectedValue)].Text,
                        txtCode.Text, 1, "Test Tags", row.Cells[int.Parse(OrgPrice.SelectedValue)].Text, row.Cells[int.Parse(ddlPrice.SelectedValue)].Text, "Test");


                    }
                    else
                    {
                        if (chkNegRecvd.Checked)
                        {
                            _layer.DecrementReceived(row.Cells[34].Text);
                            PrintTags(row.Cells[int.Parse(ddlBarcode.SelectedValue)].Text,
                        row.Cells[int.Parse(ddlOrginal.SelectedValue)].Text,
                        row.Cells[int.Parse(ddlDesc.SelectedValue)].Text,
                        txtPrefixPrice.Text + row.Cells[int.Parse(ddlPrice.SelectedValue)].Text,
                        txtPrefixOrgnlPrice.Text + row.Cells[int.Parse(OrgPrice.SelectedValue)].Text,
                        txtCode.Text, 1, " ", row.Cells[int.Parse(OrgPrice.SelectedValue)].Text, row.Cells[int.Parse(ddlPrice.SelectedValue)].Text, "- Received");

                        }
                        else
                        {
                            _layer.IncrementReceived(row.Cells[34].Text);
                            PrintTags(row.Cells[int.Parse(ddlBarcode.SelectedValue)].Text,
                          row.Cells[int.Parse(ddlOrginal.SelectedValue)].Text,
                          row.Cells[int.Parse(ddlDesc.SelectedValue)].Text,
                          txtPrefixPrice.Text + row.Cells[int.Parse(ddlPrice.SelectedValue)].Text,
                          txtPrefixOrgnlPrice.Text + row.Cells[int.Parse(OrgPrice.SelectedValue)].Text,
                          txtCode.Text, 1, " ", row.Cells[int.Parse(OrgPrice.SelectedValue)].Text, row.Cells[int.Parse(ddlPrice.SelectedValue)].Text, "Received");
                        }



                    }
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

                    if (chkNegDmgd.Checked)
                    {
                        _layer.DecrementDamaged(row.Cells[34].Text);
                        PrintTags(row.Cells[int.Parse(ddlBarcode.SelectedValue)].Text,
                         row.Cells[int.Parse(ddlOrginal.SelectedValue)].Text,
                         row.Cells[int.Parse(ddlDesc.SelectedValue)].Text,
                         txtPrefixPrice.Text + row.Cells[int.Parse(ddlPrice.SelectedValue)].Text,
                         txtPrefixOrgnlPrice.Text + row.Cells[int.Parse(OrgPrice.SelectedValue)].Text,
                         txtCode.Text, 1, " ", row.Cells[int.Parse(OrgPrice.SelectedValue)].Text, row.Cells[int.Parse(ddlPrice.SelectedValue)].Text, "- Damaged");
                    }
                    else
                    {
                        _layer.IncrementDamaged(row.Cells[34].Text);
                        PrintTags(row.Cells[int.Parse(ddlBarcode.SelectedValue)].Text,
                       row.Cells[int.Parse(ddlOrginal.SelectedValue)].Text,
                       row.Cells[int.Parse(ddlDesc.SelectedValue)].Text,
                       " ",
                       " ",
                       txtCode.Text, 1, " ", row.Cells[int.Parse(OrgPrice.SelectedValue)].Text, row.Cells[int.Parse(ddlPrice.SelectedValue)].Text, "Damaged");
                    }


                    // Add code here to add the item to the shopping cart.
                }
                SearchStringFunc();

            
            }
            catch (Exception)
            {

                lblWarning.Text = "Search String First";
            }
            txtString.Focus();
            
        }


        public void PrintTags(string barcode, string skulotNumber, string stockDesc, string price, string otherprice,string stockCode,  int NoOfCopies,string TestTag,string MsrPrice, string SjPrice, string stickerType)
        {

            if (!stickerType.Contains("-"))
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa",
                    "confirmDelete('" + HttpContext.Current.Request.Url.Scheme + ":\\\\\\\\" +
                    HttpContext.Current.Request.Url.Authority + "\\\\" + "OverStock_1.label" + "','" + barcode + "','" +
                    skulotNumber + "','" + stockDesc + "','" + price + "','" +
                    otherprice + "','" + stockCode + "','" + NoOfCopies + "','" + TestTag + "');", true);
            }

            _layer.TagLogger(skulotNumber, stockDesc, otherprice, price, barcode, stockCode,MsrPrice,SjPrice,stickerType);
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

        protected void ddlFileSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntitilaizeValues();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _layer.SjDealsSaveSettings(int.Parse(ddlFileSelect.SelectedValue),  chkPrintTest.Checked,  txtPrefixPrice.Text,  txtPrefixOrgnlPrice.Text,
             ddlBarcode.SelectedValue, ddlOrginal.SelectedValue, ddlDesc.SelectedValue, ddlPrice.SelectedValue, OrgPrice.SelectedValue, txtCode.Text)
            ;
        }

        protected void grdResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
        }
    }
}