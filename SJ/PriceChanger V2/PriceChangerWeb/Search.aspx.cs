using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using  BusinessLogic;
namespace PriceChangerWeb
{
    public partial class Search : System.Web.UI.Page
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
            GridView1.DataSource = pr;
            GridView1.DataBind();
          
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["Username"] = null;
            Response.Redirect("Login.aspx");
        }
    }
}