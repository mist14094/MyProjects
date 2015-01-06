using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shield.Web.UI;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        ShieldChart1.Width = Unit.Percentage(100);
        ShieldChart1.Height = Unit.Pixel(500);
        ShieldChart1.CssClass = "chart";
        ShieldChart1.PrimaryHeader.Text = "Menthol Only - Store : ";
        ShieldChart1.SecondaryHeader.Text = "Generated on " + DateTime.Now.ToString("MM/dd/yy hh:mm:ss");
        ShieldChart1.TooltipSettings.AxisMarkers.Enabled = true;
        ShieldChart1.TooltipSettings.AxisMarkers.Mode = ChartXYMode.XY;
        ShieldChart1.TooltipSettings.AxisMarkers.Width = new Unit(1);
        ShieldChart1.TooltipSettings.AxisMarkers.ZIndex = 3;
        ShieldChart1.ZoomMode = ChartXYMode.XY;
        var xAxisValues = new[] {"1", "2", "3", "4", "5"};
        var yAxisValues = new[] { "A", "B", "C", "D", "E" };
        ChartAxisX axisX = new ChartAxisX();
        axisX.CategoricalValues = xAxisValues;
        axisX.Title.Text = "Date";
        axisX.AxisTickText.TextAngle = 270;
        axisX.AxisTickText.Y = 35;
        //axisX.AxisTickText.
        ShieldChart1.Axes.Add(axisX);

        ChartAxisY axisY = new ChartAxisY();
        axisY.Title.Text = "Quantity";

        ShieldChart1.Axes.Add(axisY);


        for (int i = 0; i < yAxisValues.Count(); i++)
        {
            if (yAxisValues[i] == "Seneca Menthol" || yAxisValues[i] == "A" || yAxisValues[i] == "N2")
            {

                if (yAxisValues[i] == "Seneca Menthol")
                {
                    ChartBarSeries splineSeriesHouseholds = new ChartBarSeries();
                    splineSeriesHouseholds.Index = 1000;
                    splineSeriesHouseholds.Settings.Color = Color.Black;
                    splineSeriesHouseholds.DataFieldY = yAxisValues[i];
                    splineSeriesHouseholds.CollectionAlias = yAxisValues[i];
                    splineSeriesHouseholds.Settings.StackMode = ChartStackMode.None;
                    splineSeriesHouseholds.Settings.BorderColor = Color.Black;
                    splineSeriesHouseholds.Settings.EnablePointSelection = true;

                    ShieldChart1.DataSeries.Add(splineSeriesHouseholds);
                }
                else
                {
                    ChartBarSeries splineSeriesHouseholds = new ChartBarSeries();
                    splineSeriesHouseholds.Index = 0;
                    splineSeriesHouseholds.Settings.AddToLegend = false;
                    splineSeriesHouseholds.Settings.Color = Color.Black;
                    splineSeriesHouseholds.DataFieldY = yAxisValues[i];
                    splineSeriesHouseholds.CollectionAlias = yAxisValues[i];
                    splineSeriesHouseholds.Settings.StackMode = ChartStackMode.None;
                    splineSeriesHouseholds.Settings.BorderColor = Color.Black;
                    splineSeriesHouseholds.Settings.EnablePointSelection = true;
                    ShieldChart1.DataSeries.Add(splineSeriesHouseholds);
                }

            }



            else
            {
                ChartAreaSeries splineSeriesHouseholds = new ChartAreaSeries();
                splineSeriesHouseholds.DataFieldY = yAxisValues[i];
                splineSeriesHouseholds.CollectionAlias = yAxisValues[i];
                splineSeriesHouseholds.Settings.StackMode = ChartStackMode.Normal;
                //splineSeriesHouseholds.Settings.DrawNullValues = true;
                ShieldChart1.DataSeries.Add(splineSeriesHouseholds);
            }






        }
        DataTable dt = new DataTable();
        dt.Columns.Add("ticketdate", typeof (string));
        dt.Columns.Add("A", typeof (int));
        dt.Columns.Add("B" ,typeof (int));
        dt.Columns.Add("C",typeof (int));
        dt.Columns.Add("D",typeof (int));
        dt.Columns.Add("E",typeof (int));

        dt.Rows.Add("1", 1, 2, 2, 3, 4);
        dt.Rows.Add("2", 11, 12, 21, 13, 14);
        dt.Rows.Add("3", 11, 12, 21, 13, 14);
        dt.Rows.Add("4", 11, 12, 21, 13, 14);
        dt.Rows.Add("5", 11, 12, 21, 13, 14);

        try
        {
            ShieldChart1.DataSource = dt;
            ShieldChart1.DataBind();
        }
        catch (Exception ex)
        {
            
            throw;
        }
        
    }
}