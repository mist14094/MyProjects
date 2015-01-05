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
    public partial class _Default : System.Web.UI.Page
    {

        private NLog.Logger nlog = LogManager.GetCurrentClassLogger();


        protected void Page_Load(object sender, EventArgs e)
        {
            nlog.Trace("BEPGrid:Page_Load::Entering");
            try
            {
                // if (!Page.IsPostBack)
                {
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:Page_Load::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:Page_Load::Leaving");
            }
        }

        private void BindGrid()
        {
            nlog.Trace("BEPGrid:BindGrid::Entering");
            try
            {

                CacheWrapper obj = CacheWrapper.GetInstance();

                HomeScreen objHomeScreen = new HomeScreen();
                DataSet dsHS = objHomeScreen.GetHomeScreenDetails();

                foreach (DataTable dt in dsHS.Tables)
                {
                    switch (dt.TableName.ToUpper())
                    {
                        case "SUMMARY":
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    lblTotalItems.Text = dt.Rows[0]["TotalItems"].ToString();
                                    lblProfitItemCnt.Text = dt.Rows[0]["ProfitMakingItems"].ToString();
                                    lblLossItemCnt.Text = dt.Rows[0]["LossMakingItems"].ToString();
                                    double totalCost = double.Parse(dt.Rows[0]["ProfitMargin"].ToString());
                                    if (totalCost < 0)
                                        lblProfitMargin.BackColor = System.Drawing.Color.Red;
                                    else
                                        lblProfitMargin.BackColor = System.Drawing.Color.Green;

                                    lblProfitMargin.Text = totalCost.ToString("C");
                                }
                            }
                            break;
                        case "STOREPROGRESS":
                            {
                                chStoreSales.Series.Clear();
                                chStoreSales.Legends.Clear();

                                Legend lg = new Legend("Parameters");
                                lg.Font = new System.Drawing.Font("Verdana", 7, System.Drawing.FontStyle.Bold);

                                lg.Docking = Docking.Top;

                                chStoreSales.Legends.Add(lg);

                                //chStoreSales.DataBindTable(dt.DefaultView, "StoreName");
                                ////chStoreSales.X

                                //foreach(Series srs in chStoreSales.Series)
                                //{

                                //    srs.YValueMembers = "TotalSales";
                                //    srs.ChartType = SeriesChartType.StackedBar;
                                //    srs.BorderDashStyle = ChartDashStyle.Solid;
                                //    srs.BorderWidth = 3;
                                //    srs.MarkerSize = 6;
                                //    srs.MarkerStyle = MarkerStyle.Diamond;
                                //    srs.MarkerColor = System.Drawing.Color.Black;
                                //    srs.ToolTip = "Date : #VALX , Total Sales : #VALY{C}";


                                //}


                                #region
                                if (dt.Rows.Count > 0)
                                {
                                    chStoreSales.Series.Clear();

                                    List<string> storeIds = new List<string>();
                                    List<string> Days = new List<string>();
                                    Dictionary<string, Dictionary<string, double>> dataSource = new Dictionary<string, Dictionary<string, double>>();

                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        if (!Days.Contains(dr["SDAY"].ToString()))
                                            Days.Add(dr["SDAY"].ToString());
                                        if (!storeIds.Contains(dr["StoreName"].ToString()))
                                            storeIds.Add(dr["StoreName"].ToString());
                                        if (!dataSource.ContainsKey(dr["SDAY"].ToString()))
                                        {
                                            dataSource.Add(dr["SDAY"].ToString(), new Dictionary<string, double>());

                                            dataSource[dr["SDAY"].ToString()].Add(dr["StoreName"].ToString(), double.Parse(dr["TotalSales"].ToString()));
                                        }
                                        else
                                        {
                                            dataSource[dr["SDAY"].ToString()].Add(dr["StoreName"].ToString(), double.Parse(dr["TotalSales"].ToString()));
                                        }
                                    }

                                    foreach (string day in Days)
                                    {
                                        if (dataSource[day].Count != storeIds.Count)
                                        {
                                            foreach (string str in storeIds)
                                            {
                                                if (!dataSource[day].ContainsKey(str))
                                                    dataSource[day].Add(str, 0);
                                            }
                                        }
                                    }


                                    foreach (string day in Days)
                                    {

                                        Series srs = new Series(day);

                                        foreach (string str in storeIds)
                                        {
                                            srs.Points.AddXY(str, new object[] { dataSource[day][str] });
                                        }

                                        foreach (DataPoint dp in srs.Points)
                                        {
                                            dp.ToolTip = "Store : #VALX :: Sales : #VALY{C}";
                                        }
                                        //srs.Points.DataBindXY(firstView, "StoreName", firstView, "TotalSales");
                                        srs.ChartType = SeriesChartType.StackedBar;
                                        srs.BorderDashStyle = ChartDashStyle.Solid;
                                        srs.BorderWidth = 3;
                                        srs.Legend = "Parameters";
                                        srs.LegendText = day;
                                        
                                   

                                        chStoreSales.Series.Add(srs);




                                    }
                                }

                                chStoreSales.ChartAreas[0].AxisX.Title = "Stores";
                                chStoreSales.ChartAreas[0].AxisX.Interval = 1;
                                chStoreSales.ChartAreas[0].AxisY.Title = "Sales";

                    
                   
                              //  chStoreSales.Series[1].Url = "google.com";
                              //  chStoreSales.Series[1].LegendUrl = "yahoo.com";
                              //  chStoreSales.Series[2].Points[2].Url = "gooooogle.com";
                                 
                                //// Enable scale breaks
                                //chStoreSales.ChartAreas[0].AxisY.ScaleBreakStyle.Enabled = true;

                                //// Set the scale break type
                                //chStoreSales.ChartAreas[0].AxisY.ScaleBreakStyle.BreakLineStyle = BreakLineStyle.Ragged;

                                //// Set the spacing gap between the lines of the scale break (as a percentage of y-axis)
                                //chStoreSales.ChartAreas[0].AxisY.ScaleBreakStyle.Spacing = 2;

                                //        //// Set the line width of the scale break
                                //        //chStoreSales.ChartAreas[0].AxisY.ScaleBreakStyle.LineWidth = 1;

                                //        //// Set the color of the scale break
                                //        //chStoreSales.ChartAreas[0].AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.Red;

                                //        //// Show scale break if more than 25% of the chart is empty space
                                //        //chStoreSales.ChartAreas[0].AxisY.ScaleBreakStyle.CollapsibleSpaceThreshold = 20;
                                //        chStoreSales.ChartAreas[0].AxisY.IsLogarithmic = true;
                                //        chStoreSales.ChartAreas[0].AxisY.LogarithmBase = Math.E;

                                // chStoreSales.ChartAreas[0].AxisX.MinorTickMark.LineWidth = 1;
                                chStoreSales.ChartAreas[0].AxisY.MajorGrid.IntervalOffsetType = DateTimeIntervalType.Auto;

                                //        // If all data points are significantly far from zero, 
                                //        // the Chart will calculate the scale minimum value
                                //       // chStoreSales.ChartAreas[0].AxisY.ScaleBreakStyle = AutoBool.Auto;
                                //    }
                                //}

                                for (int ChartAreaCount = 0; ChartAreaCount < chStoreSales.Series.Count(); ChartAreaCount++)
                                {
                                    try
                                    {
                                        string LegendUrl = "popupgrid.aspx?ChartSource=Legend&Store=ALL&Value=" + chStoreSales.Series[ChartAreaCount].Name + "&Date=" + dt.Select("SDAY='" + chStoreSales.Series[ChartAreaCount].Name + "'")[0]["SDate"].ToString() + "&Day=" + chStoreSales.Series[ChartAreaCount].Name;
                                        chStoreSales.Series[ChartAreaCount].LegendMapAreaAttributes = "onclick=\"Popup=window.open('" + LegendUrl + "','Legends','toolbar=yes,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=1000,height=600,left=430,top=23'); return false;\"";
                                        chStoreSales.Series[ChartAreaCount].Url = "#";
                                        chStoreSales.Series[ChartAreaCount].LegendUrl = "#";
                                        for (int ChartAreaCountPoint = 0; ChartAreaCountPoint < chStoreSales.Series[ChartAreaCount].Points.Count; ChartAreaCountPoint++)
                                        {
                                            try
                                            {
                                                string strDate = dt.Select(@"StoreName='" + chStoreSales.Series[ChartAreaCount].Points[ChartAreaCountPoint].AxisLabel + "' AND SDAY ='" + chStoreSales.Series[ChartAreaCount].Name + "'")[0]["SDate"].ToString();
                                                string Url = "popupgrid.aspx?ChartSource=Point&Store=" + chStoreSales.Series[ChartAreaCount].Points[ChartAreaCountPoint].AxisLabel + "&Date=" + strDate + "&Day=" + chStoreSales.Series[ChartAreaCount].Name + "&Value=" + chStoreSales.Series[ChartAreaCount].Points[ChartAreaCountPoint].YValues[0].ToString();
                                                chStoreSales.Series[ChartAreaCount].Points[ChartAreaCountPoint].MapAreaAttributes = "onclick=\"Popup=window.open('" + Url + "','Points','toolbar=yes,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=1000,height=600,left=430,top=23'); return false;\""; ;
                                            }
                                            catch (Exception ex)
                                            {
                                                nlog.ErrorException("BEPGrid:POPUPURL::Error", ex);
                                            }

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        nlog.ErrorException("BEPGrid:POPUPURLLEGEND::Error", ex);
                                    }

                                }

                                #endregion
                            }
                            break;
                        case "25MOSTSELLINGITEMS":
                            {
                                gvTMSI.DataSource = dt.DefaultView;
                                gvTMSI.DataBind();
                            }
                            break;
                        case "25LEASTSELLINGITEMS":
                            {
                                gvTLSI.DataSource = dt.DefaultView;
                                gvTLSI.DataBind();
                            }
                            break;
                        case "5TOPREVENUEGENERATOR":
                            {
                                gvTRG.DataSource = dt.DefaultView;
                                gvTRG.DataBind();
                            }
                            break;
                        case "5MOSTSELLINGVENDOR":
                            {
                                gvTMSV.DataSource = dt.DefaultView;
                                gvTMSV.DataBind();
                            }
                            break;
                        case "5LEASTSELLINGVENDOR":
                            {
                                gvTLSV.DataSource = dt.DefaultView;
                                gvTLSV.DataBind();
                            }
                            break;
                        default:
                            break;
                    }
                }





                //GridViewHierachical 
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPGrid:BindGrid::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPGrid:BindGrid::Leaving");
            }

        }



    }
}
