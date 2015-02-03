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
    public partial class Sales : System.Web.UI.Page
    {
        public static DataTable DT;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["ID"];
                if (id != null)
                {
                    string procedureName = GetProcedureName(id);
                    if (id != "")
                    {
                        DT = GetTableSearchCriteria(procedureName);
                        lblVendorname.Text = "Sales Report for 2014 - 2015 - " + Request.QueryString["Name"];
                        GridView1.DataSource = DT;
                        GridView1.DataBind();
                        Session["GridData"] = DT;
                    }
                }

                else
                {
                    lblVendorname.Text = "Something went wrong!!!";
                }


            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlSearchCriteria.SelectedValue == "0")
            {
                
                lblcount.Text = "Select a paramater to search!!!";
                ddlSearchCriteria.Focus();
                return;
            }
            DataTable dtlTable = new DataTable();
            if (DT != null && srchTextBox.Text.Trim() != "")
            {
                try
                {
                    if (ddlSearchCriteria.SelectedValue == "Description")
                    {
                        dtlTable = DT.AsEnumerable().Where(
                                 row =>
                                     row.Field<string>(ddlSearchCriteria.SelectedValue)
                                         .ToUpper()
                                         .Contains(srchTextBox.Text.ToUpper()))
                             .CopyToDataTable();

                    }
                    if (ddlSearchCriteria.SelectedValue == "UPC")
                    {
                        Int64 i;
                        if (Int64.TryParse(srchTextBox.Text, out i))
                        {

                            dtlTable = DT.AsEnumerable()
                                .Where(
                                    row =>
                                        row.Field<Int64>(ddlSearchCriteria.SelectedValue)
                                            .ToString(CultureInfo.InvariantCulture)
                                            .ToUpper()
                                            .Contains(srchTextBox.Text.ToUpper()))
                                .CopyToDataTable();

                        }
                    }
                }
                catch (Exception)
                {
                }
                if (dtlTable != null)
                {
                    GridView1.DataSource = dtlTable;
                    GridView1.DataBind();
                    Session["GridData"] = dtlTable;
                    lblcount.Text = GridView1.Rows.Count.ToString()+" Rows found";
                }
            }
        }



        public DataTable GetTableSearchCriteria(string procedureName)
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
                        sqlCommand.CommandText = "[" + procedureName + "]";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
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


        public string GetProcedureName(string id)
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
                            string.Format("SELECT procedurename FROM [Jarvis].[dbo].[TMP_TableMap] WHERE SNO ={0}", id);
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
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

            DataTable table = (DataTable) Session["GridData"];
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