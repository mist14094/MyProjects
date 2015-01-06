using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JarvisBusinessAccess;

public partial class LineChartAxisProperties : System.Web.UI.Page
{
    JarvisBusinessAccess.DefaultBusinessAccess jbAccess = new DefaultBusinessAccess();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Username"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            //[MainChartReferenceChartNo]
            if (Request.QueryString["Sno"] == null)
            {
                
            }
            else
            {
                string MainChartReferenceChartNo =Request.QueryString["Sno"];
                //hplAddXAxis.NavigateUrl = ;
                hplAddXAxis.NavigateUrl = "javascript:window.open('" + "LineChartXAxisProperties.aspx?AddNew=yes&MainChartConfigRefNo=" + Request.QueryString["Sno"] + "',null,'left=30,top=30,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');";
                hplAddYAxis.NavigateUrl = "javascript:window.open('" + "LineChartYAxisProperties.aspx?AddNew=yes&MainChartConfigRefNo=" + Request.QueryString["Sno"] + "',null,'left=30,top=30,width=1000,height=600,toolbar=0,resizable=0,scrollbars=yes');";
                GridView1.DataSource = jbAccess.GetXAxisLine(MainChartReferenceChartNo);
                GridView1.DataBind();
                GridView2.DataSource = jbAccess.GetYAxisLine(MainChartReferenceChartNo);
                GridView2.DataBind();

            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
       
    }
}