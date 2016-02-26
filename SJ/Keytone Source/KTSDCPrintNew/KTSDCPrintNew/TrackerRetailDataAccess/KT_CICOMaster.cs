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
    public class KT_CICOMaster : DBInteractionBase
    {
        #region Constructors
        public KT_CICOMaster() { }
        #endregion

        #region Private Fields
        private int _CICOMaster_ID;
        private int _SourceStoreID;
        private long _RR_ID;
        private string _PackageSlip;
        private string _RR;
        private int _ShippedStoreId;
        private int _BillStoreID;
        private int _CreatedBy;
        private DateTime _CreatedDate;
        private int _UpdatedBy;
        private DateTime _UpdateDate;
        private string _Comments;
        private DateTime _ChekOutTime; 
        #endregion

        #region Public Properties
            public int CICOMaster_ID { get { return _CICOMaster_ID; } set { _CICOMaster_ID = value; } }
            public int SourceStoreID { get { return _SourceStoreID; } set { _SourceStoreID = value; } }
            public long RR_ID { get { return _RR_ID; } set { _RR_ID = value; } } 
            public string PackageSlip { get { return _PackageSlip; } set { _PackageSlip = value; } }
            public string RR { get { return _RR; } set { _RR = value; } }
            public int ShippedStoreId { get { return _ShippedStoreId; } set { _ShippedStoreId = value; } }
            public int BillStoreID { get { return _BillStoreID; } set { _BillStoreID = value; } }
            public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }
            public DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
            public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }
            public DateTime UpdateDate { get { return _UpdateDate; } set { _UpdateDate = value; } }
            public string Comments { get { return _Comments; } set { _Comments = value; } }
            public DateTime ChekOutTime { get { return _ChekOutTime; } set { _ChekOutTime = value; } }
        #endregion



        #region [PublicMethod]

            public bool CheckInReplenishment_Confirm(string RFIDs , bool isAdhoc)
            {                

                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_InsertCICOItemsTransactionDetails]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;

                //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
                // Use base class' connection object
                scmCmdToExecute.Connection = _trackerRetailConnection;


                string packagingID = string.Empty;
                packagingID = generatePackaging(_SourceStoreID);
                PackageSlip = packagingID;

                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@SourceStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _SourceStoreID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ShippedStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ShippedStoreId));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@RR_ID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _RR_ID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, packagingID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ReplenishmentRequest", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _RR));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CreateUserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CreatedBy));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CheoutTime", SqlDbType.DateTime, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ChekOutTime));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@isAdHoc", SqlDbType.Bit, 1, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, isAdhoc));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.
                        scmCmdToExecute.CommandTimeout = 20 * 60;
                        scmCmdToExecute.ExecuteNonQuery();
        
                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        //_log.Error("SourceStoreID : " + SourceStoreID + ", ShippedStoreID" + ShippedStoreId + ",RR_ID :" + RR_ID + ",isAdHoc:" + isAdhoc + " ,ReplenishmentRequest : " + _RR + ", RFIDs :" + RFIDs);
                        throw new Exception("Stored Procedure 'pr_InsertCICOItemsTransactionDetails' reported the ErrorCode: " + _errorCode);
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::CheckInReplenishment_Confirm::Error occured. " + ex.Message);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();
                }
            }

            public bool CheckInReplenishment_Confirm(string RFIDs, bool isAdhoc, out string packagingID)
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_InsertCICOItemsTransactionDetails]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;

                //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
                // Use base class' connection object
                scmCmdToExecute.Connection = _trackerRetailConnection;


                packagingID = string.Empty;
                packagingID = generatePackaging(_SourceStoreID);

                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@SourceStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _SourceStoreID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ShippedStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ShippedStoreId));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@RR_ID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _RR_ID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, packagingID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ReplenishmentRequest", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _RR));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CreateUserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CreatedBy));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CheoutTime", SqlDbType.DateTime, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ChekOutTime));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@isAdHoc", SqlDbType.Bit, 1, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, isAdhoc));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    scmCmdToExecute.CommandTimeout = 20 * 60;
                    scmCmdToExecute.ExecuteNonQuery();

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_InsertCICOItemsTransactionDetails' reported the ErrorCode: " + _errorCode);
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::CheckInReplenishment_Confirm::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();
                }
            }

            public bool CheckIn(string RFIDs, string packagingID, bool isAdhoc,string userName)
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_CheckIN]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;
                _log.Debug("KT_CICOMaster:CheckIn:: Entering");

                //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
                // Use base class' connection object
                scmCmdToExecute.Connection = _trackerRetailConnection;
                if (string.IsNullOrEmpty(packagingID))
                {
                    packagingID = generatePackaging(_ShippedStoreId);
                }

                try
                {
                    _log.Debug("ShippedStoreID : " + _ShippedStoreId.ToString() + "\t PackageSlip : " + packagingID + "\t CreateUserID : " + _CreatedBy.ToString() +
                                "\t CheInTime : " + _ChekOutTime.ToString() + "\n RFIDs : " + RFIDs);
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ShippedStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ShippedStoreId));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, packagingID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CreateUserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CreatedBy));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CheInTime", SqlDbType.DateTime, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ChekOutTime));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@isAdHoc", SqlDbType.Bit, 1, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, isAdhoc));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, userName));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    scmCmdToExecute.CommandTimeout = 20 * 60;
                    scmCmdToExecute.ExecuteNonQuery();

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_CheckIN' reported the ErrorCode: " + _errorCode);
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::CheckIn::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();

                    _log.Debug("KT_CICOMaster:CheckIn:: Exiting");

                }
            }
        
            public bool ValidatePackaging(string packagingID)
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_PacakageSlip_Validate]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;
                               
                scmCmdToExecute.Connection = _trackerRetailConnection;
                bool isValid = false;
                string Errmessage = string.Empty;
                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, packagingID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CurrentStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _SourceStoreID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@Message", SqlDbType.VarChar, 50, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, Errmessage));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@IsValid", SqlDbType.Bit, 1, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, isValid));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    scmCmdToExecute.ExecuteNonQuery();
                    Errmessage = scmCmdToExecute.Parameters["@Message"].Value.ToString();
                    if (!string.IsNullOrEmpty(Errmessage))
                    {
                        throw new Exception(Errmessage);
                    }

                    isValid = Convert.ToBoolean(scmCmdToExecute.Parameters["@IsValid"].Value);
                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_PacakageSlip_Validate' reported the ErrorCode: " + _errorCode);
                    }

                    return isValid;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    if (string.IsNullOrEmpty(Errmessage))
                    {
                        throw new Exception("KT_CICOMaster::ValidatePackaging::Error occured.", ex);
                    }
                    else
                    {
                        throw new Exception(Errmessage);
                    }
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();
                }
            }


            public DataTable GetCategoriesForPrinting(string PackingSlip)
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_GetCategories_OnPackageSlip_ForPrint]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;

                DataTable dtToReturn = new DataTable("ProductCategory");
                SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

                scmCmdToExecute.Connection = _trackerRetailConnection;


                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PackingSlip));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    sdaAdapter.Fill(dtToReturn);

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_GetCategories_OnPackageSlip_ForPrint' reported the ErrorCode: " + _errorCode);
                    }

                    return dtToReturn;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::GetCategoriesForPrinting::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();
                }
            }


            public DataTable GetCategoriesOnPackagingID()
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_GetCategories_OnPackageSlip]"; 
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;

                DataTable dtToReturn = new DataTable("ProductCategory");
                SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

                scmCmdToExecute.Connection = _trackerRetailConnection;
                

                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PackageSlip));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    sdaAdapter.Fill(dtToReturn);

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_GetCategories_OnPackageSlip' reported the ErrorCode: " + _errorCode);
                    }

                    return dtToReturn;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::GetCategoriesOnPackagingID::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();
                }
            }

            public DataSet GetCategoriesOnPackagingID_PID()
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_GetCategories_OnPackageSlip]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;

                DataSet dsToReturn = new DataSet("ProductCategory");
                SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

                scmCmdToExecute.Connection = _trackerRetailConnection;


                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PackageSlip));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    sdaAdapter.Fill(dsToReturn);

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_GetCategories_OnPackageSlip' reported the ErrorCode: " + _errorCode);
                    }

                    return dsToReturn;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::GetCategoriesOnPackagingID::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();
                }
            }

            public DataTable GetSourceStoreForPackageSlip(string packingSlip)
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_GetSourceStoreForPackagingID]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;

                DataTable dtToReturn = new DataTable("ProductCategory");
                SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

                scmCmdToExecute.Connection = _trackerRetailConnection;


                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, packingSlip));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    sdaAdapter.Fill(dtToReturn);

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_GetSourceStoreForPackagingID' reported the ErrorCode: " + _errorCode);
                    }

                    return dtToReturn;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::GetSourceStoreForPackageSlip::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();
                }
            }

