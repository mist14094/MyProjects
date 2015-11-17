using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SimplifiedPOBusiness;
using Telerik.Web.UI;

namespace SimplifiedPOWeb
{
    public partial class POLineItems : System.Web.UI.Page
    {
        public static string StrSno;
        SPOBL _spobl = new SPOBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            //    HtmlControl body = (HtmlControl)Master.FindControl("Body"); body.Attributes.Add("onload", "AddRequestHandler"); 
                if (Request.QueryString["Sno"] == null)
                {
                  
                    hpLink.Visible = false;

                }
                else
                {
                    StrSno = Request.QueryString["Sno"].ToString();
                    hpLink.Visible = true;
                    hpLink.NavigateUrl = "SearchItems.aspx?Sno=" + StrSno;
                }
            }


        }
        protected void RadGrid1_ItemUpdated(object source, Telerik.Web.UI.GridUpdatedEventArgs e)
        {
            if (e.Exception != null)
            {

                e.KeepInEditMode = true;
                e.ExceptionHandled = true;
                DisplayMessage(true, "Product " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Stock Code"] + " cannot be updated. Reason: " + e.Exception.Message);
            }
            else
            {
                DisplayMessage(false, "Product " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Stock Code"] + " updated");
            }
        }

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                e.KeepInInsertMode = false;
                DisplayMessage(true, "Product cannot be inserted. Reason: " + e.Exception.Message);
            }
            else
            {
                DisplayMessage(false, "Product inserted");
            }
        }

        protected void RadGrid1_ItemDeleted(object source, GridDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                DisplayMessage(true, "Product " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Stock Code"] + " cannot be deleted. Reason: " + e.Exception.Message);
            }
            else
            {
                DisplayMessage(false, "Product " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Stock Code"] + " deleted");
            }
        }

        private void DisplayMessage(bool isError, string text)
        {
            Label label = (isError) ? this.Label1 : this.Label2;
            label.Text = text;
        }
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == RadGrid.InitInsertCommandName) //"Add new" button clicked
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                editColumn.Visible = false
                    ;
            }
            else if (e.CommandName == RadGrid.RebindGridCommandName && e.Item.OwnerTableView.IsItemInserted)
            {
                e.Canceled = true;
            }
            else
            {
                GridEditCommandColumn editColumn = (GridEditCommandColumn)RadGrid1.MasterTableView.GetColumn("EditCommandColumn");
                if (!editColumn.Visible)
                    editColumn.Visible = true;
            }
        }
        protected void RadGrid1_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //   RadGrid1.EditIndexes.Add(0);
                RadGrid1.Rebind();
            }
        }
     

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridFooterItem)
            {
               GridFooterItem item = e.Item as GridFooterItem;
               if (item["TotalPrice"].Text != "&nbsp;")
               
               { 
               txtSubTotal.Text = item["TotalPrice"].Text;
                RadAjaxPanel1.RaisePostBackEvent(null);
                CalculateValues();
               }
               else
               {
                   txtSubTotal.Text = "0";
                   RadAjaxPanel1.RaisePostBackEvent(null);
                   CalculateValues();
               }
                //  double value = Double.Parse(item["TotalPrice"].Text.Split(':')[1].Trim());
            }  
        }

        protected void txtShiping_TextChanged(object sender, EventArgs e)
        {
            CalculateValues();
        }

        private void CalculateValues()
        {
            float flSubTotal, flShipping, flDiscount;
            float.TryParse(txtSubTotal.Text, out flSubTotal);
            float.TryParse(txtShiping.Text, out flShipping);
            float.TryParse(txtDiscount.Text, out flDiscount);
            txtTotal.Text = (flSubTotal + flShipping - flDiscount).ToString();
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateValues();
        }

        protected void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            CalculateValues();
            RadAjaxPanel1.FocusControl(txtShiping);
        }

        protected void txtTotal_TextChanged(object sender, EventArgs e)
        {
            CalculateValues();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            var result = _spobl.UpdateAttributesPo(Request.QueryString["Sno"].ToString(), float.Parse(txtTotal.Text),
                float.Parse(txtSubTotal.Text), float.Parse(txtShiping.Text),
                float.Parse(txtDiscount.Text), float.Parse(txtTotal.Text), chkCheckRequired.Checked, chkRFIDTags.Checked,
                rdblPurchaseype.SelectedValue, txtPReason.Text);
            Response.Redirect("PreviewPOForSubmission.aspx?sno="+Request.QueryString["Sno"].ToString());
        }
    }
}