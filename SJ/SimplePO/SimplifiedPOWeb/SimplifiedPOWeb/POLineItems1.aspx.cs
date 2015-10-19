using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SimplifiedPOWeb
{
    public partial class PoLineItems1 : System.Web.UI.Page
    {
        public static string StrSno;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                DisplayMessage(true, "Stock Code " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["sno"] + " cannot be updated. Reason: " + e.Exception.Message);
            }
            else
            {
                DisplayMessage(false, "Stock Code " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["sno"] + " updated");
            }
        }

        protected void RadGrid1_ItemInserted(object source, GridInsertedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                e.KeepInInsertMode = false;
                DisplayMessage(true, "Stock Code cannot be inserted. Reason: " + e.Exception.Message);
            }
            else
            {
                DisplayMessage(false, "Stock Code inserted");
            }
        }

        protected void RadGrid1_ItemDeleted(object source, GridDeletedEventArgs e)
        {
            if (e.Exception != null)
            {
                e.ExceptionHandled = true;
                DisplayMessage(true, "Stock Code " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["sno"] + " cannot be deleted. Reason: " + e.Exception.Message);
            }
            else
            {
                DisplayMessage(false, "Stock Code " + e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["sno"] + " deleted");
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
    }
}