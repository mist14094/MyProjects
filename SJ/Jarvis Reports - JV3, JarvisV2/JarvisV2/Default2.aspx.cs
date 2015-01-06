using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Default2 : System.Web.UI.Page
{
    public static string _RFIDSystem = "Data Source=RFIDSERVER;Initial Catalog=TrackerRetail;User ID=RFIDUser;Password=RFIDpr0sp3r1ty;";
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable allData  = GetCouponSalesDetail(TextBox1.Text);
        GridView1.DataSource = allData;
        GridView1.DataBind();
    }

    public DataTable GetCouponSalesDetail(string UPC)
    {
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT CNT.*, dbo.Products.[DESC] FROM (SELECT COUNT(*)AS CNT , UPC,SKU FROM dbo.ProductItems WHERE upc='"+ UPC+ "' AND Status <> 'Hold' AND RFID IS NOT null GROUP BY UPC,SKU) cnt INNER JOIN dbo.Products ON cnt.SKU = dbo.Products.SKU AND cnt.UPC = dbo.Products.UPC  WHERE StoreID = 10", connection);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);
            connection.Close();
        }
        catch(Exception ex)
        {
            connection.Close();
        }
        return allData;
    }

}