/********************************************************************************************************
Copyright (c) 2010 KeyTone Technologies.All Right Reserved

KeyTone's source code and documentation is copyrighted and contains proprietary information.
Licensee shall not modify, create any derivative works, make modifications, improvements, 
distribute or reveal the source code ("Improvements") to anyone other than the software 
developers of licensee's organization unless the licensee has entered into a written agreement
("Agreement") to do so with KeyTone Technologies Inc. Licensee hereby assigns to KeyTone all right,
title and interest in and to such Improvements unless otherwise stated in the Agreement. Licensee 
may not resell, rent, lease, or distribute the source code alone, it shall only be distributed in 
compiled component of an application within the licensee'organization. 
The licensee shall not resell, rent, lease, or distribute the products created from the source code
in any way that would compete with KeyTone Technologies Inc. KeyTone' copyright notice may not be 
removed from the source code.
   
Licensee may be held legally responsible for any infringement of intellectual property rights that
is caused or encouraged by licensee's failure to abide by the terms of this Agreement. Licensee may 
make copies of the source code provided the copyright and trademark notices are reproduced in their 
entirety on the copy. KeyTone reserves all rights not specifically granted to Licensee. 
 
Use of this source code constitutes an agreement not to criticize, in any way, the code-writing style
of the author, including any statements regarding the extent of documentation and comments present.

THE SOFTWARE IS PROVIDED "AS IS" BASIS, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING 
BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER  LIABILITY, 
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. KEYTONE TECHNOLOGIES SHALL NOT BE LIABLE FOR ANY DAMAGES 
SUFFERED BY LICENSEE AS A RESULT OF USING, MODIFYING OR DISTRIBUTING THIS SOFTWARE OR ITS DERIVATIVES.
********************************************************************************************************/

using System;
using System.Data;
using System.Data.SqlClient;

using System.Collections.Generic;
using System.Text;

namespace KTone.RFIDGlobal.KTAgentSpecific
{
    
   public class AgentDBMgr
    {

       private static NLog.Logger m_log = KTone.RFIDGlobal.KTLogger.KTLogManager.GetLogger();

       SqlConnection m_UpdateConn = new SqlConnection();
       

       SqlTransaction m_Trans = null; 
       SqlConnection m_SqlConn = null;
		SqlConnection m_SqlTransConn = null;
	
		SqlTransaction m_sqlTrans = null;
		string m_ConnStr = string.Empty;

        public AgentDBMgr(string connString)
		{
			this.m_ConnStr = connString;
		}

