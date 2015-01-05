using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NLog;
using BreakEvenBAL;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.HtmlControls;
using System.Web.Caching;
using System.Data;


namespace BEPWeb
{
    public partial class BEPDetails : System.Web.UI.Page
    {
        private NLog.Logger nlog = LogManager.GetCurrentClassLogger();

        static double UnitCost, VariableCost, SuggestRetail, totalCost, AvgSellPrice;
        static long RcvdQty, TotalSold;
        private static BEP_Detail selectedBEP;
       


        protected void Page_Load(object sender, EventArgs e)
        {
            nlog.Trace("BEPDetails:Page_Load::Entering");
            try
            {
                if (!Page.IsPostBack)
                {
                    string strPID = Request.QueryString["PID"];


                     selectedBEP = CacheWrapper.GetInstance().GetBEPItem(long.Parse(strPID));

                    FillData(selectedBEP);
                    InitiateChart(selectedBEP);

                }

            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPDetails:Page_Load::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPDetails:Page_Load::Leaving");
            }
        }


        private void FillData(BEP_Detail selectedBEP)
        {
            try
            {
                nlog.Trace("BEPDetails:FillData::Leaving");

                lblDesc.Text = selectedBEP.Description;
                lblQtyRcvd.Text = selectedBEP.TotalRCVD.ToString();
                lblQtySold.Text = selectedBEP.TotalSold.ToString();
                lblSKU.Text = selectedBEP.SKU;
                lblStkCode.Text = selectedBEP.StockCode;
                lblTotCOGS.Text = selectedBEP.TotalCOGS.ToString("C");
                lblTotSale.Text = selectedBEP.TotalSales.ToString("C");
                lblUPC.Text = selectedBEP.UPC;
                lblVendor.Text = selectedBEP.Vendor;
                lblMargin.Text = selectedBEP.ProfitMargin.ToString("C");

                lblAvgCOGS.Text = selectedBEP.AvgCOGS.ToString("C");
                lblAvgSellPrice.Text = selectedBEP.AvgSalePrice.ToString("C");

                lblLastSaleDate.Text = selectedBEP.LastSaleDate.ToString("MM/dd/yyyy");
                if (selectedBEP.ProfitMargin > 0)
                {
                    lblStatus.Text = "Profit";
                    lblStatus.BackColor = System.Drawing.Color.Green;
                    lblStatus.ForeColor = System.Drawing.Color.White;
                    tdStatus.BgColor = "GREEN";
                }
                else
                {
                    lblStatus.Text = "Loss";
                    lblStatus.BackColor = System.Drawing.Color.Red;
                    lblStatus.ForeColor = System.Drawing.Color.White;
                    tdStatus.BgColor = "RED";
                }

                txtProductCost.Text = selectedBEP.AvgCOGS.ToString("F2");


                lkSalesDetail.PostBackUrl = "SalesDetails.aspx?PID=" + selectedBEP.PC_ID.ToString();
                lkSalesTrend.PostBackUrl = "SalesTrend.aspx?PID=" + selectedBEP.PC_ID.ToString();
                lkPODetails.PostBackUrl = "PODetails.aspx?PID=" + selectedBEP.PC_ID.ToString();


            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPDetails:FillData::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPDetails:FillData::Leaving");
            }
        }
        private void InitiateChart(BEP_Detail selectedBEP)
        {
            nlog.Trace("BEPDetails:InitiateChart::Entering");
            try
            {


                UnitCost = selectedBEP.AvgCOGS;
                VariableCost = 0;

                SuggestRetail = selectedBEP.SuggRetail;
                RcvdQty = selectedBEP.TotalRCVD;
                TotalSold = selectedBEP.TotalSold;
                totalCost = selectedBEP.TotalCOGS;
                AvgSellPrice = selectedBEP.AvgSalePrice;


                RefreshChart();

            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPDetails:InitiateChart::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPDetails:InitiateChart::Leaving");
            }
        }

