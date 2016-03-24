using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace LotControlConstants
{
    public class LcConstants
    {
        public static string DefaultString = ConfigurationManager.ConnectionStrings["SysproCompanyA"].ConnectionString;
        public static string FactoryLotControl = ConfigurationManager.ConnectionStrings["FactoryLotControl"].ConnectionString;

        public string FactoryLotControlString
        {
            get
            {
                return FactoryLotControl;
            }
            set
            {
                FactoryLotControl = value;
            }
        }
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
        public string EMailId = ConfigurationManager.AppSettings["EmailID"].ToString();
        public string GetGrnValues = "SELECT * FROM GrnDetails WHERE Grn={0} ORDER BY  Grn ";
        public string GetLotDetails = "SELECT [LotTransactions].PurchaseOrderLin,[LotTransactions].StockCode,[LotTransactions].Warehouse,[LotTransactions].LotJob,[LotTransactions].TrnDate,[LotTransactions].Reference,[LotTransactions].JobPurchOrder,[LotTransactions].Supplier,GrnDetails.StockDescription,GrnDetails.QtyReceived,QtyUom,OrigGrnValue FROM    [SysproCompanyA].[dbo].[LotTransactions] LEFT OUTER JOIN SysproCompanyA..GrnDetails   ON SysproCompanyA.dbo.GrnDetails.PurchaseOrder = SysproCompanyA.dbo.LotTransactions.JobPurchOrder and SysproCompanyA.dbo.GrnDetails.StockCode = SysproCompanyA.dbo.LotTransactions.StockCode  where  JobPurchOrder='{0}' order by LotTransactions.PurchaseOrderLin";
        public string ImportItemsinPO = "ImportItemsinPO";
        public string GetBarcodeDetails = "SELECT *   FROM [FactoryLotControl].[dbo].[LotDetails] WHERE Sno={0}";
        public string InsertLabelPrintLog = "InsertLabelPrintLog";
        public string UpdateAltUOM = "UPDATE dbo.LotDetails SET AltUOM={0} WHERE Sno={1}";
        public string updateFinalized = "  UPDATE dbo.LotDetails SET [IsFinalized]=1 WHERE Sno={0}";
        public string GetLabelForPO = "GetLabelForPO";
    }
}
