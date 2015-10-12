using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SimplifiedPOConstants
{
    public class SPOConst
    {
        public static string DefaultString = ConfigurationManager.ConnectionStrings["EPMConnection"].ConnectionString;
        public string DefaultConnectionString
        {
            get
            {
                return DefaultString;
            }
            set
            {
                DefaultString = value;
            }
        }

        public static string RFIDString = ConfigurationManager.ConnectionStrings["TrackerConnectionString"].ConnectionString;
        public string RFIDConnectionString
        {
            get
            {
                return RFIDString;
            }
            set
            {
                RFIDString = value;
            }
        }
        public static string SJPurchaseOrder = ConfigurationManager.ConnectionStrings["SJPurchaseOrder"].ConnectionString;
        public string SJPurchaseOrderConnectionString
        {
            get
            {
                return SJPurchaseOrder;
            }
            set
            {
                SJPurchaseOrder = value;
            }
        }
        public string SalesTransationsGroupedByItem = "SalesTransationsGroupedByItem";
        public string StoreSalesGroupedByItem = "[EPM].[EPM_DW].[dbo].[SalesTransationsGroupedByItem]";
        public string CostcoSalesReport = "CostcoSalesMvmt";
        public string LoginCheck =
            "SELECT UserID, username, email, Firstname, LastName,needsChange,*  FROM sjsql.[SJIntranet].[dbo].[Intranet_Users] WHERE username='{0}' AND PassHash ='{1}'";

        public string GetAllUser = "[sjsql].[posystem].dbo.GetAllUsers";
        public string GetAllEntities = "SELECT		*	FROM 	[sjsql].[posystem].dbo.Entities ";
        public string GetSuppliers = "[sjsql].[posystem].dbo.GetSuppliers 'SysproCompany{0}'";
        public string GetSupplierAddress = "SJSQL.POSystem.dbo.GetSupplierAddress 'SysproCompany{0}', '{1}'";
        public string InsertTempPo = "InsertTempPO";

        public string GetUnSubmittedPo =
            "SELECT [Sno]      ,[BuyerName]      ,[SupplierName]      ,[CreatedDate]      ,[Notes]  FROM [SJPurchaseOrder].[dbo].[POMasterTemp] WHERE loginuserid={0}";

        public string DeleteTempPo =
            "delete FROM [SJPurchaseOrder].[dbo].[POMasterTemp] WHERE Sno={0}";
    }
}
