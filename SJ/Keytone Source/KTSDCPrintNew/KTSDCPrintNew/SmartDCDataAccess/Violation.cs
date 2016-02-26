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
    public class Violation : DBInteractionBase
    {

        private Int32 _violationid, _violationruleid, _dataOwnerId, _roleID, _notSeenItemsDays, _notreceivedItemsDays,_purgedBy;
        private long _PurgedITEMS;
        private bool _isacknowledged;
        private string _violationdescription, _violationrulename, _acknowledgeby, _acknowledgecomment, _violationids, _violationrules;
        private DateTime _acknowledgetime, _purgebeforedate;
        private string _fromDate, _toDate;
        public Violation()
        {
        }
        #region Select Violation
        public DataTable SelectAllForViolationRule()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ViolationForRule_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Violations");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerId));
                cmdToExecute.Parameters.Add(new SqlParameter("@RuleID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _violationruleid));               
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@IsAcknowledged", SqlDbType.Bit, 4, ParameterDirection.Input,false, 10, 0, "", DataRowVersion.Proposed, _isacknowledged));
                cmdToExecute.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.VarChar, 50, ParameterDirection.Input,true, 10, 0, "", DataRowVersion.Proposed, _fromDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@todate", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _toDate));

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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ViolationRule_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Violation::SelectAll::Error occured.", ex);
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
        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ViolationRule_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("ViolationRules");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {

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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_ViolationRule_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Violation::SelectAll::Error occured.", ex);
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
        public bool AcknowledgeViolation()
        {
            _log.Trace("Entering Acknowledge Violation ; " +
            " ViolationID:{0},Acknowledge Time:{1},Acknowledged By:{2},Acknowledge Comment:{3}", _violationids, _acknowledgetime,_acknowledgeby,_acknowledgecomment);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_UpdateViolations]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ViolationIDs", SqlDbType.VarChar,1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _violationids));
                cmdToExecute.Parameters.Add(new SqlParameter("@AcknowledgeTime", SqlDbType.DateTime, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _acknowledgetime));
                cmdToExecute.Parameters.Add(new SqlParameter("@AcknowledgeBy", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _acknowledgeby));
                cmdToExecute.Parameters.Add(new SqlParameter("@AcknowledgeComment", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _acknowledgecomment));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Location_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                 throw new Exception("Location::Update::Error occured.", ex);
              
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
                _log.Trace("Exiting Violation Acknowledge");
            }
        }
        #region Home
        public DataSet HomePageInfoForCount()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_HomePageInfo_toDisplay]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            //DataTable toReturn = new DataTable("HomePageInfoCount");
            DataSet toReturn = new DataSet("HomePageInfo");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerId));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_HomePageInfo_toDisplay' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Home Page Info::SelectAll::Error occured.", ex);
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
        public DataTable HomePageInfoForViolationAlerts()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_HomePageInfo_toDisplay]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("HomePageInfoCount");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_HomePageInfo_toDisplay' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Home Page Info::SelectAll::Error occured.", ex);
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

        #region PurgeViolation
        public long Purge_Violations()
        {
            _log.Trace("Entering Purge_Violations  ; ");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_PurgeViolationItems_ByRules]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@NotSeenItemsDays", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _notSeenItemsDays));
                cmdToExecute.Parameters.Add(new SqlParameter("@NotReceivedItemsDays", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _notreceivedItemsDays));
                cmdToExecute.Parameters.Add(new SqlParameter("@Purgebeforedate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _purgebeforedate));
                cmdToExecute.Parameters.Add(new SqlParameter("@Violationrules", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _violationrules));
                cmdToExecute.Parameters.Add(new SqlParameter("@PurgedBy", SqlDbType.Int, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _purgedBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerId", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerId));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@CountBinTapeIDs", SqlDbType.BigInt, 8, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _PurgedITEMS));

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
                _PurgedITEMS = Convert.ToInt64(cmdToExecute.Parameters["@CountBinTapeIDs"].Value);                
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;


                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_PurgeViolationItems_ByRules' reported the ErrorCode: " + _errorCode);
                }

                return _PurgedITEMS;
            }
            catch (Exception ex)
            {
                _log.Error("Purge_Violations:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Violation::Purge_Violations::Error occured.", ex);

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
                _log.Trace("Exiting Purge_Violations");
            }
        }
        #endregion PurgeViolation

        public Int32 ViolationID
        {
            get
            {
                return _violationid;
            }
            set
            {
                _violationid = value;
            }
        }
        public Int32 ViolationRuleID
        {
            get
            {
                return _violationruleid;
            }
            set
            {
                _violationruleid = value;
            }
        }
        public string ViolationRuleName
        {
            get
            {
                return _violationrulename;
            }
            set
            {
                _violationrulename = value;
            }
        }
        public string ViolationDescription
        {
            get
            {
                return _violationdescription;
            }
            set
            {
                _violationdescription = value;
            }
        }
        public Int32 DataOwnerId
        {
            get
            {
                return _dataOwnerId;
            }
            set
            {
                _dataOwnerId = value;
            }
        }
        public Int32 RoleID
        {
            get
            {
                return _roleID;
            }
            set
            {
                _roleID = value;
            }
        }

        public Int32 NotSeenItemsDays
        {
            get
            {
                return _notSeenItemsDays;
            }
            set
            {
                _notSeenItemsDays = value;
            }
        }
        public Int32 NotReceivedItemsDays
        {
            get
            {
                return _notreceivedItemsDays;
            }
            set
            {
                _notreceivedItemsDays = value;
            }
        }

        public Int32 PurgedBy
        {
            get
            {
                return _purgedBy;
            }
            set
            {
                _purgedBy = value;
            }
        }
        public DateTime Purgebeforedate
        {
            get
            {
                return _purgebeforedate;
            }
            set
            {
                _purgebeforedate = value;
            }
        }
        public String Violationrules
        {
            get
            {
                return _violationrules;
            }
            set
            {
                _violationrules = value;
            }
        }
        public bool IsAcknowledged
        {
            get
            {
                return _isacknowledged;
            }
            set
            {
                _isacknowledged = value;
            }
        }
        public string AknowledgeBy
        {
            get
            {
                return _acknowledgeby;
            }
            set
            {
                _acknowledgeby = value;
            }
        }
        public string AcknowledgeComment
        {
            get
            {
                return _acknowledgecomment;
            }
            set
            {
                _acknowledgecomment = value;
            }
        }
        public string ViolationIDs
        {
            get
            {
                return _violationids;
            }
            set
            {
                _violationids = value;
            }
        }
        public DateTime AcknowledgeTime
        {
            get
            {
                return _acknowledgetime;
            }
            set
            {
                _acknowledgetime = value;
            }
        }
        public string FromDate
        {
            get
            {
                return _fromDate;
            }
            set
            {
                _fromDate = value;
            }
        }
        public string ToDate
        {
            get
            {
                return _toDate;
            }
            set
            {
                _toDate = value;
            }
        }
    }
}
