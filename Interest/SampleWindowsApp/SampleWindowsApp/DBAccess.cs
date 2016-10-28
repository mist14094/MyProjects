using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

internal class DBAccess
{
    // Fields
  //public static string msAccessCon = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + GetNetworkPath() + @"\Stock.accdb;Jet OLEDB:Database Password=outlet960;");
    public static string msAccessCon = ("Provider=SQLOLEDB;Data Source=SJMTECH17\\SQLSERVER2012;Initial Catalog=movedb;User ID = sa; Password=manager@123;");
    //public static string msAccessCon = ("Provider=SQLOLEDB;Data Source=PROCESSING-7\\SQLEXPRESS;Initial Catalog=movedb;User ID = sa; Password=manager@123;");

    // Methods
    public static DataTable GetDataTable(string query, string msExcelCon)
    {
        DataTable dataTable = new DataTable();
        try
        {
            OleDbConnection connection = new OleDbConnection(msExcelCon);
            OleDbCommand selectCommand = new OleDbCommand(query, connection);
            new OleDbDataAdapter(selectCommand).Fill(dataTable);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
        catch (Exception exception)
        {
        }
        return dataTable;
    }

    private static string GetNetworkPath()
    {
        string str3;
        try
        {
            using (StreamReader reader = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\StockDBNetworkPath.txt"))
            {
                str3 = reader.ReadToEnd();
            }
        }
        catch (Exception)
        {
            str3 = "";
        }
        return str3;
    }
}


