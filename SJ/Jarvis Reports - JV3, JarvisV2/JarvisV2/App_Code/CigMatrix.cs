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
public class CigMatrix
{
    public CigMatrix()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<CigMatrix> GetCigSalesData()
    {
        string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand(" SELECT * FROM [Jarvis].[dbo].[CigMatrix]", connection);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);
            return GetCigMatrix(allData);
        }
        catch (Exception ex)
        { }

        return null;
    }


    private List<CigMatrix> GetCigMatrix(DataTable dt)
    {
        List<CigMatrix> list = new List<CigMatrix>();
        if (dt != null)
        {
            foreach (DataRow row in dt.Rows)
            {

                try
                {
                    CigMatrix matrix = new CigMatrix();

                    matrix.Description =(row["Description"]==DBNull.Value)?string.Empty: (string)row["Description"];
                    matrix.Brand = (row["Brand"] == DBNull.Value) ? string.Empty : (string)row["Brand"];
                    matrix.Style = (row["Style"] == DBNull.Value) ? string.Empty : (string)row["Style"];
                    matrix.BoxSoft = (row["BoxSoft"] == DBNull.Value) ? string.Empty : (string)row["BoxSoft"];
                    matrix.King100 = (row["King100"] == DBNull.Value) ? string.Empty : (string)row["King100"];
                    matrix.Menthol = (row["Menthol"] == DBNull.Value) ? string.Empty : (string)row["Menthol"];
                    matrix.Canadian = (row["Canadian"] == DBNull.Value) ? string.Empty : (string)row["Canadian"];
                    matrix.NonFilter = (row["NonFilter"] == DBNull.Value) ? string.Empty : (string)row["NonFilter"];
                    list.Add(matrix);
                }
                catch (Exception ex)
                {

                }

            }
        }
        return list;
    }

    public string Description { get; set; }
    public string Brand { get; set; }
    public string Style { get; set; }
    public string BoxSoft { get; set; }
    public string King100 { get; set; }
    public string Menthol { get; set; }
    public string Canadian { get; set; }
    public string NonFilter { get; set; }

}