using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

/// <summary>
/// Summary description for StoreTransCount
/// </summary>
public class StoreTransCount
{
    public StoreTransCount()
    {
        //
        // TODO: Add constructor logic here
        //
       
    }

    public List<StoreTransCount> GetStoreTransactions()
    {
        string _RFIDSystem = ConfigurationManager.ConnectionStrings["EPMString"].ConnectionString;
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT *  FROM (SELECT  [TicketDate],[str_nbr],[Transactions] FROM [EPM_DW].[dbo].[StoreTransactionCountSummary]) Summary PIVOT (sum(Transactions) FOR str_nbr IN ([2],[3],[4],[6],[7],[9],[10])) AS PVT ORDER BY ticketdate DESC", connection);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);
            return ConvertStoreCount(allData);
        }
        catch (Exception ex)
        { }

        return null;
    }


    private List<StoreTransCount> ConvertStoreCount(DataTable dt)
    {
        List<StoreTransCount> list = new List<StoreTransCount>();
        if (dt != null)
        {
            foreach (DataRow row in dt.Rows)
            {

                try
                {
                    StoreTransCount matrix = new StoreTransCount();

                    matrix.TicketDate = DateTime.Parse(row["TicketDate"].ToString());
                    matrix.Store2 = int.Parse( row["2"].ToString()==""?"0": row["2"].ToString());
                    matrix.Store3 = int.Parse(row["3"].ToString() == "" ? "0" : row["3"].ToString());
                    matrix.Store4 = int.Parse(row["4"].ToString() == "" ? "0" : row["4"].ToString());
                    matrix.Store6 = int.Parse(row["6"].ToString() == "" ? "0" : row["6"].ToString());
                    matrix.Store7 = int.Parse(row["7"].ToString() == "" ? "0" : row["7"].ToString());
                    matrix.Store9 = int.Parse(row["9"].ToString() == "" ? "0" : row["8"].ToString());
                    matrix.Store10 = int.Parse(row["10"].ToString() == "" ? "0" : row["10"].ToString());
                    list.Add(matrix);
                }
                catch (Exception ex)
                {

                }

            }
        }
        return list;
    }

    public DateTime TicketDate { get; set; }
    public int Store2 { get; set; }
    public int Store3 { get; set; }
    public int Store4 { get; set; }
    public int Store6 { get; set; }
    public int Store7 { get; set; }
    public int Store9 { get; set; }
    public int Store10 { get; set; }

}