using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using KTone.DAL.KTDBBaseLib;
using System.Collections;
using KTone.DAL.SmartDCDataAccess;


namespace KTone.DAL.KTDAGlobaApplLib
{
    public class CustomColumn : DBInteractionBase
    { 
        #region [Attributes]
        private const string CUSTCOLUMNDESC = "KTCUSTOMCOLUMN";
        private const string EXTPROCNAME = "SP_ADDEXTENDEDPROPERTY";

        String _aliasName, _regExpression, _custColName;
        Boolean _isMultivalued, _isMandatory;
        private Int32 _dataOwnerID, _regID, _category;
        #endregion [Attributes]

        /// <summary>
        /// Purpose: Class constructor.
        /// </summary>
        public CustomColumn()
        {
            // Nothing for now.
        }

        #region LookUP Custom Column Value

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="customColName"></param>
        /// <param name="whereCond"></param>
        /// <param name="custColValue"></param>
        public void Lookup(string tableName, string customColName, string whereCond, out object custColValue)
        {
            custColValue = null;
            if (whereCond.Trim() == string.Empty)
                throw new ApplicationException("Where condication not supplied");



            SqlCommand cmdToExecute = new SqlCommand();
            try
            {
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT ");
                strSQL.Append("FROM ");
                strSQL.Append(tableName);
                strSQL.Append(" WHERE " + whereCond);

                _log.Trace("CustomColumnImpl:Lookup for:: " + strSQL.ToString());

                cmdToExecute.CommandText = strSQL.ToString();
                cmdToExecute.CommandType = CommandType.Text;

                cmdToExecute.Connection = _mainConnection;

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

                custColValue = cmdToExecute.ExecuteScalar();
            }
            catch (Exception ex)
            {
                _log.Error("CustomColumn::Lookup:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CustomColumn::Lookup::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting CustomColumn::Lookup().");
            }
        }

        public void Lookup(string tableName, string customColName, string whereCond, out Type custColType, out string custColValue)
        {
            if (whereCond.Trim() == string.Empty)
                throw new ApplicationException("Where condication not supplied");
           
            DataTable toReturn = new DataTable();
            SqlCommand cmdToExecute = new SqlCommand();
            try
            {
                custColValue = string.Empty;
                custColType = null;
                
               
             
                
                StringBuilder strSQL = new StringBuilder();
                strSQL.Append("SELECT ");
                strSQL.Append("FROM ");
                strSQL.Append(tableName);
                strSQL.Append(" WHERE " + whereCond);

                _log.Trace("CustomColumn::Lookup for:: " + strSQL.ToString());

                cmdToExecute.CommandText = strSQL.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                // Use base class' connection object
                cmdToExecute.Connection = _mainConnection;

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

                adapter.Fill(toReturn);

                if (toReturn.Rows.Count > 0)
                {
                    custColType = toReturn.Columns[0].DataType;
                }

                if (toReturn.Rows.Count > 0)
                    custColValue = toReturn.Rows[0][0].ToString();

                _log.Trace("CustomColumn::Lookup: Type = " + custColType.Name + " Value = " + custColValue);
            }
            catch (Exception ex)
            {
                _log.Error("CustomColumn::Lookup:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CustomColumn::Lookup::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting CustomColumn::Lookup.");
            }
        }

        #endregion LookUP Custom Column Value


