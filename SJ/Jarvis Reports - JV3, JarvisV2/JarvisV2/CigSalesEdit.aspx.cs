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

public partial class CigSales : System.Web.UI.Page
{
    private CigrteSales CigrteSales = new CigrteSales();
    private CigMatrix CigMatrix = new CigMatrix();
    public static string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
    public static DataTable dtNew;



    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ddlTotalDays.Enabled = false;

            StartDate.Text = DateTime.Now.AddDays(-30).Date.ToShortDateString();
            EndDate.Text = DateTime.Now.AddDays(-1).Date.ToShortDateString();
            var cigSales = CigrteSales.GetCigSalesData();
            ddlStoreID.DataSource =
                cigSales.Select(sales => new {sales.Store}).Distinct().OrderBy(arg => arg.Store);

            ddlStoreID.DataTextField = "Store";
            ddlStoreID.DataValueField = "Store";
            ddlStoreID.DataBind();
            ddlStoreID.Items.Add(new ListItem("ALL", "ALL"));
            ddlStoreID.SelectedValue = "7";

            var cigMatrix = CigMatrix.GetCigSalesData();

            ddlBrand.DataSource = cigMatrix.Select(
                matrix => new {matrix.Brand}).Distinct().OrderBy(arg => arg.Brand);
            ddlBrand.DataTextField = "Brand";
            ddlBrand.DataValueField = "Brand";
            ddlBrand.DataBind();
            ddlBrand.Items.Add(new ListItem("ALL", "ALL"));
            ddlBrand.SelectedValue = "All";
            ddlBrand.SelectedIndex = ddlBrand.Items.IndexOf(new ListItem("ALL", "ALL"));



            ddlStyle.DataSource = cigMatrix.Select(
               matrix => new { matrix.Style }).Distinct().OrderBy(arg => arg.Style);
            ddlStyle.DataTextField = "Style";
            ddlStyle.DataValueField = "Style";
            ddlStyle.DataBind();
            ddlStyle.Items.Add(new ListItem("ALL", "ALL"));
            ddlStyle.SelectedValue = "All";
            ddlStyle.SelectedIndex = ddlStyle.Items.IndexOf(new ListItem("ALL", "ALL"));


            ddlBS.DataSource = cigMatrix.Select(
               matrix => new { matrix.BoxSoft }).Distinct().OrderBy(arg => arg.BoxSoft);
            ddlBS.DataTextField = "BoxSoft";
            ddlBS.DataValueField = "BoxSoft";
            ddlBS.DataBind();
            ddlBS.Items.Add(new ListItem("ALL", "ALL"));
            ddlBS.SelectedValue = "All";
            ddlBS.SelectedIndex = ddlBS.Items.IndexOf(new ListItem("ALL", "ALL"));


            ddlK100.DataSource = cigMatrix.Select(
              matrix => new { matrix.King100 }).Distinct().OrderBy(arg => arg.King100);
            ddlK100.DataTextField = "King100";
            ddlK100.DataValueField = "King100";
            ddlK100.DataBind();
            ddlK100.Items.Add(new ListItem("ALL", "ALL"));
            ddlK100.SelectedValue = "All";
            ddlK100.SelectedIndex = ddlK100.Items.IndexOf(new ListItem("ALL", "ALL"));


            ddlMenthol.DataSource = cigMatrix.Select(
              matrix => new { matrix.Menthol }).Distinct().OrderBy(arg => arg.Menthol);
            ddlMenthol.DataTextField = "Menthol";
            ddlMenthol.DataValueField = "Menthol";
            ddlMenthol.DataBind();
            ddlMenthol.Items.Add(new ListItem("ALL", "ALL"));
            ddlMenthol.SelectedValue = "All";
            ddlMenthol.SelectedIndex = ddlMenthol.Items.IndexOf(new ListItem("ALL", "ALL"));


            ddlCanadian.DataSource = cigMatrix.Select(
              matrix => new { matrix.Canadian }).Distinct().OrderBy(arg => arg.Canadian);
            ddlCanadian.DataTextField = "Canadian";
            ddlCanadian.DataValueField = "Canadian";
            ddlCanadian.DataBind();
            ddlCanadian.Items.Add(new ListItem("ALL", "ALL"));
            ddlCanadian.SelectedValue = "All";
            ddlCanadian.SelectedIndex = ddlCanadian.Items.IndexOf(new ListItem("ALL", "ALL"));



