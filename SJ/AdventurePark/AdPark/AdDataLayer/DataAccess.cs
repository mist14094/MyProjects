using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdConstants;
using NLog;

namespace AdDataLayer
{
    public class DataAccess
    {
        internal Logger Nlog = LogManager.GetCurrentClassLogger();
        private AdConstants.SqlConstants _constants = new SqlConstants();
        private string _connectionString;

        public DataAccess()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _connectionString = _constants.DefaultConnectionString;
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }


        public DataTable GetAllUsers()
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAllUsers)
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
                
                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" +ex.InnerException + ex.Message + Environment.NewLine +ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable InsertUser(string FirstName, string LastName, string Address, string City, string State, string Country, string Zipcode,
            string ContactNumber, string EmailID, DateTime DateOfBirth,string TagNumber,int LoginUsers,int ActiveMenu)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertUser),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.AddWithValue("@FirstName", FirstName);
            selectCommand.Parameters.AddWithValue("@LastName", LastName);
            selectCommand.Parameters.AddWithValue("@Address", Address);
            selectCommand.Parameters.AddWithValue("@City", City);
            selectCommand.Parameters.AddWithValue("@State", State);
            selectCommand.Parameters.AddWithValue("@Country", Country);
            selectCommand.Parameters.AddWithValue("@Zipcode", Zipcode);
            selectCommand.Parameters.AddWithValue("@ContactNumber", ContactNumber);
            selectCommand.Parameters.AddWithValue("@EmailID", EmailID);
            selectCommand.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            selectCommand.Parameters.AddWithValue("@TagNumber", TagNumber);
            selectCommand.Parameters.AddWithValue("@LoginUsers", LoginUsers);
            selectCommand.Parameters.AddWithValue("@ActiveMenu", ActiveMenu);
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
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
                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
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
                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
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
                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable GetAllActivitiesMenu()
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAllActivitiesMenu)
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable GetAllTagActivities()
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAllTagActivities)
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable GetTagDetails(string TagNumber)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetTagDetails,TagNumber),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable UpdateActivitiesForTag(string TagNumber , int LacrosseThrow, int SoftBallThrow, int HeavyBallThrow, int Maze, int BullRide, int KidsZone, int SoccerDarts, int RopeCourseInMinutes, int ZipLine, int Tubing, int JumpZone, int ExtraAct1InCount, int ExtraAct2InCount, int ExtraAct3InCount, int ExtraAct4InCount, int ExtraAct5InCount, int ExtraAct1InTime, int ExtraAct2InTime, int ExtraAct3InTime, int ExtraAct4InTime, int ExtraAct5InTime)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.UpdateActivitiesForTag, TagNumber),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_connectionString);
            selectCommand.Parameters.Add("@TagNumber", TagNumber);
            selectCommand.Parameters.Add("@LacrosseThrow", LacrosseThrow);
            selectCommand.Parameters.Add("@SoftBallThrow", SoftBallThrow);
            selectCommand.Parameters.Add("@HeavyBallThrow", HeavyBallThrow);
            selectCommand.Parameters.Add("@Maze", Maze);
            selectCommand.Parameters.Add("@BullRide", BullRide);
            selectCommand.Parameters.Add("@KidsZone", KidsZone);
            selectCommand.Parameters.Add("@SoccerDarts", SoccerDarts);
            selectCommand.Parameters.Add("@RopeCourseInMinutes", RopeCourseInMinutes);
            selectCommand.Parameters.Add("@ZipLine", ZipLine);
            selectCommand.Parameters.Add("@Tubing", Tubing);
            selectCommand.Parameters.Add("@JumpZone", JumpZone);
            selectCommand.Parameters.Add("@ExtraAct1InCount", ExtraAct1InCount);
            selectCommand.Parameters.Add("@ExtraAct2InCount", ExtraAct2InCount);
            selectCommand.Parameters.Add("@ExtraAct3InCount", ExtraAct3InCount);
            selectCommand.Parameters.Add("@ExtraAct4InCount", ExtraAct4InCount);
            selectCommand.Parameters.Add("@ExtraAct5InCount", ExtraAct5InCount);
            selectCommand.Parameters.Add("@ExtraAct1InTime", ExtraAct1InTime);
            selectCommand.Parameters.Add("@ExtraAct2InTime", ExtraAct2InTime);
            selectCommand.Parameters.Add("@ExtraAct3InTime", ExtraAct3InTime);
            selectCommand.Parameters.Add("@ExtraAct4InTime", ExtraAct4InTime);
            selectCommand.Parameters.Add("@ExtraAct5InTime", ExtraAct5InTime);


            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }



        public DataTable InsertLogMessage(string TagNumber , string Message)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertLogMessage, TagNumber,Message),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable GetAllLogs()
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAllLogs),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable GetLogsforTag(string TagNumber)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetLogsforTag, TagNumber),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable GetAllDevices()
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAllDevices),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable InsertJustOnceValue(string TableName, string TagNumber, string DeviceID,string LoginId)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertJustOnceValue,TableName,TagNumber,DeviceID,LoginId),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable UseTagForActivity(string TagNumber, string ColumnName)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.UseTagForActivity, ColumnName, TagNumber),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable InsertEngineLog(int DeviceID, string TagNumber, string DeviceValue, string LoginID)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertEngineLog , DeviceID, TagNumber, DeviceValue, LoginID),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable InsertCountInAndOut(string TableName,string ColumnName, string TagNumber)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertCountInAndOut, TableName, ColumnName, TagNumber),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable SelectCountInAndOutWithTagNumber(string TableName, string TagNumber)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SelectCountInAndOutWithTagNumber, TableName, TagNumber),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
        public DataTable SelectCountInAndOut(string TableName)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SelectCountInAndOut, TableName),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable UpdateCountInAndOut_Out(string TableName, string OutColumnName, string Sno,string InColumnName)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.UpdateCountInAndOut_Out, TableName, OutColumnName, Sno,InColumnName),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        /////////////////////
        /// 
        public DataTable InsertCountAndExpire(string TableName, string ColumnName, string TagNumber)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertCountAndExpire, TableName, ColumnName, TagNumber),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable SelectCountAndExpireWithTagNumber(string TableName, string TagNumber)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SelectCountAndExpireWithTagNumber, TableName, TagNumber),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
        public DataTable SelectCountAndExpire(string TableName)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SelectCountAndExpire, TableName),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable UpdateCountAndExpire_Out(string TableName, string OutColumnName, string Sno, string InColumnName)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.UpdateCountAndExpire_Out, TableName, OutColumnName, Sno, InColumnName),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable UpdateCountAndExpire_UseTagForActivity(string TagNumber, string ColumnName, string Value)
        {

            Nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText =
                    string.Format(_constants.UpdateCountAndExpire_UseTagForActivity,  ColumnName, TagNumber, Value),
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

                Nlog.Error(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message +
                    Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message:
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable SelectCountAndWaitWithTagNumber(string TableName, string TagNumber)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SelectCountAndWaitWithTagNumber, TableName, TagNumber),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
        public DataTable SelectCountAndWait(string TableName)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SelectCountAndWait, TableName),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable InsertCountAndWaitCounter(string TableName, string TagNumber)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertCountAndWaitCounter, TableName, TagNumber),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable UpdateExpiredCountAndWait(string TableName)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.UpdateExpiredCountAndWait, TableName),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }
        public DataTable UpdateExpiredCountAndWait_Out(string TableName,string Value, string Sno)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.UpdateExpiredCountAndWait_Out, TableName,Value,Sno),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable InsertThrowLeaderBoard(string DeviceID, string TagNumber, string Value)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertThrowLeaderBoard, DeviceID, TagNumber, Value),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }


        public DataTable SelectThrowLeaderBoard()
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.SelectThrowLeaderBoard),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable InsertEngineLogWithResults(int DeviceID, string TagNumber, string DeviceValue, string LoginID,string Message)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertEngineLogWithResults, DeviceID, TagNumber, DeviceValue, LoginID,Message),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
                throw ex;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }
        }

        public DataTable MonitorJustCount(string TableName)
        {

            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.MonitorJustCount,TableName),
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

                Nlog.Error(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error" + ex.InnerException + ex.Message + Environment.NewLine + ex.StackTrace);
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
