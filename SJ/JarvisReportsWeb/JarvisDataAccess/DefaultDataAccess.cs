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
    public class DefaultDataAccess
    {
        internal Logger nlog = LogManager.GetCurrentClassLogger();
        JarvisReportsConstant.GlobalConstants Constants = new GlobalConstants();
        private string _connectionString;

        public DefaultDataAccess()
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _connectionString = Constants.DefaultConnectionString;
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");

        }

       
        private string GetConnectionString(int sno)
        {
            nlog.Trace(message: this.GetType().Namespace+ ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetConnectionString, sno.ToString(CultureInfo.InvariantCulture))

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

        public DataTable GetAllDataBaseConnection()
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetAllDataBaseConnection)

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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

        public DataTable GetDataElements(string referenceDataBaseName, bool enabled, string elementType)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetDataElements,referenceDataBaseName.ToString(CultureInfo.InvariantCulture), enabled.ToString(), elementType)

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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

        public int GetDataBaseConnectionStringRef(int sno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetDataBaseConnectionStringRef, sno)

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
            selectCommand.Connection = connection;

            try
            {
                connection.Open();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    return int.Parse(dataTable.Rows[0][0].ToString());
                }
                else
                {
                    return 0;
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

        public string GetExecutionScript(string sno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetExecutionScript, sno)

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
            selectCommand.Connection = connection;

            try
            {
                connection.Open();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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


        public DataTable GetCharttypes()
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetCharttypes)

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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

        public DataTable GetChartValues(string sno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetChartValues,sno)

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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


        public DataTable GetAxisConfiguration(string ReferenceChartNo, string AxisType)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetAxisConfiguration, AxisType,ReferenceChartNo)

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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


        public DataTable GetChartLineSeries(string MainChartAxisBaseSno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetChartLineSeries, MainChartAxisBaseSno)

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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


        public string InsertMainChartConfiguration(string ChartPrimaryHeader,string ChartSecondaryHeader,Boolean? AllowMultipleSelection,Boolean? ExportOptionsExporttoImage,
            Boolean? ExportOptionsAllowPrint,Int32? Height,String HeightMode,Boolean? IsInverted,Int32? Width,String WidthMode,Int32? ZoomMode,Boolean? AxisMarkersEnabled,
            Int32? AxisMarkersMode,Int32? AxisMarkersWidth,Boolean? TooltipSettingsChartBound,DateTime? ModifiedDate)
        
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.InsertMainChartConfiguration),
                CommandType = CommandType.StoredProcedure,
                
            };
            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@ChartPrimaryHeader",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ChartPrimaryHeader),
                new SqlParameter("@ChartSecondaryHeader",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ChartSecondaryHeader),
                new SqlParameter("@AllowMultipleSelection",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AllowMultipleSelection),
                new SqlParameter("@ExportOptionsExporttoImage",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ExportOptionsExporttoImage),
                new SqlParameter("@ExportOptionsAllowPrint",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ExportOptionsAllowPrint),
                new SqlParameter("@Height",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,Height),
                new SqlParameter("@HeightMode",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,HeightMode),
                new SqlParameter("@IsInverted",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsInverted),
                new SqlParameter("@Width",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,Width),
                new SqlParameter("@WidthMode",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,WidthMode),
                new SqlParameter("@ZoomMode",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ZoomMode),
                new SqlParameter("@AxisMarkersEnabled",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisMarkersEnabled),
                new SqlParameter("@AxisMarkersMode",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisMarkersMode),
                new SqlParameter("@AxisMarkersWidth",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisMarkersWidth),
                new SqlParameter("@TooltipSettingsChartBound",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,TooltipSettingsChartBound),
                new SqlParameter("@ModifiedDate",SqlDbType.DateTime,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ModifiedDate),

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
            
            selectCommand.Connection = connection;
            foreach (SqlParameter parameter in Param)
            {
                selectCommand.Parameters.Add(parameter);
            }
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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


        public string UpdateMainChartConfiguration(string ChartPrimaryHeader, string ChartSecondaryHeader, Boolean? AllowMultipleSelection, Boolean? ExportOptionsExporttoImage,
            Boolean? ExportOptionsAllowPrint, Int32? Height, String HeightMode, Boolean? IsInverted, Int32? Width, String WidthMode, Int32? ZoomMode, Boolean? AxisMarkersEnabled,
            Int32? AxisMarkersMode, Int32? AxisMarkersWidth, Boolean? TooltipSettingsChartBound, DateTime? ModifiedDate, string sno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.UpdateMainChartConfiguration),
                CommandType = CommandType.StoredProcedure,

            };
            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@ChartPrimaryHeader",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ChartPrimaryHeader),
                new SqlParameter("@ChartSecondaryHeader",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ChartSecondaryHeader),
                new SqlParameter("@AllowMultipleSelection",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AllowMultipleSelection),
                new SqlParameter("@ExportOptionsExporttoImage",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ExportOptionsExporttoImage),
                new SqlParameter("@ExportOptionsAllowPrint",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ExportOptionsAllowPrint),
                new SqlParameter("@Height",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,Height),
                new SqlParameter("@HeightMode",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,HeightMode),
                new SqlParameter("@IsInverted",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsInverted),
                new SqlParameter("@Width",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,Width),
                new SqlParameter("@WidthMode",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,WidthMode),
                new SqlParameter("@ZoomMode",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ZoomMode),
                new SqlParameter("@AxisMarkersEnabled",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisMarkersEnabled),
                new SqlParameter("@AxisMarkersMode",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisMarkersMode),
                new SqlParameter("@AxisMarkersWidth",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisMarkersWidth),
                new SqlParameter("@TooltipSettingsChartBound",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,TooltipSettingsChartBound),
                new SqlParameter("@ModifiedDate",SqlDbType.DateTime,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ModifiedDate),
                new SqlParameter("@sno",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,sno),

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);

            selectCommand.Connection = connection;
            foreach (SqlParameter parameter in Param)
            {
                selectCommand.Parameters.Add(parameter);
            }
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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


        public DataTable GetChartList()
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetChartList),
                CommandType = CommandType.Text

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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

        public DataTable SelectChartTypes()
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.SelectChartTypes),
                CommandType = CommandType.Text

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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

        public DataTable SelectChartNameDesc()
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.SelectChartNameDesc),
                CommandType = CommandType.Text

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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

        public string GetDatabaseSnofromElementReference(string sno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetDatabaseSnofromElementReference, sno)

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
            selectCommand.Connection = connection;

            try
            {
                connection.Open();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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



        public string InsertMainConfigurationComp(string ChartName, string ChartDesc, int ChartTypeRefNo, int DataBaseElementSno, bool IsValid, DateTime ModifiedDate,
            string pagelink ,string BigIconPath, string SmallIconPath,  int SortOrder )
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.jrvs_InsertMainConfigurationComp),
                CommandType = CommandType.StoredProcedure,

            };
            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@ChartName",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ChartName),
                new SqlParameter("@ChartDesc",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ChartDesc),
                new SqlParameter("@ChartTypeRefNo",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ChartTypeRefNo),
                new SqlParameter("@DataBaseElementSno",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,DataBaseElementSno),
                new SqlParameter("@IsValid",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsValid),
                new SqlParameter("@ModifiedDate",SqlDbType.DateTime,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ModifiedDate),
                new SqlParameter("@pagelink",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,pagelink),
                new SqlParameter("@BigIconPath",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,BigIconPath),
                new SqlParameter("@SmallIconPath",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,SmallIconPath),
                new SqlParameter("@SortOrder",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,SortOrder),


            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);

            selectCommand.Connection = connection;
            foreach (SqlParameter parameter in Param)
            {
                selectCommand.Parameters.Add(parameter);
            }
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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

        public string UpdateMainConfigurationComp(string ChartName, string ChartDesc, int ChartTypeRefNo, int DataBaseElementSno, bool IsValid, DateTime ModifiedDate,
          string pagelink, string BigIconPath, string SmallIconPath, int SortOrder, string sno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.jrvs_UpdateMainConfigurationComp),
                CommandType = CommandType.StoredProcedure,

            };
            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@ChartName",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ChartName),
                new SqlParameter("@ChartDesc",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ChartDesc),
                new SqlParameter("@ChartTypeRefNo",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ChartTypeRefNo),
                new SqlParameter("@DataBaseElementSno",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,DataBaseElementSno),
                new SqlParameter("@sno",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,sno),
                new SqlParameter("@IsValid",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsValid),
                new SqlParameter("@ModifiedDate",SqlDbType.DateTime,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ModifiedDate),
                new SqlParameter("@pagelink",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,pagelink),
                new SqlParameter("@BigIconPath",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,BigIconPath),
                new SqlParameter("@SmallIconPath",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,SmallIconPath),
                new SqlParameter("@SortOrder",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,SortOrder),


            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);

            selectCommand.Connection = connection;
            foreach (SqlParameter parameter in Param)
            {
                selectCommand.Parameters.Add(parameter);
            }
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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



        public DataTable GetXAxisLine(string MainChartReferenceChartNo)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetXAxisLine, MainChartReferenceChartNo),
                CommandType = CommandType.Text

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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
        public DataTable GetYAxisLine(string MainChartReferenceChartNo)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetYAxisLine, MainChartReferenceChartNo),
                CommandType = CommandType.Text

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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


        public DataTable GetXAxisValueFromMCABSno(string MainChartAxisBaseSno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetXAxisValueFromMCABSno, MainChartAxisBaseSno),
                CommandType = CommandType.Text

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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

        public DataTable GetYAxisValueFromCLSSno(string ChartLineSeriesSno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetYAxisValueFromCLSSno, ChartLineSeriesSno),
                CommandType = CommandType.Text

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
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


        public string InsertMainChartAxisBase(string MainChartReferenceChartNo, int AxisType, int SortOrder, decimal TicksRepeat,
            Boolean SwapLocation, string CatagoricalValuesColumnName, int AxisTextAngle, int AxisTextAngleX, int AxisTextAngleY, 
            int AxisTextAngleStep, string TitleText, Boolean IsValid, DateTime ModifiedDate)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.InsertMainChartAxisBase),
                CommandType = CommandType.StoredProcedure,

            };
            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@MainChartReferenceChartNo",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,MainChartReferenceChartNo),
                new SqlParameter("@AxisType",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisType),
                new SqlParameter("@SortOrder",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,SortOrder),
                new SqlParameter("@TicksRepeat",SqlDbType.Decimal,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,TicksRepeat),
                new SqlParameter("@SwapLocation",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,SwapLocation),
                new SqlParameter("@CatagoricalValuesColumnName",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,CatagoricalValuesColumnName),
                new SqlParameter("@AxisTextAngle",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisTextAngle),
                new SqlParameter("@AxisTextAngleX",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisTextAngleX),
                new SqlParameter("@AxisTextAngleY",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisTextAngleY),
                new SqlParameter("@AxisTextAngleStep",SqlDbType.Decimal,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisTextAngleStep),
                new SqlParameter("@TitleText",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,TitleText),
                new SqlParameter("@IsValid",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsValid),
                new SqlParameter("@ModifiedDate",SqlDbType.DateTime,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ModifiedDate),
                
            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);

            selectCommand.Connection = connection;
            foreach (SqlParameter parameter in Param)
            {
                selectCommand.Parameters.Add(parameter);
            }
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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

        public string UpdateMainChartAxisBase(int AxisType, int SortOrder, decimal TicksRepeat,
           Boolean SwapLocation, string CatagoricalValuesColumnName, int AxisTextAngle, int AxisTextAngleX, int AxisTextAngleY,
           int AxisTextAngleStep, string TitleText, Boolean IsValid, DateTime ModifiedDate, string Sno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.UpdateMainChartAxisBase),
                CommandType = CommandType.StoredProcedure,

            };
            SqlParameter[] Param = new SqlParameter[]
            {
                
                new SqlParameter("@AxisType",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisType),
                new SqlParameter("@SortOrder",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,SortOrder),
                new SqlParameter("@TicksRepeat",SqlDbType.Decimal,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,TicksRepeat),
                new SqlParameter("@SwapLocation",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,SwapLocation),
                new SqlParameter("@CatagoricalValuesColumnName",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,CatagoricalValuesColumnName),
                new SqlParameter("@AxisTextAngle",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisTextAngle),
                new SqlParameter("@AxisTextAngleX",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisTextAngleX),
                new SqlParameter("@AxisTextAngleY",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisTextAngleY),
                new SqlParameter("@AxisTextAngleStep",SqlDbType.Decimal,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,AxisTextAngleStep),
                new SqlParameter("@TitleText",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,TitleText),
                new SqlParameter("@IsValid",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsValid),
                new SqlParameter("@ModifiedDate",SqlDbType.DateTime,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ModifiedDate),
                new SqlParameter("@sno",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,Sno),
                
            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);

            selectCommand.Connection = connection;
            foreach (SqlParameter parameter in Param)
            {
                selectCommand.Parameters.Add(parameter);
            }
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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


        public string InsertChartLineSeries(int MainChartAxisBaseSno, string DataFieldY, string CollectionAlias,
           Boolean EnablePointSelection, int DrawWidth, int DrawRadius, int StackMode, Boolean IsValid, DateTime ModifiedDate)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.InsertChartLineSeries),
                CommandType = CommandType.StoredProcedure,

            };
            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@MainChartAxisBaseSno",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,MainChartAxisBaseSno),
                new SqlParameter("@DataFieldY",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,DataFieldY),
                new SqlParameter("@CollectionAlias",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,CollectionAlias),
                new SqlParameter("@EnablePointSelection",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,EnablePointSelection),
                new SqlParameter("@DrawWidth",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,DrawWidth),
                new SqlParameter("@DrawRadius",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,DrawRadius),
                new SqlParameter("@StackMode",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,StackMode),
                new SqlParameter("@IsValid",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsValid),
                new SqlParameter("@ModifiedDate",SqlDbType.DateTime,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ModifiedDate),
                
            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);

            selectCommand.Connection = connection;
            foreach (SqlParameter parameter in Param)
            {
                selectCommand.Parameters.Add(parameter);
            }
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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


        public string UpdateChartLineSeries( string DataFieldY, string CollectionAlias,
          Boolean EnablePointSelection, int DrawWidth, int DrawRadius, int StackMode, Boolean IsValid, DateTime ModifiedDate, string Sno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.UpdateChartLineSeries),
                CommandType = CommandType.StoredProcedure,

            };
            SqlParameter[] Param = new SqlParameter[]
            {
                new SqlParameter("@DataFieldY",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,DataFieldY),
                new SqlParameter("@CollectionAlias",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,CollectionAlias),
                new SqlParameter("@EnablePointSelection",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,EnablePointSelection),
                new SqlParameter("@ModifiedDate",SqlDbType.DateTime,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,ModifiedDate),
                new SqlParameter("@DrawWidth",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,DrawWidth),
                new SqlParameter("@DrawRadius",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,DrawRadius),
                new SqlParameter("@StackMode",SqlDbType.Int,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,StackMode),
                new SqlParameter("@IsValid",SqlDbType.Bit,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,IsValid),
                new SqlParameter("@sno",SqlDbType.VarChar,50,ParameterDirection.Input,false,10,0,"",DataRowVersion.Proposed,Sno),
                
            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);

            selectCommand.Connection = connection;
            foreach (SqlParameter parameter in Param)
            {
                selectCommand.Parameters.Add(parameter);
            }
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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

        public string GetMainChartConfigurationRefNo(string sno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetMainChartConfigurationRefNo, sno)

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
            selectCommand.Connection = connection;

            try
            {
                connection.Open();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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



        public string GetMainChartAxisBaseSnofromSno(string sno)
        {
            nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(Constants.GetMainChartAxisBaseSnofromSno, sno)

            };
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(Constants.DefaultConnectionString);
            selectCommand.Connection = connection;

            try
            {
                connection.Open();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    return dataTable.Rows[0][0].ToString();
                }
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


    }
}
