using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using C1.Web.Wijmo.Controls.C1Chart;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using C1.Web.Wijmo.Controls.C1ComboBox;
using BreakEvenBAL;
public partial class BEPProduct : System.Web.UI.Page
{
    public static string _BreakEvenDB = ConfigurationManager.ConnectionStrings["BreakEven"].ConnectionString;
    public static string _TrackerRetail = ConfigurationManager.ConnectionStrings["KT_TrackerConnectionString"].ConnectionString;
    BreakEvenBAL.BEBAL BEBALLayer = new BEBAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        var series = new LineChartSeries();
        this.C1LineChart1.SeriesList.Add(series);
        this.C1LineChart1.Type = LineChartType.Area;
        string strPID = Request.QueryString["PID"];

        DateTime[] DateRange;
        double[] TotalSales;
        double[] QTYSOLD;
        double[] AvgPrice;
        DataSet ds = new DataSet();
        ds = GetSalesTrend(_BreakEvenDB, strPID);
        if (ds.Tables[0].Rows.Count == 0)
        {
            lblNoSales.Visible = true;
            C1LineChart1.Visible = false;
        }
        else
        {
            lblNoSales.Visible = false;
            C1LineChart1.Visible = true;
            DateRange = new DateTime[ds.Tables[0].Rows.Count];
            QTYSOLD = new double[ds.Tables[0].Rows.Count];
            TotalSales = new double[ds.Tables[0].Rows.Count];
            AvgPrice = new double[ds.Tables[0].Rows.Count];
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DateRange[i] = Convert.ToDateTime(ds.Tables[0].Rows[i]["SaleTime"]);
                QTYSOLD[i] = Convert.ToDouble(ds.Tables[0].Rows[i]["QTYSOLD"]);
                TotalSales[i] = Convert.ToDouble(ds.Tables[0].Rows[i]["TotalSales"]);
                AvgPrice[i] = Convert.ToDouble(ds.Tables[0].Rows[i]["AvgPrice"]);
            }


            series.Data.X.AddRange(DateRange);
            series.Data.Y.AddRange(QTYSOLD);
            series.Label = "ProductSold";
            // series.LegendEntry = true;

            series.Markers.Visible = false;

            var series1 = new LineChartSeries();
            this.C1LineChart1.SeriesList.Add(series1);
            series1.Data.X.AddRange(DateRange);
            series1.Data.Y.AddRange(TotalSales);
            series1.Label = "Sales Realised";
            //  series1.LegendEntry = true;
            series1.Markers.Type = MarkerType.Box;
            series1.Markers.Visible = true;
            series1.Display = ChartSeriesDisplay.ExcludeHole;


            var series2 = new LineChartSeries();
            this.C1LineChart1.SeriesList.Add(series2);
            series2.Data.X.AddRange(DateRange);
            series2.Data.Y.AddRange(AvgPrice);
            series2.Label = "Average Price";
            //   series2.LegendEntry = true;
            series2.Markers.Visible = false;

            var QTYSOLDStyle = new ChartStyle();
            QTYSOLDStyle.FillOpacity = 0;
            QTYSOLDStyle.StrokeWidth = 0;
            this.C1LineChart1.SeriesStyles.Add(QTYSOLDStyle);

            var TotalSalesStyle = new ChartStyle();
            TotalSalesStyle.FillOpacity = 0;
            TotalSalesStyle.StrokeWidth = 0;

            this.C1LineChart1.SeriesStyles.Add(TotalSalesStyle);

            var AvgPriceStyle = new ChartStyle();
            AvgPriceStyle.FillOpacity = 0;
            AvgPriceStyle.StrokeWidth = 0;
            AvgPriceStyle.Stroke = System.Drawing.Color.Red;
            this.C1LineChart1.SeriesStyles.Add(AvgPriceStyle);


        }

    }

    public DataSet GetSalesTrend(string ConString, string PID)
    {
        DataSet resultset = new DataSet();
        string query = "SELECT  sum(   [totalprice]) as TotalSales, sum(QtySold) as QTYSOLD , (sum(   [totalprice])/sum(QtySold)) as AvgPrice ,[SaleTime]   FROM [KT_BreakEven].[dbo].[SalesDetail] where pc_id='"+ PID+"' group by saletime order by SaleTime ";
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, ConString))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }

           
        }
        catch (Exception ex)
        {

        }
        return resultset;
    }




}