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
using SimplifiedPOConstants;
namespace SimplifiedPODataAccess
{
    public class SPODL
    {
        internal Logger Nlog = LogManager.GetCurrentClassLogger();
        private SimplifiedPOConstants.SPOConst _constants = new SPOConst();
        private string _connectionString;


        public SPODL()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _connectionString = _constants.DefaultConnectionString;
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }


        public DataTable Test(DateTime startdate, DateTime enddate, int strNbr)
        {
            throw new NotImplementedException();
        }



        //public DataTable SalesTransationsGroupedByItem(DateTime startdate, DateTime enddate, int strNbr)
        //{
        //    Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
        //    var dataTable = new DataTable();
        //    var selectCommand = new SqlCommand
        //    {
        //        CommandText = string.Format(_constants.SalesTransationsGroupedByItem)
        //        ,
        //        CommandTimeout = 180,
        //        CommandType = CommandType.StoredProcedure
        //    };
        //    selectCommand.Parameters.AddWithValue("@startdate", startdate);
        //    selectCommand.Parameters.AddWithValue("@enddate", enddate);
        //    selectCommand.Parameters.AddWithValue("@str_nbr", strNbr);

        //    var adapter = new SqlDataAdapter(selectCommand);
        //    var connection = new SqlConnection(_constants.DefaultConnectionString);
        //    selectCommand.Connection = connection;
        //    try
        //    {
        //        connection.Open();
        //        adapter.Fill(dataTable);
        //        return dataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        Nlog.Trace(
        //            this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
        //            System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //        connection.Dispose();
        //        Nlog.Trace(message:
        //            this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
        //            System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
        //    }
        //}


        public string Test(string testString)
        {
            return DateTime.Now.ToString(CultureInfo.InvariantCulture) + " " + testString;
        }

        public DataTable Login(string Username, string PasswordHash)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.LoginCheck,Username,PasswordHash)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
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


        public DataTable GetAllUsers()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAllUser)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
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

        public DataTable GetAllEntities()
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetAllEntities)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
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

        public DataTable GetSuppliers(string EntitesName)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetSuppliers, EntitesName)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
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


        public DataTable GetSupplierAddress(string EntitesName, string SupplierName)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetSupplierAddress, EntitesName,SupplierName)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
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

        public string CreateTempPO(int loginUserId, string loginUserName, string postFor, string buyerName, string buyerAddress, string buyerContactNumber, 
            int priority, string supplierEntity, string supplierId, string supplierName, string supplierAddress, string supplierContactNumber, string  notes)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.InsertTempPo),
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.AddWithValue("@loginUserId", loginUserId);
            selectCommand.Parameters.AddWithValue("@LoginUserName", loginUserName);
            selectCommand.Parameters.AddWithValue("@POPostForID", postFor);
            selectCommand.Parameters.AddWithValue("@BuyerName", buyerName);
            selectCommand.Parameters.AddWithValue("@BuyerAddress", buyerAddress);
            selectCommand.Parameters.AddWithValue("@BuyerContactNumber", buyerContactNumber);
            selectCommand.Parameters.AddWithValue("@Priority", priority);
            selectCommand.Parameters.AddWithValue("@SupplierEntity", supplierEntity);
            selectCommand.Parameters.AddWithValue("@SupplierID", supplierId);
            selectCommand.Parameters.AddWithValue("@SupplierName", supplierName);
            selectCommand.Parameters.AddWithValue("@SupplierAddress", supplierAddress);
            selectCommand.Parameters.AddWithValue("@SupplierContactNumber", supplierContactNumber);
            selectCommand.Parameters.AddWithValue("@Notes", notes);


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.SJPurchaseOrderConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
               var ID=    selectCommand.ExecuteScalar();
                return ID.ToString();
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
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
            return "";
        }

        public DataTable GetUnSubmittedPo(string userId)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.GetUnSubmittedPo, userId)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
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


        public DataTable DeleteTempPo(string sno)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.DeleteTempPo, sno)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.Text
            };


            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDConnectionString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch (Exception ex)
            {
                Nlog.Trace(
                    this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                    System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
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
    }
}
