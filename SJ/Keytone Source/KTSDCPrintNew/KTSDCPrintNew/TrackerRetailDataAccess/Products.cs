using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;

namespace TrackerRetailDataAccess
{
    public class Products : DBInteractionBase
    {
        #region Private Fields
        private string _UPC;
        private string _SKU;
        private int _StoreID;
        private string _Desc;
        private string _VendorName;
        private string _MfgName;
        private string _StyleCode;
        private string _StyleDesc;
        private string _ColorCode;
        private string _ColorDesc;
        private string _SizeCode;
        private string _SizeDesc;
        private int _QtyOnHand;
        private int _QtyMin;
        private int _QtyMax;
        private string _Loc1;
        private string _Loc2;
        private object _Price;
        private bool _Clearance;
        private DateTime _DateCreated;
        private DateTime _DateModified;
        private DateTime _DateRemoved;
        private bool _IsActive;
        private string _Custom1;
        private string _Custom2;
        private string _Custom3;
        private string _Custom4;
        private string _Custom5;
        private string _Custom6;
        private string _Custom7;
        private string _Custom8;
        private string _Custom9;
        private string _Custom10;
        private string _Custom11;
        private string _Custom12;
        private string _Custom13;
        private string _Custom14;
        private string _Custom15;
        private string _Custom16;
        private string _Custom17;
        private string _Custom18;
        private string _Custom19;
        private string _Custom20;

        private string _listNumber;

        #endregion

        #region Constructors
        public Products() { }
        #endregion

        #region Methods

        public DataTable GetCategoryForRFID_AdHoc(string rfids, int storeID)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_SelectProducts_OnRFID]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            DataTable dtToReturn = new DataTable("Product");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDS", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, rfids));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@STOREID", SqlDbType.Int, 4,ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, storeID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_SelectProducts_OnRFID' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::GetCategoryForRFID_AdHoc::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }

