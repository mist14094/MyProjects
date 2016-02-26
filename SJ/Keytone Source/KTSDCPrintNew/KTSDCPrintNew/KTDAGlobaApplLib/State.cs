using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;

namespace KTone.DAL.KTDAGlobaApplLib
{
    public class State : DBInteractionBase
    {
        #region Class Member Declarations

        
        private Int32 _stateId;
        private String _stateCode, _countryCode, _name;

        #endregion

        #region [Class Method Declaration]
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
            cmdToExecute.CommandText = "[dbo].[pr_State_SelectAll]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("State");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
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
                    throw new Exception("Stored Procedure 'pr_State_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("State::SelectAll::Error occured.", ex);
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
            cmdToExecute.CommandText = "[dbo].[pr_State_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("State");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@StateID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _stateId));
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
                    throw new Exception("Stored Procedure 'pr_State_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {
                    _name = toReturn.Rows[0]["Name"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["Name"];
                    _stateId = Convert.ToInt32(toReturn.Rows[0]["StateId"]);
                    _stateCode = toReturn.Rows[0]["StateCode"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["StateCode"];
                    _countryCode = toReturn.Rows[0]["CountryCode"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["CountryCode"];

                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("State::SelectOne::Error occured.", ex);
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
        /// Purpose: SelectStateFromCountryCode method. This method will Select all rows from the table.
        /// </summary>
        /// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///		 <LI>ErrorCode</LI>
        /// </UL>
        /// </remarks>
        public DataTable SelectStateFromCountryCode(string scountryCode)
        {

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "[dbo].[pr_State_SelectState_Country]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("State");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, scountryCode));
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
                    throw new Exception("Stored Procedure 'pr_State_SelectState_Country' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {
                    _name = toReturn.Rows[0]["Name"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["Name"];
                    _stateId = Convert.ToInt32(toReturn.Rows[0]["StateId"]);
                    _stateCode = toReturn.Rows[0]["StateCode"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["StateCode"];
                    _countryCode = toReturn.Rows[0]["CountryCode"] == System.DBNull.Value ? String.Empty : (string)toReturn.Rows[0]["CountryCode"];

                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("State::SelectStateFromCountryCode::Error occured.", ex);
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
        /// Purpose: Insert method. This method will insert one new row into the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///     <LI>@StateCode varchar(50)</LI>
        ///     <LI>@CountryCode varchar(10)</LI>
        ///     <LI>@Name varchar(50)</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///     <LI>@StateId int(0)</LI>
        ///     <LI>@ErrorCode int(0)</LI>
        /// </UL>
        /// </remarks>
        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:State ;StateCode: {0};CountryCode: {1};Name: {2};StateId: {3};", _stateCode, _countryCode, _name, _stateId);
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_State_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@StateCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _stateCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _countryCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _name));
                cmdToExecute.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, _stateId));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, _errorCode));
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
                if (cmdToExecute.Parameters["@StateId"].Value.ToString() != "")
                {
                    _stateId = (Int32)cmdToExecute.Parameters["@StateId"].Value;
                }
                else
                {
                    _stateId = 0;
                }
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                if (_errorCode == 5016)
                {
                    throw new Exception("102");
                }
                if (_errorCode == 5015)
                {
                    throw new Exception("103");
                }
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_State_Insert' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                if (_errorCode == 5016)
                {
                    throw new Exception("102");
                }
                if (_errorCode == 5015)
                {
                    throw new Exception("103");
                }
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("State::Insert::Error occured.", ex);
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

        /// <summary>
        /// Purpose: Update method. This method will update existing row into the database.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///     <LI>@StateId int(0)</LI>
        ///     <LI>@StateCode varchar(50)</LI>
        ///     <LI>@CountryCode varchar(10)</LI>
        ///     <LI>@Name varchar(50)</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///     <LI>@ErrorCode int(0)</LI>
        /// </UL>
        /// </remarks>
        public override bool Update()
        {
            _log.Trace("Entering Update - Table:State ;StateId: {0};StateCode: {1};CountryCode: {2};Name: {3};", _stateId, _stateCode, _countryCode, _name);
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_State_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _stateId));
                cmdToExecute.Parameters.Add(new SqlParameter("@StateCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _stateCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _countryCode));
                cmdToExecute.Parameters.Add(new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _name));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, _errorCode));
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
                if (_errorCode == 5017)
                {
                    throw new Exception("102");
                }
                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_State_Update' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Update:{0}", ex.Message);
                if (_errorCode == 5017)
                {
                    throw new Exception("102");
                }
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("State::Update::Error occured.", ex);
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

        /// <summary>
        /// Purpose: Delete method. This method will Delete one existing row in the database, based on the Primary Key.
        /// </summary>
        /// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
        /// <remarks>
        /// Properties needed for this method: 
        /// <UL>
        ///     <LI>@StateId int(0)</LI>
        /// </UL>
        /// Properties set after a succesful call of this method: 
        /// <UL>
        ///     <LI>@ErrorCode int(0)</LI>
        /// </UL>
        /// </remarks>
        public override bool Delete()
        {
            _log.Trace("Entering Delete - Table:State ;StateId: {0};", _stateId);
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_State_Delete]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;
            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _stateId));
                cmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Proposed, _errorCode));
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
                    throw new Exception("Stored Procedure 'pr_State_Delete' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Delete:{0}", ex.Message);

                if (((System.Data.SqlClient.SqlException)(ex)).Number.ToString() == "547")
                    throw new Exception("104");
                 
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("State::Delete::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Delete");
            }
        }

        #endregion

        #region [Class Property Declarations]
        public Int32 StateId
        {
            get
            {
                return _stateId;
            }
            set
            {
                _stateId = value;
            }
        }

        public String StateCode
        {
            get
            {
                return _stateCode;
            }
            set
            {
                _stateCode = value;
            }
        }

        public String CountryCode
        {
            get
            {
                return _countryCode;
            }
            set
            {
                _countryCode = value;
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

        #endregion
    }
}
