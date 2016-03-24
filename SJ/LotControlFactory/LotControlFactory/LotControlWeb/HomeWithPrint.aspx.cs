using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dymo;
using LotControlBusiness;

namespace LotControlWeb
{
    public partial class HomeWithPrint : System.Web.UI.Page
    {

        LotControlBusiness.LcBusiness Business = new LcBusiness();
        LotControlBusiness.LineItem Lbl = new LotControlBusiness.LineItem();
        private DymoAddInClass _dymoAddin = new DymoAddInClass();
        private DymoLabelsClass _dymoLabel = new DymoLabelsClass();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
             
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text != "")
            {
               var lblll=  Lbl.GetLablelsForPo(Business.ImportItemsinPO(TextBox1.Text));
                GridView1.DataSource = Business.ImportItemsinPO(TextBox1.Text);
                GridView1.DataBind();
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    TextBox totalTags=(row.Cells[0].FindControl("NoOfTags") as TextBox);
                    if (chkRow != null && chkRow.Checked)
                    {
                        if (totalTags != null)
                            PrintTags("Lot # "+row.Cells[8].Text.Trim(), row.Cells[2].Text.Trim(),row.Cells[3].Text.Trim(),
                                row.Cells[13].Text.Trim(),"PO # "+row.Cells[11].Text.Trim(),row.Cells[10].Text.Trim(),int.Parse(totalTags.Text));
                    }
                }
            }
        }

        public void PrintTags(string lotNumber, string stockCode, string stockDesc, string barcode, string poNumber, string grnNumber,int NoOfCopies)
        {
            const string str = @"C:\Users\vramalingam\Documents\DYMO Label\Labels\PO.label";
            //   _dymoAddin.Open(str);


            if (_dymoAddin.Open(str))
            {
                // this call returns a list of objects on the label
                string[] objNames = _dymoLabel.GetObjectNames(false).Split(new Char[] { '|' });
                // verify that our text object is on the label
                if (objNames.Contains("BARCODE"))
                {
                    _dymoLabel.SetField("LotNumber", lotNumber);
                    _dymoLabel.SetField("StockCode", stockCode);
                    _dymoLabel.SetField("StockDesc", stockDesc);
                    _dymoLabel.SetField("BARCODE", barcode);
                    _dymoLabel.SetField("PONumber", poNumber);
                    _dymoLabel.SetField("GRNNumber", grnNumber);
                    // take the to-do's list entered by the user
                    // and put in on the label
                   
                        // let's print to the first LabelWriter available
                        // on the pc, if the printer is a TwiTurbo, print
                        // to the left tray
                        string[] printers = _dymoAddin.GetDymoPrinters().Split(new char[] { '|' });
                        if (printers.Count() > 0)
                        {
                            if (_dymoAddin.SelectPrinter(printers[0]))
                            {
                                if (_dymoAddin.IsTwinTurboPrinter(printers[0]))
                                {
                                    _dymoAddin.Print2(NoOfCopies, false, 0);
                                }
                                else
                                {
                                    _dymoAddin.Print(NoOfCopies, false);
                                }
                            }
                        }
                    }
                }
            
        }
    }
}