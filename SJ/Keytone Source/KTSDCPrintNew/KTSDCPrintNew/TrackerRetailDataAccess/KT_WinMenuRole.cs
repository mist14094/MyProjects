using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;

namespace TrackerRetailDataAccess
{
    /// <summary>
    /// Windows application accessibility
    /// </summary>
    public class WinMenuRole : DBInteractionBase
    {
        #region Class Member Declarations
        private String _appName, _appExeName, _appMenuIds, _formKey, _formName, _formCaption, _menuName, _menuDesc;
        private int _appId, _roleId, _parentAppMenuID, _dataOwnerID;
        #endregion

        /// <summary>
        /// Purpose: Class constructor.
        /// </summary>
        public WinMenuRole()
        {

        }

        #region Class Methods Declarations

        public DataTable SelectAllWinAppMenuSetting()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_WinAppMenuSetting_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("WinAppMenuSetting");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));

                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }
                // Execute query.
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                if (_errorCode > 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure '[dbo].[pr_WinAppMenuSetting_SelectAll]' reported the ErrorCode: " + _errorCode);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("WinMenuRole::SelectAllWinAppMenuSetting::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }


        /// <summary>
        /// Purpose: Insert method. This method will insert one new row into the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///		 <LI>AppMenuIds</LI>
        ///		 <LI>RoleId</LI>        
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>        
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:WinMenuRole ; AppMenuID:{0},RoleID :{1},AppID:{2}", _appMenuIds, _roleId, _appId);
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_WinMenuRole_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@appMenuIDs", SqlDbType.VarChar, 2000, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _appMenuIds));
                cmdToExecute.Parameters.Add(new SqlParameter("@roleID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _roleId));
                cmdToExecute.Parameters.Add(new SqlParameter("@appID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _appId));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }
                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_WinMenuRole_Insert' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("WinMenuRole::Insert::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Insert");
            }
        }

        public bool InsertWinApplication()
        {
            _log.Trace("Entering InsertWinApplication");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_WinApplication_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@AppName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _appName));
                cmdToExecute.Parameters.Add(new SqlParameter("@ExeName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _appExeName));
                cmdToExecute.Parameters.Add(new SqlParameter("@AppID", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _appId));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }
                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                //if (_errorCode == -999)
                //{
                //    throw new Exception("Application Name already exists");
                //}
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_WinApplication_Insert' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("InsertWinApplication:{0}", ex.Message);
                if (_errorCode == -999)
                {
                    throw new Exception("Application Name already exists");
                }
                else
                {
                    // some error occured. Bubble it to caller and encapsulate Exception object
                    throw new Exception("WinMenuRole::InsertWinApplication::Error occured.", ex);
                }
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting InsertWinApplication");
            }
        }

        public bool InsertWinAppMenu()
        {
            _log.Trace("Entering InsertWinAppMenu");
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_WinAppMenu_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@AppId", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _appId));
                cmdToExecute.Parameters.Add(new SqlParameter("@ParentAppMenuID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _parentAppMenuID));
                cmdToExecute.Parameters.Add(new SqlParameter("@MenuName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _menuName));
                cmdToExecute.Parameters.Add(new SqlParameter("@MenuDesc", SqlDbType.VarChar, 200, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _menuDesc));
                cmdToExecute.Parameters.Add(new SqlParameter("@FormCaption", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _formCaption));
                cmdToExecute.Parameters.Add(new SqlParameter("@FormName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _formName));
                cmdToExecute.Parameters.Add(new SqlParameter("@FormKey", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _formKey));
                cmdToExecute.Parameters.Add(new SqlParameter("@AppMenuID", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _appMenuIds));

                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }
                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_WinApplication_Insert' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("InsertWinAppMenu:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                if (_errorCode == -888)
                {
                    throw new Exception("Menu already exists");
                }
                else
                {
                    throw new Exception("WinMenuRole::InsertWinAppMenu::Error occured.", ex);
                }
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting InsertWinAppMenu");
            }
        }

        public override bool Delete()
        {
            _log.Trace("Entering Delete - Table:WinAppMenu ; AppMenuID:{0}", _appMenuIds);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_WinAppMenu_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@AppMenuId", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _appMenuIds));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }

                // Execute query.
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_WinAppMenu_Delete' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Delete:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                //throw new Exception("Zone::Delete::Error occured.", ex);
                throw ex;
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Delete.");
            }
        }

        /// <summary>
        /// Purpose: Select All Win Application method. This method will Select all rows from the table.
        /// </summary>
        /// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public DataTable SelectAllWinApplication()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_WinApplication_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("WinApplication");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }
                // Execute query.
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure '[dbo].[pr_WinApplication_SelectAll]' reported the ErrorCode: " + _errorCode);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("WinMenuRole::SelectAllWinApplication::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        /// <summary>
        /// Purpose: Select All Application Menu with Application ID method. This method will Select all rows from the table.
        /// </summary>
        /// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public DataTable SelectAllAppMenuWAppID()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_WinMenuRole_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("WinAppMenu");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@roleID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _roleId));
                cmdToExecute.Parameters.Add(new SqlParameter("@appID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _appId));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }
                // Execute query.
                adapter.Fill(toReturn);
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure '[dbo].[pr_WinMenuRole_SelectAll]' reported the ErrorCode: " + _errorCode);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("WinMenuRole::SelectAllAppMenuWAppID::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                adapter.Dispose();
            }
        }

        #endregion

        #region [Class Property Declarations]
        public String AppName
        {
            get
            {
                return _appName;
            }
            set
            {
                _appName = value;
            }
        }
        public String AppExeName
        {
            get
            {
                return _appExeName;
            }
            set
            {
                _appExeName = value;
            }
        }
        public Int32 AppId
        {
            get
            {
                return _appId;
            }
            set
            {
                _appId = value;
            }
        }
        public String AppMenuIds
        {
            get
            {
                return _appMenuIds;
            }
            set
            {
                _appMenuIds = value;
            }
        }

        public Int32 ParentAppMenuID
        {
            get
            {
                return _parentAppMenuID;
            }
            set
            {
                _parentAppMenuID = value;
            }
        }
        public Int32 RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                _roleId = value;
            }
        }
        public String MenuName
        {
            get
            {
                return _menuName;
            }
            set
            {
                _menuName = value;
            }
        }
        public String MenuDesc
        {
            get
            {
                return _menuDesc;
            }
            set
            {
                _menuDesc = value;
            }
        }

        public String FormCaption
        {
            get
            {
                return _formCaption;
            }
            set
            {
                _formCaption = value;
            }
        }
        public String FormKey
        {
            get
            {
                return _formKey;
            }
            set
            {
                _formKey = value;
            }
        }
        public String FormName
        {
            get
            {
                return _formName;
            }
            set
            {
                _formName = value;
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
        #endregion
    }
}
