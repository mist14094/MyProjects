using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
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
    public partial class JobDetails : System.Web.UI.Page
    {

        private LotControlBusiness.JobDetails _jblDetails = new LotControlBusiness.JobDetails();
        private LotControlBusiness.JobMaster _jobMaster = new JobMaster();
        LcBusiness _business = new LcBusiness();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPo();
        }

        private void SearchPo()
        {
            if (txtJobNumber.Text != "")
            {
                var jobItemsItems = _jblDetails.GetJobDetails(txtJobNumber.Text);
                if (jobItemsItems.Count > 0)
                {
                    var jobMaster = _jobMaster.GetJobMasterDetails(txtJobNumber.Text);
                    lblJob.Text = jobMaster.Job;
                    lblJobDescr.Text = jobMaster.JobDescr;
                    lblStockCode.Text = jobMaster.StockCode;
                    // lblJobClassification.Text = jobMaster.JobClassification;
                    lblQtyToMake.Text = jobMaster.QtyToMake.ToString(CultureInfo.InvariantCulture);
                    lblJobStartDate.Text = jobMaster.JobStartDate.Date.ToShortDateString();
                    lblSUom.Text = jobMaster.Uom;
                    grdViewJob.DataSource = jobItemsItems;

                    grdViewJob.DataBind();
                    lblWarning.Text = jobItemsItems.Count.ToString() + " Items Found";
                }
              
              
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBoxHeader = (CheckBox)grdViewJob.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in grdViewJob.Rows)
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
            foreach (GridViewRow gvr in grdViewJob.Rows)
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
                string poNumber = txtJobNumber.Text;
             //   Printtags(numberoftags, chkPrint, barcode, stockCode, description, quantity, warehouse, lotnumber,
             //       grnNumber, supplier, poNumber,i.ToString());
             //   i++;
            }

        }

       


       
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
        }

        protected void grdViewJob_OnDataBound(object sender, EventArgs e)
        {
            for (int i = grdViewJob.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = grdViewJob.Rows[i];
                GridViewRow previousRow = grdViewJob.Rows[i - 1];


                for (int j = 0; j < row.Cells.Count; j++)
                {
                    var rowText = ((Label)row.FindControl(grdViewJob.HeaderRow.Cells[j].Text)).Text;

                         var previousRowText = ((Label)previousRow.FindControl(grdViewJob.HeaderRow.Cells[j].Text)).Text;
               
                    if (previousRowText == rowText)
                    {
                        if (previousRow.Cells[j].RowSpan == 0)
                        {
                            if (row.Cells[j].RowSpan == 0)
                            {
                                previousRow.Cells[j].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                            }
                            row.Cells[j].Visible = false;
                        }
                    }
                }
            }
        }
    }
}