// New Method for packingSlip data on PackingSlip and RFIDs

            public DataTable GetCategoriesOnPackagingID_OnRFID(string RFIDs)
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_GetCategories_OnPackageSlip_OnRFID]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;

                DataTable dtToReturn = new DataTable("ProductCategory");
                SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

                scmCmdToExecute.Connection = _trackerRetailConnection;


                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PackageSlip));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    sdaAdapter.Fill(dtToReturn);

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_GetCategories_OnPackageSlip_OnRFID' reported the ErrorCode: " + _errorCode);
                    }

                    return dtToReturn;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::GetCategoriesOnPackagingID_OnRFID::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();
                }
            }


//// New Method for check in 
            public bool CheckOut_OnPID(string RFIDs, bool isAdhoc, string ProductItemsIds, out string packagingID)
            {

                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_CheckOut_OnPID]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;

                //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
                // Use base class' connection object
                scmCmdToExecute.Connection = _trackerRetailConnection;


                packagingID = string.Empty;
                packagingID = generatePackaging(_SourceStoreID);
                //UniquePackingId = packagingID;

                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@SourceStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _SourceStoreID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ShippedStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ShippedStoreId));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@RR_ID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _RR_ID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, packagingID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ReplenishmentRequest", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _RR));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CreateUserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CreatedBy));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CheoutTime", SqlDbType.DateTime, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ChekOutTime));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ProductItemIds", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, ProductItemsIds));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@isAdHoc", SqlDbType.Bit, 1, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, isAdhoc));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.
                    scmCmdToExecute.CommandTimeout = 20 * 60;
                    scmCmdToExecute.ExecuteNonQuery();

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        //_log.Error("SourceStoreID : " + SourceStoreID + ", ShippedStoreID" + ShippedStoreId + ",RR_ID :" + RR_ID + ",isAdHoc:" + isAdhoc + " ,ReplenishmentRequest : " + _RR + ", RFIDs :" + RFIDs);
                        throw new Exception("Stored Procedure 'pr_CheckOut_OnPID' reported the ErrorCode: " + _errorCode);
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::CheckOut_OnPID::Error occured. " + ex.Message);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();
                }
            }

            public bool CheckOut_OnPID_HH(bool isAdhoc, string ProductItemsIds, string packagingID, out string UniquePackingId)
            {

                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_CheckOut_OnPID_HH]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;

                //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
                // Use base class' connection object
                scmCmdToExecute.Connection = _trackerRetailConnection;


                if (string.IsNullOrEmpty(packagingID))
                {
                    packagingID = generatePackaging(_ShippedStoreId);
                }
                UniquePackingId = packagingID;

                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@SourceStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _SourceStoreID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ShippedStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ShippedStoreId));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@RR_ID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _RR_ID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, packagingID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ReplenishmentRequest", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _RR));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CreateUserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CreatedBy));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CheoutTime", SqlDbType.DateTime, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ChekOutTime));
                   // scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ProductItemIds", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, ProductItemsIds));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@isAdHoc", SqlDbType.Bit, 1, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, isAdhoc));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.
                    scmCmdToExecute.CommandTimeout = 20 * 60;
                    scmCmdToExecute.ExecuteNonQuery();

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        //_log.Error("SourceStoreID : " + SourceStoreID + ", ShippedStoreID" + ShippedStoreId + ",RR_ID :" + RR_ID + ",isAdHoc:" + isAdhoc + " ,ReplenishmentRequest : " + _RR + ", RFIDs :" + RFIDs);
                        throw new Exception("Stored Procedure 'pr_CheckOut_OnPID_HH' reported the ErrorCode: " + _errorCode);
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::CheckOut_OnPID_HH::Error occured. " + ex.Message);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();
                }
            }
         

            public bool CheckIn_OnPID(string RFIDs, string packagingID, bool isAdhoc, string userName, string ProductIDs, string OverrideProductIDs)
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_CheckIN_OnPID]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;
                _log.Debug("KT_CICOMaster:CheckIn_OnPID:: Entering");

                //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
                // Use base class' connection object
                scmCmdToExecute.Connection = _trackerRetailConnection;
                if (string.IsNullOrEmpty(packagingID))
                {
                    packagingID = generatePackaging(_ShippedStoreId);
                }

                try
                {
                    _log.Debug("ShippedStoreID : " + _ShippedStoreId.ToString() + "\t PackageSlip : " + packagingID + "\t CreateUserID : " + _CreatedBy.ToString() +
                                "\t CheInTime : " + _ChekOutTime.ToString() + "\n RFIDs : " + RFIDs);
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ShippedStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ShippedStoreId));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, packagingID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CreateUserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CreatedBy));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CheInTime", SqlDbType.DateTime, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ChekOutTime));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ProductItemIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, ProductIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@overridePrdIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, OverrideProductIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@isAdHoc", SqlDbType.Bit, 1, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, isAdhoc));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, userName));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    scmCmdToExecute.CommandTimeout = 20 * 60;
                    scmCmdToExecute.ExecuteNonQuery();

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_CheckIN_OnPID' reported the ErrorCode: " + _errorCode);
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::CheckIn_OnPID::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();

                    _log.Debug("KT_CICOMaster:CheckIn_OnPID:: Exiting");

                }
            }
  
            public bool CheckIn_OnPID_HH(string packagingID, bool isAdhoc, string userName, string ProductIDs, string OverrideProductIDs,out string PackingIDonAdhoc)
 
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_CheckIN_OnPID_HH]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;
                _log.Debug("KT_CICOMaster:CheckIn_OnPID_HH:: Entering");

                //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
                // Use base class' connection object
                scmCmdToExecute.Connection = _trackerRetailConnection;
                if (string.IsNullOrEmpty(packagingID))
                {
                    packagingID = generatePackaging(_ShippedStoreId);                   
                }
                PackingIDonAdhoc = packagingID;
                try
                {
                    _log.Debug("ShippedStoreID : " + _ShippedStoreId.ToString() + "\t PackageSlip : " + packagingID + "\t CreateUserID : " + _CreatedBy.ToString() +
                                "\t CheInTime : " + _ChekOutTime.ToString() + "\n ProductIDs : " + ProductIDs);
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ShippedStoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ShippedStoreId));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, packagingID));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CreateUserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CreatedBy));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@CheckInTime", SqlDbType.DateTime, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _ChekOutTime));
                   // scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ProductItemIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, ProductIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@overridePrdIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, OverrideProductIDs));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@isAdHoc", SqlDbType.Bit, 1, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, isAdhoc));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, userName));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    scmCmdToExecute.CommandTimeout = 20 * 60;
                    scmCmdToExecute.ExecuteNonQuery();

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_CheckIN_OnPID_HH' reported the ErrorCode: " + _errorCode);
                    }

                    return true;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::CheckIn_OnPID_HH::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();

                    _log.Debug("KT_CICOMaster:CheckIn_OnPID_HH:: Exiting");

                }
            }


            public DataTable GetCategoriesOnPackagingID_OnPID(string ProductItemIds)
            {
                SqlCommand scmCmdToExecute = new SqlCommand();
                scmCmdToExecute.CommandText = "dbo.[pr_GetCategories_OnPackageSlip_OnPID]";
                scmCmdToExecute.CommandType = CommandType.StoredProcedure;
                scmCmdToExecute.CommandTimeout = 20 * 60;

                DataTable dtToReturn = new DataTable("ProductCategory");
                SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

                scmCmdToExecute.Connection = _trackerRetailConnection;

                try
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@PackageSlip", SqlDbType.VarChar, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PackageSlip));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ProductItemIds", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, ProductItemIds));
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                    // Open connection.
                    if (_trackerRetailConnectionIsCreatedLocal)
                    {
                        // Open connection.
                        _trackerRetailConnection.Open();
                    }

                    // Execute query.

                    sdaAdapter.Fill(dtToReturn);

                    _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                    if (_errorCode != 0)
                    {
                        // Throw error.
                        throw new Exception("Stored Procedure 'pr_GetCategories_OnPackageSlip_OnPID' reported the ErrorCode: " + _errorCode);
                    }

                    return dtToReturn;

                }
                catch (Exception ex)
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("KT_CICOMaster::GetCategoriesOnPackagingID_OnPID::Error occured.", ex);
                }
                finally
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                    scmCmdToExecute.Dispose();
                }
            }

           
       

