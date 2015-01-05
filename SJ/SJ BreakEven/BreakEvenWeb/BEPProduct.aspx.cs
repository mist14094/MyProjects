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
using BreakEvenBAL;
public partial class BEPProduct : System.Web.UI.Page
{
    BEBAL ObjBEBal = new BEBAL();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PID"] != "")
        {
            if (!IsPostBack)
            {
                //SaleValuesNew.Attributes.Add("onclick", "window.open('SalesDetails.aspx?PID=" + Request.QueryString["PID"] + "','name','height=550, width=790,toolbar=no,directories=no,status=no, menubar=no,scrollbars=yes,resizable=no,top=70,lef'); return false;");

                SaleValuesNew.Attributes.Add("onclick", "PopupCenter('SalesDetails.aspx?PID=" + Request.QueryString["PID"] + "', 'Sales Details',1000,500);");
                PurchaseDetails.Attributes.Add("onclick", "PopupCenter('PurchaseDetails.aspx?PID=" + Request.QueryString["PID"] + "', 'Purchase Details',1100,500);");
                SalesTrendHyper.Attributes.Add("onclick", "PopupCenter('SalesTrend.aspx?PID=" + Request.QueryString["PID"] + "', 'Sales Trend',1100,500);");
                int QTYSOLD;
                string strPID = Request.QueryString["PID"];
                lblGenerated.Visible = true;
                lblGenerated.Text = "Generated on " + DateTime.Now.ToString("F");
                if (strPID != "" && strPID != null)
                {
                    DataSet dsBreakEvenValues = new DataSet();
                    dsBreakEvenValues = ObjBEBal.GetBreakEvenValues(strPID);
                    QTYSOLD = Convert.ToInt16(dsBreakEvenValues.Tables[0].Rows[0]["Quantitysold"].ToString());
                    lblUPC.Text = dsBreakEvenValues.Tables[0].Rows[0]["UPC"].ToString();
                    lblSKU.Text = dsBreakEvenValues.Tables[0].Rows[0]["SKU"].ToString();
                    lblDesc.Text = dsBreakEvenValues.Tables[0].Rows[0]["ItemDesc"].ToString();
                    lblMoneyRealised.Text = Math.Round(Convert.ToDouble(dsBreakEvenValues.Tables[0].Rows[0]["SalesRealised"].ToString()), 2).ToString();
                    lblSold.Text = dsBreakEvenValues.Tables[0].Rows[0]["Quantitysold"].ToString();
                    lblTotalcst.Text = Math.Round(Convert.ToDouble(dsBreakEvenValues.Tables[0].Rows[0]["COGS"].ToString()), 2).ToString();
                    lblReceivedQuantity.Text = dsBreakEvenValues.Tables[0].Rows[0]["TotalProducts"].ToString();
                    lblAvg.Text = "Average Selling price : " + Math.Round(Convert.ToDouble(dsBreakEvenValues.Tables[0].Rows[0]["AverageSellingPrice"].ToString()), 2).ToString() + " $";
                    lblAvgCost.Text = "Average COGS : " + Math.Round(Convert.ToDouble(dsBreakEvenValues.Tables[0].Rows[0]["AverageCOGS"].ToString()), 2).ToString() + " $";
                    if (Convert.ToDouble(lblMoneyRealised.Text) - (Convert.ToDouble(lblTotalcst.Text)) > 0)
                    {
                        lblZone.Text = " Profit <br //>$ " + (Math.Round(Convert.ToDouble(lblMoneyRealised.Text) - (Convert.ToDouble(lblTotalcst.Text)), 2)).ToString() + "";
                        lblBreakEvn.Text = "This product has already broken even";
                        lblBreakEvn.ForeColor = System.Drawing.Color.Green;
                        lblBreakEvn.Font.Bold = true;
                        lblBreakEvn.Font.Underline = true;
                        tdProfit.Attributes.Remove("class");
                        tdProfit.Attributes.Add("class", "style2Green");
                    }
                    else
                    {
                        lblZone.Text = " Loss <br //>$ (" + (Math.Round(Convert.ToDouble(lblMoneyRealised.Text) - (Convert.ToDouble(lblTotalcst.Text)), 2)).ToString() + ")";
                        lblBreakEvn.Text = "Yet to Break-Even";
                        lblBreakEvn.ForeColor = System.Drawing.Color.Red;
                        lblBreakEvn.Font.Bold = true;
                        tdProfit.Attributes.Remove("class");
                        tdProfit.Attributes.Add("class", "style2Red");
                    }

                    OnLoadsetValues(Math.Round(Convert.ToDouble(dsBreakEvenValues.Tables[0].Rows[0]["AverageCOGS"].ToString()), 2), 0, Math.Round(Convert.ToDouble(dsBreakEvenValues.Tables[0].Rows[0]["SuggestedRetail"].ToString()), 2), Convert.ToInt32(dsBreakEvenValues.Tables[0].Rows[0]["TotalProducts"].ToString()), Convert.ToInt32(dsBreakEvenValues.Tables[0].Rows[0]["Quantitysold"].ToString()), Math.Round(Convert.ToDouble(dsBreakEvenValues.Tables[0].Rows[0]["AverageSellingPrice"].ToString()), 2));

                }
            }
        }
    }

    public void OnLoadsetValues(double ProductCost, double VariableCost, double RetailPrice, int TotalProducts, int QTYSOLD, double AveragePrice)
    {
        if (RetailPrice <= 0.01)
        {
            RetailPrice = AveragePrice;
        }
        ProductCost = Math.Round(ProductCost, 2);
        VariableCost = Math.Round(VariableCost, 2);
        RetailPrice = Math.Round(RetailPrice, 2);
        
        InitateGraph(TotalProducts, VariableCost, ProductCost, RetailPrice, QTYSOLD);
        txtProductCost.Text = ProductCost.ToString();
        if (ProductCost < 100)
        {
            txtProductCostSE.Minimum = 0;
            txtProductCostSE.Maximum = 100;
        }
        else
        {
            txtProductCostSE.Minimum = 0;
            txtProductCostSE.Maximum =ProductCost * 2;
        }


        txtVariableCost.Text = VariableCost.ToString();
        if (VariableCost < 100)
        {
            txtVariableCostSliderExtender.Minimum = 0;
            txtVariableCostSliderExtender.Maximum = 100;
        }
        else
        {
            txtVariableCostSliderExtender.Minimum = 0;
            txtVariableCostSliderExtender.Maximum = VariableCost * 2;
        }



        txtRetailPrice.Text = RetailPrice.ToString();
        if (RetailPrice < 100)
        {
            txtRetailPriceSE.Minimum = 0;
            txtRetailPriceSE.Maximum = 100;
        }
        else
        {
            txtRetailPriceSE.Minimum = 0;
            txtRetailPriceSE.Maximum = RetailPrice * 2;
        }


        txtTotalProducts.Text = TotalProducts.ToString();
        if (TotalProducts < 100)
        {
            txtTotalProductsSE.Minimum = QTYSOLD + 1;
            txtTotalProductsSE.Maximum = 100;
        }
        else
        {
            txtTotalProductsSE.Minimum = 0;
            txtTotalProductsSE.Maximum = TotalProducts * 2;
        }

    }
 
    public void InitateGraph(int TotalProducts, double VariableCost, double ProductCost, double RetailPrice, int QTYSOLD)
    {
        this.C1LineChart1.Dispose();
        double orginalvar = VariableCost;
        VariableCost = VariableCost * (ProductCost / 100);

        lblProductCost.Text = ProductCost.ToString();
        lblTotalProducts.Text = TotalProducts.ToString();
        lblVariableCost.Text = VariableCost.ToString() + " / (" + orginalvar.ToString() + "% )";
        lblRetailPrice.Text = RetailPrice.ToString();

        this.C1LineChart1.SeriesList.Clear();

        double FixedCost;
        double[] VariableCostTable, FixedCostTable, TotalCostTable, RevenueTable, LossTableX, LossTableY, ProfitTableX, ProfitTableY;

        //   TotalProducts = 12; VariableCost = 2; ProductCost = 9.135; RetailPrice = 27;

        FixedCost = ProductCost * TotalProducts;
        VariableCostTable = new double[TotalProducts + 1];
        FixedCostTable = new double[TotalProducts + 1];
        TotalCostTable = new double[TotalProducts + 1];
        RevenueTable = new double[TotalProducts + 1];




        double[] ItemTable;
        int cnt = TotalProducts + 1;
        ItemTable = new double[cnt];

        for (int i = 0; i <= TotalProducts; i++)
        {
            double i1 = Convert.ToDouble(i);
            ItemTable[i] = i1;
            VariableCostTable[i] = i1 * VariableCost;
            FixedCostTable[i] = FixedCost;
            TotalCostTable[i] = VariableCostTable[i] + FixedCostTable[i];
            RevenueTable[i] = i1 * RetailPrice;

        }
        var series = new LineChartSeries();
        this.C1LineChart1.SeriesList.Add(series);
        this.C1LineChart1.ShowChartLabels = false;
        this.C1LineChart1.Type = LineChartType.Area;

        series.Markers.Visible = false;
        series.Markers.Type = MarkerType.Circle;
        series.Data.X.AddRange(ItemTable);
        series.Data.Y.AddRange(TotalCostTable);
        series.Label = "Total Cost";

        series.LegendEntry = true;


        var series1 = new LineChartSeries();
        this.C1LineChart1.SeriesList.Add(series1);
        series1.Markers.Visible = false;
        //series1.Markers.Type = MarkerType.Circle;
        series1.Data.X.AddRange(ItemTable);
        series1.Data.Y.AddRange(FixedCostTable);
        series1.Label = "Fixed Cost";
        series1.LegendEntry = true;




        var series2 = new LineChartSeries();
        this.C1LineChart1.SeriesList.Add(series2);
        series2.Markers.Visible = false;
        //series1.Markers.Type = MarkerType.Circle;
        series2.Data.X.AddRange(ItemTable);
        series2.Data.Y.AddRange(VariableCostTable);
        series2.Label = "Variable Cost";
        series2.LegendEntry = true;





        var series3 = new LineChartSeries();
        this.C1LineChart1.SeriesList.Add(series3);
        series3.Markers.Visible = false;
        //series1.Markers.Type = MarkerType.Circle;
        series3.Data.X.AddRange(ItemTable);
        series3.Data.Y.AddRange(RevenueTable);
        series3.Label = "Revenue";
        series3.LegendEntry = true;
        //  series3.Visible = false;



        LossTableX = new double[2];
        LossTableY = new double[2];
        LossTableX[0] = 0;
        LossTableX[1] = TotalCostTable[TotalProducts] / RetailPrice; //4.94;
        LossTableY[0] = 0;
        LossTableY[1] = TotalCostTable[TotalProducts];
        var series4 = new LineChartSeries();
        this.C1LineChart1.SeriesList.Add(series4);
        series4.Markers.Visible = false;
        //series1.Markers.Type = MarkerType.Circle;
        series4.Data.X.AddRange(LossTableX);
        series4.Data.Y.AddRange(LossTableY);
        series4.Label = "Loss Area";
        series4.LegendEntry = true;

        //this.C1LineChart1.SeriesStyles[4]




        ProfitTableX = new double[2];
        ProfitTableY = new double[2];
        ProfitTableX[0] = TotalCostTable[TotalProducts] / RetailPrice;
        ProfitTableX[1] = TotalProducts;
        ProfitTableY[0] = TotalCostTable[TotalProducts];
        ProfitTableY[1] = TotalProducts * RetailPrice;
        var series5 = new LineChartSeries();
        this.C1LineChart1.SeriesList.Add(series5);
        series5.Markers.Visible = false;
        //series1.Markers.Type = MarkerType.Circle;
        series5.Data.X.AddRange(ProfitTableX);
        series5.Data.Y.AddRange(ProfitTableY);
        series5.Label = "Profit Area";
        series5.LegendEntry = true;


        var series6 = new LineChartSeries();
        this.C1LineChart1.SeriesList.Add(series6);
        series6.Markers.Visible = false;
        series6.Markers.Type = MarkerType.Circle;
        series6.Data.X.Add(TotalCostTable[TotalProducts] / RetailPrice);
        series6.Data.Y.Add(TotalCostTable[TotalProducts]);
        series6.Data.X.Add(TotalCostTable[TotalProducts] / RetailPrice + .01);
        series6.Data.Y.Add(TotalCostTable[TotalProducts] + .01);
        series6.Label = "Break-Even Point";
        series6.LegendEntry = true;

        if (TotalCostTable[TotalProducts] / RetailPrice >= TotalProducts)
        {
            this.C1LineChart1.Header.Text = "You will not Break-Even ";

            this.C1LineChart1.Header.TextStyle.Stroke = System.Drawing.Color.Red;
        }

        else
        {
            double temp1 = TotalCostTable[TotalProducts] / RetailPrice;// -Math.Floor(TotalCostTable[TotalProducts] / RetailPrice) > 0.0000000000000001 ? Convert.ToInt16(Math.Floor(TotalCostTable[TotalProducts] / RetailPrice) + 1) : Convert.ToInt16(Math.Floor(TotalCostTable[TotalProducts] / RetailPrice) + 1);
            int item1;
            if (temp1 - Math.Floor(temp1) > 0.0000000001)
            {
                item1 = Convert.ToInt16( Math.Floor(temp1) + 1);
            }
            else
            {
                item1 = Convert.ToInt16(Math.Floor(temp1) + 1);
            }
            this.C1LineChart1.Header.Text = "You Break-Even at selling " + item1+ " item(s)";
            //this.C1LineChart1.Header.Text = "You Break Even at selling " + (Math.Round(TotalCostTable[TotalProducts] / RetailPrice)).ToString() + " Item(s)";
            this.C1LineChart1.Header.TextStyle.Stroke = System.Drawing.Color.Green;
        }


        var series7 = new LineChartSeries();
        this.C1LineChart1.SeriesList.Add(series7);
        series7.Markers.Visible = false;
        series7.Markers.Type = MarkerType.Diamond;
        series7.Data.X.Add(0);
        series7.Data.Y.Add(0);

        series7.Data.X.Add(QTYSOLD);
        series7.Data.Y.Add(Convert.ToDouble(lblMoneyRealised.Text));

        series7.Label = "Current Sales";
        series7.LegendEntry = true;

        lblTotalCost.Text = (TotalProducts * RetailPrice).ToString();

        double lblBEPDollar1 = Math.Round(TotalCostTable[TotalProducts], 2);
      //  lblBEPDollar.Text = (lblBEPDollar1 >= 0 ? lblBEPDollar1.ToString() : "(" + lblBEPDollar1.ToString() + ")");

        lblBEPDollar.Text = lblTotalcst.Text;

       //lblBEPDollar.Text = (TotalCostTable[TotalProducts]).ToString();
//
        try
        {
            double temp12 = TotalCostTable[TotalProducts] / RetailPrice;// -Math.Floor(TotalCostTable[TotalProducts] / RetailPrice) > 0.0000000000000001 ? Convert.ToInt16(Math.Floor(TotalCostTable[TotalProducts] / RetailPrice) + 1) : Convert.ToInt16(Math.Floor(TotalCostTable[TotalProducts] / RetailPrice) + 1);
            int item12;
            if (temp12 - Math.Floor(temp12) > 0.0000000001)
            {
                item12 = Convert.ToInt16(Math.Floor(temp12) + 1);
            }
            else
            {
                item12 = Convert.ToInt16(Math.Floor(temp12) + 1);
            }
            lblBEPProduct.Text = (item12).ToString();
        }
        catch (Exception ex)
        {
        }
       // lblBEPProduct.Text = (Math.Round(TotalCostTable[TotalProducts] / RetailPrice)).ToString();
        double lpprofit = Math.Round(((TotalProducts * RetailPrice) - TotalCostTable[TotalProducts]), 2);
        lblProfitMade.Text = (lpprofit >= 0 ? lpprofit.ToString() : "(" + lpprofit.ToString() + ")");
        try
        {
            if (QTYSOLD <= RevenueTable.Count())
            {
                lblMoneyMade.Text = RevenueTable[QTYSOLD].ToString();
            }
            else
            {
                lblMoneyMade.Text = " No fo product sold is higher than product received";
            }
        }
        catch (Exception ex)
        {
        }
        this.C1LineChart1.Animation.Direction = LineChartDirection.Horizontal;
        this.C1LineChart1.Animation.Easing = ChartEasing.EaseOutElastic;
        this.C1LineChart1.Animation.Enabled = true;

        //    this.C1LineChart1.SeriesStyles.Add(;
        var TotalCostStyle = new ChartStyle();
        TotalCostStyle.StrokeWidth = 4;
        TotalCostStyle.FillOpacity = -110;

        this.C1LineChart1.SeriesStyles.Add(TotalCostStyle);


        var FixedCostStyle = new ChartStyle();
        FixedCostStyle.Stroke = System.Drawing.Color.Orchid;
        FixedCostStyle.StrokeWidth = 4;
        FixedCostStyle.FillOpacity = -110;
        this.C1LineChart1.SeriesStyles.Add(FixedCostStyle);

        var VariableCostStyle = new ChartStyle();
        VariableCostStyle.Stroke = System.Drawing.Color.Black;
        VariableCostStyle.StrokeWidth = 4;
        VariableCostStyle.FillOpacity = -110;
        this.C1LineChart1.SeriesStyles.Add(VariableCostStyle);

        var RevenueStyle = new ChartStyle();
        RevenueStyle.StrokeWidth = 4;
        RevenueStyle.FillOpacity = -110;
        RevenueStyle.Stroke = System.Drawing.Color.RoyalBlue;
        this.C1LineChart1.SeriesStyles.Add(RevenueStyle);

        var LossAreaStyle = new ChartStyle();
        LossAreaStyle.Fill.Color = System.Drawing.Color.OrangeRed;
        LossAreaStyle.Stroke = System.Drawing.Color.OrangeRed;
        //LossAreaStyle.StrokeOpacity = -100;
        LossAreaStyle.StrokeWidth = 0;
        this.C1LineChart1.SeriesStyles.Add(LossAreaStyle);

        var ProfitAreaStyle = new ChartStyle();
        ProfitAreaStyle.Fill.Color = System.Drawing.Color.Green;
        ProfitAreaStyle.FillOpacity = 0;
        ProfitAreaStyle.Stroke = System.Drawing.Color.Green;
       // ProfitAreaStyle.StrokeOpacity = -100;
        ProfitAreaStyle.StrokeWidth = 0;
        this.C1LineChart1.SeriesStyles.Add(ProfitAreaStyle);

        var BEPPointStyle = new ChartStyle();
        BEPPointStyle.Fill.Color = System.Drawing.Color.Red;
        BEPPointStyle.Stroke = System.Drawing.Color.Red;
        BEPPointStyle.StrokeWidth = 20;
        this.C1LineChart1.SeriesStyles.Add(BEPPointStyle);

        var QTYSOLDStyle = new ChartStyle();
        QTYSOLDStyle.Fill.Color = System.Drawing.Color.Yellow;
        QTYSOLDStyle.Stroke = System.Drawing.Color.Yellow;
        //  QTYSOLDStyle.Stroke. = System.Drawing.Color.Yellow;
        //QTYSOLDStyle.StrokeWidth = 20;
        this.C1LineChart1.SeriesStyles.Add(QTYSOLDStyle);




        if (Math.Round(Convert.ToDouble(lblAvg.Text.Substring(24).Trim('$',' ')), 2) > RetailPrice)
        {
            lblSellingSymbol.Text = "▲";
            lblSellingSymbol.ForeColor = System.Drawing.Color.Lime;
            lblSellingSymbol.Font.Size = 13;

        }

        if (Math.Round(Convert.ToDouble(lblAvg.Text.Substring(24).Trim('$', ' ')), 2) == RetailPrice)
        {
            lblSellingSymbol.Text = "■";
            lblSellingSymbol.ForeColor = System.Drawing.Color.Yellow;
            lblSellingSymbol.Font.Size = 15;

        }

        if (Math.Round(Convert.ToDouble(lblAvg.Text.Substring(24).Trim('$', ' ')), 2) < RetailPrice)
        {
            lblSellingSymbol.Text = "▼";
            lblSellingSymbol.ForeColor = System.Drawing.Color.Red;
            lblSellingSymbol.Font.Size = 13;

        }


        if (Math.Round(ObjBEBal.GetLastFinalPurchaseCost(Request.QueryString["PID"]), 2) > ProductCost)
        {


            lblPurchaseSymbol.Text = "▼";
            lblPurchaseSymbol.ForeColor = System.Drawing.Color.Lime;
            lblPurchaseSymbol.Font.Size = 13;
        }


        if (Math.Round(ObjBEBal.GetLastFinalPurchaseCost(Request.QueryString["PID"]), 2) == ProductCost)
        {
            lblPurchaseSymbol.Text = "■";
            lblPurchaseSymbol.ForeColor = System.Drawing.Color.Yellow;
            lblPurchaseSymbol.Font.Size = 15;

        }


        if (Math.Round(ObjBEBal.GetLastFinalPurchaseCost(Request.QueryString["PID"]), 2) < ProductCost)
        {
            lblPurchaseSymbol.Text = "▲";
            lblPurchaseSymbol.ForeColor = System.Drawing.Color.Red;
            lblPurchaseSymbol.Font.Size = 13;

        }

         
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < this.C1LineChart1.SeriesList[0].Data.Y.Values.Count; i++)
        {
            this.C1LineChart1.SeriesList[0].Data.Y.Values[i].DoubleValue = double.Parse(txtTotalProducts.Text);
        }
        this.C1LineChart1.Animation.Enabled = true;
        this.C1LineChart1.Animation.Duration = 0;
        this.C1LineChart1.Animation.Direction = LineChartDirection.Horizontal;
        this.C1LineChart1.Animation.Easing = ChartEasing.EaseInCubic;
        //this.C1LineChart1.Animation.Direction = LineChartDirection.Horizontal;
        //this.C1LineChart1.Animation.Easing = ChartEasing.
        //this.C1LineChart1.Animation.Enabled = true;

    }
    protected void txtProductCost_TextChanged(object sender, EventArgs e)
    {
        string strPID = Request.QueryString["PID"];

        InitateGraph(int.Parse(txtTotalProducts.Text), double.Parse(txtVariableCost.Text), double.Parse(txtProductCost.Text), double.Parse(txtRetailPrice.Text), Convert.ToInt32(lblSold.Text));
    }
    protected void txtVariableCost_TextChanged(object sender, EventArgs e)
    {
        string strPID = Request.QueryString["PID"];

        InitateGraph(int.Parse(txtTotalProducts.Text), double.Parse(txtVariableCost.Text), double.Parse(txtProductCost.Text), double.Parse(txtRetailPrice.Text), Convert.ToInt32(lblSold.Text));
    }
    protected void txtTotalProducts_TextChanged(object sender, EventArgs e)
    {
        string strPID = Request.QueryString["PID"];

        InitateGraph(int.Parse(txtTotalProducts.Text), double.Parse(txtVariableCost.Text), double.Parse(txtProductCost.Text), double.Parse(txtRetailPrice.Text), Convert.ToInt32(lblSold.Text));
    }
    protected void txtRetailPrice_TextChanged(object sender, EventArgs e)
    {
        string strPID = Request.QueryString["PID"];

        InitateGraph(int.Parse(txtTotalProducts.Text), double.Parse(txtVariableCost.Text), double.Parse(txtProductCost.Text), double.Parse(txtRetailPrice.Text), ObjBEBal.getTotalSoldByPID( strPID));
    }
   
}