using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace KTone.DAL.KTDBBaseLib
{
    public struct CustomSQLCriteria
    {
        public string Name;
        public string Value;
        public string ComparisonOperator;
    }

    public class SQLHelper : DBInteractionBase
    {

        /// <summary>
        /// Checks if the record correponding to the criteria exists in given table
        /// </summary>
        /// <param name="tableName"> Database Table Name </param>
        /// <param name="keyAttributes"> key-value pair dictionary in string format, 
        /// whihc will go in where condition</param>
        /// <returns></returns>
        public bool IsExists(string tableName, Dictionary<string, string> keyAttributes)
        {
            bool recordExist = false;

            DataTable dtData = null;
            try
            {
                dtData = GetData(tableName, keyAttributes);
                if (dtData != null && dtData.Rows.Count > 0)
                    recordExist = true;
            }
            catch { }


            return recordExist;
        }

        /// <summary>
        /// Get the records correponding to the criteria exists in given table
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keyAttributes"></param>
        /// <returns></returns>
        public DataTable GetData(string tableName, Dictionary<string, string> keyAttributes)
        {
            string strSQL = "Select * FROM " + tableName + " WHERE ";
            foreach (string keyCol in keyAttributes.Keys)
            {
                strSQL += " " + keyCol + " = '" + keyAttributes[keyCol].Replace("'", "''") + "' AND";
            }
            if (strSQL.EndsWith("AND"))
                strSQL = strSQL.Substring(0, strSQL.Length - 3);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = strSQL;
            cmdToExecute.CommandType = CommandType.Text;
            DataTable toReturn = new DataTable(tableName);
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;

            try
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }

                adapter.Fill(toReturn);
            }
            catch (Exception ex)
            {
                _log.Trace("SQLHelper:GetData::" + ex.Message);

                throw ex;
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
            return toReturn;
        }

        /// <summary>
        /// Get the records based on the valid sql query supplied
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public DataSet GetData(string strSQL)
        {


            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = strSQL;
            cmdToExecute.CommandType = CommandType.Text;
            DataSet toReturn = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(cmdToExecute);
            cmdToExecute.Connection = _mainConnection;

            try
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }

                adapter.Fill(toReturn);
            }
            catch (Exception ex)
            {
                _log.Trace("SQLHelper:GetData::" + ex.Message);

                throw ex;
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
            return toReturn;
        }

        /// <summary>
        /// Updates the table data (dataAttributes) based on the criteria (keyAttributes)
        /// </summary>
        /// <param name="tableName">table to be updated</param>
        /// <param name="keyAttributes">Condition</param>
        /// <param name="dataAttributes">data to be updated</param>
        /// <returns></returns>
        public bool UpdateData(string tableName, Dictionary<string, string> keyAttributes, Dictionary<string, string> dataAttributes)
        {
            if (dataAttributes.Count < 1)
                throw new ApplicationException("Data to be updated is not supplied.");

            string strSQL = "UPDATE " + tableName + " SET ";
            bool dataUpdated = false;
            //Data
            foreach (string keyCol in dataAttributes.Keys)
            {
                strSQL += keyCol + " = '" + dataAttributes[keyCol].Replace("'", "''") + "' ,";
            }
            if (strSQL.EndsWith(","))
                strSQL = strSQL.Substring(0, strSQL.Length - 1);

            strSQL += " WHERE ";
            //Condition
            foreach (string keyCol in keyAttributes.Keys)
            {
                strSQL += " " + keyCol + " = '" + keyAttributes[keyCol].Replace("'", "''") + "' AND";
            }
            if (strSQL.EndsWith("AND"))
                strSQL = strSQL.Substring(0, strSQL.Length - 3);

            SqlCommand cmdToExecute = new SqlCommand();
            cmdToExecute.CommandText = strSQL;
            cmdToExecute.CommandType = CommandType.Text;
            cmdToExecute.Connection = _mainConnection;

            try
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Open connection.
                    _mainConnection.Open();
                }

                int recUpdated = cmdToExecute.ExecuteNonQuery();
                if (recUpdated > 0)
                    dataUpdated = true;
            }
            catch (Exception ex)
            {
                _log.Trace("SQLHelper:UpdateData::" + ex.Message);
                throw ex;
            }
            finally
            {
                if (_mainConnectionIsCreatedLocal)
                {
                    // Close connection.
                    _mainConnection.Close();
                }
                cmdToExecute.Dispose();
            }
            return dataUpdated;
        }

        /// <summary>
        /// This method returns custom query against provided table name and custom attributes.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="keyAttributes"></param>
        /// <returns></returns>
        public string GenerateCustomQuery(string tableName, List<CustomSQLCriteria> keyAttributes)
        {
            string strSQL = string.Empty;
            try
            {
                strSQL = "Select * FROM " + tableName + " WHERE ";

                foreach (CustomSQLCriteria criteria in keyAttributes)
                {
                    string colName = criteria.Name;
                    string colValue = criteria.Value;
                    string colOperator = criteria.ComparisonOperator;

                    if (colValue.ToUpper() != "NULL")
                        strSQL += " " + colName + " " + colOperator + " " + "'" + colValue.Replace("'", "''") + "' AND";
                    else
                        strSQL += " " + colName + " " + colOperator + " " + colValue.Replace("'", "''") + " AND";
                }

                if (strSQL.EndsWith("AND"))
                    strSQL = strSQL.Substring(0, strSQL.Length - 3);
            }
            catch (Exception ex)
            {
                _log.Trace("SQLHelper:GenerateCustomQuery::" + ex.Message);
                throw ex;
            }
            return strSQL;
        }
    }

 
}
