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
   public class UserFielDGroupMap: DBInteractionBase
    { 
       #region Class Member Declarations
       private Int64 _userDefinedGroupID;
       private Int16 _assetCustomColID;
        private String _customColName, _UserDefinedFieldGroupMapXML;
        private Boolean _isDefault, _isMandatory, _webVisibility, _appVisibility;
        private Int32 _customFieldGroupID;
        #endregion

        public UserFielDGroupMap()
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
        /// </UL>
        /// </remarks>
        //public override DataTable SelectOne()
        //{
        //    SqlCommand cmdToExecute = new SqlCommand();
        //    cmdToExecute.CommandText = "[dbo].[pr_AssetCustomFieldGroupMap_SelectOne]";
        //    cmdToExecute.CommandType = CommandType.StoredProcedure;
        //    DataTable toReturn = new DataTable("AssetCustomFieldGroupMap");
        //    SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
        //    // Use base class' connection object
        //    cmdToExecute.Connection = _mainConnection;
        //    try
        //    {
        //        cmdToExecute.Parameters.Add(new SqlParameter("@assetTypeMasterID", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _assetTypeMasterID));
        //        cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
        //        if (_mainConnectionIsCreatedLocal)
        //        {
        //            // Open connection.
        //            _mainConnection.Open();
        //        }
        //        else
        //        {
        //            if (_mainConnectionProvider.IsTransactionPending)
        //            {
        //                cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
        //            }
        //        }
        //        // Execute query.
        //        adapter.Fill(toReturn);
        //        _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
        //        if (_errorCode != 0)
        //        {
        //            // Throw error.
        //            throw new Exception("Stored Procedure '[dbo].[pr_AssetCustomFieldGroupMap_SelectOne]' reported the ErrorCode: " + _errorCode);
        //        }
        //        return toReturn;
        //    }
        //    catch (Exception ex)
        //    {
        //        // some error occured. Bubble it to caller and encapsulate Exception object
        //        throw new Exception("AssetCustomFieldGroupMap::SelectOne::Error occured.", ex);
        //    }
        //    finally
        //    {
        //        if (_mainConnectionIsCreatedLocal)
        //        {
        //            // Close connection.
        //            _mainConnection.Close();
        //        }
        //        cmdToExecute.Dispose();
        //        adapter.Dispose();
        //    }
        //}


        public DataTable SelectAllCustomFieldForGroup()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_GetUserDefinedFieldForGroup]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("UserFielDGroupMap");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CustomFieldGroupID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _customFieldGroupID));
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
                    throw new Exception("Stored Procedure '[dbo].[pr_GetUserDefinedFieldForGroupc]' reported the ErrorCode: " + _errorCode);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("UserFielDGroupMap::SelectAllCustomFieldForGroup::Error occured.", ex);
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
            _log.Trace("Entering Insert - Table:UserDefinedFieldGroupMap ; UserDefinedGroupID:{0}," +
            "UserDefinedFieldGroupMapXML :{1}", _userDefinedGroupID, _UserDefinedFieldGroupMapXML);
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_UserDefinedFieldGroupMap_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Connection = _mainConnection;
            string errorMsg = string.Empty;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _customFieldGroupID));
                if (_UserDefinedFieldGroupMapXML != string.Empty)
                {
                    cmdToExecute.Parameters.Add(new SqlParameter("@UserDefinedFieldGroupMapXML", SqlDbType.Xml));
                    cmdToExecute.Parameters["@UserDefinedFieldGroupMapXML"].Value = _UserDefinedFieldGroupMapXML;
                }
                else
                    throw new ApplicationException("Supply proper data.");
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorMsg", SqlDbType.VarChar, 1000, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, errorMsg));

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
                    throw new Exception("Error Occured: " + cmdToExecute.Parameters["@ErrorMsg"].Value.ToString() + ": " +  _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("UserDefinedFieldGroupMap::Insert::Error occured.", ex);
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

        public Int16 AssetCustomColID
        {
            get
            {
                return _assetCustomColID;
            }
            set
            {
                _assetCustomColID = value;
            }
        }

        public Int64 UserDefinedGroupID
        {
            get
            {
                return _userDefinedGroupID;
            }
            set
            {
                _userDefinedGroupID = value;
            }
        }

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

        public Boolean IsDefault
        {
            get
            {
                return _isDefault;
            }
            set
            {
                _isDefault = value;
            }
        }

        public Boolean IsMandatory
        {
            get
            {
                return _isMandatory;
            }
            set
            {
                _isMandatory = value;
            }
        }

        public String UserDefinedFieldGroupMapXML
        {
            get
            {
                return _UserDefinedFieldGroupMapXML;
            }
            set
            {
                _UserDefinedFieldGroupMapXML = value;
            }
        }


        public Boolean WebVisibility
        {
            get
            {
                return _webVisibility;
            }
            set
            {
                _webVisibility = value;
            }
        }

        public Boolean AppVisibility
        {
            get
            {
                return _appVisibility;
            }
            set
            {
                _appVisibility = value;
            }
        }

        public Int32 CustomFieldGroupID
        {
            get
            {
                return _customFieldGroupID;
            }
            set
            {
                _customFieldGroupID = value;
            }
        }

        #endregion
    }
}
