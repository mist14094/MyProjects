using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DataLogic
{
    public class DataLogic
    {
        public DataLogic()
        {

        }

        public int AddMasterData(DataTable dt, string fileName, string sheetName)
        {
            SqlConnection sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString());
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InsertTBCMasterDetail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FileName", fileName);
                cmd.Parameters.AddWithValue("@SheetName", sheetName);
                cmd.Parameters.AddWithValue("@ReportName", dt.Columns[0].ColumnName);
                cmd.Parameters.AddWithValue("@Date", dt.Rows[0][0].ToString().Substring(5));
                cmd.Parameters.AddWithValue("@BaseConfiguration", dt.Rows[1][0].ToString().Substring(20));
                cmd.Parameters.AddWithValue("@Description", "");
                cmd.Parameters.AddWithValue("@FromLocation", "");
                cmd.Parameters.AddWithValue("@ToLocation", "");
                cmd.Parameters.AddWithValue("@ImportedTime", DBNull.Value);
                cmd.Parameters.AddWithValue("@isValid", "1");
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                object returnValue = cmd.ExecuteScalar();
                return int.Parse(s: returnValue.ToString());
            }

            catch (Exception ex)
            {
                return 0;
            }

            finally
            {
                sqlConnection1.Close();
            }

        }
        public int InsertSheetData(DataRow dr, int referenceId, int lineNumber, string valueTobeProcessed, string productClass)
        {

            SqlConnection sqlConnection1 = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "InsertTBCSheetData";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ReferenceID", referenceId);
                cmd.Parameters.AddWithValue("@LineNumber", lineNumber);
                cmd.Parameters.AddWithValue("@Sam", dr[0].ToString());
                cmd.Parameters.AddWithValue("@Identifier", dr[1].ToString());
                cmd.Parameters.AddWithValue("@Dil", dr[2].ToString());
                cmd.Parameters.AddWithValue("@Wgt", dr[3].ToString());
                cmd.Parameters.AddWithValue("@NicotinePercentage", dr[4].ToString());
                cmd.Parameters.AddWithValue("@TSugarPercentage", dr[5].ToString());
                cmd.Parameters.AddWithValue("@RSugarPercentage", dr[6].ToString());
                cmd.Parameters.AddWithValue("@ValueTobeProcessed", valueTobeProcessed);
                cmd.Parameters.AddWithValue("@ProductClass", productClass);
                cmd.Parameters.AddWithValue("@ImportedTime", DBNull.Value);
                cmd.Connection = sqlConnection1;
                sqlConnection1.Open();
                object returnValue = cmd.ExecuteScalar();
                return int.Parse(returnValue.ToString());
            }

            catch (Exception ex)
            {
                return 0;
            }

            finally
            {
                sqlConnection1.Close();
            }
        }
        public DataTable GetSheetDetails(string masterSno)
        {
            if (masterSno == null) throw new ArgumentNullException("masterSno");
            DataTable allData = new DataTable();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT [Sam],[Identifier],[Dil],[Wgt],[NicotinePercentage],[TSugarPercentage],[RSugarPercentage] FROM [TobaccoTest].[dbo].[TbcTest_Details] WHERE referenceid=" + masterSno, connection);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(allData);
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return allData;
        }
        public void DeleteDirtyData(string masterSno)
        {
            if (masterSno == null) throw new ArgumentNullException("masterSno");
            DataTable allData = new DataTable();
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString());
            try
            {
                SqlCommand cmd = new SqlCommand(" DELETE FROM [TbcTest_Details] WHERE [ReferenceID] ='" + masterSno + "'; DELETE FROM TbcTest_MasterDetail WHERE [Sno] ='" + masterSno + "';", connection);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(allData);
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
            }

        }

        public DataTable GetTobbaccoClass()
        {
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "[GetTobbaccoClass]";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        //sqlCommand.Parameters.Add(new SqlParameter("@sample", sample));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }

            return dt;

        }

        public DataTable GetTobbaccoClassDetails(string tobbaaccoClass)
        {
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "[GetTobbaccoClassDetails]";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@ClassName", tobbaaccoClass));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }

            return dt;

        }


        public DataTable GetTobbaccoClassByLatest()
        {
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "[GetTobbaccoClassByLatest]";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        //sqlCommand.Parameters.Add(new SqlParameter("@sample", sample));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }

            return dt;

        }

        public DataTable GetFileDetails(int sno)
        {
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "[GetFileDetails]";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@SNO", sno));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }

            return dt;

        }

        public DataTable GetFileDetailsInternal(int sno)
        {
            var dt = new DataTable();
            SqlConnection connection = null;
            try
            {
                using (connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()))
                {
                    SqlCommand sqlCommand;
                    using (sqlCommand = new SqlCommand())
                    {
                        int totalRowsAfected;
                        sqlCommand.CommandText = "[GetFileDetailsInternal]";
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@SNO", sno));
                        sqlCommand.Connection = connection;
                        connection.Open();
                        SqlDataReader sqlDataReader;
                        using (sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            dt.Load(reader: sqlDataReader);
                        }

                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (connection != null) connection.Close();
            }

            return dt;

        }

    }
}
