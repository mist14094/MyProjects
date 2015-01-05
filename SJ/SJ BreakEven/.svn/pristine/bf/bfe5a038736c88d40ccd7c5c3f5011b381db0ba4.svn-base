using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BreakEvenConstant;


namespace BreakEvenDAL
{
    public class DA_HomeScreen : BEDALBase
    {
        public DataSet GetHomeScreenDetails(int NoofDays)
        {
            nlog.Trace("BreakEvenDAL:DA_HomeScreen:GetHomeScreenDetails::Entering");
            try
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.CommandText = BEconst.pr_HomeScreen;
                SQLCmd.Parameters.Add(new SqlParameter("@NoOfDays", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, NoofDays));
                SQLCmd.Connection = Conn;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenDAL:DA_HomeScreen:GetHomeScreenDetails::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenDAL:DA_HomeScreen:GetHomeScreenDetails::Leaving");

            }


        }



        public DataSet DailyStoreDetails(string StoreName, string SaleTime)
        {
            nlog.Trace("BreakEvenDAL:DA_HomeScreen:DailyStoreDetails::Entering");
            try
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.CommandText = BEconst.pr_DailyStoreDetails;
                SQLCmd.Parameters.Add(new SqlParameter("@StoreName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed,StoreName));
                SQLCmd.Parameters.Add(new SqlParameter("@SaleTime", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, SaleTime));
                SQLCmd.Connection = Conn;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenDAL:DA_HomeScreen:DailyStoreDetails::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenDAL:DA_HomeScreen:DailyStoreDetails::Leaving");

            }


        }


        public DataSet DaySummaryAllStores(string SaleTime)
        {
            nlog.Trace("BreakEvenDAL:DA_HomeScreen:pr_Daysummaryallstores::Entering");
            try
            {

                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand SQLCmd = new SqlCommand();
                SQLCmd.CommandType = CommandType.StoredProcedure;
                SQLCmd.CommandText = BEconst.pr_Daysummaryallstores;
                SQLCmd.Parameters.Add(new SqlParameter("@SaleTime", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, SaleTime));
                SQLCmd.Connection = Conn;
                da.SelectCommand = SQLCmd;
                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenDAL:DA_HomeScreen:pr_Daysummaryallstores::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenDAL:DA_HomeScreen:pr_Daysummaryallstores::Leaving");

            }


        }




    }
}
