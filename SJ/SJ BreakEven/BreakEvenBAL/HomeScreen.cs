using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BreakEvenDAL;
using NLog;
using System.Data;

namespace BreakEvenBAL
{
    public class HomeScreen
    {
        NLog.Logger nlog = LogManager.GetCurrentClassLogger();

        public DataSet GetHomeScreenDetails(int noofDays)
        {
            nlog.Trace("BreakEvenBAL:HomeScreen:GetHomeScreenDetails::Entering");
            try
            {
                DA_HomeScreen objDAHomeScreen = new DA_HomeScreen();
                DataSet ds = objDAHomeScreen.GetHomeScreenDetails(noofDays);

                if (ds != null && ds.Tables.Count == 7)
                {
                    ds.Tables[0].TableName = "Summary";
                    ds.Tables[1].TableName = "25MostSellingItems";
                    ds.Tables[2].TableName = "25LeastSellingItems";
                    ds.Tables[3].TableName = "5TopRevenueGenerator";
                    ds.Tables[4].TableName = "5MostSellingVendor";
                    ds.Tables[5].TableName = "5LeastSellingVendor";
                    ds.Tables[6].TableName = "StoreProgress";
                }
                else
                {
                    throw new Exception("Invalid Data Source, No of Data Set is not 7");
                }
                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenBAL:HomeScreen:GetHomeScreenDetails::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenBAL:HomeScreen:GetHomeScreenDetails::Leaving");

            }
        }


        public DataSet DailyStoreDetails(string StoreName, string SaleTime)
        {
            nlog.Trace("BreakEvenBAL:HomeScreen:DailyStoreDetails::Entering");
            try
            {
                DA_HomeScreen objDAHomeScreen = new DA_HomeScreen();
                DataSet ds = objDAHomeScreen.DailyStoreDetails(StoreName, SaleTime);

                if (ds != null)
                {
                    ds.Tables[0].TableName = "Summary";
                    ds.Tables[1].TableName = "Header";

                    ds.Tables["Header"].Rows[0]["topselling"] = GetDistinctString(ds.Tables["Header"].Rows[0]["topselling"].ToString());
                    ds.Tables["Header"].Rows[0]["TopRevenue"] = GetDistinctString(ds.Tables["Header"].Rows[0]["TopRevenue"].ToString());

                }
                else
                {
                    throw new Exception("Invalid Data Source");
                }
                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenBAL:HomeScreen:DailyStoreDetails::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenBAL:HomeScreen:DailyStoreDetails::Leaving");

            }
        }


        public DataSet DaySummaryAllStores(string SaleTime)
        {
            nlog.Trace("BreakEvenBAL:HomeScreen:Daysummaryallstores::Entering");
            try
            {
                DA_HomeScreen objDAHomeScreen = new DA_HomeScreen();
                DataSet ds = objDAHomeScreen.DaySummaryAllStores(SaleTime);

                if (ds != null)
                {
                    ds.Tables[0].TableName = "Summary";
                  
                }
                else
                {
                    throw new Exception("Invalid Data Source");
                }
                return ds;
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenBAL:HomeScreen:Daysummaryallstores::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenBAL:HomeScreen:Daysummaryallstores::Leaving");

            }
        }

        public string GetDistinctString(string CommaSeperated)
        {
            nlog.Trace("BreakEvenBAL:HomeScreen:GetString::Entering");

            string result = "";
            try
            {
                string[] words = CommaSeperated.Split(',');
                IEnumerable<string> Iwords = words.Distinct();
                result = String.Join(",", Iwords);
                
            }

            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenBAL:HomeScreen:GetString::Error", ex);
                throw ex;
            }

            finally
            {
                nlog.Trace("BreakEvenBAL:HomeScreen:GetString::Leaving");
            }
            return result;
        }

        public DataSet GetHomeScreenDetails()
        {
            nlog.Trace("BreakEvenBAL:HomeScreen:GetHomeScreenDetails::Entering");
            try
            {
                return GetHomeScreenDetails(7);
            }
            catch (Exception ex)
            {
                nlog.ErrorException("BreakEvenBAL:HomeScreen:GetHomeScreenDetails::Error", ex);
                throw ex;
            }
            finally
            {
                nlog.Trace("BreakEvenBAL:HomeScreen:GetHomeScreenDetails::Leaving");

            }
        }

    }
}
