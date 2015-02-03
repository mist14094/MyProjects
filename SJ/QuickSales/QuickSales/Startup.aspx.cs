using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QuickSales
{
    public partial class Startup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlSearchCriteria.DataSource = GetVendorDetails();
          
                ddlSearchCriteria.DataTextField = "VendorName";
                ddlSearchCriteria.DataValueField = "Sno";
                ddlSearchCriteria.DataBind();
                ddlSearchCriteria.Items.Insert(0,new ListItem("--select--","0"));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlSearchCriteria.SelectedValue != "0")
            {
                Response.Redirect("sales.aspx?ID=" + ddlSearchCriteria.SelectedValue+"&Name="+ddlSearchCriteria.SelectedItem);
            }
            else
            {
                Label1.Text = "Select a vendor!";
            }
        }

        public DataTable GetVendorDetails()
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
                        sqlCommand.CommandText = "SELECT* FROM [Jarvis].[dbo].[TMP_TableMap]";
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
    }
}