        #region GET API
        /// <summary>
        /// Gets the customn columns defined for the table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataColumn[] GetAllCustomColumn(string tableName)
        {
            DataTable dt = GetAllCustomColumnAsRows(tableName);
            if (dt == null || dt.Rows.Count <= 0)
                return null;

            DataColumn[] dtCols;
            try
            {
                dtCols = new DataColumn[dt.Rows.Count];
                object val = null;
                for (int colCnt = 0; colCnt < dt.Rows.Count; colCnt++)
                {
                    DataColumn col = new DataColumn();
                    col.ColumnName = dt.Rows[colCnt]["COLUMN_NAME"].ToString();
                    SqlDbType dbtype = (System.Data.SqlDbType)System.Enum.Parse(typeof(System.Data.SqlDbType),
                                      dt.Rows[colCnt]["DATA_TYPE"].ToString(), true);
                    val = dt.Rows[colCnt]["IS_NULLABLE"];

                    if (val != null)
                        col.AllowDBNull = Convert.ToBoolean(val);

                    col.DataType = ConvertToType(dbtype);
                    if (col.DataType == typeof(string))
                        col.MaxLength = Convert.ToInt32(dt.Rows[colCnt]["MAX_LENGTH"].ToString());
                    dtCols[colCnt] = col;
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
            return dtCols;
        }

        /// <summary>
        /// Comma seperated column names
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public string GetCustomColumn(string tableName)
        {
            string strCols = string.Empty; ;

            DataTable dt = GetAllCustomColumnAsRows(tableName);
            if (dt == null || dt.Rows.Count <= 0)
                return strCols;

            try
            {
                for (int colCnt = 0; colCnt < dt.Rows.Count; colCnt++)
                {
                    strCols += dt.Rows[colCnt]["COLUMN_NAME"].ToString();
                    if (colCnt == dt.Rows.Count - 1)
                        strCols += " ";
                    else
                        strCols += ", ";
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                throw ex;
            }
            return strCols;
        }

        /// <summary>
        /// DataTable containing records with data only for the custom columns 
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetCustomColumnData(string tableName)
        {
            DataTable toReturn = new DataTable();
            SqlCommand cmdToExecute = new SqlCommand();
            try
            {
                string strCols = GetCustomColumn(tableName);
                string strSQL = "SELECT " + strCols + " FROM " + tableName;


                cmdToExecute.CommandText = strSQL.ToString();
                cmdToExecute.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                // Use base class' connection object
                cmdToExecute.Connection = _mainConnection;

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

                adapter.Fill(toReturn);

            }
            catch (Exception ex)
            {
                _log.Error("CustomColumn::GetCustomColumnData:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CustomColumn::GetCustomColumnData::Error occured.", ex);
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
                _log.Trace("Exiting CustomColumn::GetCustomColumnData.");
            }
            return toReturn;
        }
        /// <summary>
        /// Returns datatable with row for each column
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetAllCustomColumnAsRows(string tableName)
        {
          
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_GetAllCustomColumnAsRows]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                
                cmdToExecute.Parameters.Add(new SqlParameter("@TableName", SqlDbType.VarChar, 50, ParameterDirection.Input, true,0, 0, "", DataRowVersion.Proposed, tableName));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _category));
                cmdToExecute.Parameters.Add(new SqlParameter("@DataownerID", SqlDbType.Int, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _dataOwnerID));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_GetAllCustomColumnAsRows' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CustomColumn::GetAllCustomColumnAsRows::Error occured.", ex);
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
        /// Returns datatable with row for each column
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        //public DataTable GetAllCustomColumnAsRows(string tableName)
        //{
        //    DataTable toReturn = new DataTable();
        //    SqlCommand cmdToExecute = new SqlCommand();
        //    try
        //    {
        //        StringBuilder strSQL = new StringBuilder();
        //        strSQL.Append("SELECT distinct T.[Name] [TABLE_NAME], C.[name] [COLUMN_NAME], ");
        //        strSQL.Append("D.[Name] [DATA_TYPE], C.MAX_LENGTH, C.IS_NULLABLE, EP.value [COLUMN_DESCRIPTION], ");
        //        strSQL.Append("CF.AliasName, CF.IsMandatory, CF.IsMultiValued,  CF.RegExpression,CF.CategoryID ");
        //        strSQL.Append("FROM sys.extended_properties  EP ");
        //        strSQL.Append("INNER JOIN sys.tables T ON EP.major_id = T.object_id ");
        //        strSQL.Append("INNER JOIN sys.columns C ON EP.major_id = C.object_id AND EP.minor_id = C.column_id ");
        //        strSQL.Append("LEFT OUTER JOIN UserDefinedField CF ON C.[name] = CF.CustomColName ");

                
        //        strSQL.Append("INNER JOIN sys.types D ON C.System_Type_ID = D.System_Type_ID ");
        //        strSQL.Append("WHERE class = 1 and EP.value = 'KTCUSTOMCOLUMN' and T.name = '" + tableName + "' and CategoryID='" + _category + "' and DataOwnerID='" + _dataOwnerID + "' ORDER BY C.[name]");

        //        cmdToExecute.CommandText = strSQL.ToString();
        //        cmdToExecute.CommandType = CommandType.Text;
        //        SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

        //        // Use base class' connection object
        //        cmdToExecute.Connection = _mainConnection;

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

        //        adapter.Fill(toReturn);

        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error("CustomColumn::GetAllCustomColumnAsRows:{0}", ex.Message);
        //        // some error occured. Bubble it to caller and encapsulate Exception object
        //        throw new Exception("CustomColumn::GetAllCustomColumnAsRows::Error occured.", ex);
        //    }
        //    finally
        //    {
        //        if (_mainConnectionIsCreatedLocal)
        //        {
        //            // Close connection.
        //            _mainConnection.Close();
        //        }
        //        cmdToExecute.Dispose();
        //        _log.Trace("Exiting CustomColumn::GetAllCustomColumnAsRows.");
        //    }
        //    return toReturn;
        //}


        /// <summary>
        /// Returns datatable with row for each column
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>


        public DataTable GetAllCustomColumnAsRowsForCoustomColName(string custColName, string custTableName)
        {

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_GetAllCustomColumnAsRowsForCoustomColName]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CustTableName", SqlDbType.VarChar, 50, ParameterDirection.Input, true,0, 0, "", DataRowVersion.Proposed, custTableName));
                cmdToExecute.Parameters.Add(new SqlParameter("@CategoryID", SqlDbType.Int, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _category));
                cmdToExecute.Parameters.Add(new SqlParameter("@custColName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, custColName));
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_GetAllCustomColumnAsRows' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CustomColumn::GetAllCustomColumnAsRows::Error occured.", ex);
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


        //public DataTable GetAllCustomColumnAsRowsForCoustomColName(string custColName, string custTableName)
        //{
        //    DataTable toReturn = new DataTable();
        //    SqlCommand cmdToExecute = new SqlCommand();
        //    try
        //    {
        //        StringBuilder strSQL = new StringBuilder();
        //        strSQL.Append("SELECT distinct T.[Name] [TABLE_NAME], C.[name] [COLUMN_NAME], ");
        //        strSQL.Append("D.[Name] [DATA_TYPE], C.MAX_LENGTH, C.IS_NULLABLE, EP.value [COLUMN_DESCRIPTION], ");
        //        strSQL.Append("CF.CustomFieldID,CF.AliasName, CF.IsMandatory, CF.IsMultiValued, CF.RegExpression,(convert(varchar,cf.CategoryID)+'_'+ T.[Name]) CategoryID ");
        //        strSQL.Append("FROM sys.extended_properties  EP ");
        //        strSQL.Append("INNER JOIN sys.tables T ON EP.major_id = T.object_id ");
        //        strSQL.Append("INNER JOIN sys.columns C ON EP.major_id = C.object_id AND EP.minor_id = C.column_id ");
        //        strSQL.Append("LEFT OUTER JOIN UserDefinedField CF ON C.[name] = CF.CustomColName ");


        //        strSQL.Append("INNER JOIN sys.types D ON C.System_Type_ID = D.System_Type_ID ");
        //        strSQL.Append("WHERE class = 1 and EP.value = 'KTCUSTOMCOLUMN' and T.name = '" + custTableName + "' and cf.CategoryID='" + Category + "' and CF.CustomColName='" + custColName.Replace("'", "''") + "'" + " ORDER BY C.[name]");

        //        cmdToExecute.CommandText = strSQL.ToString();
        //        cmdToExecute.CommandType = CommandType.Text;
        //        SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

        //        // Use base class' connection object
        //        cmdToExecute.Connection = _mainConnection;

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

        //        adapter.Fill(toReturn);

        //    }
        //    catch (Exception ex)
        //    {
        //        _log.Error("CustomColumn::GetAllCustomColumnAsRowsForCoustomColName:{0}", ex.Message);
        //        // some error occured. Bubble it to caller and encapsulate Exception object
        //        throw new Exception("CustomColumn::GetAllCustomColumnAsRowsForCoustomColName::Error occured.", ex);
        //    }
        //    finally
        //    {
        //        if (_mainConnectionIsCreatedLocal)
        //        {
        //            // Close connection.
        //            _mainConnection.Close();
        //        }
        //        cmdToExecute.Dispose();
        //        _log.Trace("Exiting CustomColumn::GetAllCustomColumnAsRowsForCoustomColName.");
        //    }
        //    return toReturn;
        //}

        public DataTable GetAllColumnsNamesFromAssetDetailView()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_GetAllColumnsNames_AssetDetailView]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;
            DataTable toReturn = new DataTable();
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

                if (_errorCode != (int)LLBLError.AllOk)
                {
                    // Throw error.
                    throw new Exception("Stored Procedure 'pr_GetAllColumnsNames_AssetDetailView' reported the ErrorCode: " + _errorCode);
                }

                return toReturn;
            }
            catch (Exception ex)
            {
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CustomColumn::GetAllColumnsNamesFromAssetDetailView::Error occured.", ex);
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

        #endregion GET API




        #region ADD/DEL API

        /// <summary>
        /// Deletes the column with the supplied name for the table.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="colName"></param>
        public bool DeleteColumn(string tableName, string colName)
        {
            SqlCommand cmdToExecute = new SqlCommand();
            bool result = false;
            try
            {
                ConnectionProvider objConnectionProvider = new ConnectionProvider(DBInteractionBase.DBConnString);

                MainConnectionProvider = objConnectionProvider;

                MainConnectionProvider.OpenConnection();

                if (!_mainConnectionIsCreatedLocal)
                {
                    MainConnectionProvider.BeginTransaction("DeleteCustomColumn");

                    #region[DELETE]

                    DataTable dt = new DataTable();
                    string sql = "SELECT " + colName + " FROM " + tableName + " Where " + colName + " is Not Null ";

                    cmdToExecute.CommandText = sql;
                    cmdToExecute.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);

                    //// Use base class' connection object
                    //cmdToExecute.Connection = _mainConnection;

                    //if (_mainConnectionIsCreatedLocal)
                    //{
                    //    // Open connection.
                    //    _mainConnection.Open();
                    //}
                    //else
                    //{
                    //    if (_mainConnectionProvider.IsTransactionPending)
                    //    {
                    //        cmdToExecute.Transaction = _mainConnectionProvider.CurrentTransaction;
                    //    }
                    //}

                    cmdToExecute.Connection = MainConnectionProvider.DBConnection;

                    if (MainConnectionProvider.IsTransactionPending)
                    {
                        cmdToExecute.Transaction = MainConnectionProvider.CurrentTransaction;
                    }

                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                        throw new ApplicationException("The column contains data so cannot be deleted.");

                    sql = "";
                    sql = "ALTER TABLE " + tableName + " DROP COLUMN " + colName;

                    cmdToExecute.CommandText = sql;
                    cmdToExecute.ExecuteNonQuery();

                    sql = "";

                    int CustomFieldID = 0;
                    sql = "SELECT CustomFieldID FROM UserDefinedField where CategoryID= "+ _category +" and CustomColName='" + colName.Replace("'", "''") + "' ";
                    cmdToExecute.CommandText = sql;
                    cmdToExecute.CommandType = CommandType.Text;
                    adapter = new SqlDataAdapter(cmdToExecute);
                    adapter.Fill(dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        CustomFieldID = Convert.ToInt32(dt.Rows[0]["CustomFieldID"].ToString());
                    }

                    sql = "";
                    sql = "DELETE FROM  UserFielDGroupMap where CustomFieldID=" + CustomFieldID.ToString();
                    cmdToExecute.CommandText = sql;
                    cmdToExecute.ExecuteNonQuery();

                    sql = "";
                    sql = "DELETE FROM UserDefiendValues where CustomFieldID=" + CustomFieldID.ToString();
                    cmdToExecute.CommandText = sql;
                    cmdToExecute.ExecuteNonQuery();

                    sql = "";
                    sql = "DELETE FROM UserDefinedField where CategoryID=" + _category + " and CustomColName='" + colName.Replace("'", "''") + "'";
                    cmdToExecute.CommandText = sql;
                    cmdToExecute.ExecuteNonQuery();

                    //Refresh asset details view on custom column insertion
                    RefreshViews();

                    #endregion[DELETE]

                    MainConnectionProvider.CommitTransaction();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MainConnectionProvider.RollbackTransaction("DeleteCustomColumn");
                _log.Error("Delete:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                cmdToExecute.Dispose();
                MainConnectionProvider.CloseConnection(false);
                _log.Trace("Exiting Delete.");
            }
            return result;
        }


        /// <summary>
        ///  Adds columns supplied into a table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dtCols"></param>
        public void AddCustomColumn(string tableName, DataColumn[] dtCols, string UserdefinedFieldXML)
        {
            try
            {
                ConnectionProvider objConnectionProvider = new ConnectionProvider(DBInteractionBase.DBConnString);

                MainConnectionProvider = objConnectionProvider;

                MainConnectionProvider.OpenConnection();


                if (!_mainConnectionIsCreatedLocal)
                {
                    MainConnectionProvider.BeginTransaction("UserdefinedField");

                    foreach (DataColumn col in dtCols)
                    {
                        AddCustomColumn(tableName, col);
                    }
                    UserDefinedField.CreateDBConnection = false;
                    UserDefinedField objUserDefinedField = new UserDefinedField();
                    objUserDefinedField.MainConnectionProvider = objConnectionProvider;

                    objUserDefinedField.UserdefinedFieldXML = UserdefinedFieldXML;

                    objUserDefinedField.Insert();

                    MainConnectionProvider.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                MainConnectionProvider.RollbackTransaction("AddUserdefinedField");
                _log.Error("UserdefinedField::AddUserdefinedField()", ex);
                throw ex;
            }
            finally
            {
                AssetCustomField.CreateDBConnection = true;
                MainConnectionProvider.CloseConnection(false);
                _log.Trace("Exiting CustomColumn::AddUserdefinedField.");
            }
        }

        /// <summary>
        /// Adds one custom field and corrsponding details
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dtCol"></param>
        /// <param name="AssetCustomFieldXml"></param>
        public void AddCustomColumn(string tableName, DataColumn dtCol, string UserdefinedFieldXML, int dataOwnerID)
        {
            if (UserdefinedFieldXML.Equals(string.Empty))
                throw new ApplicationException("Field details is not supplied.");

            try
            {

                ConnectionProvider objConnectionProvider = new ConnectionProvider(DBInteractionBase.DBConnString);
                MainConnectionProvider = objConnectionProvider;
                MainConnectionProvider.OpenConnection();

                if (!_mainConnectionIsCreatedLocal)
                {
                    MainConnectionProvider.BeginTransaction("AddUserdefinedField");

                    AddCustomColumn(tableName, dtCol);

                    UserDefinedField.CreateDBConnection = false;
                    UserDefinedField objUserDefinedField = new UserDefinedField();
                    objUserDefinedField.MainConnectionProvider = objConnectionProvider;
                    objUserDefinedField.UserdefinedFieldXML = UserdefinedFieldXML;
                    objUserDefinedField.DataOwnerID = dataOwnerID;
                    objUserDefinedField.Insert(out _category);

                    MainConnectionProvider.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                MainConnectionProvider.RollbackTransaction("AddUserdefinedField");
                _log.Error("UserdefinedField::AddUserdefinedField()", ex);
                throw ex;
            }
            finally
            {
                AssetCustomField.CreateDBConnection = true;
                MainConnectionProvider.CloseConnection(false);
                _log.Trace("Exiting UserdefinedField::AddUserdefinedField.");
            }
        }


        

        /// <summary>
        ///  Adds columns supplied into a table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dtCols"></param>
        public void AddCustomColumn(string tableName, DataColumn[] dtCols)
        {
            try
            {
                ConnectionProvider objConnectionProvider = new ConnectionProvider(DBInteractionBase.DBConnString);

                MainConnectionProvider = objConnectionProvider;

                MainConnectionProvider.OpenConnection();


                if (!_mainConnectionIsCreatedLocal)
                {
                    MainConnectionProvider.BeginTransaction("UserdefinedField");

                    foreach (DataColumn col in dtCols)
                    {
                        AddCustomColumn(tableName, col);
                    }

                    MainConnectionProvider.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                MainConnectionProvider.RollbackTransaction("AddUserdefinedField");
                _log.Error("UserdefinedField::AddUserdefinedField()", ex);
                throw ex;
            }
            finally
            {
                MainConnectionProvider.CloseConnection(false);
                _log.Trace("Exiting UserdefinedField::AddUserdefinedField.");
            }
        }

        /// <summary>
        /// Add a single column into a table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="col"></param>
        public void AddCustomColumn(string tableName, DataColumn col)
        {
            string sql = "ALTER TABLE " + tableName + " ADD ";
            string colString = string.Empty;
            SqlCommand cmdToExecute = new SqlCommand();

            try
            {
                string sqlDbtype = ConvertToSqlDbType(col.DataType).ToString();
                
                colString = col.ColumnName + " " + sqlDbtype;
                if (col.DataType == typeof(string))
                {
                    if (col.MaxLength > 0)
                        colString += "(" + col.MaxLength.ToString() + ")";
                }
                else if (col.DataType == typeof(decimal))
                {
                    colString += "(18,4)";
                }

                sql += colString;
                _log.Trace("AddUserdefinedField SQL: " + sql);

                cmdToExecute.CommandText = sql;
                cmdToExecute.CommandType = CommandType.Text;

                cmdToExecute.Connection = MainConnectionProvider.DBConnection;

                if (MainConnectionProvider.IsTransactionPending)
                {
                    cmdToExecute.Transaction = MainConnectionProvider.CurrentTransaction;
                }

                cmdToExecute.ExecuteNonQuery(); // ADD COLUMN TO THE TABLE

                //Add desc for the column
                SqlParameter[] sqlParams = GetParamsForSP_ExtProperty(col.ColumnName, tableName, CUSTCOLUMNDESC);

                cmdToExecute.CommandText = EXTPROCNAME;
                cmdToExecute.CommandType = CommandType.StoredProcedure;
                cmdToExecute.Parameters.AddRange(sqlParams);

                cmdToExecute.ExecuteNonQuery();

                //Refresh asset details view on custom column insertion
                RefreshViews();
            }
            catch (Exception ex)
            {
                _log.Error("UserdefinedField::AddUserdefinedField:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CustomColumn::AddCustomColumn::Error occured.", ex);
            }
            finally
            {
                cmdToExecute.Dispose();
                _log.Trace("Exiting UserdefinedField::AddUserdefinedField.");
            }
        }
                
        public override bool Update()
        {
            _log.Trace("Entering Update - Table:UserDefinedField ; CustomColName :{0}," +
                "AliasName :{1},IsMandatory :{2},IsMultivalued:{3}, " +
                "RegExpression:{5}" + _custColName, _aliasName, _isMandatory, _regExpression);


            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = "dbo.[pr_UserDefinedField_Update]";
            cmdToExecute.CommandType = CommandType.StoredProcedure;

            // Use base class' connection object
            cmdToExecute.Connection = _mainConnection;

            try
            {
                cmdToExecute.Parameters.Add(new SqlParameter("@CustomColName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _custColName));
                cmdToExecute.Parameters.Add(new SqlParameter("@AliasName", SqlDbType.VarChar, 50, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _aliasName));
                cmdToExecute.Parameters.Add(new SqlParameter("@IsMandatory", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isMandatory));
                cmdToExecute.Parameters.Add(new SqlParameter("@IsMultivalued", SqlDbType.Bit, 1, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _isMultivalued));
              
                cmdToExecute.Parameters.Add(new SqlParameter("@RegExpression", SqlDbType.VarChar, 500, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _regExpression));
                if (_regID <= 0)
                    cmdToExecute.Parameters.Add(new SqlParameter("@RegID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, DBNull.Value));
                else
                    cmdToExecute.Parameters.Add(new SqlParameter("@RegID", SqlDbType.Int, 4, ParameterDirection.Input, true, 0, 0, "", DataRowVersion.Proposed, _regID));
                cmdToExecute.Parameters.Add(new SqlParameter("@Category", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, _category));
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
                    //Throw error.
                    throw new Exception("Stored Procedure 'pr_AssetCustomField_Update' reported the ErrorCode: " + _errorCode);
                }
                return true;
            }
            catch (Exception ex)
            {
                _log.Error("Update : {0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("AssetCustomField::Update::Error occured." + ex.Message, ex);
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


        #endregion ADD/DEL API

        /// <summary>
        /// Convert param of SqlDbType to Native Type
        /// </summary>
        /// <param name="sqlDbType"></param>
        /// <returns></returns>
        public static Type ConvertToType(SqlDbType sqlDbType)
        {
            Type retType = null;
            try
            {
                switch (sqlDbType)
                {
                    case SqlDbType.BigInt:
                        retType = typeof(Int64);
                        break;
                    case SqlDbType.Binary:
                        retType = typeof(Byte[]);
                        break;
                    case SqlDbType.Bit:
                        retType = typeof(Boolean);
                        break;
                    case SqlDbType.Char:
                        retType = typeof(string);
                        break;
                    case SqlDbType.DateTime:
                        retType = typeof(DateTime);
                        break;
                    case SqlDbType.Decimal:
                        retType = typeof(Decimal);
                        break;
                    case SqlDbType.Float:
                        retType = typeof(Double);
                        break;
                    case SqlDbType.Image:
                        retType = typeof(Byte[]);
                        break;
                    case SqlDbType.Int:
                        retType = typeof(Int32);
                        break;
                    case SqlDbType.Money:
                        retType = typeof(Decimal);
                        break;
                    case SqlDbType.NChar:
                        retType = typeof(String);
                        break;
                    case SqlDbType.NText:
                        retType = typeof(String);
                        break;
                    case SqlDbType.NVarChar:
                        retType = typeof(String);
                        break;
                    case SqlDbType.Real:
                        retType = typeof(Single);
                        break;
                    case SqlDbType.SmallDateTime:
                        retType = typeof(DateTime);
                        break;
                    case SqlDbType.SmallInt:
                        retType = typeof(Int16);
                        break;
                    case SqlDbType.SmallMoney:
                        retType = typeof(Decimal);
                        break;
                    case SqlDbType.Text:
                        retType = typeof(String);
                        break;
                    case SqlDbType.Timestamp:
                        retType = typeof(Byte[]);
                        break;
                    case SqlDbType.TinyInt:
                        retType = typeof(Byte);
                        break;
                    //case SqlDbType.Udt:
                    //    break;
                    case SqlDbType.UniqueIdentifier:
                        retType = typeof(Guid);
                        break;
                    case SqlDbType.VarBinary:
                        retType = typeof(Byte[]);
                        break;
                    case SqlDbType.VarChar:
                        retType = typeof(String);
                        break;
                    case SqlDbType.Variant:
                        retType = typeof(Object);
                        break;
                    //case SqlDbType.Xml:
                    //    break;
                    default:
                        retType = typeof(string);
                        break;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retType;
        }

        /// <summary>
        /// Convert param of Native Type to SqlDbType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SqlDbType ConvertToSqlDbType(Type type)
        {
            SqlDbType retType = SqlDbType.VarChar;

            try
            {
                switch (type.Name)
                {
                    case "string":
                        retType = SqlDbType.VarChar;
                        break;
                    case "Int16":
                        retType = SqlDbType.SmallInt;
                        break;
                    case "Int32":
                        retType = SqlDbType.Int;
                        break;
                    case "Int64":
                        retType = SqlDbType.BigInt;
                        break;
                    case "Boolean":
                        retType = SqlDbType.Bit;
                        break;
                    case "DateTime":
                        retType = SqlDbType.DateTime;
                        break;
                    case "Byte":
                        retType = SqlDbType.TinyInt;
                        break;
                    case "Decimal":
                        retType = SqlDbType.Decimal;
                        break;
                    case "Double":
                        retType = SqlDbType.Float;
                        break;
                    case "Single":
                        retType = SqlDbType.Real;
                        break;
                    default:
                        retType = SqlDbType.VarChar;
                        break;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retType;
        }

        private SqlParameter[] GetParamsForSP_ExtProperty(string colName, string tableName, string coldesc)
        {
            SqlParameter[] sqlParams = new SqlParameter[8];
            sqlParams[0] = new SqlParameter("@name", SqlDbType.NVarChar, 128);
            sqlParams[0].Direction = ParameterDirection.Input;
            sqlParams[0].Value = "MS_Description";

            sqlParams[1] = new SqlParameter("@value", SqlDbType.Variant);
            sqlParams[1].Direction = ParameterDirection.Input;
            sqlParams[1].Value = coldesc;

            sqlParams[2] = new SqlParameter("@level0type", SqlDbType.VarChar, 128);
            sqlParams[2].Direction = ParameterDirection.Input;
            sqlParams[2].Value = "Schema";

            sqlParams[3] = new SqlParameter("@level0name", SqlDbType.NVarChar, 128);
            sqlParams[3].Direction = ParameterDirection.Input;
            sqlParams[3].Value = "dbo";

            sqlParams[4] = new SqlParameter("@level1type", SqlDbType.VarChar, 128);
            sqlParams[4].Direction = ParameterDirection.Input;
            sqlParams[4].Value = "Table";


            sqlParams[5] = new SqlParameter("@level1name", SqlDbType.NVarChar, 128);
            sqlParams[5].Direction = ParameterDirection.Input;
            sqlParams[5].Value = tableName;

            sqlParams[6] = new SqlParameter("@level2type", SqlDbType.VarChar, 128);
            sqlParams[6].Direction = ParameterDirection.Input;
            sqlParams[6].Value = "Column";


            sqlParams[7] = new SqlParameter("@level2name", SqlDbType.NVarChar, 128);
            sqlParams[7].Direction = ParameterDirection.Input;
            sqlParams[7].Value = colName;



            return sqlParams;
        }

        /// <summary>
        /// Refreshes all views defined in database.
        /// </summary>
        public void RefreshViews()
        {
            SqlCommand cmdToExecute = new SqlCommand();
            try
            {
                cmdToExecute.CommandText = "dbo.[pr_RefreshViews]";
                cmdToExecute.CommandType = CommandType.StoredProcedure;

                cmdToExecute.Connection = MainConnectionProvider.DBConnection;

                if (MainConnectionProvider.IsTransactionPending)
                {
                    cmdToExecute.Transaction = MainConnectionProvider.CurrentTransaction;
                }

                cmdToExecute.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _log.Error("CustomColumn::RefreshViews:{0}", ex.Message);
                // some error occured. Bubble it to caller and encapsulate Exception object
                throw new Exception("CustomColumn::RefreshViews::Error occured.", ex);
            }
            finally
            {
                cmdToExecute.Dispose();
                _log.Trace("Exiting CustomColumn::RefreshViews.");
            }
        }

        #region [Class Properties]

        public String CustomColName
        {
            get
            {
                return _custColName;
            }
            set
            {
                _custColName = value;
            }
          
        }

        public String AliasName
        {
            get
            {
                return _aliasName;
            }
            set
            {
                _aliasName = value;
            }
        
        }

        public String RegularExpression
        {
            get
            {
                return _regExpression;
            }
            set
            {
                _regExpression = value;
            }
        
        }

        public Int32 RegID
        {
            get
            {
                return _regID;
            }
            set
            {
                _regID = value;
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

        public Boolean IsMultivalued
        {
            get
            {
                return _isMultivalued;
            }
            set
            {
                _isMultivalued = value;
            }
        }

      

        public Int32 Category
        {
            get
            {
                return _category;
            }

            set
            {
                _category = value;
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
        #endregion [Class Properties]
    }
}
