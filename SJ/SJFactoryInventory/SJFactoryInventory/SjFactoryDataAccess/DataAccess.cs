using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SJFactoryConstants;
using System.Reflection;

namespace SjFactoryDataAccess
{
    public class DataAccess
    {
        internal Logger Nlog = LogManager.GetCurrentClassLogger();
        private  SJFactoryConstants.SQLConstants _constants = new SQLConstants();
        private string _connectionString;

        public DataAccess()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _connectionString = _constants.DefaultConnectionString;
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public DataTable InsertUserLog(string logType, string logDesc, string logWebPage, int userId, string ipAddress, string deviceType, bool isError, bool isInternalError)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertUserLog),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.AddWithValue("@LogType", logType);
            selectCommand.Parameters.AddWithValue("@LogDescr", logDesc);
            selectCommand.Parameters.AddWithValue("@LogWebPage", logWebPage);
            selectCommand.Parameters.AddWithValue("@UserID", userId);
            selectCommand.Parameters.AddWithValue("@IpAddress", ipAddress);
            selectCommand.Parameters.AddWithValue("@DeviceType", deviceType);
            selectCommand.Parameters.AddWithValue("@IsError", isError);
            selectCommand.Parameters.AddWithValue("@IsInternalError", isInternalError);
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
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", exception: ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable InsertInternalLog(string logType, string logDesc, bool isError, bool isInternalError)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertInternalLog),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.AddWithValue("@LogType", logType);
            selectCommand.Parameters.AddWithValue("@LogDescr", logDesc);
            selectCommand.Parameters.AddWithValue("@IsError", isError);
            selectCommand.Parameters.AddWithValue("@IsInternalError", isInternalError);
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
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", exception: ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                 Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable CheckLogin(string username, string password)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.CheckLogin)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.AddWithValue("@Username", username);
            selectCommand.Parameters.AddWithValue("@Password", password);
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
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", exception: ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                 Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable GetProducts()
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetProducts)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
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
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", exception: ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable GetLocations()
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetLocations)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
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
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", exception: ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable CreateNewToteLabel(string tobaccoType, string tobaccoDesc, DateTime mfgDate, int totalWeightLb, float finalMoisture, string newTote, string rfid, int location,
             bool isDecommissioned, int createdUserId)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.CreateNewLabel)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.AddWithValue("@TobaccoType", tobaccoType);
            selectCommand.Parameters.AddWithValue("@TobaccoDesc", tobaccoDesc);
            selectCommand.Parameters.AddWithValue("@MfgDate", mfgDate);
            selectCommand.Parameters.AddWithValue("@TotalWeightLb", totalWeightLb);
            selectCommand.Parameters.AddWithValue("@FinalMoisture", finalMoisture);
            selectCommand.Parameters.AddWithValue("@NewTote", newTote);
            selectCommand.Parameters.AddWithValue("@Rfid", rfid);
            selectCommand.Parameters.AddWithValue("@Location", location);
            selectCommand.Parameters.AddWithValue("@IsDecommissioned", isDecommissioned);
            selectCommand.Parameters.AddWithValue("@CreatedUserId", createdUserId);
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
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", exception: ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }



        public DataTable GetAllToteLabels()
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAllToteLabels)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
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
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", exception: ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable GetToteLabelsDetails(string Sno)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetToteLabelsDetails)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            
            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_connectionString);
            selectCommand.Parameters.AddWithValue("@Sno", Sno);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", exception: ex);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
    }
}