        private void RefreshChart()
        {
            nlog.Trace("BEPDetails:RefreshChart::Entering");
            try
            {
                double FixedCost;
                double[] VariableCostTable, FixedCostTable, TotalCostTable, RevenueTable, SaleRealised, BreakEven;





                FixedCost = UnitCost * RcvdQty;
                long cntUpdater = 1;
                long saleCntUpdater = 1;
                if (RcvdQty > 1000)
                { cntUpdater = (long)(RcvdQty / 100); saleCntUpdater = (long)(TotalSold / 100); }


                long[] noOfProducts;
                long[] noOfProducts1;

                if (cntUpdater > 1)
                {
                    VariableCostTable = new double[cntUpdater + 1];
                    FixedCostTable = new double[cntUpdater + 1];
                    TotalCostTable = new double[cntUpdater + 1];
                    RevenueTable = new double[cntUpdater + 1];
                    SaleRealised = new double[saleCntUpdater + 1];
                    noOfProducts = new long[cntUpdater + 1];
                    noOfProducts1 = new long[saleCntUpdater + 1];
                    BreakEven = new double[cntUpdater + 1];
                }
                else
                {
                    VariableCostTable = new double[RcvdQty + 1];
                    FixedCostTable = new double[RcvdQty + 1];
                    TotalCostTable = new double[RcvdQty + 1];
                    RevenueTable = new double[RcvdQty + 1];
                    SaleRealised = new double[TotalSold + 1];
                    noOfProducts = new long[RcvdQty + 1];
                    noOfProducts1 = new long[TotalSold + 1];
                    BreakEven = new double[RcvdQty + 1];
                }




                for (long i = 0, j = 0; j <= RcvdQty; i++)
                {
                    double i1 = Convert.ToDouble(i );
                    VariableCostTable[i] = VariableCost;
                    FixedCostTable[i] = (VariableCostTable[i] * RcvdQty) + FixedCost;
                    if (cntUpdater > 1)
                    {
                        TotalCostTable[i] = (VariableCostTable[i] + UnitCost) * i1 * 100;
                        RevenueTable[i] = i1 * 100 * SuggestRetail;
                        noOfProducts[i] = (long)(i1 * 100);
                        j = j + 100;
                    }
                    else
                    {
                        TotalCostTable[i] = (VariableCostTable[i] + UnitCost) * i1;
                        RevenueTable[i] = i1 * SuggestRetail;
                        noOfProducts[i] = (long)i1;
                        j++;
                    }


                }

                for (long i = 0, j = 0; j <= TotalSold; i++)
                {
                    if (saleCntUpdater > 1)
                    {
                        SaleRealised[i] = i * 100 * AvgSellPrice;
                        noOfProducts1[i] = (long)(i * 100);
                        j = j + 100;
                    }
                    else
                    {
                        SaleRealised[i] = i * AvgSellPrice;
                        noOfProducts1[i] = (long)(i);
                       
                        j++;
                    }
                }

                chProductDetails.Series.Clear();
                chProductDetails.Legends.Clear();


                Legend lg = new Legend("Parameters");
                lg.Font = new System.Drawing.Font("Verdana", 7, System.Drawing.FontStyle.Bold);
                
                lg.Docking = Docking.Top;
                
                chProductDetails.Legends.Add(lg);


                Series srsVariable = new Series("Variable");
                srsVariable.ChartType = SeriesChartType.Area;
                srsVariable.Points.DataBindXY(noOfProducts, "Items", VariableCostTable, "Variable");
                srsVariable.BorderDashStyle = ChartDashStyle.Solid;
                srsVariable.BorderWidth = 10;
                srsVariable.Color = System.Drawing.Color.FromArgb(127, System.Drawing.Color.Violet);
                srsVariable.ToolTip = "Variable Cost: #VALY{C}";
                srsVariable.Legend = "Parameters";
                srsVariable.LegendText = "Varaible";

                chProductDetails.Series.Add(srsVariable);





                Series srsFixed = new Series("Fixed");
                srsFixed.ChartType = SeriesChartType.Line;
                srsFixed.Points.DataBindXY(noOfProducts, "Items", FixedCostTable, "Fixed");
                srsFixed.BorderDashStyle = ChartDashStyle.Solid;
                srsFixed.BorderWidth = 3;
                srsFixed.Color = System.Drawing.Color.FromArgb(127, System.Drawing.Color.Blue);
                srsFixed.ToolTip = "Fixed Cost: #VALY{C}";
                chProductDetails.Series.Add(srsFixed);
                srsFixed.Legend = "Parameters";
                srsFixed.LegendText = "Fixed + Variable";
                //lg = new Legend("Fixed + Variable");
                //lg.Font = new System.Drawing.Font("Verdana", 6);
                //chProductDetails.Legends.Add(lg);





                Series srsRevenue = new Series("Revenue");
                srsRevenue.ChartType = SeriesChartType.Area;
                srsRevenue.Points.DataBindXY(noOfProducts, "Items", RevenueTable, "Revenue");
                srsRevenue.BorderDashStyle = ChartDashStyle.Solid;
                srsRevenue.BorderDashStyle = ChartDashStyle.Dash;
                srsRevenue.BorderWidth = 10;
                srsRevenue.Color = System.Drawing.Color.FromArgb(127, System.Drawing.Color.Green);
                srsRevenue.ToolTip = "Revenue: #VALY{C}";
                chProductDetails.Series.Add(srsRevenue);

                srsRevenue.Legend = "Parameters";
                srsRevenue.LegendText = "Revenue";
                
                //lg = new Legend("Revenue");
                //lg.Font = new System.Drawing.Font("Verdana", 6);
                //chProductDetails.Legends.Add(lg);


                Series srsTotalCost = new Series("TotalCost");
                srsTotalCost.ChartType = SeriesChartType.Area;
                srsTotalCost.Points.DataBindXY(noOfProducts, "Items", TotalCostTable, "TotalCost");
                srsTotalCost.BorderDashStyle = ChartDashStyle.Solid;
                srsTotalCost.BorderWidth = 10;
                srsTotalCost.Color = System.Drawing.Color.FromArgb(127, System.Drawing.Color.Red);
                // srsTotalCost.ToolTip = "Cost: #VALY";
                chProductDetails.Series.Add(srsTotalCost);
                srsTotalCost.Legend = "Parameters";
                srsTotalCost.LegendText = "TotalCost";
                
                //lg = new Legend("TotalCost");
                //lg.Font = new System.Drawing.Font("Verdana", 6);
                //chProductDetails.Legends.Add(lg);

                Series srsSalesRealised = new Series("SalesRealised");
                srsSalesRealised.ChartType = SeriesChartType.Area;
                srsSalesRealised.Points.DataBindXY(noOfProducts1, "Items", SaleRealised, "Sales Realised");
                srsSalesRealised.BorderDashStyle = ChartDashStyle.Solid;
                srsSalesRealised.BorderDashStyle = ChartDashStyle.Dash;
                srsSalesRealised.BorderWidth = 10;
                srsSalesRealised.Color = System.Drawing.Color.FromArgb(127, System.Drawing.Color.Yellow);
                srsSalesRealised.ToolTip = "Sales Realised: #VALY{C}";
                chProductDetails.Series.Add(srsSalesRealised);
                srsSalesRealised.Legend = "Parameters";
                srsSalesRealised.LegendText = "Sales Realised";
                

                //lg = new Legend("Sales Realised");
                //lg.Font = new System.Drawing.Font("Verdana", 6);
                //chProductDetails.Legends.Add(lg);

                double y = (UnitCost * RcvdQty) + (RcvdQty * VariableCost);
                long x1 = (long)Math.Ceiling(y / AvgSellPrice);
                long x = (long)Math.Ceiling(y / SuggestRetail);



                Series srsBEP = new Series("Break Even Point (Sugg Retail)");
                srsBEP.ChartType = SeriesChartType.Point;
                // srsBEP.Points.DataBindXY(noOfProducts, "Items", BreakEven, "Break Even Point");
                srsBEP.Points.AddXY(x, y);
                srsBEP.MarkerStyle = MarkerStyle.Circle;
                srsBEP.MarkerSize = 20;
                srsBEP.MarkerColor = System.Drawing.Color.Purple;
                srsBEP.IsVisibleInLegend = true;
                srsBEP.BorderWidth = 0;
                srsBEP.Color = System.Drawing.Color.Purple;
                srsBEP.ToolTip = "Break Even on Sugg Retail: Qty #VALX :: Price #VALY{C}";
                srsBEP.Legend = "Parameters";
                srsBEP.LegendText = "BEP (Sugg Retail)";
               
                chProductDetails.Series.Add(srsBEP);


                //lg = new Legend("Break Even Point (Sugg Retail)");
                //lg.Font = new System.Drawing.Font("Verdana", 6);
                //chProductDetails.Legends.Add(lg);

                Series srsBEP1 = new Series("Break Even Point (Avg Sell Price)");
                srsBEP1.ChartType = SeriesChartType.Point;
                // srsBEP.Points.DataBindXY(noOfProducts, "Items", BreakEven, "Break Even Point");
                srsBEP1.Points.AddXY(x1, y);
                srsBEP1.MarkerStyle = MarkerStyle.Circle;
                srsBEP1.MarkerSize = 20;
                srsBEP1.MarkerColor = System.Drawing.Color.Red;
                srsBEP1.IsVisibleInLegend = true;
                srsBEP1.BorderWidth = 0;
                srsBEP1.Color = System.Drawing.Color.Purple;
                srsBEP1.ToolTip = "Break Even on Avg Sell Price: Qty #VALX :: Price #VALY{C}";
                srsBEP1.Legend = "Parameters";
                srsBEP1.LegendText = "BEP (Avg Sell Price)";
               
                chProductDetails.Series.Add(srsBEP1);


                //lg = new Legend("Break Even Point (Avg Sell Price)");
                //lg.Font = new System.Drawing.Font("Verdana", 6);

                //chProductDetails.Legends.Add(lg);






                //txtVariableCost.Text = VariableCost.ToString();

                lblPotentialRevenue.Text = (SuggestRetail * RcvdQty).ToString("C");
                lblPotentialProfit.Text = ((SuggestRetail * RcvdQty) - (totalCost + (VariableCost * RcvdQty))).ToString("C");
                lblBreakEvenQty.Text = Math.Ceiling(((UnitCost + VariableCost) * RcvdQty) / SuggestRetail).ToString();
                lblBreakEvenPnt.Text = ((UnitCost + VariableCost) * RcvdQty).ToString("C");



                slVariableCost.Minimum = 0;
                slVariableCost.Maximum = 100;


                slTotalProducts.Maximum = RcvdQty + 100;
                if (RcvdQty - 100 > 0)
                    slTotalProducts.Minimum = RcvdQty - 100;
                else
                    slTotalProducts.Minimum = 0;

                slExtProductCost.Maximum = UnitCost + 100;
                if (UnitCost - 100 > 0)
                    slExtProductCost.Minimum = UnitCost - 100;
                else
                    slExtProductCost.Minimum = 0;


                slSuggestRetail.Maximum = SuggestRetail + 100;
                if (SuggestRetail - 100 > 0)
                    slSuggestRetail.Minimum = SuggestRetail - 100;
                else
                    slSuggestRetail.Minimum = 0;

                txtProductCost.Text = UnitCost.ToString();
                txtSuggestRetail.Text = SuggestRetail.ToString();
                txtTotalProducts.Text = RcvdQty.ToString();

            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPDetails:RefreshChart::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPDetails:RefreshChart::Leaving");
            }
        }




