using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CigSales
/// </summary>
public class CigrteSales
{
    public CigrteSales()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<CigrteSales> GetCigSalesData()
    {
        string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand(" SELECT * FROM [Jarvis].[dbo].[SJ_CigSales]", connection);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);
            return GetCigSales(allData);
        }
        catch(Exception ex)
        { }

        return null;
    }

    public List<CigrteSales> GetCigarSalesData()
    {
        string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand(" SELECT * FROM [Jarvis].[dbo].[SJ_CigarSales]", connection);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);
            return GetCigSales(allData);
        }
        catch (Exception ex)
        { }

        return null;
    }


    private List<CigrteSales> GetCigSales(DataTable dt)
    {
        List<CigrteSales> list = new List<CigrteSales>();
        if (dt != null)
        {
            foreach (DataRow row in dt.Rows)
            {

                try
                {
                    CigrteSales sales = new CigrteSales();

                    sales.TicketDate = (DateTime) row["TicketDate"];
                    sales.Store = (int) row["Store"];
                    sales.Sku = (string) row["sku"];
                    sales.Upc = (string) row["upc"];
                    sales.SkuDesc = (string) row["sku_dsc"];
                    sales.TotalUnits = (int) row["TotalUnits"];
                    sales.TotalDollars = (decimal) row["TotalDollars"];
                    sales.GrossProfit = (decimal) row["GrossProfit"];
                    sales.DateCreated = (DateTime) row["DateCreated"];
                    list.Add(sales);
                }
                catch (Exception ex)
                {
                    
                }

            }
        }
        return list;
    }

    public DateTime TicketDate { get; set; }
    public int Store { get; set; }
    public string Sku { get; set; }
    public string Upc { get; set; }
    public string SkuDesc { get; set; }
    public int TotalUnits { get; set; }
    public decimal TotalDollars { get; set; }
    public decimal GrossProfit { get; set; }
    public DateTime DateCreated { get; set; }
}