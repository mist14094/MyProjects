using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JarvisBusinessAccess;

public partial class MainChart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            JarvisBusinessAccess.DefaultBusinessAccess access= new DefaultBusinessAccess();
            ddViewsList.DataSource = access.GetChartList();
            ddViewsList.DataTextField = "ChartDesc";
            ddViewsList.DataValueField = "sno";
            ddViewsList.DataBind();
            ddViewsList.Items.Insert(0,"--Select Any Chart--");
        }

    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect("MainChartMainConfig.aspx");
    }
    protected void ddViewsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddViewsList.SelectedIndex > 0)
        {
            Response.Redirect("MainChartMainConfig.aspx?sno="+ddViewsList.SelectedValue);
        }
    }
}