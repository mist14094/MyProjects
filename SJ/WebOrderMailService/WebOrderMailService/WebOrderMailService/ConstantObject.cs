using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebOrderMailService
{
    public class ConstantObject
    {

        public string RFIDString = ConfigurationManager.ConnectionStrings["TrackerConnectionString"].ConnectionString;
        public string EMailID = ConfigurationManager.AppSettings["EmailID"].ToString();
        public string EMailPass = ConfigurationManager.AppSettings["EmailPass"].ToString();
        public string strCcAddress = ConfigurationManager.AppSettings["ccAddress"].ToString();
        public string strBccAddress = ConfigurationManager.AppSettings["BccAddress"].ToString();
        public string strSubject = ConfigurationManager.AppSettings["Subject"].ToString();
        public string strMailType = ConfigurationManager.AppSettings["MailType"].ToString();
        public string strURL = ConfigurationManager.AppSettings["URL"].ToString();
        public string strImageURL = ConfigurationManager.AppSettings["ImageURL"].ToString();
        public string strNoStockExistMail = ConfigurationManager.AppSettings["NoStockExistMail"].ToString();
        public string strNoStockDefnMail = ConfigurationManager.AppSettings["NoStockDefnMail"].ToString();
        public string EmailTempl = ConfigurationManager.AppSettings["EmailTempl"].ToString();
        public string GetAllItemsToBeMailed =
           "SELECT [MailItemID],increment_id AS order_id ,[item_id] ,[created_at] ,[product_id] ,[qty_ordered] ,[SKU] ,[Name] ,[Price] ,[Desc] ,[Short Desc] ,[URL] ,[Image]  ,[MailSentFlag] ,[MailSentTime],MailItems.ImportedTime , ShippingMethod FROM [SJWebOrders].[dbo].[SJ_MailItems] MailItems INNER JOIN [SJWebOrders].[dbo].[sj_MailMaster] MailMaster ON MailItems.order_id = MailMaster.EntityID WHERE MailSentFlag = 0";
        public string GetInventoryOnHand =
            "SELECT sku as SKU, Stores.StoreID as [Store ID],Stores.KT_StoreName as [Store Name],COUNT(*) AS QOH FROM TrackerRetail.dbo.ProductItems " +
            "LEFT OUTER JOIN TrackerRetail.dbo.vwLocations ON TrackerRetail.dbo.vwLocations.ZoneID = TrackerRetail.dbo.ProductItems.ZoneID " +
            "LEFT OUTER JOIN TrackerRetail.dbo.Stores ON TrackerRetail.dbo.Stores.StoreID = TrackerRetail.dbo.vwLocations.StoreID " +
            "WHERE sku='{0}' AND RFID IS NOT NULL AND Status <> 'Hold' " +
            "GROUP BY SKU, Stores.StoreID,Stores.KT_StoreName " +
            "ORDER BY QOH DESC, Stores.StoreID ";
        public string UpdateStoreQoh =
            "UPDATE  [SJWEBORDERS].[DBO].[SJ_MAILITEMS] SET MailLogID = {0},MailSentFlag=1 where MailItemID = {1}";
        public string GetUserNameForStoreId=
            "select sp +','  from (SELECT ( EmailID) as 'sp', VALUE AS 'STORE',SNO   FROM [SJWebOrders].[dbo].[SJ_MailDirectory]  CROSS APPLY DBO.FN_SPLIT(STOREID,',') WHERE IsActive = 1 and VALUE = '{0}') a where a.sp = a.sp for xml path ('')";
        public string SjMailAlertLog = "SJWebOrders.[dbo].[InsertMailLog]";

    }
}