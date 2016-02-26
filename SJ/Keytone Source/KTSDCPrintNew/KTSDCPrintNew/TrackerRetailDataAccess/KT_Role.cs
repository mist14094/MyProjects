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
    /// Purpose: Data Access class for the table 'Role'.
    /// </summary>
    public class Role : DBInteractionBase
    {
        #region Class Member Declarations
        private Boolean _default;
        private Int32 _roleID, _dataOwnerID;
        private String _description, _role, _menuIDs,_adPath;

        #endregion


        /// <summary>
        /// Purpose: Class constructor.
        /// </summary>
        public Role()
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
        ///		 <LI>Role</LI>
        ///		 <LI>Description</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>RoleID</LI>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:Role ; RoleName:{0}," +
            "Description :{1}, Menus:{2}", _role, _description, _menuIDs);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Role_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _role));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _description));
                cmdToExecute.Parameters.Add(new SqlParameter("@Menus", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _menuIDs));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _roleID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Default", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _default));                
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
                _roleID = (Int32)cmdToExecute.Parameters["@RoleID"].Value;
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Role_Insert' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Role::Insert::Error occured.", ex);
            }
            finally
            {
                if (_trackerRetailConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _trackerRetailConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Insert.");
            }
        }

        /// <summary>
        /// Purpose: Update method. This method will Update one existing row in the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///		 <LI>RoleID</LI>
        ///		 <LI>Role</LI>
        ///		 <LI>Description</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public override bool Update()
        {
            _log.Trace("Entering Update - Table:Role ; RoleName:{0}," +
            "Description :{1}, Menus:{2},RoleID:{3}", _role, _description, _menuIDs, _roleID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Role_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _trackerRetailConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _roleID));
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _role));
                cmdToExecute.Parameters.Add(new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _description));
                cmdToExecute.Parameters.Add(new SqlParameter("@Menus", SqlDbType.VarChar, 2000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _menuIDs));                
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
                    throw new Exception("Stored Procedure 'pr_Role_Update' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Update:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Role::Update::Error occured.", ex);
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
        /// Purpose: Delete method. This method will Delete one existing row in the database, based on the Primary Key.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
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
        public override bool Delete()
        {
            _log.Trace("Entering Delete - Table:Role ; RoleID:{0}", _roleID);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Role_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

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
                cmdToExecute.ExecuteNonQuery();
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_Role_Delete' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Delete:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Role::Delete::Error occured.", ex);
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
        ///		 <LI>RoleID</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        ///		 <LI>RoleID</LI>
        ///		 <LI>Role</LI>
        ///		 <LI>Description</LI>
        /// </UL>
        /// Will fill all properties corresponding with a field in the table with the value of the row selected.
        /// </remarks>
        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_Role_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Role");
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
                    throw new Exception("Stored Procedure 'pr_Role_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {
                    _roleID = (Int32)toReturn.Rows[0]["RoleID"];
                    _role = (string)toReturn.Rows[0]["RoleName"];
                    _description = (string)toReturn.Rows[0]["Description"];
                   // _adPath = toReturn.Rows[0]["ADPath"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["ADPath"];
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Role::SelectOne::Error occured.", ex);
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
            cmdToExecute.CommandText = "dbo.[pr_Role_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("Role");
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
                    throw new Exception("Stored Procedure 'pr_Role_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Role::SelectAll::Error occured.", ex);
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


        #region Class Property Declarations
        public Int32 RoleID
        {
            get
            {
                return _roleID;
            }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentOutOfRangeException("RoleID", "RoleID can't be NULL");
                }
                _roleID = value;
            }
        }


        public String RoleName
        {
            get
            {
                return _role;
            }
            set
            {
                if (value == string.Empty)
                {
                    throw new ArgumentOutOfRangeException("Role", "Role can't be NULL");
                }
                _role = value;
            }
        }


        public String Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        public String MenuIDs
        {
            get
            {
                return _menuIDs;
            }
            set
            {
                _menuIDs = value;
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

        public String ADPath
        {
            get
            {
                return _adPath;
            }
            set
            {
                _adPath = value;
            }
        }

        #endregion
    }
}
