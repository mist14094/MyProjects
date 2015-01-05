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
    public partial class SalesTrend : System.Web.UI.Page
    {
        private NLog.Logger nlog = LogManager.GetCurrentClassLogger();
        private static BEP_Detail selectedBEP;
        protected void Page_Load(object sender, EventArgs e)
        {
            nlog.Trace("BEPDetails:Page_Load::Entering");
            try
            {
                //if (!Page.IsPostBack)
                //{
                string strPID = Request.QueryString["PID"];


                BEP_Detail objSelectedBEP = CacheWrapper.GetInstance().GetBEPItem(long.Parse(strPID));
                selectedBEP = objSelectedBEP;
                FillData(selectedBEP);
                InitiateChart(selectedBEP);
                //  }

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
                lkSalesDetail.PostBackUrl = "SalesDetails.aspx?PID=" + selectedBEP.PC_ID.ToString();
                //lkSalesTrend.PostBackUrl = "SalesTrend.aspx?PID=" + selectedBEP.PC_ID.ToString();
                lkPODetails.PostBackUrl = "PODetails.aspx?PID=" + selectedBEP.PC_ID.ToString();
                lkBEPDetails.PostBackUrl = "BEPDetails.aspx?PID=" + selectedBEP.PC_ID.ToString();
               
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

                SalesDetail objSD = new SalesDetail();
                DataSet ds = objSD.GetSalesTrend(selectedBEP.PC_ID);

                chSaleTrends.Series.Clear();

                chSaleTrends.DataBindTable(ds.Tables[1].DefaultView, "SDate");
                if (chSaleTrends.Series.Count > 0)
                {
                    chSaleTrends.Series[0].ChartType = SeriesChartType.Area;
                    chSaleTrends.Series[0].BorderDashStyle = ChartDashStyle.Solid;
                    chSaleTrends.Series[0].ToolTip = "Date : #VALX , Total Qty : #VALY ";
                    chSaleTrends.Series[0].BorderWidth = 10;
                    chSaleTrends.Series[0].MarkerStyle = MarkerStyle.Diamond;
                    chSaleTrends.Series[0].MarkerSize = 10;
                    chSaleTrends.Series[0].MarkerColor = System.Drawing.Color.Blue;
                    chSaleTrends.Series[0].Color = System.Drawing.Color.Green;



                }


                chSalesStorePie.Series.Clear();
                chSalesStorePie.Click += new ImageMapEventHandler(chSalesStorePie_Click);
                // chSalesStorePie.DataSource = ds.Tables[0].DefaultView;
                chSalesStorePie.Series.Clear();



                chSalesStorePie.Series.Add(new Series());
                if (chSalesStorePie.Series.Count > 0)
                {
                    chSalesStorePie.Series[0].ChartType = SeriesChartType.Pie;
                    chSalesStorePie.Series[0].BorderDashStyle = ChartDashStyle.Solid;

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        //chSalesStorePie.Series[0].YValueMembers = "TotalSale";
                        //chSalesStorePie.Series[0].XValueMember = "StoreName";
                        DataPoint dp = new DataPoint();
                        dp.SetValueXY(dr["StoreName"].ToString(), new object[] { dr["TotalSale"].ToString() });
                        dp.ToolTip = "Store Name : " + dr["StoreName"].ToString() + ", Total Sales : " + double.Parse(dr["TotalSale"].ToString()).ToString("C");

                        dp.PostBackValue = dr["StoreID"].ToString();
                        chSalesStorePie.Series[0].Points.Add(dp);

                    }
                  
                    chSalesStorePie.Series[0].BorderWidth = 10;
                    chSalesStorePie.Series[0].Color = System.Drawing.Color.Green;
                 }


                double min, max;
                string store;
                long maxQty;
                min = 0; max = 0; store = ""; maxQty = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    double min1, max1;
                    string store1;
                    long maxQty1;

                    min1 = double.Parse(dr["MinPrice"].ToString());
                    max1 = double.Parse(dr["MaxPrice"].ToString());
                    maxQty1 = long.Parse(dr["TotalQty"].ToString());
                    store1 = dr["StoreName"].ToString();

                    if (min1 < min || min == 0)
                    {
                        min = min1;
                    }
                    if (max1 > max)
                    {
                        max = max1;
                    }
                    if (maxQty < maxQty1)
                    {
                        maxQty = maxQty1;
                        store = store1 + " (" + maxQty.ToString() + ")";
                    }
                }

                lblMinPrice.Text = min.ToString("C");
                lblMaxPrice.Text = max.ToString("C");
                lblStoreName.Text = store;


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

        public void chSalesStorePie_Click(object sender, ImageMapEventArgs e)
        {
            nlog.Trace("SalesTrend:chSalesStorePie_Click::Entering");
            try
            {
                string str = e.PostBackValue;
                Response.Redirect(Request.RawUrl.Replace("SalesTrend.aspx?PID=" + selectedBEP.PC_ID.ToString(), "") + "/SalesDetails.aspx?PID=" + selectedBEP.PC_ID.ToString() + "&StoreID=" + str, false);
                        
            }
            catch (Exception ex)
            {
                nlog.ErrorException("SalesTrend:chSalesStorePie_Click::Error", ex);
            }
            finally
            {
                nlog.Trace("SalesTrend:chSalesStorePie_Click::Leaving");
            }
        }


    }
}