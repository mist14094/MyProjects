using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;
using JarvisDataAccess;
using Shield.Web.UI;

namespace JarvisBusinessAccess
{
    public class BasicChart
    {

        public ShieldChart GetBasicChartWithSetting(string ID)
        {

            ShieldChart chart = new ShieldChart();
            JarvisDataAccess.DefaultDataAccess access = new DefaultDataAccess();
            DataTable dt = access.GetChartValues(ID);

            foreach (DataRow dr in dt.Rows)
            {
                chart.PrimaryHeader.Text = dr["ChartPrimaryHeader"].ToString();
                chart.SecondaryHeader.Text = "Generated on " + DateTime.Now.ToString(CultureInfo.InvariantCulture);
                chart.AllowMultiItemSelection = Convert.ToBoolean(dr["AllowMultipleSelection"]);
                chart.ExportOptions.AllowExportToImage = Convert.ToBoolean(dr["ExportOptionsExporttoImage"]);
                chart.ExportOptions.AllowPrint = Convert.ToBoolean(dr["ExportOptionsAllowPrint"]);
                chart.Height = dr["HeightMode"].ToString() =="Percentage" ? Unit.Percentage(Convert.ToDouble(dr["Height"].ToString())) : Unit.Pixel(Convert.ToInt16(dr["Height"].ToString()));
                chart.Width = dr["WidthMode"].ToString() == "Percentage" ? Unit.Percentage(Convert.ToDouble(dr["Width"].ToString())) : Unit.Pixel(Convert.ToInt16(dr["Width"].ToString()));
                if (Convert.ToBoolean(dr["IsInverted"].ToString())) chart.IsInverted = true;
                chart.ZoomMode = (ChartXYMode)Convert.ToInt16(dr["ZoomMode"].ToString());
                chart.TooltipSettings.AxisMarkers.Enabled = Convert.ToBoolean(dr["AxisMarkersEnabled"]);
                chart.TooltipSettings.AxisMarkers.Mode = (ChartXYMode)Convert.ToInt16(dr["AxisMarkersMode"].ToString());
                chart.TooltipSettings.AxisMarkers.Width = new Unit(Convert.ToInt16(dr["AxisMarkersWidth"].ToString()));
                chart.TooltipSettings.ChartBound = Convert.ToBoolean(dr["TooltipSettingsChartBound"]);
                //chart
            }

            return chart;
        }
    }
}
