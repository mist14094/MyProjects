using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;
namespace KTone.DAL.SmartDCDataAccess
{
    public class CycleCountInstance : DBInteractionBase
    {
        #region Class Member Declarations

        private Int32 _cycleCountID, _dataOwnerID;
        private String _instanceName, _createdBy, _comments, _performedBy, _currentState, _cycleCountInstanceID, _tagIDs;
        private DateTime _createDate, _updateDate;
        private SqlString _assetXML;


        private long _CCInstanceID;
        private int _DataOwnerID;
        private long _CCMasterID;
        private DateTime _StartDate;
        private DateTime _EndDate;
        private int _CurrentStatus;
        private int _TotalGTINS;
        private int _UploadedGTINS;
        private int _FoundGTINS;
        private int _MissingGTINS;
        private int _NewGTINS;
        private string _Comment;
        private DateTime _CreatedDate;
        private int _CreatedBy;
        private DateTime _UpdatedDate;
        private int _UpdatedBy;

        #endregion

        #region [Class Method Declaration]

        public DataTable SelectAllCycleCountInstanceWCCID()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_CycleCountInstance_SelectAllWCCID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CycleCountInstance");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@iCycleCountID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _cycleCountID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
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
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_SelectAllCycleCountInstanceWCCID' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::SelectOne::Error occured.", ex);
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