        public DataTable GetProducts_OnRFID_CICOAdhoc(string rfids, int storeID, string OperationType)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_SelectProducts_OnRFID_CICOAdHoc]";// "dbo.[pr_SelectProducts_OnRFID_CheckIN]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            DataTable dtToReturn = new DataTable("Product");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDS", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, rfids));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@STOREID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, storeID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@OperationType", SqlDbType.VarChar, 25, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, OperationType));            
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_SelectProducts_OnRFID_CICOAdHoc' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::GetProducts_OnRFID_CICOAdhoc::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }


        public DataTable GetCategoryForRFID_CheckInAdHoc(string rfids, int storeID)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_SelectProducts_CheckInAdHoc]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            DataTable dtToReturn = new DataTable("Product");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDS", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, rfids));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@STOREID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, storeID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_SelectProducts_CheckInAdHoc' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::GetCategoryForRFID_CheckInAdHoc::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }

        public DataTable GetCategoryForRFID_Replenishment(string rfids, int storeID, long rrNumber)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_SelectProductsForCheckOut_OnRFID]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            DataTable dtToReturn = new DataTable("Product");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDS", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, rfids));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@STOREID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, storeID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RR_ID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, rrNumber));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_SelectProductsForCheckOut_OnRFID' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::GetCategoryForRFID_Replenishment::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }


        public DataTable GetProductdetailsForUPC()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_GetProductDetailsForUPC]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            DataTable dtToReturn = new DataTable("Product");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
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
                    throw new Exception("Stored Procedure 'pr_GetProductDetailsForUPC' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::GetProductdetailsForUPC::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }


        public DataTable GetProductdetailsForUPC1()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_GetProductDetailsForUPC1]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            DataTable dtToReturn = new DataTable("Product");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
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
                    throw new Exception("Stored Procedure 'pr_GetProductDetailsForUPC' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::GetProductdetailsForUPC::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }

        public DataTable FetchPONumber(string PONumber)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_FetchPOOrder]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            DataTable dtToReturn = new DataTable("PO");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = _trackerRetailConnection;

            SqlString errMsg = SqlString.Null;
            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@PONumber", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, PONumber));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORMSG", SqlDbType.VarChar, 50, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, errMsg));

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
                    errMsg = (SqlString)scmCmdToExecute.Parameters["@ERRORCODE"].Value;
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_GetProductDetailsForUPC' reported the ErrorCode: " + _errorCode +" Message: " + errMsg.ToString());
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::GetProductdetailsForUPC::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }


        public bool InsertProduct(string UPC, string SKU, int storeId, string Description, string Cost, float Price, int BatchUOM, string VendorName)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_InsertProduct_Printing]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Debug("Products:InsertProduct:: Entering");
            _log.Debug("Products:InsertProduct:: Entering");

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            _errorCode = 0;
            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@SKU", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, SKU));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, storeId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Description));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Cost", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Cost));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Price));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@BatchUOM", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, BatchUOM));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@VendorName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, VendorName));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
              
                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = 0;
                _errorCode = SqlInt32.Parse(scmCmdToExecute.Parameters["@ERRORCODE"].Value.ToString());
             
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_InsertProduct_Printing' reported the ErrorCode: " + _errorCode);
                }


                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                _log.Error("Products::InsertProduct::Error occured.", ex.Message);
                throw new Exception("Products::InsertProduct::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:InsertProduct:: Exiting");

            }
        }

        public bool UpdateProduct(string UPC, string SKU, int storeId, string Description, string Cost, float Price, int BatchUOM, string VendorName)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_UpdateProduct_Printing]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Debug("Products:InsertProduct:: Entering");
            _log.Debug("Products:InsertProduct:: Entering");

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            _errorCode = 0;
            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@SKU", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, SKU));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, storeId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Description));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Cost", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Cost));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Price));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@BatchUOM", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, BatchUOM));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@VendorName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, VendorName));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = 0;
                _errorCode = SqlInt32.Parse(scmCmdToExecute.Parameters["@ERRORCODE"].Value.ToString());

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_InsertProduct_Printing' reported the ErrorCode: " + _errorCode);
                }


                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                _log.Error("Products::InsertProduct::Error occured.", ex.Message);
                throw new Exception("Products::InsertProduct::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:InsertProduct:: Exiting");

            }
        }


        public bool UpdateSingleAssociatedItem(int DeviceID,bool IsReturned,string RFID)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_InsertProducts_OnSingleAssociation]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Debug("Products:UpdateSingleAssociatedItem:: Entering");

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDTagID", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, RFID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@SKU", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, SKU));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@STOREID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, DeviceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@IsReturned", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, IsReturned));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                if (_errorCode != 0 && _errorCode != -1)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_InsertProducts_OnSingleAssociation' reported the ErrorCode: " + _errorCode);
                }

                
                if (_errorCode == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::UpdateSingleAssociatedItem::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:UpdateSingleAssociatedItem:: Exiting");

            }
        }

        public bool UpdateProductsOnUPC(int SearchOnType)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_InsertProduct_OnUpcSku]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Debug("Products:UpdateProductsOnUPC:: Entering");
             
            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@SearchOnType", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, SearchOnType));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Value", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@STOREID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

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
                throw new Exception("Products::UpdateProductsOnUPC::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:UpdateProductsOnUPC:: Exiting");
            }

        }

        public bool InsertProduct_OnUpcSku(int SeachType , out int ErrorCode)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_InsertProduct_OnUpcSku]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Debug("Products:InsertProduct_OnUpcSku:: Entering");

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            ErrorCode = 0;
            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@SearchOnType", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, SeachType));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Value", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                if (_errorCode == 9999)
                {
                    ErrorCode = 9999;
                }

                else if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_InsertProduct_OnUpcSku' reported the ErrorCode: " + _errorCode);
                }

                
                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::InsertProduct_OnUpcSku::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:InsertProduct_OnUpcSku:: Exiting");

            }
        }

        public bool InsertProductItems(string RFIDs, out int Rejected, out int Added)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_InsertProductItems_Association]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20*60;
            _log.Debug("Products:InsertProductItems:: Entering");

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            Rejected = 0;
            Added = 0;

           
            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@SKU", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, SKU));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, 0));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Rejected", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Added", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;
                Rejected = Convert.ToInt32(scmCmdToExecute.Parameters["@Rejected"].Value);
                Added = Convert.ToInt32(scmCmdToExecute.Parameters["@Added"].Value);
               
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_InsertProductItems_Association' reported the ErrorCode: " + _errorCode);
                }


                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                _log.Error("Products::InsertProductItems::Error occured.", ex.Message);
                throw new Exception("Products::InsertProductItems::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:InsertProductItems:: Exiting");

            }
        }

        public bool InsertProductItems(int DeviceID, string RFIDs, out int Rejected, out int Added)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_InsertProductItems_Association]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Debug("Products:InsertProduct_OnUpcSku:: Entering");
            _log.Debug("Products:InsertProductItems:: Entering");

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            Rejected = 0;
            Added = 0;


            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@SKU", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, SKU));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, DeviceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Rejected", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Added", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;
                Rejected = Convert.ToInt32(scmCmdToExecute.Parameters["@Rejected"].Value);
                Added = Convert.ToInt32(scmCmdToExecute.Parameters["@Added"].Value);

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_InsertProductItems_Association' reported the ErrorCode: " + _errorCode);
                }


                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                _log.Error("Products::InsertProductItems::Error occured.", ex.Message);
                throw new Exception("Products::InsertProductItems::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:InsertProductItems:: Exiting");

            }
        } 
        

        public bool GeneratePutAwayList(string ListType , string status , int userID,out string ListNumber , string PutAwayDetails)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_GeneratePutAwayList]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Debug("Products:GeneratePutAwayList:: Entering");

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            if (string.IsNullOrEmpty(ListNumberGenerated))
            {
                ListNumber = string.Empty;
                ListNumber = generateListNumber(StoreID);
            }
            else
                ListNumber = ListNumberGenerated;
            //ListNumber = packagingID;


            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ListNumber", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, ListNumber));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ListType", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, ListType));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, status));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@STOREID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@PutAwayDetails", SqlDbType.Xml, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, PutAwayDetails));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, userID));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;

                if (_errorCode != 0 && _errorCode != -1)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_GeneratePutAwayList' reported the ErrorCode: " + _errorCode);
                }


                if (_errorCode == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                _log.Debug("Products::GeneratePutAwayList::Error occured:", ex.Message);
                throw new Exception("Products::GeneratePutAwayList::Error occured.", ex);
                
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:GeneratePutAwayList:: Exiting");

            }
        }


        public string generateListNumber(int StoreID)
        {

            string packaging = string.Empty;

            StringBuilder ListNumber = new StringBuilder();
            //packagingslip.Append(AppConfigSettings.StoreID.ToString());

            string storeID = StoreID.ToString();

            int i = 0;
            for (i = storeID.Length; i < 4; i++)
            {
                storeID = "0" + storeID;
            }
            ListNumber.Append(storeID);
            ListNumber.Append(DateTime.Now.Year.ToString().Remove(0, 2));

            string daysofyear = DateTime.Now.DayOfYear.ToString();
            if (daysofyear.Length < 3)
            {
                for (i = daysofyear.Length; i < 3; i++)
                {
                    daysofyear = "0" + daysofyear;
                }
            }

            ListNumber.Append(daysofyear);

            string hour_24 = DateTime.Now.Hour.ToString();
            if (hour_24.Length < 2)
                hour_24 = "0" + hour_24;

            ListNumber.Append(hour_24);

            string minutes = DateTime.Now.Minute.ToString();
            if (minutes.Length < 2)
                minutes = "0" + minutes;

            ListNumber.Append(minutes);

            string seconds = DateTime.Now.Second.ToString();
            if (seconds.Length < 2)
                seconds = "0" + seconds;

            ListNumber.Append(seconds);


            return ListNumber.ToString();

        }

        //public DataTable GetProductDetailsForUPC()
        //{
        //    SqlCommand scmCmdToExecute = new SqlCommand();
        //    scmCmdToExecute.CommandText = "dbo.[pr_GetProductDetailsForUPC]";
        //    scmCmdToExecute.CommandType = CommandType.StoredProcedure;
        //    DataTable dtToReturn = new DataTable("Product");
        //    SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

        public DataTable GetUPCImage(string UPC, int storeId)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_SelectUPCImage]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 0;
            DataTable dtToReturn = new DataTable("Product");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@storeID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, storeId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_SelectUPCImage' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::GetUPCImage::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }

        public bool SaveImageForUPC(byte[] img,string UPC,int StoreID)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_UpdateImageForUPC]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 0;
            _log.Debug("Products:SaveImageForUPC:: Entering");

            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Image", SqlDbType.Image, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, img));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_UpdateImageForUPC' reported the ErrorCode: " + _errorCode);
                }

                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::SaveImageForUPC::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:SaveImageForUPC:: Exiting");
            }
        }

        public bool UpdateUPC(string UPC, int storeID, string Desc, string vendorName, double price, int minQty, int maxQty)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_UpdateUPC]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 0;
            _log.Debug("Products:UpdateUPC:: Entering");

            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UPC", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, UPC));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, storeID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Desc));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Vendor", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, vendorName));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, price));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@MaxQty", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, maxQty));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@MinQty", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, minQty));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_UpdateUPC' reported the ErrorCode: " + _errorCode);
                }

                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::UpdateUPC::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:UpdateUPC:: Exiting");
            }
        }


        public DataTable GetRFIDDetails_OnRFIDs(string rfids)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_GetRFIDDetails_onRFID]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            DataTable dtToReturn = new DataTable("RFIDs");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDLIST", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, rfids));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_GetRFIDDetails_onRFID' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::GetRFIDDetails_OnRFIDs::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }


        public bool UndoDecommissionRFID( long ProductItemID, string RFID)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_UndoDecommissionRFID]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
      
            scmCmdToExecute.Connection = _trackerRetailConnection;

            int errorCode;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ProductItemID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ProductItemID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFID", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RFID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
               

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();

                errorCode = Convert.ToInt32(Convert.ToString(scmCmdToExecute.Parameters["@Error"].Value));
             

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_UndoDecommissionRFID' reported the ErrorCode: " + _errorCode);
                }


                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                _log.Error("Products::UndoDecommissionRFID::Error occured.", ex.Message);
                throw new Exception("Products::UndoDecommissionRFID::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:DecommissionRFID:: Exiting");

            }
        }

        public bool UndoDecommission_OnRFIDs(string ProductItemIDs, string RFIDs)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_UndoDecommission_OnRFIDs]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;

            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ProductItemIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ProductItemIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));


                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;


                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_UndoDecommission_OnRFIDs' reported the ErrorCode: " + _errorCode);
                }


                return true;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                _log.Error("Products::UndoDecommissionRFID::Error occured.", ex.Message);
                throw new Exception("Products::UndoDecommissionRFID::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("Products:DecommissionRFID:: Exiting");

            }
        }

        public DataTable GetCategoryForRFID_Replenishment_OnRFID(string rfids, int storeID, long rrNumber)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_SelectProductsForCheckOut_PI_OnRFID]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            DataTable dtToReturn = new DataTable("Product");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            //SqlConnection _mainConnection = new SqlConnection(AppConfigSettings.GetInstance().TrackRetailConnectoinString);
            // Use base class' connection object
            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDS", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, rfids));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@STOREID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, storeID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RR_ID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, rrNumber));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_SelectProductsForCheckOut_PI_OnRFID' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Products::GetCategoryForRFID_Replenishment_OnRFID::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }



        #endregion

        #region Public Properties
        public string UPC { get { return _UPC; } set { _UPC = value; } }
        public string SKU { get { return _SKU; } set { _SKU = value; } }
        public int StoreID { get { return _StoreID; } set { _StoreID = value; } }
        public string Desc { get { return _Desc; } set { _Desc = value; } }
        public string VendorName { get { return _VendorName; } set { _VendorName = value; } }
        public string MfgName { get { return _MfgName; } set { _MfgName = value; } }
        public string StyleCode { get { return _StyleCode; } set { _StyleCode = value; } }
        public string StyleDesc { get { return _StyleDesc; } set { _StyleDesc = value; } }
        public string ColorCode { get { return _ColorCode; } set { _ColorCode = value; } }
        public string ColorDesc { get { return _ColorDesc; } set { _ColorDesc = value; } }
        public string SizeCode { get { return _SizeCode; } set { _SizeCode = value; } }
        public string SizeDesc { get { return _SizeDesc; } set { _SizeDesc = value; } }
        public int QtyOnHand { get { return _QtyOnHand; } set { _QtyOnHand = value; } }
        public int QtyMin { get { return _QtyMin; } set { _QtyMin = value; } }
        public int QtyMax { get { return _QtyMax; } set { _QtyMax = value; } }
        public string Loc1 { get { return _Loc1; } set { _Loc1 = value; } }
        public string Loc2 { get { return _Loc2; } set { _Loc2 = value; } }
        public object Price { get { return _Price; } set { _Price = value; } }
        public bool Clearance { get { return _Clearance; } set { _Clearance = value; } }
        public DateTime DateCreated { get { return _DateCreated; } set { _DateCreated = value; } }
        public DateTime DateModified { get { return _DateModified; } set { _DateModified = value; } }
        public DateTime DateRemoved { get { return _DateRemoved; } set { _DateRemoved = value; } }
        public bool IsActive { get { return _IsActive; } set { _IsActive = value; } }
        public string Custom1 { get { return _Custom1; } set { _Custom1 = value; } }
        public string Custom2 { get { return _Custom2; } set { _Custom2 = value; } }
        public string Custom3 { get { return _Custom3; } set { _Custom3 = value; } }
        public string Custom4 { get { return _Custom4; } set { _Custom4 = value; } }
        public string Custom5 { get { return _Custom5; } set { _Custom5 = value; } }
        public string Custom6 { get { return _Custom6; } set { _Custom6 = value; } }
        public string Custom7 { get { return _Custom7; } set { _Custom7 = value; } }
        public string Custom8 { get { return _Custom8; } set { _Custom8 = value; } }
        public string Custom9 { get { return _Custom9; } set { _Custom9 = value; } }
        public string Custom10 { get { return _Custom10; } set { _Custom10 = value; } }
        public string Custom11 { get { return _Custom11; } set { _Custom11 = value; } }
        public string Custom12 { get { return _Custom12; } set { _Custom12 = value; } }
        public string Custom13 { get { return _Custom13; } set { _Custom13 = value; } }
        public string Custom14 { get { return _Custom14; } set { _Custom14 = value; } }
        public string Custom15 { get { return _Custom15; } set { _Custom15 = value; } }
        public string Custom16 { get { return _Custom16; } set { _Custom16 = value; } }
        public string Custom17 { get { return _Custom17; } set { _Custom17 = value; } }
        public string Custom18 { get { return _Custom18; } set { _Custom18 = value; } }
        public string Custom19 { get { return _Custom19; } set { _Custom19 = value; } }
        public string Custom20 { get { return _Custom20; } set { _Custom20 = value; } }

        public string ListNumberGenerated { get { return _listNumber; } set { _listNumber = value; } }
        #endregion
    }
}
