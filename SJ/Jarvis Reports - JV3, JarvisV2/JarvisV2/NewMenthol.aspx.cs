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

    protected void Page_Load(object sender, System.EventArgs e)
    {
        SetValues();
    }

    public void SetValues()
    {
        string storeid = "";
        storeid = Request.QueryString["StoreID"];
        int numberofDays =0;
        try
        {
            numberofDays = int.Parse(Request.QueryString["numberofdays"]);
        }
        catch(Exception ex)
        {
            numberofDays = 25;
        }
        
        if (storeid != null)
        {

        }
        else
        {
            storeid = "2,3,4,5,6,9,10,12";
        }
        DataTable dt = new DataTable();
        dt = GetMentholValues(storeid,numberofDays);

        string[] yAxisValues = MentholReportYAxis(dt);
        string[] xAxisValues = MentholReportXAxis(dt); 


        ShieldChart1.Width = Unit.Percentage(100);
        ShieldChart1.Height = Unit.Pixel(500);
        ShieldChart1.CssClass = "chart";
        ShieldChart1.PrimaryHeader.Text = "Menthol Only - Store : " + storeid;
        ShieldChart1.SecondaryHeader.Text =  "Generated on " +DateTime.Now.ToString("MM/dd/yy hh:mm:ss");
        ShieldChart1.TooltipSettings.AxisMarkers.Enabled = true;
        ShieldChart1.TooltipSettings.AxisMarkers.Mode = ChartXYMode.XY;
        ShieldChart1.TooltipSettings.AxisMarkers.Width = new Unit(1);
        ShieldChart1.TooltipSettings.AxisMarkers.ZIndex = 3;
        ShieldChart1.ZoomMode = ChartXYMode.XY;
        
        ChartAxisX axisX = new ChartAxisX();
        axisX.CategoricalValues = xAxisValues;
        axisX.Title.Text = "Date";
        axisX.AxisTickText.TextAngle = 270;
        axisX.AxisTickText.Y = 35;
        //axisX.AxisTickText.
        ShieldChart1.Axes.Add(axisX);

        ChartAxisY axisY = new ChartAxisY();
        axisY.Title.Text = "Quantity";
     
        ShieldChart1.Axes.Add(axisY);


        for (int i = 0; i < yAxisValues.Count(); i++)
        {
            if (yAxisValues[i] == "Seneca Menthol" || yAxisValues[i] == "N1" || yAxisValues[i] == "N2" )
            {

                if (yAxisValues[i] == "Seneca Menthol")
                {
                    ChartBarSeries splineSeriesHouseholds = new ChartBarSeries();
                    splineSeriesHouseholds.Index = 1000;
                    splineSeriesHouseholds.Settings.Color = Color.Black;
                    splineSeriesHouseholds.DataFieldY = yAxisValues[i];
                    splineSeriesHouseholds.CollectionAlias = yAxisValues[i];
                    splineSeriesHouseholds.Settings.StackMode = ChartStackMode.None;
                    splineSeriesHouseholds.Settings.BorderColor = Color.Black;
                    splineSeriesHouseholds.Settings.EnablePointSelection = true;
                    
                    ShieldChart1.DataSeries.Add(splineSeriesHouseholds);
                }
                else
                {
                    ChartBarSeries splineSeriesHouseholds = new ChartBarSeries();
                    splineSeriesHouseholds.Index = 0;
                    splineSeriesHouseholds.Settings.AddToLegend = false;
                    splineSeriesHouseholds.Settings.Color = Color.Black;
                    splineSeriesHouseholds.DataFieldY = yAxisValues[i];
                    splineSeriesHouseholds.CollectionAlias = yAxisValues[i];
                    splineSeriesHouseholds.Settings.StackMode = ChartStackMode.None;
                    splineSeriesHouseholds.Settings.BorderColor = Color.Black;
                    splineSeriesHouseholds.Settings.EnablePointSelection = true;
                    ShieldChart1.DataSeries.Add(splineSeriesHouseholds);
                }

            }


            
            else
            {
                ChartAreaSeries splineSeriesHouseholds = new ChartAreaSeries();
                splineSeriesHouseholds.DataFieldY = yAxisValues[i];
                splineSeriesHouseholds.CollectionAlias = yAxisValues[i];
                splineSeriesHouseholds.Settings.StackMode = ChartStackMode.Normal;
                //splineSeriesHouseholds.Settings.DrawNullValues = true;
                ShieldChart1.DataSeries.Add(splineSeriesHouseholds);
            }




         

        }

        ShieldChart1.DataSource = dt;
        
    }


    
    
     public string[] MentholReportXAxis(DataTable dt)
     {
         var x1 = (from r in dt.AsEnumerable()
                   select r["ticketDate"]).Distinct().ToList();
         string[] arr = ((IEnumerable)x1).Cast<object>()
                                  .Select(x => x.ToString().Substring(0, x.ToString().IndexOf(" ")))
                                  .ToArray();
         return arr;
     }

     public string[] MentholReportYAxis(DataTable dt)
     {
         DataTable dttemp = new DataTable();
         dttemp = dt.Clone();
         dttemp.Columns.Remove("ticketdate");
         string[] result = dttemp.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
         return result;
         
       
     }

     public DataTable GetMentholValues(string Parameters, int NoOfDays)
     {
         DataTable allData = new DataTable();
         SqlConnection connection = new SqlConnection(_RFIDSystem);
         try
         {
             SqlCommand cmd = new SqlCommand("GetMentholReport", connection);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add(new SqlParameter("@StoreNo", Parameters));
             cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
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

             allData.Columns.Add("N1");
             allData.Columns.Add("N2");
             connection.Close();
         }
         catch
         {
             connection.Close();
         }
         return allData;
     }


   
}