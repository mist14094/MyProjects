using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuickSales
{
    public partial class SalesDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["UPC"] != null)
                {
                    lblVendorname.Text = "UPC : " +Request.QueryString["UPC"] ;
                   Session["GridData"] = GetDetailData(Request.QueryString["UPC"], GetTableName(Request.QueryString["ID"]));
                    GridView1.DataSource = (DataTable) Session["GridData"];
                    GridView1.DataBind();
                    lblStore2Ordered.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "2" && int.Parse(row["QTY"].ToString()) >0 ).ToString(CultureInfo.InvariantCulture);
                    lblStore3Ordered.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "3" && int.Parse(row["QTY"].ToString()) > 0).ToString(CultureInfo.InvariantCulture);
                    lblStore4Ordered.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "4" && int.Parse(row["QTY"].ToString()) > 0).ToString(CultureInfo.InvariantCulture);
                    lblStore6Ordered.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "6" && int.Parse(row["QTY"].ToString()) > 0).ToString(CultureInfo.InvariantCulture);
                    lblStore7Ordered.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "7" && int.Parse(row["QTY"].ToString()) > 0).ToString(CultureInfo.InvariantCulture);
                    lblStore9Ordered.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "9" && int.Parse(row["QTY"].ToString()) > 0).ToString(CultureInfo.InvariantCulture);
                    lblStore10Ordered.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "10" && int.Parse(row["QTY"].ToString()) > 0).ToString(CultureInfo.InvariantCulture);
                    lblStore12Ordered.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "12" && int.Parse(row["QTY"].ToString()) > 0).ToString(CultureInfo.InvariantCulture);
                    lblStoreTotalOrdered.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => int.Parse(row["QTY"].ToString()) > 0).ToString(CultureInfo.InvariantCulture);


                    //lblStore2Sold.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => Convert.ToInt16(row["NumberSoldinFinanceDays"].ToString()) > 0).ToString(CultureInfo.InvariantCulture);
                    
                    
                    lblStore2Sold.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "2" && int.Parse(row["QTY"].ToString()) > 0 && int.Parse(row["NumberSoldinFinanceDays"].ToString()) >= int.Parse(row["QTY"].ToString())).ToString(CultureInfo.InvariantCulture);
                    lblStore3Sold.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "3" && int.Parse(row["QTY"].ToString()) > 0 && int.Parse(row["NumberSoldinFinanceDays"].ToString()) >= int.Parse(row["QTY"].ToString())).ToString(CultureInfo.InvariantCulture);
                    lblStore4Sold.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "4" && int.Parse(row["QTY"].ToString()) > 0 && int.Parse(row["NumberSoldinFinanceDays"].ToString()) >= int.Parse(row["QTY"].ToString())).ToString(CultureInfo.InvariantCulture);
                    lblStore6Sold.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "6" && int.Parse(row["QTY"].ToString()) > 0 && int.Parse(row["NumberSoldinFinanceDays"].ToString()) >= int.Parse(row["QTY"].ToString())).ToString(CultureInfo.InvariantCulture);
                    lblStore7Sold.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "7" && int.Parse(row["QTY"].ToString()) > 0 && int.Parse(row["NumberSoldinFinanceDays"].ToString()) >= int.Parse(row["QTY"].ToString())).ToString(CultureInfo.InvariantCulture);
                    lblStore9Sold.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "9" && int.Parse(row["QTY"].ToString()) > 0 && int.Parse(row["NumberSoldinFinanceDays"].ToString()) >= int.Parse(row["QTY"].ToString())).ToString(CultureInfo.InvariantCulture);
                    lblStore10Sold.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "10" && int.Parse(row["QTY"].ToString()) > 0 && int.Parse(row["NumberSoldinFinanceDays"].ToString()) >= int.Parse(row["QTY"].ToString())).ToString(CultureInfo.InvariantCulture);
                    lblStore12Sold.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => row["StoreNumber"].ToString() == "12" && int.Parse(row["QTY"].ToString()) > 0 && int.Parse(row["NumberSoldinFinanceDays"].ToString()) >= int.Parse(row["QTY"].ToString())).ToString(CultureInfo.InvariantCulture);
                    lblStoreTotalSold.Text = ((DataTable)Session["GridData"]).AsEnumerable().Count(row => int.Parse(row["QTY"].ToString()) > 0 && int.Parse(row["NumberSoldinFinanceDays"].ToString()) >= int.Parse(row["QTY"].ToString())).ToString(CultureInfo.InvariantCulture);
                    


                }
            }
        }

        public string GetTableName(string id)
        {

            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (
                    connection =
                        new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText =
                            string.Format("SELECT TableName FROM [Jarvis].[dbo].[TMP_TableMap] WHERE SNO ={0}", id);
                        sqlCommand.CommandType = CommandType.Text;
                        //sqlCommand.Parameters.Add(new SqlParameter("@sample", sample));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }
                        if (dt.Rows.Count > 0)
                        {
                            return dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null) connection.Close();
            }

        }

        public DataTable GetDetailData(string UPC, string TableName)
        {

            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = string.Format("SELECT  CAST([RecordDate] AS DATE) AS RecordDate  ,[INVOICE],[ITEMSTR],[DESCRIPTION],[QTY],[UNIT],[UNITCOST],[UNITRETAIL],[ITEMRETL],[StoreNumber],cast( isnull([NumberSoldinFinanceDays],0) as int)as NumberSoldinFinanceDays FROM [Jarvis].[dbo].[" + TableName + "]WHERE [ITEMSTR] IN (SELECT [itemstr]  FROM [Jarvis].[dbo].[TMP_ItemSTRMatch] WHERE upc='{0}')", UPC);
                        sqlCommand.CommandType = CommandType.Text;
                        //sqlCommand.Parameters.Add(new SqlParameter("@sample", sample));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null) connection.Close();
            }
            return dt;

        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            string direction = string.Empty;
            if (SortDirection == SortDirection.Ascending)
            {
                SortDirection = SortDirection.Descending;
                direction = " DESC";
            }
            else
            {
                SortDirection = SortDirection.Ascending;
                direction = " ASC";
            }

            DataTable table = (DataTable)Session["GridData"];
            table.DefaultView.Sort = sortExpression + direction;
            GridView1.DataSource = table;
            GridView1.DataBind();
        }

        public SortDirection SortDirection
        {
            get
            {
                if (ViewState["SortDirection"] == null)
                {
                    ViewState["SortDirection"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["SortDirection"];
            }
            set
            {
                ViewState["SortDirection"] = value;
            }

        }
    }
}