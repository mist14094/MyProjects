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
    public class UserDefinedField : DBInteractionBase
    {
        #region Class Member Declarations
        private String _UserdefinedFieldXML;
        private Int32 _dataOwnerID, _categoryID;
        #endregion

        public bool Insert(out Int32 _categoryID)
        {
            _log.Trace("Entering Insert - Table:UserdefinedField UserdefinedFieldXML :{0}" + _UserdefinedFieldXML);
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_UserDefinedField_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Connection = _mainConnection;
            string errorMsg = string.Empty;
            _categoryID = 0;
            try
            {

                if (_UserdefinedFieldXML != string.Empty)
                {
                    cmdToExecute.Parameters.Add(new SqlParameter("@UserdefinedFieldXML", SqlDbType.Xml));
                    cmdToExecute.Parameters["@UserdefinedFieldXML"].Value = _UserdefinedFieldXML;
                }
                else
                    throw new ApplicationException("Supply proper data.");
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMsg", SqlDbType.VarChar, 1000, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, errorMsg));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int, 10, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _categoryID));

                if (_mainConnectionProvider.IsTransactionPending)
                {
                    cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                _categoryID = (Int32)cmdToExecute.Parameters["@CategoryID"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Error Occured: " + cmdToExecute.Parameters["@ErrorMsg"].Value.ToString() + ": " + _errorCode);
                }
     
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("UserdefinedField::Insert::Error occured.", ex);
            }
            finally
            {
                cmdToExecute.Dispose();
                _log.Trace("Exiting Insert");
            }
        }

        public DataTable GetCustomColumns()
        {
            _log.Trace("Entering GetCustomColumns - Table:UserDefinedField ");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_UserDefinedField_GetColumns]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("UserDefinedField");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int, 10, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _categoryID));
                // cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

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
                //_errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                //if (_errorCode != 0)
                //{
                //    // Throw error.
                //    throw new Exception("Stored Procedure 'pr_AssetMasterCustom_SelectAll' reported the ErrorCode: " + _errorCode);
                //}

                return toReturn;
            }
            catch (Exception ex)
            {
                _log.Error("GetCustomColumns:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("UserDefinedField::GetCustomColumns::Error occured.", ex);
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
                _log.Trace("Exiting GetCustomColumns");
            }
        }


        public String UserdefinedFieldXML
        {
            get
            {
                return _UserdefinedFieldXML;
            }
            set
            {
                _UserdefinedFieldXML = value;
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
    }
}
