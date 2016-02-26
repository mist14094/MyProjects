///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'NotifyCacheUpdate'
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace KTone.DAL.KTDBBaseLib
{
    /// <summary>
    /// Purpose: Data Access class for the table 'NotifyCacheUpdate'.
    /// </summary>
    public class NotifyCacheUpdate : DBInteractionBase
    {
        #region Class Member Declarations
        private SqlDateTime m_daTimeStamp;
        private SqlInt32 m_iID;
        SqlConnection _mainConnection = null;
        private NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();
        
        #endregion


        /// <summary>
        /// Purpose: Class constructor.
        /// </summary>
        public NotifyCacheUpdate(SqlConnection conn)
        {
            _mainConnection = conn;
        }

        /// <summary>
        /// Purpose: Update method. This method will Update one existing row in the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///		 <LI>daTimeStamp</LI>
        ///		 <LI>iID</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>iErrorCode</LI>
        /// </UL>
        /// </remarks>
        internal bool Update()
        {
            _log.Trace("Entering Update - Table:NotifyCacheUpdate ; daTimeStamp:{0}," +
             "iID :{1}", m_daTimeStamp, m_iID);
            SqlInt32 _errorCode = 0;
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_NotifyCacheUpdate_Update]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }
                else
                {
                    if (_mainConnectionProvider.IsTransactionPending)
                    {
                        scmCmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                scmCmdToExecute.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Update:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("NotifyCacheUpdate::Update::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                scmCmdToExecute.Dispose();
                _log.Trace("Exiting Update.");
            }
        }


        #region Class Property Declarations
        public SqlDateTime daTimeStamp
        {
            get
            {
                return m_daTimeStamp;
            }
            set
            {
                SqlDateTime daTimeStampTmp = (SqlDateTime)value;
                if (daTimeStampTmp.IsNull)
                {
                    throw new ArgumentOutOfRangeException("daTimeStamp", "daTimeStamp can't be NULL");
                }
                m_daTimeStamp = value;
            }
        }


        public SqlInt32 iID
        {
            get
            {
                return m_iID;
            }
            set
            {
                SqlInt32 iIDTmp = (SqlInt32)value;
                if (iIDTmp.IsNull)
                {
                    throw new ArgumentOutOfRangeException("iID", "iID can't be NULL");
                }
                m_iID = value;
            }
        }
        #endregion
    }
}
