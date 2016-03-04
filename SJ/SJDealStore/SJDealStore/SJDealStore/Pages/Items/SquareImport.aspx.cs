using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SJDealStore.Pages.Items
{
    public partial class SquareImport : System.Web.UI.Page
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
                IntitilaizeValues();
            }
        }

        private void IntitilaizeValues()
        {
            
        }

      

        public void SearchStringFunc()
        {
            if (RadioButtonList1.SelectedValue == "Received")
            {
                grdResult.DataSource = _layer.SJDeals_GetReceivedReport(ddlFileSelect.SelectedValue);
            }
            else
            {
                grdResult.DataSource = _layer.SJDeals_GetDamagedReport(ddlFileSelect.SelectedValue);
            }
               grdResult.DataBind();
               lblWarning.Text = grdResult.Rows.Count.ToString() + " Rows Found";
            lblreport.Text = "Report Generated on " + DateTime.Now.ToString("D");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchStringFunc();
        }

        protected void grdResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
           

        }


     
        protected void ddlFileSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchStringFunc();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchStringFunc();
        }

        protected void btnExportToExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        protected void ExportToExcel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

               

                grdResult.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grdResult.HeaderRow.Cells)
                {
                    cell.BackColor = grdResult.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grdResult.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grdResult.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grdResult.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grdResult.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            grdResult.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
       //     sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataTable dt;
            if (RadioButtonList1.SelectedValue == "Received")
            {
                dt = _layer.SJDeals_GetReceivedReport(ddlFileSelect.SelectedValue);
            }
            else
            {
                dt = _layer.SJDeals_GetDamagedReport(ddlFileSelect.SelectedValue);
            }

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["ItemID"].ToString() == "")
                        {
                            try
                            {
                              var result = Api.InsertProduct(txtCategoryName.Text, dr["MasterID"].ToString(),
                                    WebUtility.HtmlDecode(dr["Descr"].ToString()), WebUtility.HtmlDecode(dr["UPCNumber"].ToString()),
                                     WebUtility.HtmlDecode(dr["SJPrice"].ToString()).Replace("$", "").Replace(".", ""), WebUtility.HtmlDecode(dr["Quantity"].ToString()));

                            }
                            catch (Exception ex)
                            {
                                
                            }
                           
                        }

                      
                    }
               //     Api.InsertProduct("Walmart", "10848", "Sample Name", "Sample Descri", "4999", "1");
                }
            }
        }
     
    }
}