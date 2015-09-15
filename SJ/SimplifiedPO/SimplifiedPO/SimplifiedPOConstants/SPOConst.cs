using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

        public string SalesTransationsGroupedByItem = "SalesTransationsGroupedByItem";
        public string StoreSalesGroupedByItem = "[EPM].[EPM_DW].[dbo].[SalesTransationsGroupedByItem]";
        public string CostcoSalesReport = "CostcoSalesMvmt";
    }
}
