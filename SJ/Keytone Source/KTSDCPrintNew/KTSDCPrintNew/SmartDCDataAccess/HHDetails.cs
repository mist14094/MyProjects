using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;

namespace KTone.DAL.SmartDCDataAccess
{
    public class HHDetails : DBInteractionBase
    {
        #region Class Member Declaration

        private SqlInt32  m_iHHID, m_iGTINsUploaded,
            m_iGTINFound, m_iGTINMissing, m_iGTINNew;
        private SqlInt64 m_iHHCCAssoID, m_iCCInstanceID;
        private SqlDateTime m_dLastUploadToHH,m_dLastUploadToServer,m_dCreateTime,m_dLastUpdationTime;
        private SqlBoolean m_bIsBusy;                   
        
        #endregion Class Member Declaration

        #region Constructor
        public HHDetails()
        {

        }
        #endregion Constructor

        #region DBInteractionBase methods

        public override bool Insert()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_HHDetails_Insert]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCInstanceID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_iCCInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_iHHID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@LastUploadToHH", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_dLastUploadToHH));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@LastUploadToServer", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_dLastUploadToServer));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_dCreateTime));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@LastUpdationTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_dLastUpdationTime));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@IsBusy", SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_bIsBusy));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@GTINsUploaded", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iGTINsUploaded));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@GTINFound", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iGTINFound));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@GTINMissing", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iGTINMissing));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@GTINNew", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iGTINNew));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                
                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_HHDetails_Insert' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("HHDetails::Insert::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public override bool Update()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_HHDetails_Update]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCInstanceID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_iCCInstanceID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@HHID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_iHHID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@LastUploadToHH", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_dLastUploadToHH));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@LastUploadToServer", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_dLastUploadToServer));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_dCreateTime));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@LastUpdationTime", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_dLastUpdationTime));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@IsBusy", SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_bIsBusy));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@GTINsUploaded", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iGTINsUploaded));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@GTINFound", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iGTINFound));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@GTINMissing", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iGTINMissing));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@GTINNew", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iGTINNew));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@HHCCAssoID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_iHHCCAssoID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_HHDetails_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("HHDetails::Update::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        public override bool Delete()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_HHDetails_Delete]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@HHCCAssoID", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iHHCCAssoID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode == -123)
                {
                    // Throw error.
                    throw new Exception("Can not delete HHDetails. It is in use.");
                }

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_HHDetails_Delete' reported the ErrorCode: " + _errorCode);
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

        public override DataTable SelectAll()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_HHDetails_SelectAll]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("HHDetails");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_HHDetails_SelectAll' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("HHDetails::SelectAll::Error occured.", ex);
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
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_HHDetails_SelectOne]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("HHDetails");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@HHCCAssoID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iHHCCAssoID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_HHDetails_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("HHDetails::SelectOne::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }

        public DataTable GetAllBusyHandhelds()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_HHDetails_GetBusyHandhelds]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable("HHDetails");
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_HHDetails_GetBusyHandhelds' reported the ErrorCode: " + _errorCode);
                }

            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("HHDetails::GetAllBusyHandhelds::Error occured.", ex);
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

        public DataTable SelectAllBusyCCInstancesWHHId(int hhId)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_HHDetails_SelectAllBusyCCInstancesWHHId]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable();
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@HHId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, hhId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_HHDetails_SelectAllBusyCCInstancesWHHId' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("HHDetails::SelectAllBusyCCInstancesWHHId::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }

        public  bool UpdateBusyStatus(long ccInstanceId,int handheldId)
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_HHDetails_UpdateBusyStatus]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@CCInstanceId", SqlDbType.BigInt, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed,ccInstanceId));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@HHId", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed,handheldId));
                

                // Open connection.
                _mainConnection.Open();

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();                

                return true;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("HHDetails::UpdateBusyStatus::Error occured.", ex);
            }
            finally
            {
                // Close connection.
                _mainConnection.Close();
                scmCmdToExecute.Dispose();
            }
        }

        #endregion DBInteractionBase methods

        #region Class Properties

        public SqlInt64 iHHCCAssoID
        {
            get
            {
                return m_iHHCCAssoID;
            }
            set
            {
                m_iHHCCAssoID = value;
            }
        }

        public SqlInt64 iCCInstanceID
        {
            get
            {
                return m_iCCInstanceID;
            }
            set
            {
                m_iCCInstanceID = value;
            }
        }

        public SqlInt32 iHHID
        {
            get
            {
                return m_iHHID;
            }
            set
            {
                m_iHHID = value;
            }
        }

        public SqlInt32 iGTINsUploaded
        {
            get
            {
                return m_iGTINsUploaded;
            }
            set
            {
                m_iGTINsUploaded = value;
            }
        }

        public SqlInt32 iGTINFound
        {
            get
            {
                return m_iGTINFound;
            }
            set
            {
                m_iGTINFound = value;
            }
        }

        public SqlInt32 iGTINMissing
        {
            get
            {
                return m_iGTINMissing;
            }
            set
            {
                m_iGTINMissing = value;
            }
        }

        public SqlInt32 iGTINNew
        {
            get
            {
                return m_iGTINNew;
            }
            set
            {
                m_iGTINNew = value;
            }
        }

        public SqlDateTime dLastUploadToHH
        {
            get
            {
                return m_dLastUploadToHH;
            }
            set
            {
                m_dLastUploadToHH = value;
            }
        }

        public SqlDateTime dLastUploadToServer
        {
            get
            {
                return m_dLastUploadToServer;
            }
            set
            {
                m_dLastUploadToServer = value;
            }
        }

        public SqlDateTime dCreateTime
        {
            get
            {
                return m_dCreateTime;
            }
            set
            {
                m_dCreateTime = value;
            }
        }

        public SqlDateTime dLastUpdationTime
        {
            get
            {
                return m_dLastUpdationTime;
            }
            set
            {
                m_dLastUpdationTime = value;
            }
        }

        public SqlBoolean bIsBusy
        {
            get
            {
                return m_bIsBusy;
            }
            set
            {
                m_bIsBusy = value;
            }
        }

    #endregion Class Properties

    }
}
