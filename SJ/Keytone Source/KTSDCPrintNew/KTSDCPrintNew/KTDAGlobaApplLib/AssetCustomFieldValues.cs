using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;

namespace KTone.DAL.KTDAGlobaApplLib
{
    public class AssetCustomFieldValues : DBInteractionBase
    {
        #region Class Member Declarations
        private Int32 _customFieldID;
        private String _customColName, _custColValues, _module = "ASSET" ;
        #endregion

        public AssetCustomFieldValues()
		{
			// Nothing for now.
        }

        #region [Class Methods Declarations]

        /// <summary>
        /// Purpose: Select method. This method will Select one existing row from the database.
        /// </summary>
        /// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///		 <LI>AssetTypeMasterID</LI>
        ///      <LI>CustColName</LI>
        /// </UL>
        /// </remarks>
        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_GetAssetCustomFieldValues]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("AssetCustomFieldValues");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CustomFieldName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _customColName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Module", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _module));
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
                    throw new Exception("Stored Procedure '[dbo].[pr_GetAssetCustomFieldValues]' reported the ErrorCode: " + _errorCode);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("AssetCustomFieldValues::SelectOne::Error occured.", ex);
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
            _log.Trace("Entering Insert - Table:AssetCustomFieldValues ; CustomColName :{0},CustomColValues :{1}", _customColName,_custColValues);
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_AssetCustomFieldValues_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Connection = _mainConnection;
            string errorMsg = string.Empty;
            try
            {
              
                cmdToExecute.Parameters.Add(new SqlParameter("@CustColName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _customColName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Module", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _module));
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
        public String Module
        {
            get
            {
                return _module;
            }
            set
            {
                _module = value;
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
