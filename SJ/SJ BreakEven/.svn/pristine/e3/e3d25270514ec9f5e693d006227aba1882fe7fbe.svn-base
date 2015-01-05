using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BreakEvenBAL;
using System.Linq.Expressions;
using NLog;
using System.Web.Caching;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;

namespace BEPWeb
{
    public partial class popupgrid : System.Web.UI.Page
    {
        private NLog.Logger nlog = LogManager.GetCurrentClassLogger();

        string strChartSource = "";
        string strDate = "";
        string strDay = "";
        string strStoreName = "";
        string strValue = "";
        string strSummary = "";
        protected void Page_Load(object sender, EventArgs e)
        {

             nlog.Trace("POPUPGRID:Page_Load::Entering");

             try
             {
                 strChartSource = Request.QueryString["ChartSource"];
                 strDay = Request.QueryString["Day"];
                 strStoreName = Request.QueryString["Store"];
                 strValue = Request.QueryString["Value"];
                 strDate = Convert.ToDateTime(Request.QueryString["Date"]).ToString("yyyy-MM-dd");
                 strSummary = "Date : " + Convert.ToDateTime(Request.QueryString["Date"]).ToString("dddd, dd MMMM yyyy");
                 //Response.Write(Environment.NewLine + strChartSource);
                 //Response.Write(Environment.NewLine + strDay);
                 //Response.Write(Environment.NewLine + strStoreName);
                 //Response.Write(Environment.NewLine + strValue);
                 //Response.Write(Environment.NewLine + strDate);
                 if (strChartSource == "Legend")
                 {
                     HomeScreen objHomeScreen = new HomeScreen();
                     DataSet dsHS = objHomeScreen.DaySummaryAllStores(strDate);
                     grdLegend.DataSource = dsHS.Tables["Summary"];
                     grdLegend.DataBind();
                     strSummary = strSummary + " | Store : ALL";

                 }
                 else
                 {
                     HomeScreen objHomeScreen = new HomeScreen();
                     DataSet dsHS = objHomeScreen.DailyStoreDetails(strStoreName, strDate);
                     grdPointerHeader.DataSource = dsHS.Tables["Header"];
                     grdPointerHeader.DataBind();
                     grdPointerSummary.DataSource = dsHS.Tables["Summary"];
                     grdPointerSummary.DataBind();
                     strSummary = strSummary + " | Store : " + strStoreName;

                 }
                 lblSummary.Text = strSummary;

             }
             catch (Exception ex)
             {
                 nlog.ErrorException("POPUPGRID:Page_Load::Error", ex);
             }
             finally
             {
                 nlog.Trace("POPUPGRID:Page_Load::Leaving");
             }
        }
    }
}