        protected void txtProductCost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                nlog.Trace("BEPDetails:txtProductCost_TextChanged::Entering");
                UnitCost = double.Parse(txtProductCost.Text);
                RefreshChart();


            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPDetails:txtProductCost_TextChanged::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPDetails:txtProductCost_TextChanged::Leaving");
            }
        }

        protected void txtVariableCost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                nlog.Trace("BEPDetails:txtProductCost_TextChanged::Entering");
                VariableCost = double.Parse(txtVariableCost.Text);
                VariableCost = (UnitCost * VariableCost) / 100;
                RefreshChart();


            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPDetails:txtProductCost_TextChanged::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPDetails:txtProductCost_TextChanged::Leaving");
            }
        }

        protected void txtTotalProducts_TextChanged(object sender, EventArgs e)
        {
            try
            {
                nlog.Trace("BEPDetails:txtProductCost_TextChanged::Entering");
                RcvdQty = int.Parse(txtTotalProducts.Text);
                RefreshChart();


            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPDetails:txtProductCost_TextChanged::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPDetails:txtProductCost_TextChanged::Leaving");
            }
        }


        protected void txtSuggestRetail_TextChanged(object sender, EventArgs e)
        {
            try
            {
                nlog.Trace("BEPDetails:txtProductCost_TextChanged::Entering");
                SuggestRetail = double.Parse(txtSuggestRetail.Text);
                RefreshChart();


            }
            catch (Exception ex)
            {
                nlog.ErrorException("BEPDetails:txtProductCost_TextChanged::Error", ex);
            }
            finally
            {
                nlog.Trace("BEPDetails:txtProductCost_TextChanged::Leaving");
            }
        }

    }
}