		#region [DBFunction]
		private void CreateConnection(ref SqlConnection conn)
		{
			try
			{
				if(conn != null && conn.State == ConnectionState.Open)
					return;
				//string connString = System.Configuration.ConfigurationManager.AppSettings["ConnString"];
				string connString  = m_ConnStr;
				conn = new SqlConnection(connString);
				conn.Open();
				if(conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
					throw new ApplicationException("Connection is in close state.");
			
			}
			catch(Exception ex)
			{
				throw new ApplicationException("Error in creating Connection: " + this.m_ConnStr, ex);
			}
		}

	
		public void BiginTrans()
		{
			CreateConnection(ref m_SqlTransConn);
			try
			{
				m_sqlTrans = m_SqlTransConn.BeginTransaction();
			}
			catch(SqlException ex)
			{
				throw new ApplicationException ("Failed to start transaction.", ex) ;
			}
		}

		
		public void CommitTrans()
		{
			try
			{
				if(m_sqlTrans != null)
					m_sqlTrans.Commit();				
			}
			catch(SqlException ex)
			{
				throw new ApplicationException ("Failed to commit transaction.", ex) ;
			}
			finally
			{
				m_sqlTrans = null;
			}
		}

		
		public void RollBackTrans()
		{
			try
			{
				if(m_sqlTrans != null)
					m_sqlTrans.Rollback();
			}
			catch(SqlException ex)
			{
				throw new ApplicationException ("Failed to rollback transaction.", ex) ;
			}
			finally
			{
				m_sqlTrans = null;
			}
		}

       //public void OpenConnection()
       //{
       //    if (m_SqlConn != null && m_SqlConn.State == ConnectionState.Closed)
       //    {
       //        m_SqlConn.Open();
       //    }
       //    if(m_SqlTransConn != null && m_SqlTransConn.State == ConnectionState.Open)
       //    {
       //         m_SqlTransConn.Open();
       //    }
       //}
		public void CloseConnection()
		{
			if(m_SqlConn != null && m_SqlConn.State == ConnectionState.Open)
			{
				m_SqlConn.Close();
				m_SqlConn.Dispose();
			}
			if(m_SqlTransConn != null && m_SqlTransConn.State == ConnectionState.Open)
			{
				m_SqlTransConn.Close();
				m_SqlTransConn.Dispose();
			}
		}

		
		public int GetNextVal(string sSequenceName) 
		{
			
				SqlCommand cmd = new SqlCommand() ;
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.Parameters.Clear();
				SqlParameter sqlp1 = cmd.Parameters.Add("@sequence",SqlDbType.VarChar,100);
				sqlp1.Value = sSequenceName ;
			
				SqlParameter sqlp = cmd.Parameters.Add("@sequence_Id",SqlDbType.Int);
				sqlp.Direction = ParameterDirection.Output;
				
				cmd.CommandText = "Nextval"  ;
				cmd.Connection = m_SqlConn;
					cmd.ExecuteScalar();
				return Convert.ToInt32(cmd.Parameters["@sequence_Id"].Value);
			
		}
		/// <summary>
		/// Executes any SP
		/// </summary>
		/// <param name="sqlParamArr"></param>
		/// <param name="spName"></param>
		/// <returns></returns>
		public int ExecuteSP(SqlParameter[] sqlParamArr, string spName, bool useTransaction)
		{
			int recAffected = 0;
			SqlConnection conn = null;
			if(useTransaction)
				conn = m_SqlTransConn;
			else
				conn = m_SqlConn;

			CreateConnection(ref conn);
			SqlCommand sqlCmd = new SqlCommand();
			sqlCmd.CommandText = spName;
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Connection = conn;

			if(m_sqlTrans != null)
				sqlCmd.Transaction = m_sqlTrans;
			if(sqlParamArr != null && sqlParamArr.Length >0)
				foreach(SqlParameter sqlParam in sqlParamArr)
					sqlCmd.Parameters.Add(sqlParam);

            sqlCmd.CommandTimeout = 0;
			return recAffected = sqlCmd.ExecuteNonQuery();
		}


		/// <summary>
		///Executes any SP with one parameter with direction of Out
		/// </summary>
		/// <param name="sqlParamArr"></param>
		/// <param name="spName"></param>
		/// <param name="useTransaction"></param>
		/// <param name="sqlCmd"></param>
		/// <returns></returns>
		public int ExecuteSP(SqlParameter[] sqlParamArr, string spName, bool useTransaction, out SqlCommand sqlCmd)
		{
			int recAffected = 0;
			SqlConnection conn = null;
			if(useTransaction)
				conn = m_SqlTransConn;
			else
				conn = m_SqlConn;

			CreateConnection(ref conn);
			sqlCmd = new SqlCommand();
			sqlCmd.CommandText = spName;
			sqlCmd.CommandType = CommandType.StoredProcedure;
			sqlCmd.Connection = conn;

			if(m_sqlTrans != null)
				sqlCmd.Transaction = m_sqlTrans;
			if(sqlParamArr != null && sqlParamArr.Length >0)
				foreach(SqlParameter sqlParam in sqlParamArr)
					sqlCmd.Parameters.Add(sqlParam);

			return recAffected = sqlCmd.ExecuteNonQuery();
		}


		/// <summary>
		/// Executes any SQL
		/// </summary>
		/// <param name="sqlParamArr"></param>
		/// <param name="spName"></param>
		/// <returns></returns>
		public int ExecuteSQL(string sqlString, bool useTransaction)
		{
			int recAffected = 0;
			SqlConnection conn = null;
			if(useTransaction)
				conn = m_SqlTransConn;
			else
				conn = m_SqlConn;
			CreateConnection(ref conn);
			SqlCommand sqlCmd = new SqlCommand();
			sqlCmd.CommandText = sqlString;
			sqlCmd.CommandType = CommandType.Text;
			sqlCmd.Connection = conn;
			if(useTransaction && m_sqlTrans != null)
				sqlCmd.Transaction = m_sqlTrans;
			recAffected = sqlCmd.ExecuteNonQuery();
			return recAffected;
		}

	
		public object ExecuteScalar(string sqlString)
		{
			CreateConnection(ref m_SqlConn);
			SqlCommand sqlCmd = new SqlCommand();
			sqlCmd.CommandText = sqlString;
			sqlCmd.CommandType = CommandType.Text;
			sqlCmd.Connection = m_SqlConn;
			return sqlCmd.ExecuteScalar();
		}

        /// <summary>
        /// Execute the SQL statement and returns first column from the first row of resultset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="sqlParam"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType commandType, string commandText, SqlParameter sqlParam)
        {
            if (sqlParam == null)
                throw new ApplicationException("Parameter supplied is invalid.");

            SqlCommand sqlCmd = null;
            object sqlOutput = null; ;

            try
            {
                SqlParameter[] sqlParams = new SqlParameter[1];
                sqlParams[0] = sqlParam;
                sqlCmd = PrepareCommand(commandType, commandText, sqlParams);

                sqlOutput = sqlCmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                m_log.Error("DataAccessLib: ExecuteScalar: " + ex.Message + " \r\n\tCommandType:" + commandType.ToString() + " \r\n\tCommandText " + commandText);
                throw ex;
            }
            catch (Exception ex)
            {
                m_log.Error("DataAccessLib: ExecuteScalar: " + ex.Message + " \r\n\tCommandType:" + commandType.ToString() + " \r\n\tCommandText " + commandText);
                throw ex;
            }
            finally
            {
                if (sqlCmd != null)
                    sqlCmd.Dispose();
                sqlCmd = null;
            }
            return sqlOutput;
        }

        /// <summary>
        /// Execute the SQL statement and returns first column from the first row of resultset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            SqlCommand sqlCmd = null;
            object sqlOutput = null; ;

            try
            {
                sqlCmd = PrepareCommand(commandType, commandText, null);

                sqlOutput = sqlCmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                m_log.Error("DataAccessLib: ExecuteScalar: " + ex.Message + " \r\n\tCommandType:" + commandType.ToString() + " \r\n\tCommandText " + commandText);
                throw ex;
            }
            catch (Exception ex)
            {
                m_log.Error("DataAccessLib: ExecuteScalar: " + ex.Message + " \r\n\tCommandType:" + commandType.ToString() + " \r\n\tCommandText " + commandText);
                throw ex;
            }
            finally
            {
                if (sqlCmd != null)
                    sqlCmd.Dispose();
                sqlCmd = null;
            }
            return sqlOutput;
        }


