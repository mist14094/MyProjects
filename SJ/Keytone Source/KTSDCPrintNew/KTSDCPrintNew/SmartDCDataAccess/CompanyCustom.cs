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
    public class CompanyCustom : DBInteractionBase
    {
        int _categoryID, _dataOwnerID, _companyID;
        string _custTableName;
          public DataTable GetCustomColumnSchema()
        {
            _log.Trace("Entering GetCustomColumnSchema - Table:Customcolumn");

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_UserDefinedFields_GetCustomColumnSchema]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable("CustomcolumnSchema");
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _categoryID));
                cmdToExecute.Parameters.Add(new SqlParameter("@CustTableName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _custTableName));
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
                _log.Error("GetCustomColumnSchema:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("Customcolumn::GetCustomColumnSchema::Error occured.", ex);
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
                _log.Trace("Exiting GetCustomColumnSchema");
            }
        }

          public DataTable SelectGroup()
          {
              SqlCommand cmdToExecute = new SqlCommand();
              cmdToExecute.CommandText = "dbo.[pr_GetUserDefinedGroup]";
              cmdToExecute.CommandType = CommandType.StoredProcedure;
              DataTable toReturn = new DataTable("UserDefinedGroup");
              SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

              // Use base class' connection object
              cmdToExecute.Connection = _mainConnection;

              try
              {
                  cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                  cmdToExecute.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _categoryID));
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
                      throw new Exception("Stored Procedure 'pr_GetUserDefinedGroup' reported the ErrorCode: " + _errorCode);
                  }

                  return toReturn;
              }
              catch (Exception ex)
              {
                  // some error occured. Bubble it to caller and encapsulate Exception object
                  throw new Exception("pr_GetUserDefinedGroup::GetUserDefinedGroup::Error occured.", ex);
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
          public DataTable FieldsCount()
          {
              SqlCommand cmdToExecute = new SqlCommand();
              cmdToExecute.CommandText = "dbo.[pr_GetUserDefinedFieldsCount]";
              cmdToExecute.CommandType = CommandType.StoredProcedure;
              DataTable toReturn = new DataTable("UserDefinedField");
              SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

              // Use base class' connection object
              cmdToExecute.Connection = _mainConnection;

              try
              {
                  cmdToExecute.Parameters.Add(new SqlParameter("@DataOwnerID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
                  cmdToExecute.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _categoryID));
                  

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
                //  _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;

                  if (_errorCode != 0)
                  {
                      // Throw error.
                      throw new Exception("Stored Procedure 'pr_GetUserDefinedFieldsCount' reported the ErrorCode: " + _errorCode);
                  }

                  return toReturn;
              }
              catch (Exception ex)
              {
                  // some error occured. Bubble it to caller and encapsulate Exception object
                  throw new Exception("UserDefinedFieldsCount::UserDefinedFieldsCount::Error occured.", ex);
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

          public bool Insert(long CompId, string columns, string colunmvalues)
          {
              _log.Trace("Entering Insert - Table:CompanyCustom ; CompanyID :{0}," +
                  "Columns :{1},ColumnValues :{2}", CompId, columns, colunmvalues);

              SqlCommand cmdToExecute = new SqlCommand();
              cmdToExecute.CommandText = "dbo.[pr_CompanyCustom_Insert]";
              cmdToExecute.CommandType = CommandType.StoredProcedure;

              // Use base class' connection object
              cmdToExecute.Connection = _mainConnection;

              try
              {
                  cmdToExecute.Parameters.Add(new SqlParameter("@CompId", SqlDbType.BigInt, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed, CompId));
                  cmdToExecute.Parameters.Add(new SqlParameter("@Columns", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, columns));
                  cmdToExecute.Parameters.Add(new SqlParameter("@ColumnValues", SqlDbType.VarChar, 8000, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, colunmvalues));
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
                  cmdToExecute.ExecuteNonQuery();
                  _errorCode = (SqlInt32)cmdToExecute.Parameters["@ErrorCode"].Value;
                  if (_errorCode != 0)
                  {
                      // Throw error.
                      throw new Exception("Stored Procedure 'pr_CompanyCustom_Insert' reported the ErrorCode: " + _errorCode);
                  }

                  return true;
              }
              catch (Exception ex)
              {
                  _log.Error("Insert:{0}", ex.Message);
                  // some error occured. Bubble it to caller and encapsulate Exception object
                  throw new Exception("CompanyCustom::Insert::Error occured.", ex);
              }
              finally
              {
                  if (_mainConnectionIsCreatedLocal)
                  {
                      // Close connection.
                      _mainConnection.Close();
                  }
                  cmdToExecute.Dispose();
                  UpdateNotifyCacheUpdateTable();
                  //if (CompanyCustom.CreateDBConnection)
                  //    UpdateNotifyCacheUpdateTable();
                  //else
                  //    UpdateNotifyCacheInTransaction(MainConnectionProvider);
                
                  _log.Trace("Exiting Insert.");
              }
          }

          public override DataTable SelectOne()
          {
              SqlCommand cmdToExecute = new SqlCommand();
              cmdToExecute.CommandText = "dbo.[pr_CompanyCustom_SelectOne]";
              cmdToExecute.CommandType = CommandType.StoredProcedure;
              DataTable toReturn = new DataTable("CompanyCustom");
              SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

              // Use base class' connection object
              cmdToExecute.Connection = _mainConnection;

              try
              {
                  cmdToExecute.Parameters.Add(new SqlParameter("@CompID", SqlDbType.Int, 8, ParameterDirection.Input, false, 19, 0, "", DataRowVersion.Proposed,_companyID));
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
                      throw new Exception("Stored Procedure 'pr_CompanyCustom_SelectOne' reported the ErrorCode: " + _errorCode);
                  }

                  return toReturn;
              }
              catch (Exception ex)
              {
                  // some error occured. Bubble it to caller and encapsulate Exception object
                  throw new Exception("CompanyCustom::SelectOne::Error occured.", ex);
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

    #region Class Property Declarations

		
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

        public Int32 CompanyID
        {
            get
            {
                return _companyID;
            }
            set
            {
                _companyID = value;
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
        

             public string  CustTableName
        {
            get
            {
                return _custTableName;
            }
            set
            {
                _custTableName = value;
            }
        }
		#endregion
	}
    }

