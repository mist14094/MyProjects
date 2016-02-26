/********************************************************************************************************
Copyright (c) 2005 KeyTone Technologies.All Right Reserved

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

namespace KTone.RFIDGlobal.ImportData
{
	/// <summary>
	/// Summary description for DBManager.
	/// </summary>
	public class DBManager
	{
		//private static DBManager objDBManager = null;
		SqlConnection m_SqlConn = null;
		SqlConnection m_SqlTransConn = null;
	
		SqlTransaction m_sqlTrans = null;
		string m_ConnStr = string.Empty;
		//SqlCommand m_sqlCmd = null;
      
		public DBManager(string connString)
		{
            
			//
			// TODO: Add constructor logic here
			//
			//CreateConnection();
			this.m_ConnStr = connString;
		}

		//		public static DBManager GetInstance()
		//		{
		//			if(objDBManager == null)
		//				objDBManager = new DBManager();
		//			return objDBManager;
		//		}

		#region "Common DBFunction"
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
                string msg = "BiginTrans:" + ex.Message;
                if (ex.InnerException != null)
                    msg += Environment.NewLine + "Inner Exception:" + ex.InnerException.Message;
                msg += Environment.NewLine + "Error Number:" + ex.Number;
                throw new ApplicationException(msg, ex);
				//throw new ApplicationException ("Failed to start transaction."+Environment.NewLine+ ex.Message, ex) ;
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
                throw new ApplicationException("Failed to commit transaction." + Environment.NewLine + ex.Message, ex);
			}
			finally
			{
                if(m_sqlTrans != null)
                    m_sqlTrans.Dispose();
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
                throw new ApplicationException("Failed to rollback transaction." + Environment.NewLine + ex.Message, ex);
			}
			finally
			{
                if (m_sqlTrans != null)
                    m_sqlTrans.Dispose();
				
				m_sqlTrans = null;
			}
		}

	
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

                CreateConnection(ref m_SqlConn);
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
           // sqlCmd.Dispose();
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

        public string ExecuteInsertStatment(string strInsertStmt, bool useTransaction)
        {
            int recAffected = 0;
            SqlConnection conn = null;
            if (useTransaction)
                conn = m_SqlTransConn;
            else
                conn = m_SqlConn;

            CreateConnection(ref conn);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "pr_ExecuteInsertStmt";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Connection = conn;

            if (m_sqlTrans != null)
                sqlCmd.Transaction = m_sqlTrans;
            sqlCmd.Parameters.Add(new SqlParameter("@strSQL",strInsertStmt));
            sqlCmd.Parameters.Add(new SqlParameter("@id",SqlDbType.VarChar,100,ParameterDirection.Output,true,0,0,"",DataRowVersion.Proposed,0)); 

            recAffected = sqlCmd.ExecuteNonQuery();

            return sqlCmd.Parameters["@id"].Value.ToString();


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

        public DataTable GetInTransDataTable(string spName, SqlParameter[] sqlParamArr)
        {
            try
            {
                DataTable dtLocal = new DataTable();
                CreateConnection(ref m_SqlTransConn);
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = spName;
                sqlCmd.Connection = m_SqlTransConn;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter sqlParam in sqlParamArr)
                    sqlCmd.Parameters.Add(sqlParam);
                SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

                sqlDA.SelectCommand.Transaction = m_sqlTrans;
                sqlDA.Fill(dtLocal);
                return dtLocal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public SqlDataReader LastIdentity(bool useTransaction)
        {
            SqlConnection conn = null;
            if (useTransaction)
                conn = m_SqlTransConn;
            else
                conn = m_SqlConn;
            CreateConnection(ref conn);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "SELECT @@IDENTITY as ID";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = conn;
            if (useTransaction && m_sqlTrans != null)
                 sqlCmd.Transaction = m_sqlTrans;

            SqlDataReader ident_result = sqlCmd.ExecuteReader();
            if (ident_result.Read()) { return ident_result; }
            else
            return ident_result;
        }

        public List<string> GetColumns(string tableName, bool useTransaction)
        {
            List<string> cols = new List<string>();
            SqlConnection conn = null;
            if (useTransaction)
                conn = m_SqlTransConn;
            else
                conn = m_SqlConn;
            CreateConnection(ref conn);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "SELECT UPPER(COLUMN_NAME) FROM INFORMATION_SCHEMA.Columns where TABLE_NAME = '" + tableName + "' ";
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = conn;
            if (useTransaction && m_sqlTrans != null)
                sqlCmd.Transaction = m_sqlTrans;
            DataSet ds= new DataSet();
            SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);
				sqlDA.Fill(ds);
            if(ds != null && ds.Tables.Count >0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    cols.Add(dr[0].ToString());
                }
            }
           
            return cols;
        }
	
		#endregion "Common DBFunction"
	}
}
