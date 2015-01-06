using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    public static string _RFIDSystem = "Data Source=SYSPRO;Initial Catalog=SysproCompanyH;User ID=administrator;Password=K*gXhFs3+;";
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    { DataTable table = new DataTable();
        try
        {
           
            table.Columns.Add("Sno", typeof(int));
            table.Columns.Add("StockCode", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("LongDesc", typeof(string));
            table.Columns.Add("AlternateKey1", typeof(string));
            table.Columns.Add("UnitCost", typeof(string));
            table.Columns.Add("SellingPrice", typeof(string));
            string query = "SELECT a.StockCode, a.Description, a.LongDesc, a.AlternateKey1, a.AlternateKey2, a.StockUom, a.AlternateUom, a.OtherUom, a.Mass, a.Volume, a.Supplier, a.ProductClass, a.KitType, a.Buyer, a.Planner, a.LeadTime, a.PartCategory, a.WarehouseToUse, a.BuyingRule, a.Decimals, a.Ebq, a.PanSize, a.UserField1, a.UserField2, a.UserField3, a.UserField4, a.UserField5, a.DrawOfficeNum, b.QtyOnHand, b.QtyAllocated, b.QtyOnOrder, b.QtyOnBackOrder, b.QtyInTransit, b.QtyAllocatedWip, b.QtyInInspection, b.MinimumQty, b.MaximumQty, b.UnitCost, b.DefaultBin, b.UserField1, b.UserField2, b.UserField3, c.SellingPrice, c.PriceBasis, a.EccFlag, a.Version, a.Release, a.EccUser, a.ClearingFlag FROM SysproCompanyH..InvMaster a WITH (NOLOCK) LEFT JOIN SysproCompanyH..InvWarehouse b WITH (NOLOCK) ON (a.StockCode = b.StockCode AND a.WarehouseToUse = b.Warehouse) LEFT JOIN SysproCompanyH..InvPrice c WITH (NOLOCK) ON (a.StockCode = c.StockCode AND a.ListPriceCode = c.PriceCode) WHERE  ( a.StockCode LIKE samplequery12345 ) OR ( a.Description LIKE samplequery12345 ) OR ( a.LongDesc LIKE samplequery12345 ) OR ( a.AlternateKey1 LIKE samplequery12345 ) OR ( a.AlternateKey2 LIKE samplequery12345 ) OR ( a.Supplier LIKE samplequery12345 ) ";
            string str = TextBox1.Text;
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
                            table.Rows.Add(i, dt.Rows[j]["StockCode"], dt.Rows[j]["Description"], dt.Rows[j]["LongDesc"], dt.Rows[j]["AlternateKey1"], dt.Rows[j]["UnitCost"], dt.Rows[j]["SellingPrice"]);
                        }
                    }
                    else
                    {
                        table.Rows.Add(i, "", "", "", words[i], "", "");
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
}