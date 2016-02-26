using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace TrackerRetailDataAccess
{
    public class KT_POIntegeration : DBInteractionBase
    {

        public bool InsertPODetail(string PONo, DateTime PODate, string TaggingLocation, string lineNumber, string stockCode,
            string UPC, string SKU, string Description, int OrderQty,
            float Cost, float Price, string VendorName, string OrderUOM, string POStatus, int printedTags, string Rfids, int userId)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_InsertPODetails]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Debug("KT_POIntegeration:InsertProduct:: Entering");
            _log.Debug("KT_POIntegeration:InsertProduct:: Entering");

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            _errorCode = 0;
            try
            {
                //          

                scmCmdToExecute.Parameters.Add(new SqlParameter("@PONumber", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, PONo));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@PODate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, PODate));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Supplier", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, VendorName));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@TaggingLocation", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, TaggingLocation));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@LineNo", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, lineNumber));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StockCode", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, stockCode));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@SKU", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, SKU));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DESC", SqlDbType.VarChar, 500, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, Description));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ORDERQTY", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, OrderQty));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Cost", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Cost));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Price));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UOM", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, OrderUOM));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@POSTATUS", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, POStatus));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@TAGPRINTED", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, printedTags));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Rfids));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = 0;
                _errorCode = SqlInt32.Parse(scmCmdToExecute.Parameters["@ERRCODE"].Value.ToString());

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_InsertPODetails' reported the ErrorCode: " + _errorCode);
                }


                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                _log.Error("KT_POIntegeration::InsertPODetail::Error occured.", ex.Message);
                throw new Exception("KT_POIntegeration::InsertPODetail::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("KT_POIntegeration:InsertPODetail:: Exiting");

            }
        }



        public DataSet GetPODetailsOnPONumber(string PONumber, out int @ErrorNumber)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_GetPODetailsForReceipt_onPONumber]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet toReturn = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            @ErrorNumber = 0;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@PONumber", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PONumber));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                adapter.Fill(toReturn);
                //  _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                ErrorNumber = Convert.ToInt32(Convert.ToString(cmdToExecute.Parameters["@ErrorCode"].Value));

                if (ErrorNumber != 0) // != (int)LLBLError.AllOk)
                {
                    if (ErrorNumber != 1)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_GetPODetailsForReceipt_onPONumber' reported the ErrorCode: " + _errorCode);
                    }



                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("KT_POIntegeration::GetPODetailsOnPONumber::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }



        public DataTable GetPODetailsOnPONumber_ForRFIDs(string PONumber, string RFIDs)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_GetPODetailsForReceiptAfterScan_OnRFID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@PONumber", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PONumber));
                cmdToExecute.Parameters.Add(new SqlParameter("@RFIDList", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_GetPODetailsForReceiptAfterScan_OnRFID' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("KT_POIntegeration::GetPODetailsOnPONumber_ForRFIDs::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }



        public DataTable GetPO_PrintDetails_ForReceipt_onRFIDs(string RFIDs)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_GetPO_PrintDetails_ForReceipt_OnRFID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RFIDList", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_GetPO_PrintDetails_ForReceipt_OnRFID' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("KT_POIntegeration::GetPO_PrintDetails_ForReceipt_onRFIDs::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }


        public bool GenerateReceiptForPONumber(long POID, string PODIDs, string POTagIDs, int Location, int userID, out string RecieptNumber)
        {

            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_GenerateReceiptOnPONumber]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;
            RecieptNumber = string.Empty;


            string ReceiptID = string.Empty;
            ReceiptID = generateReceipt();

            RecieptNumber = ReceiptID;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ReceiptID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ReceiptID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@POID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, POID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@PODIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, PODIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@POTagIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, POTagIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Location", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Location));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                scmCmdToExecute.CommandTimeout = 20 * 60;
                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    //_log.Error("SourceStoreID : " + SourceStoreID + ", ShippedStoreID" + ShippedStoreId + ",RR_ID :" + RR_ID + ",isAdHoc:" + isAdhoc + " ,ReplenishmentRequest : " + _RR + ", RFIDs :" + RFIDs);
                    throw new Exception("Stored Procedure 'pr_GenerateReceiptOnPONumber' reported the ErrorCode: " + _errorCode);
                }

                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                _log.Error("Stored Procedure 'pr_GenerateReceiptOnPONumber' reported the ErrorCode: " + ex.Message);
                return false;
                //throw new Exception("KT_POIntegration::GenerateReceiptForPONumber::Error occured. " + ex.Message);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }



        public bool GenerateReciptOnManulPrints_POs(string POIDs, string PODIDs, string POTagIDs, int Location, int userID, out string RecieptNumber, string MPIDs, string MPTagIDs)
        {

            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_GenerateReciptOnManulPrints_POs]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;
            RecieptNumber = string.Empty;


            string ReceiptID = string.Empty;
            ReceiptID = generateReceipt();

            RecieptNumber = ReceiptID;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ReceiptID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ReceiptID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@POIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, POIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@PODIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, PODIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@POTagIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, POTagIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@MPIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, MPIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@MPTagIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, MPTagIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Location", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Location));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                scmCmdToExecute.CommandTimeout = 20 * 60;
                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    // _log.Error("Stored Procedure 'pr_GenerateReciptOnManulPrints_POs' reported the ErrorCode: " + _errorCode);
                    throw new Exception("Stored Procedure 'pr_GenerateReciptOnManulPrints_POs' reported the ErrorCode: " + _errorCode);
                }

                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                _log.Error("Stored Procedure 'pr_GenerateReciptOnManulPrints_POs' reported the ErrorCode: " + ex.Message);
                return false;
                //throw new Exception("KT_POIntegration::GenerateReciptOnManulPrints_POs::Error occured. " + ex.Message);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////


        public string generateReceipt()
        {

            string ReceiptID = string.Empty;

            StringBuilder receiptID = new StringBuilder();
            //packagingslip.Append(AppConfigSettings.StoreID.ToString());

            receiptID.Append(DateTime.Now.Year.ToString());//.Remove(0, 2));

            //string year = Convert.ToString(DateTime.Now.Year);

            string daysofyear = DateTime.Now.DayOfYear.ToString();
            if (daysofyear.Length < 3)
            {
                for (int i = daysofyear.Length; i < 3; i++)
                {
                    daysofyear = "0" + daysofyear;
                }
            }

            receiptID.Append(daysofyear);

            string hour_24 = DateTime.Now.Hour.ToString();
            if (hour_24.Length < 2)
                hour_24 = "0" + hour_24;

            receiptID.Append(hour_24);

            string minutes = DateTime.Now.Minute.ToString();
            if (minutes.Length < 2)
                minutes = "0" + minutes;

            receiptID.Append(minutes);

            string seconds = DateTime.Now.Second.ToString();
            if (seconds.Length < 2)
                seconds = "0" + seconds;

            receiptID.Append(seconds);


            return receiptID.ToString();

        }



    }
}
