using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Shield.Web.UI;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections;
using System.Drawing;

public partial class _Default : System.Web.UI.Page
{
    public static string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
    public static DataTable dtNew;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        string selection = Request.QueryString["selection"];
        string GraphType = Request.QueryString["GraphType"];
        string Title="";
        if (!IsPostBack)
        {
            int numberofDays = 0;
            try
            {
                numberofDays = int.Parse(Request.QueryString["numberofdays"]);
            }
            catch (Exception ex)
            {
                numberofDays = 25;
            }

            dtNew = GetCouponSalesDetail(numberofDays);

            CheckBoxList1.DataSource = dtNew.Columns.Cast<DataColumn>()
                               .Select(x => x.ColumnName)
                               .ToArray();
            CheckBoxList1.DataBind();
            CheckBoxList1.Items.RemoveAt(0);
            
            switch (selection)
            {

                case "CanadaCurrent":
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("EX CAN BLUE"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("EX CAN RED"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJ BLUE"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJ CAN BLUE"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJ RED "))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJN SILVER 100"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Smokin Joes Canadian Ff King"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Smokin Joes Premium Non Filter"))].Selected = true;
                    Title = "Canadian Current";
                    break;

                case "Stacked15Area":
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Smokin Joes Premium Non Filter"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Smokin Joes Canadian Ff King"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJN SILVER 100"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJ RED "))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJ CAN BLUE"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJ BLUE"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("EX CAN RED"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("EX CAN BLUE"))].Selected = true;
                    Title = "$15 Coupon";
                    break;
                case "CanStackedArea":
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("EX CAN BLUE"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("EX CAN RED"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJ CAN BLUE"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Smokin Joes Canadian Ff King"))].Selected = true;
                    Title = "Canadian ";
                    break;

                case "CanLine":
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("EX CAN BLUE"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("EX CAN RED"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJ CAN BLUE"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Smokin Joes Canadian Ff King"))].Selected = true;
                    Title = "Canadian ";
                    break;
                case "MentholSPPacked":
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Smokin Joes Premium Menthol Ki"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Smokin Joes Premium Menthol 10"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Smokin Joes Menthol King"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Smokin Joes Menthol 100"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJP MEN GOLD"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJN MN GLD 100"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJN MEN 100"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJ MEN GOLD 100"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("SMJ MEN GOLD"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("MRKT MEN BLUE"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("MARKT MEN GLD"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Market Menthol King"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Market Menthol 100"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("MAR MEN GOLD 100"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("LEW MEN KING"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("LEW MEN GOLD 100"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("LEW MEN GOLD "))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("LEW MEN 100"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Exact Menthol King"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Exact Menthol 100"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Exact Elite Menthol King"))].Selected = true;
                    CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Exact Elite Menthol 100"))].Selected = true;
                    Title = "Menthol Soft Pack";
                    try
                    {
                        dtNew.Rows[0]["Exact Menthol 100"] = "962";
                    }
                    catch (Exception ex) { }

                    break;
            }

        }
        SetValues(GraphType,Title);
    }

    public void SetValues(string GraphType, string Title)
    {

        bool deletecurrentDate = true;
        ListItem[] result = CheckBoxList1.Items.Cast<ListItem>().Where(x => x.Selected).ToArray();

        DataTable dtBasedonSelection = new DataTable();
        dtBasedonSelection = dtNew.Copy();
        int j = dtBasedonSelection.Columns.Count;
        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {

            if (!CheckBoxList1.Items[i].Selected)
            {
                dtBasedonSelection.Columns.Remove(CheckBoxList1.Items[i].Value);
            }

        }

        if (deletecurrentDate)
        {
            if (DateTime.Parse(dtBasedonSelection.Rows[dtBasedonSelection.Rows.Count - 1]["TicketDate"].ToString()).Date == DateTime.Now.Date)
            {
                dtBasedonSelection.Rows.RemoveAt(dtBasedonSelection.Rows.Count - 1);
            }
        }


        string[] yAxisValues = ReportYAxis(dtBasedonSelection);
        string[] xAxisValues = ReportXAxis(dtBasedonSelection);


        ShieldChart1.DataSeries.Clear();
        ShieldChart1.Axes.Clear();
        //    ShieldChart1.Dispose();
        ShieldChart1.Width = Unit.Percentage(100);
        ShieldChart1.Height = Unit.Pixel(500);
        ShieldChart1.CssClass = "chart";
        ShieldChart1.PrimaryHeader.Text = Title;
        ShieldChart1.SecondaryHeader.Text = "Generated on " + DateTime.Now.ToString("MM/dd/yy hh:mm:ss");
        ShieldChart1.TooltipSettings.AxisMarkers.Enabled = true;
        ShieldChart1.TooltipSettings.AxisMarkers.Mode = ChartXYMode.XY;
        ShieldChart1.TooltipSettings.AxisMarkers.Width = new Unit(1);
        ShieldChart1.TooltipSettings.AxisMarkers.ZIndex = 3;
        // ShieldChart1.ZoomMode = ChartXYMode.XY;

        ChartAxisX axisX = new ChartAxisX();
        axisX.CategoricalValues = xAxisValues;
        axisX.Title.Text = "Date";
        axisX.AxisTickText.TextAngle = 270;
        axisX.AxisTickText.Y = 35;
        //axisX.AxisTickText.
        ShieldChart1.Axes.Add(axisX);

        ChartAxisY axisY = new ChartAxisY();
        axisY.Title.Text = "Quantity";
        axisY.Min = 0;
        ShieldChart1.Axes.Add(axisY);



        for (int i = 0; i < yAxisValues.Count(); i++)
        {



            if (GraphType =="Line")
            {
                ChartLineSeries splineSeriesHouseholds = new ChartLineSeries();
                splineSeriesHouseholds.DataFieldY = yAxisValues[i];
                splineSeriesHouseholds.CollectionAlias = yAxisValues[i];
                //splineSeriesHouseholds.Settings.DrawNullValues = true;
                ShieldChart1.DataSeries.Add(splineSeriesHouseholds);
                ShieldChart1.PrimaryHeader.Text = Title +" - [Line]";
            }
            else
            {
                ChartAreaSeries splineSeriesHouseholds = new ChartAreaSeries();
                splineSeriesHouseholds.DataFieldY = yAxisValues[i];
                splineSeriesHouseholds.CollectionAlias = yAxisValues[i];
                splineSeriesHouseholds.Settings.StackMode = ChartStackMode.Normal;
                //splineSeriesHouseholds.Settings.DrawNullValues = true;
                ShieldChart1.DataSeries.Add(splineSeriesHouseholds);
                ShieldChart1.PrimaryHeader.Text = Title + " - [Stacked]";
            }





        }

        ShieldChart1.DataSource = dtBasedonSelection;
        ShieldChart1.DataBind();

    }




    public string[] ReportXAxis(DataTable dt)
    {
        var x1 = (from r in dt.AsEnumerable()
                  select r["TicketDate"]).Distinct().ToList();
        string[] arr = ((IEnumerable)x1).Cast<object>()
                                 .Select(x => x.ToString().Substring(0, x.ToString().IndexOf(" ")))
                                 .ToArray();
        return arr;
    }

    public string[] ReportYAxis(DataTable dt)
    {
        DataTable dttemp = new DataTable();
        dttemp = dt.Clone();
        try
        {
            dttemp.Columns.Remove("ticketdate");
        }
        catch (Exception ex)
        {
        }
        string[] result = dttemp.Columns.Cast<DataColumn>()
                                .Select(x => x.ColumnName)
                                .ToArray();
        return result;


    }

    public DataTable GetCouponSalesDetail(int NoOfDays)
    {
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand("GetCouponSalesDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);

            for (int i = 0; i < allData.Rows.Count; i++)
            {
                for (int j = 0; j < allData.Columns.Count; j++)
                {
                    if (allData.Rows[i][j].ToString() == "")
                    {
                        allData.Rows[i][j] = 0;
                    }
                }
            }
            connection.Close();
        }
        catch
        {
            connection.Close();
        }
        return allData;
    }



    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}