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
    /// Purpose: Data Access class for the table 'User'.
    /// </summary>
    public class User : DBInteractionBase
    {
        #region Class Member Declarations
        private Boolean _active,_default;
        private DateTime _modifiedDate, _createdDate;
        private Int32 _roleID, _roleIDOld, _userID, _dataOwnerID,_adUsersUpdated,_adUsersAdded ,_baseLocation;
        private String _name, _userName, _password, _emailID, _dataOwnerName, _productName, _roleName;
        private bool _useLocalCredential, _useADCredential;
        private string _adPath, _adPathDataOwner,_usersNotUpdated;
        #endregion

        #region [Class Methods Declarations]

        /// <summary>
        /// Purpose: Class constructor.
        /// </summary>
        public User()
        {
            // Nothing for now.
        }

        /// <summary>
        /// Purpose: Insert method. This method will insert one new row into the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///      <LI>Name. May be SqlString.Null</LI>
        ///		 <LI>EmailID. May be SqlString.Null</LI>
        ///		 <LI>UserName</LI>
        ///		 <LI>Password</LI>
        ///		 <LI>RoleID</LI>
        ///		 <LI>Active. May be SqlBoolean.Null</LI>
        ///		 <LI>ModifiedDate. May be SqlDateTime.Null</LI>
        ///		 <LI>CreatedDate. May be SqlDateTime.Null</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>UserID</LI>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:User ; Name:{0}," +
            "UserName :{1}, Password:{2},RoleID:{3},Active:{4}," +
            "ModifiedDate:{5},CreatedDate:{6},EmailID:{7}", _name, _userName, _password, _roleID, _active, _modifiedDate, _createdDate, _emailID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_User_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _name));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _password));
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _roleID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _active));
                cmdToExecute.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _modifiedDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _createdDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@Default", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _default));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@UseLocalCredential", SqlDbType.Bit, 2, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _useLocalCredential));
                cmdToExecute.Parameters.Add(new SqlParameter("@UseADCredential", SqlDbType.Bit, 2, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _useADCredential));       
                if (_emailID != string.Empty)
                {
                    cmdToExecute.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _emailID));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                }

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
                if (cmdToExecute.Parameters["@UserID"].Value.ToString() != "")
                {
                    _userID = (Int32)cmdToExecute.Parameters["@UserID"].Value;
                }
                else
                {
                    _userID = 0;
                }
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_User_Insert' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::Insert::Error occured.", ex);
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

        /// <summary>
        /// Purpose: Update method. This method will Update one existing row in the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///		 <LI>UserID</LI>
        ///		 <LI>Name. May be SqlString.Null</LI>
        ///		  <LI>EmailID. May be SqlString.Null</LI>
        ///		 <LI>UserName</LI>
        ///		 <LI>Password</LI>
        ///		 <LI>RoleID</LI>
        ///		 <LI>Active. May be SqlBoolean.Null</LI>
        ///		 <LI>ModifiedDate. May be SqlDateTime.Null</LI>
        ///		 <LI>CreatedDate. May be SqlDateTime.Null</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public override bool Update()
        {
            _log.Trace("Entering Update - Table:User ; Name:{0}," +
           "UserName :{1}, Password:{2},RoleID:{3},Active:{4}," +
           "ModifiedDate:{5},CreatedDate:{6},UserID:{7},EmailID:{8}", _name, _userName, _password, _roleID, _active, _modifiedDate, _createdDate, _userID, _emailID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_User_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _name));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _password));
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _roleID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _active));
                cmdToExecute.Parameters.Add(new SqlParameter("@ModifiedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _modifiedDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@CreatedDate", SqlDbType.DateTime, 8, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _createdDate));
                cmdToExecute.Parameters.Add(new SqlParameter("@UseLocalCredential", SqlDbType.Bit, 2, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _useLocalCredential));
                cmdToExecute.Parameters.Add(new SqlParameter("@UseADCredential", SqlDbType.Bit, 2, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _useADCredential));       
                if (_emailID != string.Empty)
                {
                    cmdToExecute.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _emailID));
                }
                else
                {
                    cmdToExecute.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                }

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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_User_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Update:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::Update::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Update.");
            }
        }

        /// <summary>
        /// Method for Updating the users from Active Directory
        /// </summary>
        /// <param name="usersAsXML"></param>
        /// <returns></returns>
        public bool UpdateADUsers(string usersAsXML)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_ADUsers_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@UsersXML", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, usersAsXML));
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _roleID));
                cmdToExecute.Parameters.Add(new SqlParameter("@UsersAdded", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _adUsersAdded));
                cmdToExecute.Parameters.Add(new SqlParameter("@UsersUpdated", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _adUsersUpdated));
                cmdToExecute.Parameters.Add(new SqlParameter("@UsersNoChange", SqlDbType.VarChar, 8000, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, _usersNotUpdated));
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
                    throw new Exception("Stored Procedure 'pr_ADUsers_Update' reported the ErrorCode: " + _errorCode);
                }

                _adUsersAdded = (Int32)cmdToExecute.Parameters["@UsersAdded"].Value;
                _adUsersUpdated = (Int32)cmdToExecute.Parameters["@UsersUpdated"].Value;
                _usersNotUpdated = (string)cmdToExecute.Parameters["@UsersNoChange"].Value;
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("UpdateADUsers:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::UpdateADUsers::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting UpdateADUsers.");
            }           
        }         

        /// <summary>
        /// Purpose: Update method for updating one or more rows using the Foreign Key 'RoleID.
        /// This method will Update one or more existing rows in the database. It will reset the field 'RoleID' in
        /// all rows which have as value for this field the value as set in property 'RoleIDOld' to 
        /// the value as set in property 'RoleID'.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///		 <LI>RoleID</LI>
        ///		 <LI>RoleIDOld</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public bool UpdateAllWRoleIDLogic()
        {
            _log.Trace("Entering UpdateAllWRoleIDLogic - Table:User ; RoleID:{0}," +
           "RoleIDOld :{1}", _roleID, _roleIDOld);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_User_UpdateAllWRoleIDLogic]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _roleID));
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleIDOld", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _roleIDOld));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_User_UpdateAllWRoleIDLogic' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("UpdateAllWRoleIDLogic:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::UpdateAllWRoleIDLogic::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting UpdateAllWRoleIDLogic.");
            }
        }

        /// <summary>
        /// Purpose: Delete method. This method will Delete one existing row in the database, based on the Primary Key.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///		 <LI>UserID</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public override bool Delete()
        {
            _log.Trace("Entering Delete - Table:User ; UserID:{0}", _userID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_User_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_User_Delete' reported the ErrorCode: " + _errorCode);
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
        /// Purpose: Select method. This method will Select one existing row from the database, based on the Primary Key.
        /// </summary>
        /// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///		 <LI>UserID</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        ///		 <LI>UserID</LI>
        ///		 <LI>Name</LI>
        ///		  <LI>EmailID</LI>
        ///		 <LI>UserName</LI>
        ///		 <LI>Password</LI>
        ///		 <LI>RoleID</LI>
        ///		 <LI>Active</LI>
        ///		 <LI>ModifiedDate</LI>
        ///		 <LI>CreatedDate</LI>
        /// </UL>
        /// Will fill all properties corresponding with a field in the table with the value of the row selected.
        /// </remarks>
        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_User_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("User");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _userID));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_User_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {
                    _userID = (Int32)toReturn.Rows[0]["UserID"];
                    _name = toReturn.Rows[0]["Name"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["Name"];
                    _emailID = toReturn.Rows[0]["EmailID"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["EmailID"];
                    _userName = (string)toReturn.Rows[0]["UserName"];
                    _password = toReturn.Rows[0]["Password"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["Password"];
                    _roleID = (Int32)toReturn.Rows[0]["RoleID"];
                    _active = toReturn.Rows[0]["Active"] == System.DBNull.Value ? false : (bool)toReturn.Rows[0]["Active"];
                    _modifiedDate = toReturn.Rows[0]["ModifiedDate"] == System.DBNull.Value ? DateTime.MinValue : (DateTime)toReturn.Rows[0]["ModifiedDate"];
                    _createdDate = toReturn.Rows[0]["CreatedDate"] == System.DBNull.Value ? DateTime.MinValue : (DateTime)toReturn.Rows[0]["CreatedDate"];
                    _useLocalCredential = toReturn.Rows[0]["UseLocalCredential"] == System.DBNull.Value ? false : (bool)toReturn.Rows[0]["UseLocalCredential"];
                    _useADCredential = toReturn.Rows[0]["UseADCredential"] == System.DBNull.Value ? false : (bool)toReturn.Rows[0]["UseADCredential"];
                   // _adPath = toReturn.Rows[0]["ADPath"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["ADPath"];
                    _adPathDataOwner = toReturn.Rows[0]["DataOwnerADPath"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["DataOwnerADPath"];
                    _default = toReturn.Rows[0]["Default"] == System.DBNull.Value ? false : (bool)toReturn.Rows[0]["Default"];
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::SelectOne::Error occured.", ex);
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

        public DataTable SelectByUserName()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_User_SelectByUserName]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("User");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar ,50, ParameterDirection.Input, false,0, 0, "", DataRowVersion.Proposed, _userName));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.                   
                    throw new Exception("Stored Procedure 'pr_User_SelectByUserName' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {
                    _userID = (Int32)toReturn.Rows[0]["UserID"];
                    _name = toReturn.Rows[0]["Name"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["Name"];
                    _emailID = toReturn.Rows[0]["EmailID"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["EmailID"];
                    _userName = (string)toReturn.Rows[0]["UserName"];
                    _password = toReturn.Rows[0]["Password"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["Password"];
                    _roleID = (Int32)toReturn.Rows[0]["RoleID"];
                    _active = toReturn.Rows[0]["Active"] == System.DBNull.Value ? false : (bool)toReturn.Rows[0]["Active"];
                    _modifiedDate = toReturn.Rows[0]["ModifiedDate"] == System.DBNull.Value ? DateTime.MinValue : (DateTime)toReturn.Rows[0]["ModifiedDate"];
                    _createdDate = toReturn.Rows[0]["CreatedDate"] == System.DBNull.Value ? DateTime.MinValue : (DateTime)toReturn.Rows[0]["CreatedDate"];
                    _useLocalCredential = toReturn.Rows[0]["UseLocalCredential"] == System.DBNull.Value ? false : (bool)toReturn.Rows[0]["UseLocalCredential"];
                    _useADCredential = toReturn.Rows[0]["UseADCredential"] == System.DBNull.Value ? false : (bool)toReturn.Rows[0]["UseADCredential"];
                    _adPath = toReturn.Rows[0]["DataOwnerADPath"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["DataOwnerADPath"];
                    _adPathDataOwner = toReturn.Rows[0]["DataOwnerADPath"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["DataOwnerADPath"];
                    _dataOwnerName = toReturn.Rows[0]["DataOwnerName"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["DataOwnerName"];
                    _productName = toReturn.Rows[0]["ProductName"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["ProductName"];
                    _dataOwnerID = (Int32)toReturn.Rows[0]["DataOwnerID"];
                    //_adPath = toReturn.Rows[0]["ADPath"]
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::SelectByUserName::Error occured.", ex);
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
        /// Purpose: SelectAll method. This method will Select all rows from the table.
        /// </summary>
        /// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public override DataTable SelectAll()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_User_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("User");
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_User_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::SelectAll::Error occured.", ex);
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

        public DataTable SelectAllBywner()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_User_SelectAllByOwner]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("User");
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_User_SelectAllByOwner' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::SelectAllByOwner::Error occured.", ex);
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
        /// Purpose: Select method for a foreign key. This method will Select one or more rows from the database, based on the Foreign Key 'RoleID'
        /// </summary>
        /// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///		 <LI>RoleID</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public DataTable SelectAllWRoleIDLogic()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_User_SelectAllWRoleIDLogic]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("User");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _roleID));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_User_SelectAllWRoleIDLogic' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::SelectAllWRoleIDLogic::Error occured.", ex);
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


        public bool Validate()
        {
            _log.Trace("Entering Validate - Table:User ;UserName:{0},Password:{1},UserID:{2}", _userName, _password, _userID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_User_Validate]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _password));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 100, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _name));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerName", SqlDbType.VarChar, 50, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _dataOwnerName));

                cmdToExecute.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar, 50, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _productName));
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _roleID));
                cmdToExecute.Parameters.Add(new SqlParameter("@StoreID", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _baseLocation));
                //cmdToExecute.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.VarChar, 100, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _roleName));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@UseLocalCredential", SqlDbType.Bit, 2, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, _useLocalCredential));
                cmdToExecute.Parameters.Add(new SqlParameter("@UseADCredential", SqlDbType.Bit, 2, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, _useADCredential)); 

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
                if (cmdToExecute.Parameters["@UserID"].Value != DBNull.Value)
                {
                    _userID = (Int32)cmdToExecute.Parameters["@UserID"].Value;
                }

                if (cmdToExecute.Parameters["@Name"].Value != DBNull.Value)
                {
                    _name = (String)cmdToExecute.Parameters["@Name"].Value;
                }

                if (cmdToExecute.Parameters["@DataOwnerID"].Value != DBNull.Value)
                {
                    _dataOwnerID = (Int32)cmdToExecute.Parameters["@DataOwnerID"].Value;
                }

                if (cmdToExecute.Parameters["@DataOwnerName"].Value != DBNull.Value)
                {
                    _dataOwnerName = (String)cmdToExecute.Parameters["@DataOwnerName"].Value;
                }

                if (cmdToExecute.Parameters["@ProductName"].Value != DBNull.Value)
                {
                    _productName = (String)cmdToExecute.Parameters["@ProductName"].Value;
                }

                //if (cmdToExecute.Parameters["@RoleName"].Value != DBNull.Value)
                //{
                //    _roleName = (String)cmdToExecute.Parameters["@RoleName"].Value;
                //}

                if (cmdToExecute.Parameters["@RoleID"].Value != DBNull.Value)
                {
                    _roleID = (Int32)cmdToExecute.Parameters["@RoleID"].Value;
                }
                if (cmdToExecute.Parameters["@StoreID"].Value != DBNull.Value)
                {
                    _baseLocation = cmdToExecute.Parameters["@StoreID"].Value == System.DBNull.Value ? 0 : (Int32)cmdToExecute.Parameters["@StoreID"].Value;
                }
                if (cmdToExecute.Parameters["@ProductName"].Value != DBNull.Value)
                {
                    _productName = cmdToExecute.Parameters["@ProductName"].Value.ToString();
                }
                if (cmdToExecute.Parameters["@UseLocalCredential"].Value != DBNull.Value)
                {
                   _useLocalCredential= (bool)cmdToExecute.Parameters["@UseLocalCredential"].Value;
                }
                if (cmdToExecute.Parameters["@UseADCredential"].Value != DBNull.Value)
                {
                    _useADCredential = (bool)cmdToExecute.Parameters["@UseADCredential"].Value;
                }

                if (_errorCode != (int)LLBLError.AllOk || _userID <= 0)
                {
                    if (_errorCode == 1)
                    {
                        throw new Exception("User name is invalid.");
                    }
                    if (_errorCode == 2)
                    {
                        throw new Exception("User name and password combination is invalid.");
                    }
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_User_Insert' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Validate:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                //throw new Exception("User::Validate::Error occured.", ex);
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
                _log.Trace("Exiting Validate");
            }
        }

        public string GetProductNameByDataOwnerID()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_GetProductNameByDataOwnerID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Connection = _trackerRetailConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }

                string productName = string.Empty;
                if (cmdToExecute.ExecuteScalar() != null)
                    productName = cmdToExecute.ExecuteScalar().ToString();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure '[dbo].[pr_GetProductNameByDataOwnerID]' reported the ErrorCode: " + _errorCode);
                }
                return productName;
            }
            catch (Exception ex)
            {
                _log.Error("GetProductNameByDataOwnerID:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::GetProductNameByDataOwnerID::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }

        public string GetWinAppSettingByUserID(string _appID)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_GetWinAppSettingByUserID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            cmdToExecute.Connection = _trackerRetailConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _name));
                cmdToExecute.Parameters.Add(new SqlParameter("@ApplicationName", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _appID));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }

                string xmlSetting = string.Empty;
                if (cmdToExecute.ExecuteScalar() != null)
                    xmlSetting = cmdToExecute.ExecuteScalar().ToString();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure '[dbo].[pr_GetWinAppSettingByUserID]' reported the ErrorCode: " + _errorCode);
                }
                return xmlSetting;
            }
            catch (Exception ex)
            {
                _log.Error("GetWinAppSettingByUserID:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::GetWinAppSettingByUserID::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
            }
        }

        public bool UpdateWinAppSettingByUserID(string applicationName, string xmlSettings)
        {
            _log.Trace("Entering Validate - Table:User ;UserName:{0},Password:{1},UserID:{2}", _userName, _password, _userID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_UpdateWinAppSettingByUserID]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _name));
                cmdToExecute.Parameters.Add(new SqlParameter("@ApplicationName", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, applicationName));
                cmdToExecute.Parameters.Add(new SqlParameter("@CurrentSetting", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, xmlSettings));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    _trackerRetailConnection.Open();
                }
                else
                {
                    if (_trackerRetailConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = _trackerRetailConnectionProvider.CurrentTransaction;
                    }
                }

                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Validate:{0}", ex.Message);
                throw ex;
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Validate");
            }
        }
      


        /// <summary>
        /// Method to return LookUp setting for Role
        /// </summary>
        /// 

        public  DataTable GetLookUpSettingForRole()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_GetLookUpSettingForRole]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("LookUpSetting");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _roleID));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_GetLookUpSettingForRole' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::GetLookUpSettingForRole::Error occured.", ex);
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

        public DataTable GetLookUpSettingForBarCode()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_GetLookUpSettingForBarCode]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("LookUpSetting");
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_GetLookUpSettingForBarCode' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("User::GetLookUpSettingForBarCode::Error occured.", ex);
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
        public Int32 UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }
        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public String UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                SqlString userNameTmp = (SqlString)value;
                if (userNameTmp.IsNull)
                {
                    throw new ArgumentOutOfRangeException("UserName", "UserName can't be NULL");
                }
                _userName = value;
            }
        }
        public String Password
        {
            get
            {
                return _password;
            }
            set
            {
                String passwordTmp = (String)value;
                if (passwordTmp == null)
                {
                    throw new ArgumentOutOfRangeException("Password", "Password can't be NULL");
                }
                _password = value;
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

        public Int32 BaseLocation
        {
            get
            {
                return _baseLocation;
            }
            set
            {
                _baseLocation = value;
            }
        }

        public String RoleName
        {
            get
            {
                return _roleName;
            }
            set
            {
                _roleName = value;
            }
        }
        public Int32 RoleIDOld
        {
            get
            {
                return _roleIDOld;
            }
            set
            {
                _roleIDOld = value;
            }
        }
        public Boolean Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }

        public Boolean Default
        {
            get
            {
                return _default;
            }
            set
            {
                _default = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }
        public String EmailID
        {
            get
            {
                return _emailID;
            }
            set
            {
                _emailID = value;
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
        public string DataOwnerName
        {
            get
            {
                return _dataOwnerName;
            }
        }

        public bool UseLocalCredential
        {
            get
            {
                return _useLocalCredential;
            }
            set
            {
                _useLocalCredential = value;
            }
        }

        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                _productName = value;
            }
        }

        public bool UseADCredential
        {
            get
            {
                return _useADCredential;
            }
            set
            {
                _useADCredential = value;
            }
        }

        public string ADPath
        {
            get
            {
                return _adPath;
            }  
         
        }

        public string ADPathDataOwner
        {
            get
            {
                return _adPathDataOwner;
            }
        }

        public int ADUsersUpdated
        {
            get
            {
                return _adUsersUpdated;
            }
        }

        public int ADUsersAdded
        {
            get
            {
                return _adUsersAdded;
            }
        }

        public List<string> UsersNotUpdated
        {
            get
            {
                if (_usersNotUpdated != string.Empty)
                {
                    _usersNotUpdated = _usersNotUpdated.TrimEnd(',');
                    return _usersNotUpdated.Split(',').ToList<string>();
                }
                else
                {
                    return null;
                }               
            }
        }
        
        #endregion
    }
}
