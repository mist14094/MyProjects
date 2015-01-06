using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisDataAccess
{
    public class CreateReport : JarvisDataAccessBase
    {

        public CreateReport()
        {

        }

        public DataSet GetViewandProcedureName()
        {
            nlog.Trace("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandType = CommandType.Text;
                SQLCmd.CommandText = JarvisConstant.QRGetViewandProcedureName;
                SQLCmd.Connection = EPMSQLConnection;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                nlog.Error("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }


        }
        public DataSet GetSampleDataView(string ViewName)
        {
            nlog.Trace("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandTimeout = 600;
                SQLCmd.CommandType = CommandType.Text;
                SQLCmd.CommandText = "select top 10 * from " + ViewName;
                SQLCmd.Connection = EPMSQLConnection;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                nlog.Error("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }


        }
        public DataSet GetViewColumnName(string ViewName)
        {
            nlog.Trace("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandTimeout = 600;
                SQLCmd.CommandType = CommandType.Text;
                SQLCmd.CommandText = "SELECT column_name FROM information_schema.columns WHERE table_name = '" + ViewName +"'";
                SQLCmd.Connection = EPMSQLConnection;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                nlog.Error("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }


        }
        public DataSet SelectDataBaseNames()
        {
            nlog.Trace("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandTimeout = 600;
                SQLCmd.CommandType = CommandType.Text;
                SQLCmd.CommandText = "SELECT [Sno],[DataBaseName],[EquivalentString]  FROM [Jarvis].[dbo].[DataBaseInstanceNames] WHERE Enabled =1";
                SQLCmd.Connection = EPMSQLConnection;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                nlog.Error("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }


        }
        public DataSet GetDatabaseElementNames(string  ReferenceSNO)
        {
            nlog.Trace("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            try
            {
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandTimeout = 600;
                SQLCmd.CommandType = CommandType.Text;
                SQLCmd.CommandText = "SELECT [sno],[ElementName],[ElementAliasName],[ElementDesc],[ElementType],[Enabled],[CreatedTime],[ExecutionScript],[ReferenceDatabaseName]  FROM [Jarvis].[dbo].[DataBaseElements] WHERE Enabled = 1 AND ReferenceDatabaseName ="+ReferenceSNO+" AND ElementType IN ('C','V')";
                SQLCmd.Connection = EPMSQLConnection;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                nlog.Error("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("JarvisDataAccess:CreateReport:" + System.Reflection.MethodBase.GetCurrentMethod().Name + "::Leaving");
            }


        }

        public DataSet GetInitialDataBaseNames()
        {
            throw new NotImplementedException();
        }
    }
}
