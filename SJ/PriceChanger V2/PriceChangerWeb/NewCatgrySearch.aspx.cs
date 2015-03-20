using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using  BusinessLogic;
namespace PriceChangerWeb
{
    public partial class NewCatgrySearch : System.Web.UI.Page
    {
        BusinessLogic.BusinessLayer bl = new BusinessLayer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] == null)
            {
                    Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                    ddlSearchCriteria.DataSource = bl.GetTableSearchCriteria();
                    ddlSearchCriteria.DataValueField    = "ColumnName";
                    ddlSearchCriteria.DataTextField     = "AliasColumnName";
                    ddlSearchCriteria.DataBind();
                    lblWelcome.Text                     = "Welcome " + Session["Username"] + "!";
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
            lblProduct.Text =  pr.Count.ToString(CultureInfo.InvariantCulture) ;
            GridView1.DataSource = pr;
            GridView1.DataBind();
          
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Response.Redirect("Login.aspx");
        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)GridView1.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkRow");
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
            if (ddlCatagories.SelectedValue != "")
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3]
                {
                    new DataColumn("UPC", typeof (string)),
                    new DataColumn("SKU", typeof (string)),
                    new DataColumn("CatagoryID", typeof (int))
                });

                foreach (GridViewRow row in GridView1.Rows)
                {
                    if ((row.FindControl("chkRow") as CheckBox).Checked)
                    {
                        dt.Rows.Add(row.Cells[1].Text, row.Cells[2].Text, ddlCatagories.SelectedValue);
                    }
                }

                if (dt.Rows.Count > 0)
                {

                    bl.InsertCatagory(dt, 1, DateTime.Now);

                    var pr = new List<ProductSimple>();
                    if (ddlSearchCriteria.SelectedValue != "" && srchTextBox.Text != "")
                    {
                        pr = bl.GetSimpleProductDetails(ddlSearchCriteria.SelectedValue, srchTextBox.Text);
                    }
                    lblProduct.Text = pr.Count.ToString(CultureInfo.InvariantCulture);
                    GridView1.DataSource = pr;
                    GridView1.DataBind();

                    string cleanMessage = dt.Rows.Count.ToString() + " products catagorized sucessfully !!!".Replace("'", "\'");
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    string script = string.Format("alert('{0}');", cleanMessage);
                    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                    {
                        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);
                    } 
                }

                else
                {
                    string cleanMessage = "No products selected to catagorize !!!".Replace("'", "\'");
                    Page page = HttpContext.Current.CurrentHandler as Page;
                    string script = string.Format("alert('{0}');", cleanMessage);
                    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                    {
                        page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);
                    } 
                    
                }
            }
            else
            {
                string cleanMessage = "No catagories selected !!!".Replace("'", "\'");
                Page page = HttpContext.Current.CurrentHandler as Page;
                string script = string.Format("alert('{0}');", cleanMessage);
                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
                {
                    page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true /* addScriptTags */);
                } 
            }
        }

     

        protected void imbSearchCatg_Click(object sender, ImageClickEventArgs e)
        {
           DataTable dt =  bl.SearchCategory(txtCatagories.Text);
            lblCatg.Text = dt.Rows.Count.ToString();
            ddlCatagories.DataSource = dt;
            ddlCatagories.DataTextField = "Result";
            ddlCatagories.DataValueField = "Sno";
            ddlCatagories.DataBind();
            ddlCatagories.Items.Insert(0,"");

        }



      
    }
}