///////////////////////////////////////////

            public string generatePackaging(int StoreID)
            {

                string packaging = string.Empty;

                StringBuilder packagingslip = new StringBuilder();
                //packagingslip.Append(AppConfigSettings.StoreID.ToString());

                string storeID = StoreID.ToString();

                int i = 0;
                for (i = storeID.Length; i < 4; i++)
                {
                    storeID = "0" + storeID;
                }
                packagingslip.Append(storeID);
                packagingslip.Append(DateTime.Now.Year.ToString().Remove(0, 2));

                string daysofyear = DateTime.Now.DayOfYear.ToString();
                if (daysofyear.Length < 3)
                {
                    for (i = daysofyear.Length; i < 3; i++)
                    {
                        daysofyear = "0" + daysofyear;
                    }
                }

                packagingslip.Append(daysofyear);

                string hour_24 = DateTime.Now.Hour.ToString();
                if (hour_24.Length < 2)
                    hour_24 = "0" + hour_24;

                packagingslip.Append(hour_24);

                string minutes = DateTime.Now.Minute.ToString();
                if (minutes.Length < 2)
                    minutes = "0" + minutes;

                packagingslip.Append(minutes);

                string seconds = DateTime.Now.Second.ToString();
                if (seconds.Length < 2)
                    seconds = "0" + seconds;

                packagingslip.Append(seconds);


                return packagingslip.ToString();

            }

        #endregion


    }
}
