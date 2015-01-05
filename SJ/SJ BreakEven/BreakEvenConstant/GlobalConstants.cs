using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace BreakEvenConstant
{
    public class GlobalConstants
    {
        //Connection String
        static string _ConnBEValue = ConfigurationManager.ConnectionStrings["BreakEven"].ConnectionString;
        static string _ConnKTTrackerRetail = ConfigurationManager.ConnectionStrings["KT_TrackerConnectionString"].ConnectionString;
        public string Conn_BreakEven
        {
            get
            {
                return _ConnBEValue;
            }
            set
            {
                _ConnBEValue = value;
            }
        }

        public string Conn_KTTrackerRetail
        {
            get
            {
                return _ConnKTTrackerRetail;
            }
            set
            {
                _ConnKTTrackerRetail = value;
            }
        }

        //Stored Procedure
        public string tblReceivingDetails = "sp_GetReceivingDetailsCustomFields";
        public string tblSalesDetail = "sp_GetSalesDetailCustomFields";
        public string tblProductCatalog = "sp_GetProductCatalogCustomFields";
        public string ProductSearch = "KT_ProductSearch";
        public string BreakEvenProductSearch = "KT_ProductSearch";
        public string GetLastFinalPurchaseCost = "sp_GetLastFinalPurchaseCost";
        public string GetDistinctVendorName = "GetDistinctVendorName";
        public string GetDistinctSKU = "GetDistinctSKU";
        public string GetDistinctDescription = "GetDistinctDescription";
        public string KT_GET_UPCSKU_BY_DESC = "KT_GET_UPCSKU_BY_DESC";
        public string KT_GetBreakEvenValues = "KT_GetBreakEvenValues";
        public string pr_GetBreakEvenSS = "pr_GetBreakEvenSS";
        public string pr_GetAllSalesDetail = "pr_GetAllSalesDetail";
        public string pr_GetAllPODetail = "pr_GetAllPODetail";
        public string pr_GetSalesTrend = "pr_GetSalesTrend";
        public string pr_GetUpdateTime = "pr_GetUpdateTime";
        public string pr_HomeScreen = "pr_HomeScreen";
        public string pr_DailyStoreDetails = "pr_DailyStoreDetails";
        public string pr_Daysummaryallstores = "pr_Daysummaryallstores";
    }
}