        /// <summary>
        /// Execute the SQL statement and returns first column from the first row of resultset
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText">SQL query/ Stored Proc name</param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType commandType, string commandText, SqlParameter[] sqlParams)
        {
            if (sqlParams == null)
                throw new ApplicationException("Parameter supplied is invalid.");

            SqlCommand sqlCmd = null;
            object sqlOutput = null; ;

            try
            {
                sqlCmd = PrepareCommand(commandType, commandText, sqlParams);
                sqlOutput = sqlCmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                m_log.Error("DataAccessLib: ExecuteScalar: " + ex.Message + " \r\n\tCommandType:" + commandType.ToString() + " \r\n\tCommandText " + commandText);
                throw ex;
            }
            catch (Exception ex)
            {
                m_log.Error("DataAccessLib: ExecuteScalar: " + ex.Message + " \r\n\tCommandType:" + commandType.ToString() + " \r\n\tCommandText " + commandText);
                throw ex;
            }
            finally
            {
                if (sqlCmd != null)
                    sqlCmd.Dispose();
                sqlCmd = null;
            }
            return sqlOutput;
        }
        /// <summary>
        /// Fills the datatable based on the query passed. 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="strSQL"></param>
        public void FillDataTable(DataTable table, string strSQL)
        {
            SqlParameter sqlParam = null;
            FillDataTable(table, strSQL, sqlParam);
        }
        /// <summary>
        /// Fills the datatable based on the resultset of query passed.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="strSQL"></param>
        /// <param name="sqlParam"></param>
        public void FillDataTable(DataTable table, string strSQL, SqlParameter sqlParam)
        {
            SqlParameter[] sqlParams = null;
            if (sqlParam != null)
            {
                sqlParams = new SqlParameter[1];
                sqlParams[0] = sqlParam;
            }
            FillDataTable(table, strSQL, sqlParams);
        }
        /// <summary>
        /// Fills the datatable based on the resultset of query passed.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="strSQL"></param>
        /// <param name="sqlParams"></param>
        public void FillDataTable(DataTable table, string strSQL,
            SqlParameter[] sqlParams, CommandType cmdType)
        {
            SqlCommand cmd = null;
            SqlDataAdapter da = null;
            SqlConnection conn = null;
            try
            {
                bool useTransaction = false;
                //SqlConnection conn = null;
                CreateConnection(ref m_SqlConn);
                if (useTransaction)
                    conn = m_SqlTransConn;
                else
                    conn = m_SqlConn;
                CreateConnection(ref conn);
                cmd = PrepareCommand(cmdType, strSQL, sqlParams);
                da = new SqlDataAdapter();
                da.SelectCommand= cmd;
                da.Fill(table);
            }
            catch (SqlException ex)
            {
                m_log.Error("DataAccessLib: FillDataTable: " + ex.Message + " \r\n\tTable:" + table.TableName + " \r\n\tQuery " + strSQL);
                throw ex;
            }
            catch (Exception ex)
            {
                m_log.Error("DataAccessLib: FillDataTable: " + ex.Message + " \r\n\tTable:" + table.TableName + " \r\n\tQuery " + strSQL);
                throw ex;
            }
            finally
            {
                if (da != null)
                    da.Dispose();
                da = null;
                if (cmd != null)
                    cmd.Dispose();
                cmd = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        private SqlCommand PrepareCommand(CommandType commandType, string commandText, SqlParameter[] commandParameters)
        {

            //associate the connection with the command
            SqlCommand command = new SqlCommand();

            command.Connection = m_SqlConn;
            
            command.CommandText = commandText;

            //if we were provided a transaction, assign it.
            if (m_Trans != null)
                command.Transaction = m_Trans;

            //set the command type
            command.CommandType = commandType;
           
            //attach the command parameters if they are provided
            if (commandParameters != null)
            {
                foreach (SqlParameter param in commandParameters)
                {
                    if ((param.Direction == ParameterDirection.InputOutput) && (param.Value == null))
                        param.Value = DBNull.Value;
                    command.Parameters.Add(param);
                }
            }


            return command;
        }
        /// <summary>
        /// Fills the datatable based on the resultset of query passed.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="strSQL"></param>
        /// <param name="sqlParams"></param>
        /// <param name="cmdType"></param>
        public void FillDataTable(DataTable table, string strSQL, SqlParameter[] sqlParams)
        {
            FillDataTable(table, strSQL, sqlParams, CommandType.Text);
        }
		
		public DataSet GetDataSet(string strSQL)
		{
			try
			{
				DataSet dsLocal = new DataSet();
				CreateConnection(ref m_SqlConn);
				SqlDataAdapter sqlDA = new SqlDataAdapter(strSQL, m_SqlConn);
				sqlDA.Fill(dsLocal);
				return dsLocal;
			}
			catch(Exception ex)
			{
				throw ex;
			}
		}

        public DataSet GetInTransDataSet(string strSQL)
        {
            try
            {
                DataSet dsLocal = new DataSet();
                CreateConnection(ref m_SqlTransConn);
                SqlDataAdapter sqlDA = new SqlDataAdapter(strSQL, m_SqlTransConn);
                sqlDA.SelectCommand.Transaction = m_sqlTrans;
                sqlDA.Fill(dsLocal);
                return dsLocal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
	
		#endregion [DBFunction]
	}
}
