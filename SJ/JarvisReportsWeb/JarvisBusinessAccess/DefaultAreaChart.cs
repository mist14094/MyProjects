using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using JarvisDataAccess;
using Shield.Web.UI;

namespace JarvisBusinessAccess
{
    public class DefaultAreaChart : BasicChart
    {
        public ShieldChart GetDefaultChart(string MainConfigurationComp, DataTable dt)
        {
            BasicChart bc = new BasicChart();
            ShieldChart scBasiChart= new ShieldChart();
            scBasiChart = bc.GetBasicChartWithSetting(GetMainChartConfigurationRefNo(MainConfigurationComp));
            scBasiChart.EnableAutoFit = false;
            foreach (DataRow dr in GetAxisConfiguration(GetMainChartConfigurationRefNo(MainConfigurationComp), "0").Rows)
            {
                var chartAxisX = new ChartAxisX
                {
                    TicksRepeat = Convert.ToDouble(dr["TicksRepeat"].ToString() ),
                    SwapLocation = Convert.ToBoolean(dr["SwapLocation"].ToString()),
                    CategoricalValues =
                    dt.AsEnumerable().Select(row => row[dr["CatagoricalValuesColumnName"].ToString()].ToString()).ToArray(),
                    AxisTickText =
                    {
                        TextAngle = Convert.ToInt16(dr["AxisTextAngle"].ToString()),
                        X = Convert.ToInt16(dr["AxisTextAngleX"].ToString()),
                        Y = Convert.ToInt16(dr["AxisTextAngleY"].ToString()),
                        Step = Convert.ToDouble(dr["AxisTextAngleStep"].ToString()),
                    },
                    Title =
                    {
                        Text = Convert.ToString(dr["TitleText"].ToString()),
                    }
                };

                scBasiChart.Axes.Add(chartAxisX);
            }


            foreach (DataRow dr in GetAxisConfiguration(GetMainChartConfigurationRefNo(MainConfigurationComp), "1").Rows)
            {
                var chartAxisY = new ChartAxisY
                {
                    TicksRepeat = Convert.ToDouble(dr["TicksRepeat"].ToString()),
                    SwapLocation = Convert.ToBoolean(dr["SwapLocation"].ToString()),
                    AxisTickText =
                    {
                        TextAngle = Convert.ToInt16(dr["AxisTextAngle"].ToString()),
                        X = Convert.ToInt16(dr["AxisTextAngleX"].ToString()),
                        Y = Convert.ToInt16(dr["AxisTextAngleY"].ToString()),
                        Step = Convert.ToDouble(dr["AxisTextAngleStep"].ToString()),
                    },
                    Title =
                    {
                        Text = Convert.ToString(dr["TitleText"].ToString()),
                    }
                };

                scBasiChart.Axes.Add(chartAxisY);

                DataTable ChartLineSeries = GetChartLineSeries(dr["sno"].ToString());

                foreach (DataRow CRDR in ChartLineSeries.Rows)
                {
                    var chartlineseries = new ChartAreaSeries()
                    {
                        DataFieldY =  Convert.ToString(CRDR["DataFieldY"].ToString()),
                        CollectionAlias =  Convert.ToString(CRDR["CollectionAlias"].ToString())
                    };
                    chartlineseries.Settings.EnablePointSelection = Convert.ToBoolean(CRDR["EnablePointSelection"].ToString());
                    chartlineseries.Settings.PointMark.ActiveSettings.PointSelectedState.DrawWidth = Convert.ToInt16(CRDR["DrawWidth"].ToString());
                    chartlineseries.Settings.PointMark.ActiveSettings.PointSelectedState.DrawRadius = Convert.ToInt16(CRDR["DrawRadius"].ToString());

                    chartlineseries.Settings.StackMode = (ChartStackMode)Convert.ToInt16(CRDR["StackMode"].ToString());
                    chartlineseries.Settings.DrawNullValues = true;
                    //chartlineseries.Settings.DrawWidth = new Unit(10);
                    scBasiChart.DataSeries.Add(chartlineseries);

                }
            }


            scBasiChart.DataSource = dt;
            scBasiChart.DataBind();

            return scBasiChart;
        }

        private DataTable GetAxisConfiguration(string ReferenceChartNo, string AxisType)
        {
            JarvisDataAccess.DefaultDataAccess dl = new DefaultDataAccess();
            return dl.GetAxisConfiguration(ReferenceChartNo, AxisType);
        }

        private DataTable GetChartLineSeries(string MainChartAxisBaseSno)
        {
            JarvisDataAccess.DefaultDataAccess dl = new DefaultDataAccess();
            return dl.GetChartLineSeries(MainChartAxisBaseSno);
        }

        public string GetMainChartConfigurationRefNo(string sno)
        {
            JarvisDataAccess.DefaultDataAccess dl = new DefaultDataAccess();
            return dl.GetMainChartConfigurationRefNo(sno);

        }
    }
}
