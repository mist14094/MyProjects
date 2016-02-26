using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib ;


namespace KTone.DAL.SmartDCDataAccess
{
   public class UserdefiendValues:DBInteractionBase
    {
       #region Class Member Declarations
       private Int32 _customFieldID, _categoryID,_dataownerID;
        private String _customColName, _custColValues ;
        #endregion

        public UserdefiendValues()
		{
			// Nothing for now.
        }

        #region [Class Methods Declarations]

      
        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_GetUserdefiendValues]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("UserdefiendValues");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CustomFieldName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _customColName));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _categoryID));
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
                    throw new Exception("Stored Procedure '[dbo].[pr_GetUserdefiendValues]' reported the ErrorCode: " + _errorCode);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("pr_GetUserdefiendValues::SelectOne::Error occured.", ex);
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
            cmdToExecute.CommandText = "[dbo].[pr_GetAllUserdefinedVlaues]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("UserdefiendValues");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _categoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataownerID));
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
                    throw new Exception("Stored Procedure '[dbo].[pr_GetAllUserdefinedVlaues]' reported the ErrorCode: " + _errorCode);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("pr_GetAllUserdefinedVlaues::SelectAll::Error occured.", ex);
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:UserdefiendValues ; CustomColName :{0},CustomColValues :{1}", _customColName, _custColValues);
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_UserdefiendValues_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Connection = _mainConnection;
            string errorMsg = string.Empty;
            try
            {
              
                cmdToExecute.Parameters.Add(new SqlParameter("@CustColName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _customColName));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _categoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CustColValues", SqlDbType.Xml));
                cmdToExecute.Parameters["@CustColValues"].Value = _custColValues;
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMsg", SqlDbType.VarChar, 1000, ParameterDirection.Output, true, 0, 0, "", DataRowVersion.Proposed, errorMsg));

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
                    throw new Exception(cmdToExecute.Parameters["@ErrorMsg"].Value.ToString() +  ": " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Error occured: " + ex.Message , ex);
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

        #region [Class Property Declarations]

      

        public String CustomColName
        {
            get
            {
                return _customColName;
            }
            set
            {
                _customColName = value;
            }
        }

        public Int32 CustomFieldID
        {
            get
            {
                return _customFieldID;
            }
            set
            {
                _customFieldID = value;
            }
        }
        
        /// <summary>
        /// Refers to table for whihc you want to add values right now only ASSET is supported
        /// </summary>
        public Int32 CategoryID
        {
            get
            {
                return _categoryID;
            }
            set
            {
                _categoryID = value;
            }
        }
        public Int32 DataOwnerID
        {
            get
            {
                return _dataownerID ;
            }
            set
            {
                _dataownerID = value;
            }
        }

        public String CustColValues
        {
            get
            {
                return _custColValues;
            }
            set
            {
                _custColValues = value;
            }
        }

        #endregion

    }
}