            ddlNonFilter.DataSource = cigMatrix.Select(
              matrix => new { matrix.NonFilter }).Distinct().OrderBy(arg => arg.NonFilter);
            ddlNonFilter.DataTextField = "NonFilter";
            ddlNonFilter.DataValueField = "NonFilter";
            ddlNonFilter.DataBind();
            ddlNonFilter.Items.Add(new ListItem("ALL", "ALL"));
            ddlNonFilter.SelectedIndex = ddlNonFilter.Items.IndexOf(new ListItem("ALL", "ALL"));


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
                StoreID = 7;
            }
            Title = "Store " + StoreID.ToString();
            dtNew = GetCouponSalesDetail(StoreID);

            //  var daas = dtNew.Select(@"TicketDate>= '8/1/2016'").CopyToDataTable();


            if (dtNew != null)
            {
                CheckBoxList1.DataSource = dtNew.Columns.Cast<DataColumn>()
                    .Select(x => x.ColumnName)
                    .ToArray().OrderBy(s => s.ToLower());
                CheckBoxList1.DataBind();
                CheckBoxList1.Items.RemoveAt(CheckBoxList1.Items.IndexOf(new ListItem("TicketDate")));
                CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("EX ELT RED 100"))].Selected = true;
                CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("Exact Elite Menthol 100"))].Selected = true;
                CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("LEW MEN 100 BX FS"))].Selected = true;
                CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("LEW MEN KING"))].Selected = true;
                CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("LEW RED BOX"))].Selected = true;
            }

        }
        else
        {
            ShieldChart1.PrimaryHeader.Text = "No Data Found";
        }
        Title = "Store " + ddlStoreID.SelectedValue + " Cig. Sales";
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
        try
        {
            SqlCommand cmd = new SqlCommand("GetCigStoreSalesDetail", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@StoreID", NoOfDays));
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
            for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            {

                if (!CheckBoxList1.Items[i].Selected)
                {
                    dtBasedonSelection.Columns.Remove(CheckBoxList1.Items[i].Value);
                }

            }
           
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

        var cigMatrix = CigMatrix.GetCigSalesData();

        if (ddlBrand.SelectedValue != "ALL")
        {
            cigMatrix = cigMatrix.Where(matrix => matrix.Brand == ddlBrand.SelectedValue).ToList();
        }

        if (ddlStyle.SelectedValue != "ALL")
        {
            cigMatrix = cigMatrix.Where(matrix => matrix.Style == ddlStyle.SelectedValue).ToList();
        }

        if (ddlBS.SelectedValue != "ALL")
        {
            cigMatrix = cigMatrix.Where(matrix => matrix.BoxSoft == ddlBS.SelectedValue).ToList();
        }
        if (ddlK100.SelectedValue != "ALL")
        {
            cigMatrix = cigMatrix.Where(matrix => matrix.King100 == ddlK100.SelectedValue).ToList();
        }

        if (ddlMenthol.SelectedValue != "ALL")
        {
            cigMatrix = cigMatrix.Where(matrix => matrix.Menthol == ddlMenthol.SelectedValue).ToList();
        }

        if (ddlCanadian.SelectedValue != "ALL")
        {
            cigMatrix = cigMatrix.Where(matrix => matrix.Canadian == ddlCanadian.SelectedValue).ToList();
        }

        if (ddlNonFilter.SelectedValue != "ALL")
        {
            cigMatrix = cigMatrix.Where(matrix => matrix.NonFilter == ddlNonFilter.SelectedValue).ToList();
        }

        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            if (cigMatrix.Count(matrix => matrix.Description == CheckBoxList1.Items[i].ToString()) != 0)
            {
                CheckBoxList1.Items[i].Selected = true;
            }
            else
            {
                CheckBoxList1.Items[i].Selected = false;
            }
            //  CheckBoxList1.Items[CheckBoxList1.Items.IndexOf(new ListItem("EX ELT RED 100"))].Selected = true;



        }

        Title = "Store " + ddlStoreID.SelectedValue + " Cig. Sales";
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
        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            CheckBoxList1.Items[i].Selected = false;
        }
        ddlBrand.SelectedIndex = ddlBrand.Items.IndexOf(new ListItem("ALL", "ALL"));
        ddlStyle.SelectedIndex = ddlStyle.Items.IndexOf(new ListItem("ALL", "ALL"));
        ddlBS.SelectedIndex = ddlBS.Items.IndexOf(new ListItem("ALL", "ALL"));
        ddlK100.SelectedIndex = ddlK100.Items.IndexOf(new ListItem("ALL", "ALL"));
        ddlCanadian.SelectedIndex = ddlCanadian.Items.IndexOf(new ListItem("ALL", "ALL"));
        ddlNonFilter.SelectedIndex = ddlNonFilter.Items.IndexOf(new ListItem("ALL", "ALL"));
        ShieldChart1.DataSeries.Clear();
        ShieldChart1.Axes.Clear();
    }

    protected void chkTrend_CheckedChanged(object sender, EventArgs e)
    {
        if (chkTrend.Checked)
        {
            ddlTotalDays.Enabled = true;
            StartDate.Text = DateTime.Now.AddDays(-int.Parse(ddlTotalDays.SelectedValue) - 1).Date.ToShortDateString();
            EndDate.Text = DateTime.Now.AddDays(-1).Date.ToShortDateString();
        }
        else
        {
            ddlTotalDays.Enabled = false;
        }
        SetCigValues();
    }

    protected void ddlTotalDays_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkTrend.Checked)
        {
            ddlTotalDays.Enabled = true;
            StartDate.Text = DateTime.Now.AddDays(-int.Parse(ddlTotalDays.SelectedValue) - 1).Date.ToShortDateString();
            EndDate.Text = DateTime.Now.AddDays(-1).Date.ToShortDateString();
        }
        else
        {
            ddlTotalDays.Enabled = false;
        }
        SetCigValues();

    }
}