        public override bool Insert()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@dataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@cCMasterID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CCMasterID));
                cmdToExecute.Parameters.Add(new SqlParameter("@startDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, StartDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@currentStatus", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CurrentStatus));
                cmdToExecute.Parameters.Add(new SqlParameter("@totalGTINS", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, TotalGTINS));
                cmdToExecute.Parameters.Add(new SqlParameter("@uploadedGTINS", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UploadedGTINS));
                cmdToExecute.Parameters.Add(new SqlParameter("@foundGTINS", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, FoundGTINS));
                cmdToExecute.Parameters.Add(new SqlParameter("@missingGTINS", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, MissingGTINS));
                cmdToExecute.Parameters.Add(new SqlParameter("@newGTINS", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, NewGTINS));
                cmdToExecute.Parameters.Add(new SqlParameter("@comment", SqlDbType.VarChar, 300, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Comment));
                cmdToExecute.Parameters.Add(new SqlParameter("@createdDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, CreatedDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@createdBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, Convert.ToInt32(CreatedBy)));

                cmdToExecute.Parameters.Add(new SqlParameter("@cCInstanceID", SqlDbType.BigInt, 8, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, CCInstanceID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                //CycleCountID = (SqlInt32)cmdToExecute.Parameters["@CycleCountID"].Value;
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstance_Insert' reported the ErrorCode: " + _errorCode);
                }
                _CCInstanceID = Convert.ToInt32(cmdToExecute.Parameters["@cCInstanceID"].Value);
                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::Insert::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
            }
        }

        public bool InsertGTINS()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountInstanceAndDetails_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@dataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, DataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@cCMasterID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CCMasterID));
                cmdToExecute.Parameters.Add(new SqlParameter("@startDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, StartDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@createdDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, CreatedDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@createdBy", SqlDbType.Int, 4, ParameterDirection.Input ,false, 10, 0, "", DataRowVersion.Proposed, Convert.ToInt32(CreatedBy)));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                _mainConnection.Open();

                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstanceAndDetails_Insert' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::InsertGTINS::Error occured." + ex.Message, ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
            }
        }

        public void UpdateOnDownload(int hhid, int hhAssoID, bool isPartialDownload, string downloadXML, bool hasNewGtins)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountInstace_UpdateDBOnDownLoad]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CCINSTANCEID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CCInstanceID));
                cmdToExecute.Parameters.Add(new SqlParameter("@HHASSOID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, hhAssoID));
                cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, hhid));
                cmdToExecute.Parameters.Add(new SqlParameter("@ISPARTIALDOWNLOAD", SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, isPartialDownload));
                cmdToExecute.Parameters.Add(new SqlParameter("@DOC", SqlDbType.Xml, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, downloadXML));
                cmdToExecute.Parameters.Add(new SqlParameter("@HASNEWGTINS", SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, hasNewGtins));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                _mainConnection.Open();

                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    throw new Exception("Stored Procedure 'pr_CycleCountInstace_UpdateDBOnDownLoad' reported the ErrorCode: " + _errorCode);
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::UpdateOnDownload::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
            }
        }

        public void UpdateMissingGTINSOnPartialDownload(int hhAssoID)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_UpdateMissingGTINSOnPartialDownload]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@Handheld_Asso_id", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, hhAssoID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CCInstanceID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CCInstanceID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                _mainConnection.Open();

                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    throw new Exception("Stored Procedure 'pr_CycleCountInstance_UpdateMissingGTINSOnPartialDownload' reported the ErrorCode: " + _errorCode);
                }
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::UpdateMissingGTINSOnPartialDownload::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
            }
        }

        public override DataTable SelectAll()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_SelectAll]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("CycleCountInstance");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));

                scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstance_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::SelectAll::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }


        #region CycleCount Instance Active

        public DataTable SelectActiveInstances()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_SelectActive]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("CycleCountInstance");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstance_SelectActive' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::SelectActiveInstances::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }

        #endregion

        public DataTable SelectOneByCCMasterID()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_SelectOne_ByCCMasterID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("CycleCountInstance");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ccMasterID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CCMasterID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstance_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::SelectOne::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }

        public int GetGTINCount(string filterString)
        {
            int gtinCount = 0;
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_GetGTINCount]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            scmCmdToExecute.Connection = _mainConnection;
            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@filterString", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, filterString));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@GtinCount", SqlDbType.Int, 4, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, gtinCount));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _errorCode));

                _mainConnection.Open();

                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@Error"].Value;


                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstance_GetGTINCount' reported the ErrorCode: " + _errorCode);
                }
                gtinCount = (int)scmCmdToExecute.Parameters["@GtinCount"].Value;
            }
            catch (Exception ex)
            {
                throw new Exception("CycleCountInstance::GetGTINCount::Error occured.", ex);
            }
            finally
            {
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
            return gtinCount;
        }

        public override bool Update()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CycleCountInstanceID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _cycleCountInstanceID));
                cmdToExecute.Parameters.Add(new SqlParameter("@PerformedBy", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _performedBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@Comments", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _comments));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstance_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::Update::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
            }
        }


        public virtual bool UpdateCCInstanceStatus()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_UpdateStatus]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@iCycleCountInstanceID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _cycleCountInstanceID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CurrentState", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _currentState));
                cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstance_UpdateStatus' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::UpdateCCInstanceStatus::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
            }
        }

        public bool Update_CurrentStatus(long ccInstanceId)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_UpdateCurrentStatus]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCInstanceId", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ccInstanceId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _UpdatedBy));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::Update_CurrentStatus::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_SelectOne_ByCCMasterID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("CycleCountInstance");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ccMasterID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _CCMasterID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Error", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@Error"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstance_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::SelectOne::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }



        public override bool Delete()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_Delete]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CycleCountInstanceID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _cycleCountInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstance_Delete' reported the ErrorCode: " + _errorCode);
                }

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

        public DataTable GetMissingAssetInstances()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_GetMissingAssetInstances]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("MissingInstance");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure '[pr_GetMissingAssetInstances]' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::GetMissingAssetInstances::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                cmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }

        /// <summary>
        /// CycleCountInstance's Name should be unique.Returns true if name is available
        /// Mandatory class property :InstanceName
        /// </summary>
        /// <returns></returns>
        public bool IsCCINameAvailable()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_CheckName]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.Connection = _mainConnection;
            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@InstanceName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _instanceName));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                int ReturnVal = (int)scmCmdToExecute.ExecuteScalar();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountInstance_CheckName' reported the ErrorCode: " + _errorCode);
                }

                if (ReturnVal == 0)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::IsCCINameAvailable::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        #region [CCINstanceAssets]

        public virtual DataSet GetInstanceAssets()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_GetAllAssets]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet dsToReturn = new DataSet("CCInstanceAssets");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iCycleCountInstanceID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _cycleCountInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dsToReturn);
                if (dsToReturn.Tables.Count > 0)
                {
                    dsToReturn.Tables[0].TableName = "Asset";
                    if (dsToReturn.Tables.Count > 1)
                        dsToReturn.Tables[1].TableName = "AssetTags";
                }

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountAssets_SelectOne' reported the ErrorCode: " + _errorCode);
                }
                return dsToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::GetInstanceAssets::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }

        public virtual DataSet GetInstanceSections()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountInstance_GetAllSection]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet dsToReturn = new DataSet("CCInstanceSection");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iCycleCountInstanceID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _cycleCountInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dsToReturn);
                if (dsToReturn.Tables.Count > 0)
                {
                    dsToReturn.Tables[0].TableName = "Section";
                    if (dsToReturn.Tables.Count > 1)
                        dsToReturn.Tables[1].TableName = "SectionTags";
                }

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCountAssets_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                return dsToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::GetInstanceSections::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }

        /// <summary>
        /// When assets are downloaded into hh or uploaded to server call this menthod
        /// </summary>
        /// <param name="cycleCountInstanceID"></param>
        /// <param name="handHeldID"></param>
        /// <param name="synchOperation">Upload/Download</param>
        /// <param name="assetDetailXML"></param>
        /// <returns></returns>
        public virtual bool UpdateCycleCountAssetOnSynch(string cycleCountInstanceID, int handHeldID,
            string synchOperation, string assetDetailXML)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCount_UpdateOnUploadDownLoad]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CycleCountInstanceID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, cycleCountInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, handHeldID));
                SqlParameter param = new SqlParameter("@AssetDetailXML", SqlDbType.Xml);
                param.Direction = ParameterDirection.Input;
                param.Value = assetDetailXML;
                scmCmdToExecute.Parameters.Add(param);

                scmCmdToExecute.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, synchOperation));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCount_UpdateOnUploadDownLoad' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::UpdateCycleCountAssetOnSynch::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        /// <summary>
        /// When missing assets are downloaded into hh or uploaded to server call this menthod
        /// </summary>
        /// <param name="cycleCountInstanceID"></param>
        /// <param name="handHeldID"></param>
        /// <param name="synchOperation">Upload/Download</param>
        /// <param name="assetDetailXML"></param>
        /// <returns></returns>
        public virtual bool UpdateMissingAssetOnSynch(string cycleCountInstanceID, int handHeldID,
            string synchOperation, string assetDetailXML)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountMissingAsset_UpdateOnUploadDownload]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CycleCountInstanceID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, cycleCountInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, handHeldID));
                SqlParameter param = new SqlParameter("@AssetDetailXML", SqlDbType.Xml);
                param.Direction = ParameterDirection.Input;
                param.Value = assetDetailXML;
                scmCmdToExecute.Parameters.Add(param);

                scmCmdToExecute.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, synchOperation));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure '[pr_CycleCountMissingAsset_UpdateOnUploadDownload]' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountAssets::UpdateMissingAssetOnSynch::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public bool UpdateAsManuallyReconciled(string cycleCountInstanceID, string assetDetailXML)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCountAsset_UpdateAsManuallyReconciled]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CycleCountInstanceID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, cycleCountInstanceID));
                SqlParameter param = new SqlParameter("@AssetDetailXML", SqlDbType.Xml);
                param.Direction = ParameterDirection.Input;
                param.Value = assetDetailXML;
                scmCmdToExecute.Parameters.Add(param);

                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure '[pr_CycleCountAsset_UpdateAsManuallyReconciled]' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::UpdateAsManuallyReconciled::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        #endregion [CCINstanceAssets]

        public DataTable GetAuditFoundAssetsOnTagList()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_GetAuditFoundAssetsOnTagList]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CycleCountInstance");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@AuditInstanceID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _cycleCountInstanceID));
                cmdToExecute.Parameters.Add(new SqlParameter("@TagIDs", SqlDbType.VarChar, 0, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _tagIDs));
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
                    throw new Exception("Stored Procedure 'pr_GetAuditFoundAssetsOnTagList' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::GetAuditFoundAssetsOnTagList::Error occured.", ex);
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

        public virtual bool UpdateCycleCountAssetsInOnlineMode(string cycleCountInstanceID, int handHeldID)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CycleCount_UpdateOnDownloadInOnlineMode]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CycleCountInstanceID", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, cycleCountInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, handHeldID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_CycleCount_UpdateOnDownloadInOnlineMode' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CycleCountInstance::UpdateCycleCountAssetsInOnlineMode::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        #endregion

        #region [Class Property Declarations]

        public String PerformedBy
        {
            get
            {
                return _performedBy;
            }
            set
            {
                _performedBy = value;
            }
        }

        public String CurrentState
        {
            get
            {
                return _currentState;
            }
            set
            {
                _currentState = value;
            }
        }

        public String InstanceName
        {
            get
            {
                return _instanceName;
            }
            set
            {
                _instanceName = value;
            }
        }

        public String CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }

        public String Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
            }
        }

        public String CycleCountInstanceID
        {
            get
            {
                return _cycleCountInstanceID;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("CycleCountInstanceID", "CycleCountInstanceID can't be NULL");
                }
                _cycleCountInstanceID = value;
            }
        }

        public Int32 CycleCountID
        {
            get
            {
                return _cycleCountID;
            }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentOutOfRangeException("CycleCountID", "CycleCountID can't be NULL");
                }
                _cycleCountID = value;
            }
        }

        public SqlString AssetXML
        {
            get
            {
                return _assetXML;
            }
            set
            {
                _assetXML = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
            }
        }

        public DateTime UpdateDate
        {
            get
            {
                return _updateDate;
            }
            set
            {
                SqlDateTime daUpdateDateTmp = (SqlDateTime)value;
                if (daUpdateDateTmp.IsNull)
                {
                    throw new ArgumentOutOfRangeException("daUpdateDate", "daUpdateDate can't be NULL");
                }
                _updateDate = value;
            }
        }

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

        public String TagIDs
        {
            get
            {
                return _tagIDs;
            }
            set
            {
                _tagIDs = value;
            }
        }


        //Added by Datta
        public long CCInstanceID { get { return _CCInstanceID; } set { _CCInstanceID = value; } }
        public long CCMasterID { get { return _CCMasterID; } set { _CCMasterID = value; } }
        public DateTime StartDate { get { return _StartDate; } set { _StartDate = value; } }
        public DateTime EndDate { get { return _EndDate; } set { _EndDate = value; } }
        public int CurrentStatus { get { return _CurrentStatus; } set { _CurrentStatus = value; } }
        public int TotalGTINS { get { return _TotalGTINS; } set { _TotalGTINS = value; } }
        public int UploadedGTINS { get { return _UploadedGTINS; } set { _UploadedGTINS = value; } }
        public int FoundGTINS { get { return _FoundGTINS; } set { _FoundGTINS = value; } }
        public int MissingGTINS { get { return _MissingGTINS; } set { _MissingGTINS = value; } }
        public int NewGTINS { get { return _NewGTINS; } set { _NewGTINS = value; } }
        public string Comment { get { return _Comment; } set { _Comment = value; } }
        public DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
        //public int CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }
        public DateTime UpdatedDate { get { return _UpdatedDate; } set { _UpdatedDate = value; } }
        public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }

        #endregion
    }
}
