using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CigMatrix
/// </summary>
public class WinBinMatrix
{
    public WinBinMatrix()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<WinBinMatrix> GetWinBinData()
    {
        string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand(" SELECT * FROM [Jarvis].[dbo].[WinBinMatrix]", connection);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);
            return GetWinBinMatrix(allData);
        }
        catch (Exception ex)
        { }

        return null;
    }


    private List<WinBinMatrix> GetWinBinMatrix(DataTable dt)
    {
        List<WinBinMatrix> list = new List<WinBinMatrix>();
        if (dt != null)
        {
            foreach (DataRow row in dt.Rows)
            {

                try
                {
                    WinBinMatrix matrix = new WinBinMatrix();

                    matrix.StoreName =(row["StoreName"] ==DBNull.Value)?string.Empty: (string)row["StoreName"];
                    matrix.Desc = (row["Desc"] == DBNull.Value) ? string.Empty : (string)row["Desc"];
                    list.Add(matrix);
                }
                catch (Exception ex)
                {

                }

            }
        }
        return list;
    }

    public string StoreName { get; set; }
    public string Desc { get; set; }

}