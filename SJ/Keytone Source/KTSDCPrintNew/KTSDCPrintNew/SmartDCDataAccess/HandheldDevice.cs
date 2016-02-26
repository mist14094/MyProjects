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
    public class HandheldDevice : DBInteractionBase
    {
        private Int32 _HHID, _DataOwnerID, _CreatedBy, _UpdatedBy, _dataOwnerId;
        private String _HHName,_HHDescription,_Model,_Make,_MacAddress,_DNSName,_PortNo,_errMsg="";
        private Boolean _isActive,_isRemoved;
        private DateTime _CreatedDate, _UpdatedDate;

        public HandheldDevice()
        {
        }

        #region select 

        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_HHMaster_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _DataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _HHID));
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
                    throw new Exception("Stored Procedure 'pr_HHMaster_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {

                    _HHName = toReturn.Rows[0]["HHName"].ToString();
                    _HHDescription = toReturn.Rows[0]["HHDescription"].ToString();
                    _Model = toReturn.Rows[0]["Model"].ToString();
                    _Make = toReturn.Rows[0]["Make"].ToString();
                    _MacAddress = toReturn.Rows[0]["MacAddress"].ToString();
                    _DNSName = toReturn.Rows[0]["DNSName"].ToString();
                    _PortNo = toReturn.Rows[0]["PortNo"].ToString();
                   // _SWVersion = toReturn.Rows[0]["Model"].ToString();

                    _isActive = Convert.ToBoolean(toReturn.Rows[0]["IsActive"]);
                    _isRemoved = Convert.ToBoolean(toReturn.Rows[0]["IsRemoved"]);

                    _DataOwnerID = Convert.ToInt32(toReturn.Rows[0]["DataOwnerID"]);
                    _CreatedBy = Convert.ToInt32(toReturn.Rows[0]["CreatedBy"]);
                    _UpdatedBy = Convert.ToInt32(toReturn.Rows[0]["UpdatedBy"]);

                    _CreatedDate = Convert.ToDateTime(toReturn.Rows[0]["CreatedDate"]);
                    _UpdatedDate = Convert.ToDateTime(toReturn.Rows[0]["UpdatedDate"]);
               
                  
                    
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("HandheldCategory::SelectOne::Error occured.", ex);
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

        public new DataSet ValidateHH()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_HandHeld_Validate]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataSet toReturn = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _DataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@MacAddr", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 19, 0, "", DataRowVersion.Proposed, _MacAddress));
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
                    throw new Exception("Stored Procedure 'pr_HandHeld_Validate' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("HandheldDevice::ValidateHH::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[pr_HHMaster_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("HHMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _DataOwnerID));
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
                    throw new Exception("Stored Procedure 'pr_HHMaster_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Handheld::SelectAll::Error occured.", ex);
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

        public DataTable SelectAllLocationIdLogic()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_HHMaster_SelectAllHandheldIdLogic]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("HHMaster");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;

            try
            {

                cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _HHID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _DataOwnerID));

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
                    throw new Exception("Stored Procedure 'pr_HHMaster_SelectAllHHIdLogic' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("::SelectAllHandheldIdLogic::Error occured.", ex);
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

        #region insert

        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:HHMaster ; " +
            " HHName:{0},Description:{1}", _HHName, _HHDescription);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_HHMaster_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
             
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@HHName",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"",DataRowVersion.Proposed,_HHName));
                cmdToExecute.Parameters.Add(new SqlParameter("@HHDescription", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _HHDescription));
                cmdToExecute.Parameters.Add(new SqlParameter("@Model", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Model));
                cmdToExecute.Parameters.Add(new SqlParameter("@Make", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Make));
                cmdToExecute.Parameters.Add(new SqlParameter("@MacAddress", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _MacAddress));
                cmdToExecute.Parameters.Add(new SqlParameter("@DNSName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _DNSName));
                cmdToExecute.Parameters.Add(new SqlParameter("@PortNo", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _PortNo));

                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _DataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _CreatedBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _UpdatedBy));

                cmdToExecute.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isActive));
                cmdToExecute.Parameters.Add(new SqlParameter("@IsRemoved", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isRemoved));

                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _CreatedDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _UpdatedDate));

               
                cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _HHID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMessage", SqlDbType.VarChar, 200, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, _errMsg));

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

                if (!string.IsNullOrEmpty(cmdToExecute.Parameters["@ErrorMessage"].Value.ToString()))
                {
                    _errMsg = Convert.ToString(cmdToExecute.Parameters["@ErrorMessage"].Value.ToString());
                    throw new Exception(_errMsg);
                }

                if (cmdToExecute.Parameters["@HHID"].Value.ToString() != "")
                {
                    _HHID = (Int32)cmdToExecute.Parameters["@HhID"].Value;
                }
                else
                {
                    _HHID = 0;
                }
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_HHMaster_Insert' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception(ex.Message);
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

        #region Update

        public override bool Update()
        {
        
            _log.Trace("Entering Insert - Table:HHMaster ; " + " HHName:{0},Description:{1}", _HHName, _HHDescription);


            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_HHMaster_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@HHName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _HHName));
                cmdToExecute.Parameters.Add(new SqlParameter("@HHDescription", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _HHDescription));
                cmdToExecute.Parameters.Add(new SqlParameter("@Model", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Model));
                cmdToExecute.Parameters.Add(new SqlParameter("@Make", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _Make));
                cmdToExecute.Parameters.Add(new SqlParameter("@MacAddress", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _MacAddress));
                cmdToExecute.Parameters.Add(new SqlParameter("@DNSName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _DNSName));
                cmdToExecute.Parameters.Add(new SqlParameter("@PortNo", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _PortNo));

                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _DataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _CreatedBy));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedBy", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _UpdatedBy));

                cmdToExecute.Parameters.Add(new SqlParameter("@isActive", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isActive));
                cmdToExecute.Parameters.Add(new SqlParameter("@IsRemoved", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isRemoved));

                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _CreatedDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@UpdatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _UpdatedDate));


                cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _HHID));
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
                    throw new Exception("Stored Procedure 'pr_HHMaster_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("HHMaster::Update::Error occured.", ex);
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

        #region Delete Handheld

        public override bool Delete()
        {
            _log.Trace("Entering Delete - Table:HHMaster ; HHID:{0}", _HHID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_HHMaster_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _HHID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _DataOwnerID));

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
                    throw new Exception("Stored Procedure 'pr_HHMaster_Delete' reported the ErrorCode: " + _errorCode);
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

        #endregion

        #region Class Property Declarations
        public Int32 HHID
        {
            get
            {
                return _HHID;

            }
            set
            {
                _HHID= value;
            }
        }

        public String HHName
        {
            get
            {
                return _HHName;
            }
            set
            {
                _HHName = value;
            }
        }
        public String HHDescription
        {
            get
            {
                return _HHDescription;
            }
            set
            {
                _HHDescription = value;
            }
        }
        public String Model
        {
            get
            {
                return _Model;
            }
            set
            {
                _Model = value;
            }
        }
        public String Make
        {
            get
            {
                return _Make;
            }
            set
            {
                _Make = value;
            }
        }
        public String MacAddress
        {
            get
            {
                return _MacAddress;
            }
            set
            {
                _MacAddress = value;
            }
        }
        public String DNSName
        {
            get
            {
                return _DNSName;
            }
            set
            {
                _DNSName = value;
            }
        }
        public String PortNo
        {
            get
            {
                return _PortNo;
            }
            set
            {
                _PortNo = value;
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
        public Boolean IsRemoved
        {
            get
            {
                return _isRemoved;
            }
            set
            {
                _isRemoved = value;
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
        #endregion

        #region [LicenceCheck]
        public void HandHeldSummary(out Int32 liveHHCount, out Int32 systemHHCount)
        {
            liveHHCount = 0;
            systemHHCount = 0;
            string strSQL = string.Empty;
            strSQL = "Select SUM(LiveHHCount) LiveHHCount, SUM(TotalHHCount) TotalHHCount " +
                      " FROM " +
                      " (Select Count(HHID) LiveHHCount, 0 TotalHHCount From HHMaster Where IsActive = 'True' AND ISRemoved = 'False' " +
                      " UNION " +
                      " Select 0, Count(HHID) TotalHHCount From HHMaster) VW";
            SqlDataAdapter da = new SqlDataAdapter(strSQL, _mainConnection);
            DataTable dtHHCount = new DataTable();
            da.Fill(dtHHCount);
            if (dtHHCount.Rows.Count > 0)
            {
                liveHHCount = Convert.ToInt32(dtHHCount.Rows[0]["LiveHHCount"].ToString());
                systemHHCount = Convert.ToInt32(dtHHCount.Rows[0]["TotalHHCount"].ToString());
            }
        }
        #endregion [LicenceCheck]
    }
}
