using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogic;
using Shield.Web.UI;

public partial class ViewClassDetails : System.Web.UI.Page
{
    BusinessLogic.Reports Reports = new Reports();
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString[name: "Class"] != null)
        {
            var tobbaccoClassDetails = Reports.TobbaccoDetails(className: Request.QueryString[name: "Class"]);
            if (tobbaccoClassDetails != null)
            {

             //   tobbaccoClassDetails =
              //      tobbaccoClassDetails.AsEnumerable()
               //         .Where(values => (!(values.Date <= DateTime.Now.AddYears(-1))))
                //        .ToList();

                ShieldChart1.PrimaryHeader.Text = "Truck Samples : " + Request.QueryString[name: "Class"];
                ShieldChart1.SecondaryHeader.Text = "Generated on " + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                ShieldChart1.ExportOptions.AllowExportToImage = true;
                ShieldChart1.ExportOptions.AllowPrint = true;
                ShieldChart1.TooltipSettings.AxisMarkers.Enabled = true;
                ShieldChart1.TooltipSettings.AxisMarkers.Mode = ChartXYMode.XY;
                ShieldChart1.TooltipSettings.AxisMarkers.Width = new Unit(1);
                ShieldChart1.TooltipSettings.AxisMarkers.ZIndex = 3;
                ShieldChart1.ZoomMode = ChartXYMode.X;
                


                var chartAxisX = new ChartAxisX
                {
                    CategoricalValues =
                        tobbaccoClassDetails.AsEnumerable().Select(data => data.Date.ToString("MM/dd/yyyy")).ToArray(),
                    AxisTickText = {TextAngle = 270, Y = 35}
                };


                var chartAxisY = new ChartAxisY {AxisTickText = {Format = "{text}"}, Title = {Text = "Y Axis"}};
                ShieldChart1.Axes.Add(item: chartAxisX);
                ShieldChart1.Axes.Add(item: chartAxisY);
                ShieldChart1.TooltipSettings.ChartBound = true;

                var avgNicotinePercentagechartLineSeries = new ChartLineSeries
                {
                    DataFieldY = "AvgNicotinePercentage",
                    CollectionAlias = "Average Nicotine (%)"
                };
                avgNicotinePercentagechartLineSeries.Settings.EnablePointSelection = true;
                avgNicotinePercentagechartLineSeries.Settings.PointMark.ActiveSettings.PointSelectedState.DrawWidth = 4;
                avgNicotinePercentagechartLineSeries.Settings.PointMark.ActiveSettings.PointSelectedState.DrawRadius = 4;
                ShieldChart1.DataSeries.Add(avgNicotinePercentagechartLineSeries);


                
                var avgtSugarPercentagechartLineSeries = new ChartLineSeries
                {
                    DataFieldY = "AvgtSugarPercentage",
                    CollectionAlias = "Average T Sugar (%)"
                };
                avgtSugarPercentagechartLineSeries.Settings.EnablePointSelection = true;
                avgtSugarPercentagechartLineSeries.Settings.PointMark.ActiveSettings.PointSelectedState.DrawWidth = 4;
                avgtSugarPercentagechartLineSeries.Settings.PointMark.ActiveSettings.PointSelectedState.DrawRadius = 4;
                ShieldChart1.DataSeries.Add(avgtSugarPercentagechartLineSeries);

                var avgrSugarPercentagechartLineSeries = new ChartLineSeries
                {
                    DataFieldY = "AvgrSugarPercentage",
                    CollectionAlias = "Average R Sugar (%)"
                };
                avgrSugarPercentagechartLineSeries.Settings.EnablePointSelection = true;
                avgrSugarPercentagechartLineSeries.Settings.PointMark.ActiveSettings.PointSelectedState.DrawWidth = 4;
                avgrSugarPercentagechartLineSeries.Settings.PointMark.ActiveSettings.PointSelectedState.DrawRadius = 4;
                ShieldChart1.DataSeries.Add(avgrSugarPercentagechartLineSeries);

            }

            ShieldChart1.DataSource = tobbaccoClassDetails;
            ShieldChart1.DataBind();

            var tbcAnalytics =Reports.TbcAnalytics(tobbaccoClassDetails);
            lblAvgNicotine.Text =Math.Round(  tbcAnalytics.TotalAverageNicotine,2).ToString(CultureInfo.InvariantCulture);
            lblAvgTSugar.Text =  Math.Round( tbcAnalytics.TotalAverageTSugar,2).ToString(CultureInfo.InvariantCulture);
            lblAvgRSugar.Text =  Math.Round( tbcAnalytics.TotalAverageRSugar,2).ToString(CultureInfo.InvariantCulture);




            lblLatestValueNIC.Text = Math.Round( tbcAnalytics.LatestNicotine,2).ToString(CultureInfo.InvariantCulture) +"<br/>"+
                                     Environment.NewLine + tbcAnalytics.DateLatestNicotine.ToString("yyyy MMM dd");
            lblLatestValueTSugar.Text = Math.Round( tbcAnalytics.LatestTSugar,2).ToString(CultureInfo.InvariantCulture) + "<br/>" +
                                     Environment.NewLine + tbcAnalytics.DateLatestTSugar.ToString("yyyy MMM dd");
            lblLatestValueRSugar.Text = Math.Round( tbcAnalytics.LatestRSugar,2).ToString(CultureInfo.InvariantCulture) + "<br/>" +
                                     Environment.NewLine + tbcAnalytics.DateLatestRSugar.ToString("yyyy MMM dd");



            lblHighestValueNIC.Text = Math.Round( tbcAnalytics.HighestNicotine,2).ToString(CultureInfo.InvariantCulture) + "<br/>" +
                                     Environment.NewLine + tbcAnalytics.DateHighestNicotine.ToString("yyyy MMM dd");
            lblHighestValueTSugar.Text = Math.Round( tbcAnalytics.HighestTSugar,2).ToString(CultureInfo.InvariantCulture) + "<br/>" +
                                     Environment.NewLine + tbcAnalytics.DateHighestTSugar.ToString("yyyy MMM dd");
            lblHighestValueRSugar.Text = Math.Round( tbcAnalytics.HighestRSugar,2).ToString(CultureInfo.InvariantCulture) + "<br/>" +
                                     Environment.NewLine + tbcAnalytics.DateHighestRSugar.ToString("yyyy MMM dd");


            lblLowestValueNIC.Text = Math.Round( tbcAnalytics.LowestNicotine,2).ToString(CultureInfo.InvariantCulture) + "<br/>" +
                                     Environment.NewLine + tbcAnalytics.DateLowestNicotine.ToString("yyyy MMM dd");
            lblLowestValueTSugar.Text =Math.Round(  tbcAnalytics.LowestTSugar,2).ToString(CultureInfo.InvariantCulture) + "<br/>" +
                                     Environment.NewLine + tbcAnalytics.DateLowestTSugar.ToString("yyyy MMM dd");
            lblLowestValueRSugar.Text = Math.Round( tbcAnalytics.LowestRSugar,2).ToString(CultureInfo.InvariantCulture) + "<br/>" +
                                     Environment.NewLine + tbcAnalytics.DateLowestRSugar.ToString("yyyy MMM dd");
        }

    }
}