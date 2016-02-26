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
    public class KT_CycleCount : DBInteractionBase
    {

        #region Private Fields
        private int _StoreID;
        private int _ZoneID;
        private int _DeptID;
        private string _DeptName;
        private string _ZoneName;
        private DateTime _CreatedDate;
        private DateTime _UpdatedDate;
        private int _UpdatedBy;
        private int _CyclecountID;
        private int _DeviceID;
        #endregion

        #region Constructors
        public KT_CycleCount() { }
        #endregion


        #region Methods

        public DataTable GetDeptsAndZones()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_GetDepartmentAndZones]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            DataTable dtToReturn = new DataTable("CycleCount");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
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
                    throw new Exception("Stored Procedure 'pr_GetDepartmentAndZones' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("KT_CycleCount::GetDeptsAndZones::Error occured.", ex);
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

        public DataTable CheckCycleCountForDay()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_CheckCycleCountForDay]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            DataTable dtToReturn = new DataTable("CycleCount");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            scmCmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ZoneID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ZoneID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CreatedDate));
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
                    throw new Exception("Stored Procedure 'pr_CheckCycleCountForDay' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("KT_CycleCount::CheckCycleCountForDay::Error occured.", ex);
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

        public bool UpdateCycleCount(string RFIDs, out int Cyclecount, out int Unknown)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_UpdateCycleCountItems]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Warn("KT_CycleCount:UpdateCycleCount:: Entering");

            scmCmdToExecute.Connection = _trackerRetailConnection;
            Cyclecount = 0; Unknown = 0;
            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ZoneID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ZoneID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CreatedDate));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UpdatedBy));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCountID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CycleCountID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CycleCount", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Unknown", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                _log.Warn("KT_CycleCount:UpdateCycleCount:: DB call");
                scmCmdToExecute.ExecuteNonQuery();
                _log.Warn("KT_CycleCount:UpdateCycleCount:: DB call END");
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;
                Cyclecount = Convert.ToInt32(scmCmdToExecute.Parameters["@CycleCount"].Value);
                Unknown = Convert.ToInt32(scmCmdToExecute.Parameters["@Unknown"].Value);

                if (_errorCode == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("KT_CycleCount::UpdateCycleCount::Error occured." + ex.Message, ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("KT_CycleCount:UpdateCycleCount:: Exiting");

            }
        }

        public bool UpdateCycleCount_LastCall(string RFIDs, bool? IsLastCall, out int Cyclecount, out int Unknown)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_UpdateCycleCountItems]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Warn("KT_CycleCount:UpdateCycleCount:: Entering");

            scmCmdToExecute.Connection = _trackerRetailConnection;
            Cyclecount = 0; Unknown = 0;
            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ZoneID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ZoneID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CreatedDate));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UpdatedBy));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCountID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CycleCountID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@IsLastCall", SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, IsLastCall));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CycleCount", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Unknown", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.
                _log.Warn("KT_CycleCount:UpdateCycleCount:: DB call");
                scmCmdToExecute.ExecuteNonQuery();
                _log.Warn("KT_CycleCount:UpdateCycleCount:: DB call END");
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;
                Cyclecount = Convert.ToInt32(scmCmdToExecute.Parameters["@CycleCount"].Value);
                Unknown = Convert.ToInt32(scmCmdToExecute.Parameters["@Unknown"].Value);

                if (_errorCode == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("KT_CycleCount::UpdateCycleCount::Error occured." + ex.Message, ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("KT_CycleCount:UpdateCycleCount:: Exiting");

            }
        }


        //Display Complaince
        public bool UpdateCycleCountForDC(string RFIDs)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_UpdateCycleCountforDisplayComplaince]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Debug("KT_CycleCount:UpdateCycleCountForDC:: Entering");

            scmCmdToExecute.Connection = _trackerRetailConnection;
            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DisplayZone", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, ZoneID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 25, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CreatedDate));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, UpdatedBy));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCountID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, CycleCountID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, DeviceID));
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

                if (_errorCode == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("KT_CycleCount::UpdateCycleCountForDC::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("KT_CycleCount:UpdateCycleCountForDC:: Exiting");

            }
        }


        public bool UpdateDeCommissionedItems(string RFIDs, int DeviceID, bool Isdamaged, out int Decommissioned, out int Rejected)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_UpdateDecommissionedItems]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            scmCmdToExecute.CommandTimeout = 20 * 60;
            _log.Debug("KT_CycleCount:UpdateDeCommissionedItems:: Entering");
            Decommissioned = 0; Rejected = 0;
            scmCmdToExecute.Connection = _trackerRetailConnection;
            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, StoreID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DeviceID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, DeviceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@IsDamaged", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, Isdamaged));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@RFIDs", SqlDbType.VarChar, Int32.MaxValue, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, RFIDs));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ERRORCODE", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Decommissioned", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@Rejected", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, 0));

                // Open connection.
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }

                // Execute query.

                scmCmdToExecute.ExecuteNonQuery();

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ERRORCODE"].Value;
                Decommissioned = Convert.ToInt32(scmCmdToExecute.Parameters["@Decommissioned"].Value);
                Rejected = Convert.ToInt32(scmCmdToExecute.Parameters["@Rejected"].Value);

                if (_errorCode == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("KT_CycleCount::UpdateDeCommissionedItems::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _trackerRetailConnection.Close();
                scmCmdToExecute.Dispose();

                _log.Debug("KT_CycleCount:UpdateDeCommissionedItems:: Exiting");

            }
        }


        #endregion Methods

        #region Public Properties

        public int StoreID { get { return _StoreID; } set { _StoreID = value; } }
        public int ZoneID { get { return _ZoneID; } set { _ZoneID = value; } }
        public int DeptID { get { return _DeptID; } set { _DeptID = value; } }
        public string DeptName { get { return _DeptName; } set { _DeptName = value; } }
        public string ZoneName { get { return _ZoneName; } set { _ZoneName = value; } }
        public DateTime CreatedDate { get { return _CreatedDate; } set { _CreatedDate = value; } }
        public DateTime UpdatedDate { get { return _UpdatedDate; } set { _UpdatedDate = value; } }
        public int UpdatedBy { get { return _UpdatedBy; } set { _UpdatedBy = value; } }
        public int CycleCountID { get { return _CyclecountID; } set { _CyclecountID = value; } }
        public int DeviceID { get { return _DeviceID; } set { _DeviceID = value; } }

        #endregion
    }
}
