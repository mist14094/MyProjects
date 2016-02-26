using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace KTone.DAL.SmartDCDataAccess
{
    public class CycleCountInstanceDetail : DBInteractionBase
    {
        #region Constructors
        public CycleCountInstanceDetail() { }
        #endregion

        #region Private Fields
        private int _CCInstanceDetailID;
        private int  _dataOwnerID;
        private long _CCInstanceID;
        private int _SKU_ID;
        private int _HHID;
        private int _LocationId;
        private int _StatusID;
        private int _ItemID;
        private string _GTIN;
        private DateTime _LastSeenTimeStamp;
        private string _CapturedBy;
        private bool _ManuallyReconciled;
        private bool _MissingGTINUploaded;
        private string _Comment;
        private DateTime _LastUpdateTime;
        private int _NoOfTimeUpdated;
        private bool _IsUploaded;
        private int _updatedBy;
        #endregion

        #region Public Methodes

        public int Insert(string filterString)
        {
            string header = "CycleCountInstanceDetail:Insert";
            _log.Trace(header + "entering");
            int gtinCount = 0;
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountDetails_InsertGTINs]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            try
            {
                cmdToExecute.Connection = _mainConnection;

                cmdToExecute.Parameters.Add(new SqlParameter("@filterString", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, filterString   ));
                cmdToExecute.Parameters.Add(new SqlParameter("@ccInstanceID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _CCInstanceID));
                cmdToExecute.Parameters.Add(new SqlParameter("@GtinCount", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, gtinCount));
                cmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ItemMaster_Insert' reported the ErrorCode: " + _errorCode);
                }
            }
            catch (Exception ex)
            {
                _log.Error(header + " Insert : {0}", ex.Message);
                throw new Exception(header + "::Error occured.", ex);
            } 
            finally
            {
                _mainConnection.Close();
                cmdToExecute.Dispose();
                _log.Trace(header + "Leaving");
            }
            return gtinCount;
        }

        public DataTable SelectGTINDataToUpload_WCCInstanceId( int uploadCount)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceDetails_SelectGTIN_WCCInstanceID]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("CycleCountDetails");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCInstanceID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CCInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@uploadCount", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, uploadCount));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstanceDetails_SelectGTIN_WCCInstanceID' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstanceDetails::SelectGTINDataToUpload_WCCInstanceId::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }

        public DataTable Select_SKUDetails(string filterString)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCount_SKUDetails]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable();
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@filterString", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, filterString));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@cycleCountInstanceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CCInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCount_SKUDetails' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstanceDetail::Select_SKUDetails::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }

        public override DataTable SelectAll()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceDetails_SelectAll]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("CycleCountInstance");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstanceDetails_SelectAll' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstanceDetails::SelectAll::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
            return dtToReturn;
        }

        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceDetails_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("CycleCountInstanceDetails");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CCInstanceID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CCInstanceID));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstanceDetails_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstanceDetails::SelectOne::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }


        public DataTable SelectGTINs_WSkuMasterId( int skuMasterid, int GtinCount)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceDetails_SelectOnSKUId]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable();
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCInstanceID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CCInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@SKUMasterID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, skuMasterid));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@gtinCount", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, GtinCount));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstanceDetails_SelectOnSKUId' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstanceDetail::SelectGTINs_WSkuMasterId::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }


        }

        public DataTable SelectMissingGTINs_WCCInstanceId()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceDetails_SelectMissingGtins]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable();
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCInstanceID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CCInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountDetails_SelectMissingGtins' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstanceDetail::pr_CycleCountInstanceDetails_SelectMissingGtins::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }


        }

        public bool UpdateCycleCountDetailsOnUpload( int gtinCount, int hhId,int updatedBy)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceDetails_Update_WGTINCount]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ccInstanceID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CCInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@hhid", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, hhId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@gtinCount", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, gtinCount));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@updatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, updatedBy));
                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstanceDetails::pr_CycleCountInstanceDetails_Update_WGTINCount::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool UpdateCycleCountDetailsOnGTIN(int hhId, string gtinUrn, int updatedBy)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceDetails_Update_WGTIN]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ccInstanceID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CCInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@hhid", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, hhId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@gtin", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, gtinUrn));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@updatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, updatedBy));
                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstanceDetails::pr_CycleCountInstanceDetails_Update_WGTIN::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool UpdateCycleCountDetailsOnSKUUpload(int hhId, int skuMasterid, int gtinCount, int updatedBy)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceDetails_Update_WSKUId]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ccInstanceID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CCInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@hhid", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, hhId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@skuid", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, skuMasterid));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@gtinCount", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, gtinCount));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@updatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, updatedBy));
                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstanceDetails::pr_CycleCountInstanceDetails_Update_WSKUId::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool UpdateCycleCountDetailsOnMissingUpload(int hhid)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceDetails_UpdateOnMissingUpload]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCInstanceID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CCInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@hhId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, hhid));

                _mainConnection.Open();

                scmCmdToExecute.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("CycleCountInstanceDetails::pr_CycleCountInstanceDetails_UpdateOnMissingUpload::Error occured",ex);
            }
            finally
            {
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }


        public bool UpdateCycleCountDetailOnDownload(string gtinUrn, DateTime lastSeenTime, int HandheldID, string locationId,
            bool isFound, bool isNewGtin, bool isPartialDownload, int handheld_Asso_id, long CCInstanceID)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceDetails_UpdateOnDownLoad]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {

                scmCmdToExecute.Parameters.Add(new SqlParameter("@GTINUrn", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, gtinUrn));
                
                scmCmdToExecute.Parameters.Add(new SqlParameter("@HandheldId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, HandheldID));
                if (lastSeenTime.ToString() == "1/1/0001 12:00:00 AM")
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@LastSeenTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, null));
                }
                else
                {
                    scmCmdToExecute.Parameters.Add(new SqlParameter("@LastSeenTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, lastSeenTime));
                }
                scmCmdToExecute.Parameters.Add(new SqlParameter("@LocationId", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, locationId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@isFound", SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isFound));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@isNewGtin", SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isNewGtin));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@isPartialDownload", SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isPartialDownload));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Handheld_Asso_id", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, handheld_Asso_id));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ccInstanceId", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CCInstanceID));
                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception(ex.Message);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool UpdateCcycleCountInstanceForRFTAGID(string RFTAGIDs)
        {
            _log.Trace("Entering pr_CycleCountInstanceForRFTAGID_Update -  Table: LocationID:{0} ," + _LocationId);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceForRFTAGID_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RFTAGIDS", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, RFTAGIDs));
                cmdToExecute.Parameters.Add(new SqlParameter("@CCINSTANCEID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed,_CCInstanceID));
                cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _HHID));
                cmdToExecute.Parameters.Add(new SqlParameter("@LOCATIONID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _LocationId));
                cmdToExecute.Parameters.Add(new SqlParameter("@LASTSEENTIME", SqlDbType.DateTime, 20, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _LastSeenTimeStamp));
                cmdToExecute.Parameters.Add(new SqlParameter("@UPDATEDBY", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _updatedBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@DATAOWNERID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;


                if (_errorCode == -558)
                {
                    throw new Exception("Can not Update CCInstanceDetails.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstanceForRFTAGID_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("pr_CycleCountInstanceForRFTAGID_Update::Update::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                UpdateNotifyCacheUpdateTable();
                _log.Trace("Exiting UpdateCcycleCountInstanceForRFTAGID");
            }
        }

        #endregion

        #region Public Properties

        public int CCInstanceDetailID { get { return _CCInstanceDetailID; } set { _CCInstanceDetailID = value; } }
        public long CCInstanceID { get { return _CCInstanceID; } set { _CCInstanceID = value; } }
        public int SKU_ID { get { return _SKU_ID; } set { _SKU_ID = value; } }
        public int HHID { get { return _HHID; } set { _HHID = value; } }
        public int LocationId { get { return _LocationId; } set { _LocationId = value; } }
        public int UpdatedBy { get { return _updatedBy; } set { _updatedBy = value; } }
        public int StatusID { get { return _StatusID; } set { _StatusID = value; } }
        public int ItemID { get { return _ItemID; } set { _ItemID = value; } }
        public string GTIN { get { return _GTIN; } set { _GTIN = value; } }
        public DateTime LastSeenTimeStamp { get { return _LastSeenTimeStamp; } set { _LastSeenTimeStamp = value; } }
        public string CapturedBy { get { return _CapturedBy; } set { _CapturedBy = value; } }
        public bool ManuallyReconciled { get { return _ManuallyReconciled; } set { _ManuallyReconciled = value; } }
        public bool MissingGTINUploaded { get { return _MissingGTINUploaded; } set { _MissingGTINUploaded = value; } }
        public string Comment { get { return _Comment; } set { _Comment = value; } }
        public DateTime LastUpdateTime { get { return _LastUpdateTime; } set { _LastUpdateTime = value; } }
        public int NoOfTimeUpdated { get { return _NoOfTimeUpdated; } set { _NoOfTimeUpdated = value; } }
        public bool IsUploaded { get { return _IsUploaded; } set { _IsUploaded = value; } }
        public Int32 DataOwnerID
        {
            get
            {
                return _dataOwnerID;
            }
            set
            {
                _dataOwnerID = value;
            }
        }
        #endregion
    }
}
