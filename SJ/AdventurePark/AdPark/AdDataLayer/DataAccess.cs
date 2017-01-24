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
            string ContactNumber, string EmailID, DateTime DateOfBirth,string TagNumber)
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
