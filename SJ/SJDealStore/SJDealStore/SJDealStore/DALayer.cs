using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using NLog;
namespace SJDealStore
{

    public class SalesResult
    {
        public string ErrorCode { get; set; }
        public string Decommissioned { get; set; }
        public string Rejected { get; set; }
    }
    public class DALayer
    {
        private Constants _constants = new Constants();
        internal Logger Nlog = LogManager.GetCurrentClassLogger();

        public SalesResult MakeSales(string RFIDs)
        {
         SalesResult result= new SalesResult();
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.pr_UpdateDecommissionedItems)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };
            selectCommand.Parameters.Add("@StoreID", _constants.StoreID);
            selectCommand.Parameters.Add("@DeviceID",  _constants.DeviceID);
            selectCommand.Parameters.Add("@IsDamaged", _constants.SalesIsDamaged);
            selectCommand.Parameters.Add("@RFIDs", RFIDs);
            int ErrorCode=0;
            selectCommand.Parameters.Add("@ERRORCODE", ErrorCode);
            selectCommand.Parameters["@ERRORCODE"].Direction = ParameterDirection.Output;

            int Decommissioned = 0;
            selectCommand.Parameters.Add("@Decommissioned", Decommissioned);
            selectCommand.Parameters["@Decommissioned"].Direction = ParameterDirection.Output;

            int Rejected = 0;
            selectCommand.Parameters.Add("@Rejected", Rejected);
            selectCommand.Parameters["@Rejected"].Direction = ParameterDirection.Output;



            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                selectCommand.ExecuteNonQuery();
                result.Rejected = selectCommand.Parameters["@Rejected"].Value.ToString();
                result.ErrorCode = selectCommand.Parameters["@ErrorCode"].Value.ToString();
                result.Decommissioned = selectCommand.Parameters["@Decommissioned"].Value.ToString();
                return result;

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
        public DataTable SelectProducts(string RFIDs)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.pr_SelectProducts_OnRFID)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@RFIDS", RFIDs);
            selectCommand.Parameters.Add("@STOREID", _constants.StoreID);
            int ErrorCode = 0;
            selectCommand.Parameters.Add("@ERROR", ErrorCode);
            selectCommand.Parameters["@ERROR"].Direction = ParameterDirection.Output;

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
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
        public DataTable GetProductDetails(string UPC)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.pr_GetProductDetailsForUPC)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@UPC", UPC);
            selectCommand.Parameters.Add("@STOREID", _constants.StoreID);
            int ErrorCode = 0;
            selectCommand.Parameters.Add("@ERRORCODE", ErrorCode);
            selectCommand.Parameters["@ERRORCODE"].Direction = ParameterDirection.Output;

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
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
        public string ReturnProduct(string UPC,string SKU, string RFID)
        {
            Nlog.Trace(message: this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            var dataTable = new DataTable();
            var selectCommand = new SqlCommand
            {
                CommandText = string.Format(_constants.pr_InsertProducts_OnSingleAssociation)
                ,
                CommandTimeout = 180,
                CommandType = CommandType.StoredProcedure
            };

            selectCommand.Parameters.Add("@UPC", UPC);
            selectCommand.Parameters.Add("@SKU", SKU);
            selectCommand.Parameters.Add("@STOREID", _constants.StoreID);
            selectCommand.Parameters.Add("@RFIDTagID", RFID);
            selectCommand.Parameters.Add("@DeviceID", _constants.DeviceID);
            selectCommand.Parameters.Add("@IsReturned", "1");
           
            int ErrorCode = 0;
            selectCommand.Parameters.Add("@ERRORCODE", ErrorCode);
            selectCommand.Parameters["@ERRORCODE"].Direction = ParameterDirection.Output;

            var adapter = new SqlDataAdapter(selectCommand);
            var connection = new SqlConnection(_constants.RFIDString);
            selectCommand.Connection = connection;
            try
            {
                connection.Open();
                selectCommand.ExecuteNonQuery();
                return selectCommand.Parameters["@ErrorCode"].Value.ToString();
               
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