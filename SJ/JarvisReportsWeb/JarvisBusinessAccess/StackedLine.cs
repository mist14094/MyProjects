using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using Shield.Web.UI;
namespace JarvisBusinessAccess
{
    class StackedLine
    {
        public StackedLine()
        {
           
        }

        public string PrimaryHeaderText { get; set; }
        public string SecondaryHeaderText { get; set; }
        public ChartXYMode AxisMarkersMode { get; set; }
        public int AxisMarkersWidth { get; set; }
        public int AxisMarkersZIndex { get; set; }
        public ChartXYMode AllowExportToImage { get; set; }
        public bool AllowPrint { get; set; }
        public bool AxisMarkersEnabled { get; set; }
        public ChartXYMode ZoomMode { get; set; }
        public ChartAxisX ChartAxisX { get; set; }
        public ChartAxisY ChartAxisY { get; set; }


        public ShieldChart GetStackedLineChart(string primaryHeaderText, string secondaryHeaderText,
            ChartXYMode axisMarkersMode, int axisMarkersWidth, int axisMarkersZIndex, ChartXYMode allowExportToImage,
            bool allowPrint, bool axisMarkersEnabled, ChartXYMode zoomMode, ChartAxisY chartAxisY, ChartAxisX chartAxisX)
        {
            var shieldChart = new ShieldChart();
            shieldChart.PrimaryHeader.Text = primaryHeaderText;
            shieldChart.SecondaryHeader.Text = "Generated on " + DateTime.Now.ToString(CultureInfo.InvariantCulture);
            shieldChart.ExportOptions.AllowExportToImage = true;
            shieldChart.ExportOptions.AllowPrint = true;
            shieldChart.TooltipSettings.AxisMarkers.Enabled = true;
            shieldChart.TooltipSettings.AxisMarkers.Mode = ChartXYMode.XY;
            shieldChart.TooltipSettings.AxisMarkers.Width = new Unit(1);
            shieldChart.TooltipSettings.AxisMarkers.ZIndex = 3;
            shieldChart.ZoomMode = ChartXYMode.X;
            return shieldChart;

        }

    }
}
