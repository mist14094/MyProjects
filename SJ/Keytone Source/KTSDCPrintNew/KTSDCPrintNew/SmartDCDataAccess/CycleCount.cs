using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;


namespace KTone.DAL.SmartDCDataAccess
{
    public class CycleCount : DBInteractionBase
    {

        private Int32 _Type, _DataOwnerID, _CreatedBy, _UpdatedBy, _dataOwnerId;
        private Int64 _CCMasterID;
        private String _Description, _Name, _Comments, _RequestedBy, _MonitoredBy, _ApprovedBy, _ErrorMessage;
        private Boolean _isActive;
        private DateTime _CreatedDate, _UpdatedDate;

        public CycleCount()
        {
        }


        #region select

        //SelectOne()

        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountMaster_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CCMasterID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _CCMasterID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _DataOwnerID));

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
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountMaster_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {
                    _Name = toReturn.Rows[0]["Name"].ToString();
                    _Type = Convert.ToInt32(toReturn.Rows[0]["Type"]);
                    _Description = toReturn.Rows[0]["Description"].ToString();
                    _Comments = toReturn.Rows[0]["Comments"].ToString();
                    _RequestedBy = toReturn.Rows[0]["RequestedBy"].ToString();
                    _MonitoredBy = toReturn.Rows[0]["MonitoredBy"].ToString();
                    _ApprovedBy = toReturn.Rows[0]["ApprovedBy"].ToString();
                    _DataOwnerID = Convert.ToInt32(toReturn.Rows[0]["DataOwnerID"]);
                    _isActive = Convert.ToBoolean(toReturn.Rows[0]["IsActive"]);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCount::SelectOne::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        //selectAll()

        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountMaster_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CycleCountMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _DataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

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
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountMaster_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCount::SelectAll::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }


        // SelectAllLocationIdLogic()
        public DataTable SelectAllCycleCountLogic()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountMaster_SelectAllHHIdLogic]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CycleCountMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new SqlParameter("@CCMasterID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _CCMasterID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _DataOwnerID));

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
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountMaster_SelectAllHHIdLogic' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("::SelectAllCCMasterIDLogic::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        #endregion


        #region insert CycleCount

        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:CycleCountMaster ; " + " Name:{0},Description:{1}", _Name, _Description);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountMaster_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Name));
                cmdToExecute.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Type));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Description));
                cmdToExecute.Parameters.Add(new SqlParameter("@Comments", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Comments));
                cmdToExecute.Parameters.Add(new SqlParameter("@RequestedBy", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _RequestedBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@MonitoredBy", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _MonitoredBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@ApprovedBy", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _ApprovedBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isActive));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _DataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _CreatedBy)); 
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _CreatedDate));                  
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@CCMasterID", SqlDbType.BigInt, 8, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _CCMasterID));

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
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                _CCMasterID = (Int64)cmdToExecute.Parameters["@CCMasterID"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountMaster_Insert' reported the ErrorCode: " + _errorCode);
                }
                if (_CCMasterID == 0)
                { 
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountMaster_Insert' reported the ErrorCode: " + _CCMasterID);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCount::Insert::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Insert");
            }
        }

        #endregion

        #region Update CycleCount

        public override bool Update()
        {
            _log.Trace("Entering Update - Table:CycleCountMaster ; " + " Name:{0},Description:{1}", _Name, _Description);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountMaster_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CCMasterID", SqlDbType.BigInt, 8, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _CCMasterID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Name));
                cmdToExecute.Parameters.Add(new SqlParameter("@Type", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Type));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Description));
                cmdToExecute.Parameters.Add(new SqlParameter("@Comments", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _Comments));
                cmdToExecute.Parameters.Add(new SqlParameter("@RequestedBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _RequestedBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@MonitoredBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _MonitoredBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@ApprovedBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _ApprovedBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _isActive));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _DataOwnerID));                
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _UpdatedBy));                
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _UpdatedDate));

                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

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

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountMaster_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCount::Update::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Update");
            }
        }

        #endregion
 
        public override bool Delete()
        {
            _log.Trace("Entering Delete - Table:CycleCount ; CCMasterID:{0}", _CCMasterID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountMaster_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CCMasterID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CCMasterID));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _DataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _ErrorMessage));
                

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
                _ErrorMessage = Convert.ToString(cmdToExecute.Parameters["@ErrorMessage"].Value);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (!string.IsNullOrEmpty(_ErrorMessage))
                {
                    throw new Exception(_ErrorMessage);
                }
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountMaster_Delete' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Delete:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Delete.");
            }
        }


        /// <summary>
        /// This method returns all SKU's from trSKU_Master table with it's Company and Product Info.
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAllSKUs()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCount_GetAllSKUs]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable();
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (Int32)((System.Data.SqlTypes.SqlInt32)(scmCmdToExecute.Parameters["@ErrorCode"].Value)).Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCount_GetAllSKUs' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCount::SelectAllSKUs::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }


        //public DataTable GetCyclecountDetails(long CycleCountMasterId)
        //{
        //    SqlCommand scmCmdToExecute = new SqlCommand();
        //    scmCmdToExecute.CommandText = "dbo.[pr_CycleCount_GetDetails]";
        //    scmCmdToExecute.CommandType = CommandType.StoredProcedure;
        //    DataTable dtToReturn = new DataTable("CycleCountInstanceDetail");
        //    SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

        //    // Use base class' connection object
        //    scmCmdToExecute.Connection = _mainConnection;

        //    try
        //    {
        //        scmCmdToExecute.Parameters.Add(new SqlParameter("@CycleCountId", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CycleCountMasterId));
        //        scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
        //        // Open connection.
        //        _mainConnection.Open();

        //        // Execute query.
        //        sdaAdapter.Fill(dtToReturn);

        //        _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

        //        if (_errorCode != (int)LLBLError.AllOk)
        //        {
        //            // Throw error.
        //            throw new Exception("Stored Procedure 'pr_CycleCount_Details' reported the ErrorCode: " + _errorCode);
        //        }

        //        return dtToReturn;
        //    }
        //    catch (Exception ex)
        //    {
        //        // some error occured. Bubble it to caller and encapsulate Exception object
        //        throw new Exception("CycleCountMaster::GetCyclecountDetails::Error occured.", ex);
        //    }
        //    finally
        //    {
        //        // Close connection.
        //        _mainConnection.Close();
        //        scmCmdToExecute.Dispose();
        //        sdaAdapter.Dispose();
        //    }
        //}

        //public DataTable GetCyclecountInstanceDetails(long CycleCountInstanceId)
        //{
        //    SqlCommand scmCmdToExecute = new SqlCommand();
        //    scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_GetDetails]";
        //    scmCmdToExecute.CommandType = CommandType.StoredProcedure;
        //    DataTable dtToReturn = new DataTable("CycleCountDetials");
        //    SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

        //    // Use base class' connection object
        //    scmCmdToExecute.Connection = _mainConnection;

        //    try
        //    {
        //        scmCmdToExecute.Parameters.Add(new SqlParameter("@CycleCountInstanceId", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CycleCountInstanceId));
        //        scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
        //        // Open connection.

        //        _mainConnection.Open();

        //        // Execute query.
        //        sdaAdapter.Fill(dtToReturn);

        //        _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

        //        if (_errorCode!= (int)LLBLError.AllOk)
        //        {
        //            // Throw error.
        //            throw new Exception("Stored Procedure 'pr_CycleCountInstance_GetDetails' reported the ErrorCode: " + _errorCode);
        //        }

        //        return dtToReturn;
        //    }
        //    catch (Exception ex)
        //    {
        //        // some error occured. Bubble it to caller and encapsulate Exception object
        //        throw new Exception("CycleCountMaster::GetCyclecountInstanceDetails::Error occured.", ex);
        //    }
        //    finally
        //    {
        //        // Close connection.
        //        _mainConnection.Close();
        //        scmCmdToExecute.Dispose();
        //        sdaAdapter.Dispose();
        //    }
        //}

        //public DataTable GetCyclecount_ViewNameAndGtinCount(int CCMasterID)
        //{
        //    SqlCommand scmCmdToExecute = new SqlCommand();
        //    scmCmdToExecute.CommandText = "dbo.[pr_CycleCount_GetViewNameAndGTINCount]";
        //    scmCmdToExecute.CommandType = CommandType.StoredProcedure;
        //    DataTable dtToReturn = new DataTable("CycleCountInstanceDetails");
        //    SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

        //    // Use base class' connection object
        //    scmCmdToExecute.Connection = _mainConnection;
        //    try
        //    {
        //        scmCmdToExecute.Parameters.Add(new SqlParameter("@CCMasterID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CCMasterID));
        //        // Open connection.
        //        _mainConnection.Open();
        //        // Execute query.
        //        sdaAdapter.Fill(dtToReturn);

        //        return dtToReturn;
        //    }
        //    catch (Exception ex)
        //    {
        //        // some error occured. Bubble it to caller and encapsulate Exception object
        //        throw new Exception("CycleCountMaster::pr_CycleCount_GetViewNameAndGTINCount::Error occured.", ex);
        //    }
        //    finally
        //    {
        //        // Close connection.
        //        _mainConnection.Close();
        //        scmCmdToExecute.Dispose();
        //        sdaAdapter.Dispose();
        //    }

        //}

        //public DataTable Select_CC_GTIN_View_WCCMasterID(long CCMasterID)
        //{
        //    SqlCommand scmCmdToExecute = new SqlCommand();
        //    scmCmdToExecute.CommandText = "dbo.[pr_CC_GTIN_View_SelectOneWCCMasterID]";
        //    scmCmdToExecute.CommandType = CommandType.StoredProcedure;
        //    DataTable dtToReturn = new DataTable("CycleCountMaster");
        //    SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

        //    // Use base class' connection object
        //    scmCmdToExecute.Connection = _mainConnection;

        //    try
        //    {
        //        scmCmdToExecute.Parameters.Add(new SqlParameter("@CCMasterID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CCMasterID));
        //        scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

        //        // Open connection.
        //        _mainConnection.Open();

        //        // Execute query.
        //        sdaAdapter.Fill(dtToReturn);
        //        _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

        //        if (_errorCode != (int)LLBLError.AllOk)
        //        {
        //            // Throw error.
        //            throw new Exception("Stored Procedure 'Select_CC_GTIN_View_WCCMasterID' reported the ErrorCode: " + _errorCode);
        //        }
        //        return dtToReturn;
        //    }
        //    catch (Exception ex)
        //    {
        //        // some error occured. Bubble it to caller and encapsulate Exception object
        //        throw new Exception("CycleCountMaster::Select_CC_GTIN_View_WCCMasterID::Error occured.", ex);
        //    }
        //    finally
        //    {
        //        // Close connection.
        //        _mainConnection.Close();
        //        scmCmdToExecute.Dispose();
        //        sdaAdapter.Dispose();
        //    }

        //}


        public Int64 CCMasterID
        {
            get
            {
                return _CCMasterID;

            }
            set
            {
                _CCMasterID = value;
            }
        }

        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public int Type
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }

        public String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        public String Comments
        {
            get
            {
                return _Comments;
            }
            set
            {
                _Comments = value;
            }
        }

        public String RequestedBy
        {
            get
            {
                return _RequestedBy;
            }
            set
            {
                _RequestedBy = value;
            }
        }

        public String MonitoredBy
        {
            get
            {
                return _MonitoredBy;
            }
            set
            {
                _MonitoredBy = value;
            }
        }

        public String ApprovedBy
        {
            get
            {
                return _ApprovedBy;
            }
            set
            {
                _ApprovedBy = value;
            }
        }

        public Int32 DataOwnerID
        {
            get
            {
                return _DataOwnerID;
            }

            set
            {
                _DataOwnerID = value;
            }
        }

        public Int32 CreatedBy
        {
            get
            {
                return _CreatedBy;
            }

            set
            {
                _CreatedBy = value;
            }
        }

        public Int32 UpdatedBy
        {
            get
            {
                return _UpdatedBy;
            }

            set
            {
                _UpdatedBy = value;
            }
        }

        public Boolean IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
            }
        }

        public DateTime UpdatedDate
        {
            get
            {
                return _UpdatedDate;
            }
            set
            {
                _UpdatedDate = value;
            }
        }

        public String ErrorMessage
        {
            get
            {
                return _ErrorMessage;
            }
            set
            {
                _ErrorMessage = value;
            }

        }
    }
}