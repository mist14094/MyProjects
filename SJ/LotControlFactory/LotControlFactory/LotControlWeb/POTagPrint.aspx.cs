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

        private LotControlBusiness.LineItem Lbl = new LotControlBusiness.LineItem();
        LcBusiness Business = new LcBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPO();
        }

        private void SearchPO()
        {
            btnFinalize.Visible = false;
            if (txtPONumber.Text != "")
            {
                var poItems = Lbl.GetLablelsForPo(Business.ImportItemsinPO(txtPONumber.Text));
                GridView1.DataSource = poItems;
                GridView1.DataBind();
                lblWarning.Text = poItems.Count.ToString() + " Items Found";
                if (poItems.Count > 0)
                {
                    btnFinalize.Visible = true;
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


                if ((bool)DataBinder.Eval(e.Row.DataItem, "OddNumberofTags") == true)
                {
                    e.Row.BackColor = System.Drawing.Color.LightBlue;

                }

                if ((bool) DataBinder.Eval(e.Row.DataItem, "isFinalized") == true)
                {
                    ((TextBox) e.Row.Cells[0].FindControl("AltUOM")).Enabled = false;
                    ((Button)e.Row.Cells[0].FindControl("UpdateTxt")).Enabled = false;
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
           // int i = 0;
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
             //   Printtags(numberoftags, chkPrint, barcode, stockCode, description, quantity, warehouse, lotnumber,
             //       grnNumber, supplier, poNumber,i.ToString());
             //   i++;
            }

        }

        protected void UpdateClick(object sender, System.EventArgs e)
        {
            //Get the button that raised the event
            Button btn = (Button)sender;

            //Get the row that contains this button
            GridViewRow gvr = (GridViewRow)btn.NamingContainer;
            Lbl.UpdateAltUOM(int.Parse(gvr.Cells[1].Text),float.Parse(((TextBox) gvr.Cells[0].FindControl("AltUOM")).Text));
            SearchPO();
        } 



        protected void btnFinalize_Click(object sender, EventArgs e)
        {
            var poItems = Lbl.GetLablelsForPo(Business.ImportItemsinPO(txtPONumber.Text));
            foreach ( LineItem item in poItems)
            {
                item.FinalizeLine();
                
            }
            Response.Redirect(string.Format("poLabels.aspx?PONumber={0}", txtPONumber.Text));
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }
    }
}