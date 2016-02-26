using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KTone.DAL.KTDBBaseLib;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Data;

namespace KTone.DAL.KTDAGlobaApplLib
{
    public class DataOwner : DBInteractionBase
    {
        #region Class Member Declarations
        private Int32 _dataOwnerID,_roleID, _userID;

        private bool _useLocalCredential, _useADCredential;

        private String _password, _dataOwnerName, _comments, _roleName,_user, _userName, _menus, _description, _productName,_adPath;

        #endregion

        public DataOwner()
		{
			// Nothing for now.
		}

        public override bool Insert()
        {
            _log.Trace("Entering Insert - Table:DataOwner");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_DataOwner_Insert]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _dataOwnerName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Comments", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _comments));
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _roleName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Menus", SqlDbType.VarChar, 2000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _menus));
                cmdToExecute.Parameters.Add(new SqlParameter("@User", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _user));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userName));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _password));
                cmdToExecute.Parameters.Add(new SqlParameter("@ADPath", SqlDbType.NVarChar,1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _adPath));
                cmdToExecute.Parameters.Add(new SqlParameter("@UseLocalCredential", SqlDbType.Bit, 2, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _useLocalCredential));
                cmdToExecute.Parameters.Add(new SqlParameter("@UseADCredential", SqlDbType.Bit,2, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _useADCredential));                
                cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));                

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
               
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_DataOwner_Insert' reported the ErrorCode: " + _errorCode);
                }

                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Insert:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("DataOwner::Insert::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Insert.");
            }
        }

        public override DataTable SelectAll()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_DataOwner_SelectAll]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable dtToReturn = new DataTable();
            SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_mainConnectionIsCreatedLocal)
                {
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
                sdaAdapter.Fill(dtToReturn);
                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_DataOwner_SelectAll' reported the ErrorCode: " + _errorCode);
                }

                return dtToReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("DataOwner::SelectAll::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                scmCmdToExecute.Dispose();
                sdaAdapter.Dispose();
            }
        }

        public override bool Delete()
        {
            SqlCommand scmCmdToExecute = new SqlCommand();
            scmCmdToExecute.CommandText = "dbo.[pr_DataOwner_Delete]";
            scmCmdToExecute.CommandType = CommandType.StoredProcedure;


            // Use base class' connection object
            scmCmdToExecute.Connection = _mainConnection;

            try
            {
                scmCmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                scmCmdToExecute.Parameters.Add(new SqlParameter("@ErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, _errorCode));

                // Open connection.
                if (_mainConnectionIsCreatedLocal)
                {
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

                _errorCode = (SqlInt32)scmCmdToExecute.Parameters["@ErrorCode"].Value;

                if (_errorCode == 547)
                {
                    throw new Exception("Selected DataOwner is in use.");
                }

                if (_errorCode != 0)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_DataOwner_Delete' reported the ErrorCode: " + _errorCode);
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
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                scmCmdToExecute.Dispose();

            }
        }

        public override DataTable SelectOne()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_DataOwner_SelectOne]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, _dataOwnerID));
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
                    throw new Exception("Stored Procedure 'pr_DataOwner_SelectOne' reported the ErrorCode: " + _errorCode);
                }

                if (toReturn.Rows.Count > 0)
                {
                    _dataOwnerName = toReturn.Rows[0]["Name"].ToString();
                    _roleName = toReturn.Rows[0]["RoleName"].ToString();
                    _user = toReturn.Rows[0]["Name1"].ToString();
                    _userName = toReturn.Rows[0]["UserName"].ToString();
                    _password = toReturn.Rows[0]["Password"].ToString();
                    _roleID = Convert.ToInt32(toReturn.Rows[0]["RoleID"]);
                    _comments = toReturn.Rows[0]["Comments"].ToString();
                    _adPath = toReturn.Rows[0]["ADPath"].ToString();
                    _useLocalCredential = Convert.ToBoolean(toReturn.Rows[0]["UseLocalCredential"]);
                    _useADCredential = Convert.ToBoolean(toReturn.Rows[0]["UseADCredential"]);
                }
                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("DataOwner::SelectOne::Error occured.", ex);
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

        public override bool Update()
        {
            _log.Trace("Entering Update - Table:DataOwner.. ");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_DataOwner_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _userID));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@RoleID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, _roleID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Commnets", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _comments));
                cmdToExecute.Parameters.Add(new SqlParameter("@Menus", SqlDbType.VarChar, 2000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _menus));
                cmdToExecute.Parameters.Add(new SqlParameter("@User", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _user));
                cmdToExecute.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _userName));
                cmdToExecute.Parameters.Add(new SqlParameter("@ProductName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _productName));
                cmdToExecute.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _password));
                cmdToExecute.Parameters.Add(new SqlParameter("@ADPath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _adPath));
                cmdToExecute.Parameters.Add(new SqlParameter("@UseLocalCredential", SqlDbType.Bit, 2, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _useLocalCredential));
                cmdToExecute.Parameters.Add(new SqlParameter("@UseADCredential", SqlDbType.Bit, 2, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, _useADCredential)); 
                cmdToExecute.Parameters.Add(new SqlParameter("@iErrorCode", SqlDbType.Int, 4, ParameterDirection.Output, true, 10, 0, "", DataRowVersion.Proposed, _errorCode));

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
                _errorCode = (SqlInt32)cmdToExecute.Parameters["@iErrorCode"].Value;

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_DataOwner_Update' reported the ErrorCode: " + _errorCode);
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
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting Update.");
            }
        }
        #region Class Property Declarations
       
        public Int32 DataOwnerID
        {
            get
            {
                return _dataOwnerID;
            }
            set
            {
                Int32 iDataOwnerIDTmp = (Int32)value;
                _dataOwnerID = value;
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
                Int32 iRoleIDTmp = (Int32)value;
                _roleID= value;
            }
        }
        

        public Int32 UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                Int32 iUserIDTmp = (Int32)value;
                _userID = value;
            }
        }

        public String DataOwnerName
        {
            get
            {
                return _dataOwnerName;
            }
            set
            {
               String sNameTmp = (String)value;
                if (sNameTmp==null)
                {
                    throw new ArgumentOutOfRangeException("Name", "Name can't be NULL");
                }
                _dataOwnerName = value;
            }
        }

        public String Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                String sCommentsTmp = (String)value;
                if (sCommentsTmp==null)
                {
                    throw new ArgumentOutOfRangeException("Comments", "Comments can't be NULL");
                }
                _comments = value;
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
                String sRoleNameTmp = (String)value;
                if (sRoleNameTmp==null)
                {
                    throw new ArgumentOutOfRangeException("RoleName", "RoleName can't be NULL");
                }
                _roleName = value;
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
                String sUserNameTmp = (String)value;
                if (sUserNameTmp==null)
                {
                    throw new ArgumentOutOfRangeException("UserName", "UserName can't be NULL");
                }
                _userName = value;
            }
        }

        public String User
        {
            get
            {
                return _user;
            }
            set
            {
                String sUserTmp = (String)value;
                if (sUserTmp == null)
                {
                    throw new ArgumentOutOfRangeException("User", "User can't be NULL");
                }
                _user = value;
            }
        }

        public String ProductName
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

        public String Description
        {
            get
            {
                return _description;
            }
            set
            {
                String sDescriptionTmp = (String)value;
                if (sDescriptionTmp==null)
                {
                    throw new ArgumentOutOfRangeException("Description", "Description can't be NULL");
                }
                _description = value;
            }
        }

        public String Menus
        {
            get
            {
                return _menus;
            }
            set
            {
                String sMenusTmp = (String)value;
                if (sMenusTmp==null)
                {
                    throw new ArgumentOutOfRangeException("Menus", "Menus can't be NULL");
                }
                _menus = value;
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
                String sPasswordTmp = (String)value;
                if (sPasswordTmp==null )
                {
                    throw new ArgumentOutOfRangeException("Password", "Password can't be NULL");
                }
                _password = value;
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
                String sADPAthTmp =(String)value;
                if (sADPAthTmp == null)
                {
                    throw new ArgumentOutOfRangeException("ADPath", "ADPath can't be NULL");
                }
                _adPath = value;
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

        #endregion


    }
}