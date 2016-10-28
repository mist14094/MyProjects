using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shield.Web.UI;

public partial class StoreTransaction : System.Web.UI.Page
{

   private StoreTransCount Count = new StoreTransCount();
    public static string _RFIDSystem = ConfigurationManager.ConnectionStrings["EPMString"].ConnectionString;
    public static DataTable dtNew;


    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            StartDate.Text = DateTime.Now.AddDays(-30).Date.ToShortDateString();
            EndDate.Text = DateTime.Now.AddDays(-1).Date.ToShortDateString();
            ddlStoreID.Items.Add(new ListItem("2", "2"));
            ddlStoreID.Items.Add(new ListItem("3", "3"));
            ddlStoreID.Items.Add(new ListItem("4", "4"));
            ddlStoreID.Items.Add(new ListItem("6", "6"));
            ddlStoreID.Items.Add(new ListItem("7", "7"));
            ddlStoreID.Items.Add(new ListItem("8", "8"));
            ddlStoreID.Items.Add(new ListItem("10", "10"));
            ddlStoreID.Items.Add(new ListItem("ALL", "0"));
            ddlStoreID.SelectedValue = "0";
        }



        string GraphType = Request.QueryString["GraphType"];
        string Title = "";
        if (!IsPostBack)
        {
            int StoreID = 0;
            try
            {
                StoreID = int.Parse(ddlStoreID.DataTextField);
            }
            catch (Exception ex)
            {
                StoreID = 0;
            }
            Title = "Store " + StoreID.ToString()=="0"?"All": StoreID.ToString();
            dtNew = GetCouponSalesDetail(StoreID);
        }
        else
        {
            ShieldChart1.PrimaryHeader.Text = "No Data Found";
        }


        Title = "Store " + (ddlStoreID.SelectedValue == "0" ? "ALL - " : ddlStoreID.SelectedValue) + " Total Transaction";
        ShieldChart1.DataSeries.Clear();
        ShieldChart1.Axes.Clear();

        SetValues(GraphType, Title);
    }

    public void SetValues(string GraphType, string Title)
    {

        int StoreID = 0;
        try
        {
            if (ddlStoreID.SelectedValue != "ALL")
            {
                StoreID = int.Parse(ddlStoreID.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            StoreID = 7;
        }

        dtNew = GetCouponSalesDetail(StoreID);
        if (dtNew != null)
        {

            bool deletecurrentDate = true;
            //ListItem[] result = CheckBoxList1.Items.Cast<ListItem>().Where(x => x.Selected).ToArray();

            DataTable dtBasedonSelection = new DataTable();
            dtBasedonSelection = dtNew.Copy();
            int j = dtBasedonSelection.Columns.Count;
            //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            //{

            //    if (!CheckBoxList1.Items[i].Selected)
            //    {
            //        dtBasedonSelection.Columns.Remove(CheckBoxList1.Items[i].Value);
            //    }

            //}

            if (deletecurrentDate)
            {
                if (
                    DateTime.Parse(dtBasedonSelection.Rows[dtBasedonSelection.Rows.Count - 1]["TicketDate"].ToString())
                        .Date ==
                    DateTime.Now.Date)
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



                if (GraphType == "Line")
                {
                    ChartLineSeries splineSeriesHouseholds = new ChartLineSeries();
                    splineSeriesHouseholds.DataFieldY = yAxisValues[i];
                    splineSeriesHouseholds.CollectionAlias = yAxisValues[i];
                    //splineSeriesHouseholds.Settings.DrawNullValues = true;
                    ShieldChart1.DataSeries.Add(splineSeriesHouseholds);
                    ShieldChart1.PrimaryHeader.Text = Title + " - [Line]";
                }
                else
                {
                    ChartBarSeries splineSeriesHouseholds = new ChartBarSeries();
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
    }





    public string[] ReportXAxis(DataTable dt)
    {
        var x1 = (from r in dt.AsEnumerable()
            select r["TicketDate"]).Distinct().ToList();
        string[] arr = ((IEnumerable) x1).Cast<object>()
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
        string sqlCommandText = "";
        try
        {
            if (NoOfDays == 0)
            {
                sqlCommandText =
                    "SELECT *  FROM (SELECT  [TicketDate],[str_nbr],[Transactions] FROM [EPM_DW].[dbo].[StoreTransactionCountSummary]) Summary PIVOT (sum(Transactions) FOR str_nbr IN ([2],[3],[4],[6],[7],[9],[10])) AS PVT ORDER BY ticketdate DESC";
            }
            else
            {
                sqlCommandText =
                    string.Format(
                        "SELECT *  FROM (SELECT  [TicketDate],[str_nbr],[Transactions] FROM [EPM_DW].[dbo].[StoreTransactionCountSummary]) Summary PIVOT (sum(Transactions) FOR str_nbr IN ([{0}])) AS PVT ORDER BY ticketdate DESC",
                        NoOfDays.ToString());


            }
            SqlCommand cmd = new SqlCommand(sqlCommandText, connection);
            cmd.CommandType = CommandType.Text;
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
        catch(Exception ex)
        {
            connection.Close();
        }


        try
        {
            allData = allData.Select(@"TicketDate>='" + DateTime.Parse(StartDate.Text)
                                     + "' and TicketDate<='" + DateTime.Parse(EndDate.Text) + "'").CopyToDataTable()
                ;
        }
        catch (Exception)
        {
            allData = null;

        }

        return allData;
    }



    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void ddlStoreID_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void imbDownloadExcel_Click(object sender, ImageClickEventArgs e)
    {
        int StoreID = 0;
        try
        {
            if (ddlStoreID.SelectedValue != "ALL")
            {
                StoreID = int.Parse(ddlStoreID.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            StoreID = 7;
        }

        dtNew = GetCouponSalesDetail(StoreID);
        if (dtNew != null)
        {
            DataTable dtBasedonSelection = new DataTable();
            dtBasedonSelection = dtNew.Copy();
            int j = dtBasedonSelection.Columns.Count;
            //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            //{

            //    if (!CheckBoxList1.Items[i].Selected)
            //    {
            //        dtBasedonSelection.Columns.Remove(CheckBoxList1.Items[i].Value);
            //    }

            //}
           
                if (
                    DateTime.Parse(dtBasedonSelection.Rows[dtBasedonSelection.Rows.Count - 1]["TicketDate"].ToString())
                        .Date ==
                    DateTime.Now.Date)
                {
                    dtBasedonSelection.Rows.RemoveAt(dtBasedonSelection.Rows.Count - 1);

                }

            UploadDataTableToExcel(dtBasedonSelection);


        }
    }

    protected void UploadDataTableToExcel(DataTable dtRecords)
    {
        string Filename = DateTime.Now.ToString("yyyyMMddhhmmsstt") + "_CigSales";
        string XlsPath = Server.MapPath(@"~/Add_data/"+ Filename + ".xls");
        string attachment = string.Empty;
        if (XlsPath.IndexOf("\\") != -1)
        {
            string[] strFileName = XlsPath.Split(new char[] { '\\' });
            attachment = "attachment; filename=" + strFileName[strFileName.Length - 1];
        }
        else
            attachment = "attachment; filename=" + XlsPath;
        try
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = string.Empty;

            foreach (DataColumn datacol in dtRecords.Columns)
            {
                Response.Write(tab + datacol.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");

            foreach (DataRow dr in dtRecords.Rows)
            {
                tab = "";
                for (int j = 0; j < dtRecords.Columns.Count; j++)
                {
                    Response.Write(tab + Convert.ToString(dr[j]));
                    tab = "\t";
                }

                Response.Write("\n");
            }
            Response.End();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
    }

    protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
    {

        SetCigValues();

    }

    public void SetCigValues()
    {

        //var cigarMatrix = CigarMatrix.GetCigarSalesData();

        //if (ddlBrand.SelectedValue != "ALL")
        //{
        //    cigarMatrix = cigarMatrix.Where(matrix => matrix.Brand == ddlBrand.SelectedValue).ToList();
        //}

        //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        //{
        //    if (cigarMatrix.Count(matrix => matrix.Description == CheckBoxList1.Items[i].ToString()) != 0)
        //    {
        //        CheckBoxList1.Items[i].Selected = true;
        //    }
        //    else
        //    {
        //        CheckBoxList1.Items[i].Selected = false;
        //    }
        //    //  CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("EX ELT RED 100"))].Selected = true;



        //}

        Title = "Store " + ddlStoreID.SelectedValue + " Cigar. Sales";
        ShieldChart1.DataSeries.Clear();
        ShieldChart1.Axes.Clear();
        string GraphType = Request.QueryString["GraphType"];
        SetValues(GraphType, Title);
    }

    protected void ddlStyle_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCigValues();
    }

    protected void ddlBS_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCigValues();
    }

    protected void ddlK100_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCigValues();
    }

    protected void ddlMenthol_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCigValues();
    }

    protected void ddlCanadian_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCigValues();
    }

    protected void ddlNonFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetCigValues();

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        //{
        //    CheckBoxList1.Items[i].Selected = false;
        //}
        //ddlBrand.SelectedIndex = ddlBrand.Items.IndexOf(new ListItem("ALL", "ALL"));
        //ddlStyle.SelectedIndex = ddlStyle.Items.IndexOf(new ListItem("ALL", "ALL"));
        //ddlBS.SelectedIndex = ddlBS.Items.IndexOf(new ListItem("ALL", "ALL"));
        //ddlK100.SelectedIndex = ddlK100.Items.IndexOf(new ListItem("ALL", "ALL"));
        //ddlCanadian.SelectedIndex = ddlCanadian.Items.IndexOf(new ListItem("ALL", "ALL"));
        //ddlNonFilter.SelectedIndex = ddlNonFilter.Items.IndexOf(new ListItem("ALL", "ALL"));
        ShieldChart1.DataSeries.Clear();
        ShieldChart1.Axes.Clear();
    }
}

