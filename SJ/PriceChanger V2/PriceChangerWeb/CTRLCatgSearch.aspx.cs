using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using  BusinessLogic;
using C1.Web.Wijmo.Controls.C1GridView;

namespace PriceChangerWeb
{
    public partial class CTRLCatgSearch : System.Web.UI.Page
    {
        BusinessLogic.BusinessLayer bl = new BusinessLayer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
          //      Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                    ddlSearchCriteria.DataSource = bl.GetTableSearchCriteria();
                    ddlSearchCriteria.DataValueField = "ColumnName";
                    ddlSearchCriteria.DataTextField = "AliasColumnName";
                    ddlSearchCriteria.DataBind();
                lblWelcome.Text = "Welcome " + Session["Username"] + "!";
                //   ddlSearchCriteria.Items.Insert(0, new ListItem("All", ""));

            }
        
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var pr = new List<ProductSimple>();
            if (ddlSearchCriteria.SelectedValue != "" &&  srchTextBox.Text!="")
            {
                pr = bl.GetSimpleProductDetails(ddlSearchCriteria.SelectedValue, srchTextBox.Text);
            }
            Label1.Text = "Total Records found : " + pr.Count ;
        //    GridView1.DataSource = pr;
        //    GridView1.DataBind();

            C1GridView1.DataSource = pr;
            C1GridView1.DataBind();

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Response.Redirect("Login.aspx");
        }
        private bool flag = false;

        protected void Filter(object sender, C1GridViewFilterEventArgs e)
        {
            flag = true;
        }

        protected void C1GridView1_DataBound(object sender, EventArgs e)
        {
            if (flag)
            {
                C1GridView1.SelectedIndex = 0;
            }
        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)C1GridView1.HeaderRow.FindControl("chkboxSelectAll");
            foreach (C1GridViewRow row in C1GridView1.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chk");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        protected void btnAddCatagory_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("UPC", typeof(string)),
                    new DataColumn("SKU", typeof(string)),
                    new DataColumn("CatagoryID",typeof(int)) });

            foreach (GridViewRow row in GridView1.Rows)
            {
                if ((row.FindControl("chkRow") as CheckBox).Checked)
                {
                    dt.Rows.Add(row.Cells[1].Text, row.Cells[2].Text, 1);
                }
            }

            if (dt.Rows.Count > 0)
            {
                bl.InsertCatagory(dt, 1, DateTime.Now);
            }
        }
    }
}