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
    public partial class POLabels : System.Web.UI.Page
    {

   
        private readonly LotControlBusiness.Labels _lbl = new LotControlBusiness.Labels();

        private static string _poNumber = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["PONumber"] !=null)
                { 
                    _poNumber = Request.QueryString["PONumber"].ToString();
                    SearchPO();
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPO();
        }

        private void SearchPO()
        {
            btnPrint.Visible = false;
            if (_poNumber!= "")
            {
                var poItems = _lbl.GetLabelForPO(_poNumber);
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

            if ((string)DataBinder.Eval(e.Row.DataItem, "Notes") != "Normal")
                {
                   e.Row.BackColor = System.Drawing.Color.LightBlue;

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
                int numberoftags = 1;
                bool chkPrint = ((CheckBox) gvr.Cells[0].FindControl("chkRow")).Checked;
                int barcode = int.Parse(gvr.Cells[1].Text);
                string stockCode = gvr.Cells[3].Text;
                string description = gvr.Cells[4].Text;
                string quantity = ((Label)gvr.Cells[5].FindControl("Quantity")).Text + " " +
                                  ((Label) gvr.Cells[6].FindControl("UOM")).Text; //gvr.Cells[4].Text;

                if (gvr.BackColor == System.Drawing.Color.LightBlue)
                {
                    quantity = quantity + " - (P)";
                }
                string warehouse = gvr.Cells[14].Text;
                string lotnumber = gvr.Cells[7].Text;
                string grnNumber = gvr.Cells[8].Text;
                string supplier = gvr.Cells[9].Text;
                string poNumber = _poNumber;
                string masterLineId = "Line# : "+ gvr.Cells[1].Text;
                Printtags(numberoftags, chkPrint, barcode, stockCode, description, quantity, warehouse, lotnumber,
                    grnNumber, supplier, poNumber, i.ToString(), masterLineId);
                i++;
            }

        }

       
        private void Printtags(int numberoftags, bool chkPrint, int barcode,
            string stockCode, string description, string quantity, string warehouse,
            string lotnumber, string grnNumber, string supplier, string poNumber, string counts, string masterLineId
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
                    "','Supplier : " + supplier  +
                    "','LOT# " + lotnumber +
                    "','" + stockCode +
                    "','" + description +
                    "','" + poNumber +
                    "','" + grnNumber +
                     "','" + quantity +
                      "','" + masterLineId +
                    "','" + numberoftags +
                    "');",true);

                _lbl.PrintLog(numberoftags, chkPrint, barcode,
                stockCode, description, quantity, warehouse,
                lotnumber, grnNumber, supplier, poNumber, counts);
            }
        }


    }
}