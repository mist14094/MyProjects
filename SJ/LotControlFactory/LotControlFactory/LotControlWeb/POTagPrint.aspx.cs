using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using LotControlBusiness;
using Label = System.Web.UI.WebControls.Label;

namespace LotControlWeb
{
    public partial class POTagPrint : System.Web.UI.Page
    {

        private LotControlBusiness.LcBusiness Business = new LcBusiness();
        private LotControlBusiness.Label Lbl = new LotControlBusiness.Label();


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            btnPrint.Visible = false;
            if (txtPONumber.Text != "")
            {
                var poItems = Lbl.GetLablelsForPo(Business.ImportItemsinPO(txtPONumber.Text));
                GridView1.DataSource = poItems;
                GridView1.DataBind();
                lblWarning.Text = poItems.Count.ToString() + " Items Found";
                if (poItems.Count > 0)
                {
                    btnPrint.Visible = true;
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem != null)
            {
                if (Convert.ToInt16(DataBinder.Eval(e.Row.DataItem, "TagsNeededForThisLine")) == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.PaleGoldenrod;

                }

                if ((string) DataBinder.Eval(e.Row.DataItem, "LotNumber") == "")
                {
                    e.Row.BackColor = System.Drawing.Color.Orange;

                }
            }
        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBoxHeader = (CheckBox) GridView1.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chkBoxRows = (CheckBox) row.FindControl("chkRow");
                if (chkBoxHeader.Checked == true)
                {
                    chkBoxRows.Checked = true;
                }
                else
                {
                    chkBoxRows.Checked = false;
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                int numberoftags = int.Parse(((TextBox) gvr.Cells[0].FindControl("NoOfTags")).Text);
                bool chkPrint = ((CheckBox) gvr.Cells[0].FindControl("chkRow")).Checked;
                int barcode = int.Parse(gvr.Cells[1].Text);
                string stockCode = gvr.Cells[2].Text;
                string description = gvr.Cells[3].Text;
                string quantity = ((Label)gvr.Cells[0].FindControl("lblAltUOM")).Text + " " +
                                  ((Label) gvr.Cells[0].FindControl("UOM")).Text; //gvr.Cells[4].Text;
                string warehouse = gvr.Cells[5].Text;
                string lotnumber = gvr.Cells[6].Text;
                string grnNumber = gvr.Cells[7].Text;
                string supplier = gvr.Cells[8].Text;
                string poNumber = txtPONumber.Text;
                Printtags(numberoftags, chkPrint, barcode, stockCode, description, quantity, warehouse, lotnumber,
                    grnNumber, supplier, poNumber,i.ToString());
                i++;
            }

        }

        private void Printtags(int numberoftags, bool chkPrint, int barcode,
            string stockCode, string description, string quantity, string warehouse,
            string lotnumber, string grnNumber, string supplier, string poNumber,string counts
            )
        {
            if (chkPrint)
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa" +counts,
                    "PrintTags('" +
                    
                  //  + HttpContext.Current.Request.Url.Scheme + ":\\\\\\\\" +
                  //  HttpContext.Current.Request.Url.Authority + "\\\\" + "OverStock_1.label" 


                    HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.LocalPath, WebConfigurationManager.AppSettings["LabelPath"]) + 
                    "','" +barcode +
                    "','Supplier : " + supplier + " | Total Quantity : " + quantity +
                    "','LOT# " + lotnumber +
                    "','" + stockCode +
                    "','" + description +
                    "','" + poNumber +
                    "','" + grnNumber +
                    "','" + numberoftags +
                    "');",true);

                Lbl.PrintLog(numberoftags, chkPrint, barcode,
                stockCode, description, quantity, warehouse,
                lotnumber, grnNumber, supplier, poNumber, counts);
            }
        }
    }
}