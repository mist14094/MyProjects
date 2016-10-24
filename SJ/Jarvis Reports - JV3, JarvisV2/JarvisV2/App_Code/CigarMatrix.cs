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
public class CigarMatrix
{
    public CigarMatrix()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<CigarMatrix> GetCigarSalesData()
    {
        string _RFIDSystem = ConfigurationManager.ConnectionStrings["RFIDString"].ConnectionString;
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand(" SELECT * FROM [Jarvis].[dbo].[CigarMatrix]", connection);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);
            return GetCigarMatrix(allData);
        }
        catch (Exception ex)
        { }

        return null;
    }


    private List<CigarMatrix> GetCigarMatrix(DataTable dt)
    {
        List<CigarMatrix> list = new List<CigarMatrix>();
        if (dt != null)
        {
            foreach (DataRow row in dt.Rows)
            {

                try
                {
                    CigarMatrix matrix = new CigarMatrix();

                    matrix.Description =(row["Description"]==DBNull.Value)?string.Empty: (string)row["Description"];
                    matrix.Brand = (row["Brand"] == DBNull.Value) ? string.Empty : (string)row["Brand"];
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

}