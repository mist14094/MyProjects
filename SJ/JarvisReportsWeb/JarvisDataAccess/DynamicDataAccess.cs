using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;
using JarvisReportsConstant;

namespace JarvisDataAccess
{
    public class DynamicDataAccess
    {
        internal Logger nlog = LogManager.GetCurrentClassLogger();
        JarvisReportsConstant.GlobalConstants Constants = new GlobalConstants();
        private string _connectionString;

        public DynamicDataAccess(int connectionString)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            if (connectionString == 0)
            {
                _connectionString = Constants.DefaultConnectionString;
            }
            else
            {
                _connectionString = GetConnectionString(connectionString);
            }
            
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

       
        private string GetConnectionString(int sno)
        {
            nlog.Trace(message: this.GetType().Namespace+ ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetConnectionString, sno.ToString(CultureInfo.InvariantCulture)),
                CommandTimeout = 3000

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
            selectCommand.Connection = connection;

            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count >0) 
                {return dataTable.Rows[0]["ConnectionString"].ToString();}
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        /*

         * 
         * 
         * 
         * public DataTable SelectAllDevices()
{
    DataTable dataTable = new DataTable();
    SqlCommand selectCommand = new SqlCommand {
        CommandText = "dbo.[pr_SelectAllDevices]",
        CommandType = CommandType.StoredProcedure
    };
    SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
    SqlConnection connection = new SqlConnection(this._ConnectionString);
    selectCommand.Connection = connection;
    try
    {
        selectCommand.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
        connection.Open();
        adapter.Fill(dataTable);
        int num = 0;
        if (!string.IsNullOrEmpty(Convert.ToString(selectCommand.Parameters["@Error"].Value)))
        {
            num = Convert.ToInt32(selectCommand.Parameters["@Error"].Value);
        }
        if (num == 0)
        {
        }
    }
    catch
    {
        dataTable = null;
    }
    return dataTable;
}

 

 
  public DataTable SelectAllDevices()
{
    DataTable dataTable = new DataTable();
    SqlCommand selectCommand = new SqlCommand {
        CommandText = "dbo.[pr_SelectAllDevices]",
        CommandType = CommandType.StoredProcedure
    };
    SqlDataAdapter adapter = new SqlDataAdapter(selectCommand);
    SqlConnection connection = new SqlConnection(this._ConnectionString);
    selectCommand.Connection = connection;
    try
    {
        selectCommand.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
        connection.Open();
        adapter.Fill(dataTable);
        int num = 0;
        if (!string.IsNullOrEmpty(Convert.ToString(selectCommand.Parameters["@Error"].Value)))
        {
            num = Convert.ToInt32(selectCommand.Parameters["@Error"].Value);
        }
        if (num == 0)
        {
        }
    }
    catch
    {
        dataTable = null;
    }
    return dataTable;
}

 

        public DataSet GetAllDataBaseConnection()
        {
            nlog.Trace(message: this.GetType().Namespace+ ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandType = CommandType.Text;
             //   SQLCmd.CommandText = JarvisConstant.QRGetViewandProcedureName;
             //   SQLCmd.Connection = EPMSQLConnection;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                nlog.Trace( this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
         * */


        public DataTable GetSampleData(string ExecutionScript)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetSampleData, ExecutionScript),
                CommandTimeout = 3000
                
                
            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_connectionString);
            selectCommand.Connection = connection;

            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable GetRealData(string ExecutionScript)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetRealData, ExecutionScript),
                CommandTimeout = 3000


            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_connectionString);
            selectCommand.Connection = connection;

            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
    }
}
