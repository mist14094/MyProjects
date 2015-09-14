using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _PODetails : System.Web.UI.Page
{
    public static string _RFIDSystem = ConfigurationManager.ConnectionStrings["SysproDString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    { DataTable table = new DataTable();
        try
        {
           
            table.Columns.Add("Sno#", typeof(int));
            table.Columns.Add("Syspro PO#", typeof(string));
            table.Columns.Add("Supplier", typeof(string));
            table.Columns.Add("Entry Date", typeof(string));
            table.Columns.Add("Due Date", typeof(string));
            table.Columns.Add("Customer", typeof(string));
            table.Columns.Add("Customer P0#", typeof(string));
            table.Columns.Add("Supplier Name", typeof(string));
            string query = "SELECT a.PurchaseOrder, a.Supplier, a.OrderEntryDate, a.OrderDueDate,  a.Customer, a.CustomerPoNumber, b.SupplierName FROM PorMasterHdr a WITH (NOLOCK) LEFT JOIN ApSupplier b WITH (NOLOCK) ON (a.Supplier = b.Supplier) WHERE a.OrderStatus IN( '0', '1', '4' ) AND ( ( a.PurchaseOrder LIKE samplequery12345 ) OR ( a.Supplier LIKE samplequery12345 ) OR ( b.SupplierName LIKE samplequery12345 ) OR ( a.CustomerPoNumber LIKE samplequery12345 ) OR ( a.Customer LIKE samplequery12345 ) ) ORDER BY a.PurchaseOrder desc";
            string str = TextBox1.Text.ToUpper();
            str = str.Replace("\r\n", ",");
            string[] words = str.Split(',');
            for (int i = 0; i < words.Count(); i++)
            {
                try
                {
                    string newquery = query.Replace("samplequery12345", "'%[" + words[i].ToString().Substring(0, 1) + "]" + words[i].ToString().Substring(1, words[i].ToString().Length - 1) + "%'");
                    DataTable dt = GetCouponSalesDetail(newquery);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            table.Rows.Add(i, dt.Rows[j]["PurchaseOrder"], dt.Rows[j]["Supplier"], dt.Rows[j]["OrderEntryDate"], dt.Rows[j]["OrderDueDate"], dt.Rows[j]["Customer"], dt.Rows[j]["CustomerPoNumber"], dt.Rows[j]["SupplierName"]);
                        }
                    }
                    else
                    {
                        table.Rows.Add(i, "", "", "", words[i], "", "","");
                    }
                }

                catch (Exception ex)
                {
                }
            }
        }
        catch(Exception ex)
        {
        }


        GridView1.DataSource = table;
        GridView1.DataBind();

       
    }

    public DataTable GetCouponSalesDetail(string query)
    {
        DataTable allData = new DataTable();
        SqlConnection connection = new SqlConnection(_RFIDSystem);
        try
        {
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add(new SqlParameter("@numberofdays", NoOfDays));
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(allData);

            connection.Close();
        }
        catch
        {
            connection.Close();
        }
        return allData;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
 DataTable table = new DataTable();
        try
        {
           
            table.Columns.Add("Sno#", typeof(int));
            table.Columns.Add("Syspro PO#", typeof(string));
            table.Columns.Add("Supplier", typeof(string));
            table.Columns.Add("Entry Date", typeof(string));
            table.Columns.Add("Due Date", typeof(string));
            table.Columns.Add("Customer", typeof(string));
            table.Columns.Add("Customer P0#", typeof(string));
            table.Columns.Add("Supplier Name", typeof(string));
            string query = "SELECT TOP 30 a.PurchaseOrder, a.Supplier, a.OrderStatus, a.OrderEntryDate, a.OrderDueDate, a.MemoDate, a.MemoCode, a.OrderType, a.Customer, a.CustomerPoNumber, a.Currency, b.SupplierName, b.Branch FROM PorMasterHdr a WITH (NOLOCK) LEFT JOIN ApSupplier b WITH (NOLOCK) ON (a.Supplier = b.Supplier) WHERE a.OrderStatus IN( '0', '1', '4' )ORder BY PurchaseOrder desc";
            string str = TextBox1.Text.ToUpper();
            str = str.Replace("\r\n", ",");
            string[] words = str.Split(',');
            for (int i = 0; i < words.Count(); i++)
            {
                try
                {
                    DataTable dt = GetCouponSalesDetail(query);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            table.Rows.Add(i, dt.Rows[j]["PurchaseOrder"], dt.Rows[j]["Supplier"], dt.Rows[j]["OrderEntryDate"], dt.Rows[j]["OrderDueDate"], dt.Rows[j]["Customer"], dt.Rows[j]["CustomerPoNumber"], dt.Rows[j]["SupplierName"]);
                        }
                    }
                    else
                    {
                        table.Rows.Add(i, "", "", "", words[i], "", "","");
                    }
                }

                catch (Exception ex)
                {
                }
            }
        }
        catch(Exception ex)
        {
        }


        GridView1.DataSource = table;
        GridView1.DataBind();

       
    }
}