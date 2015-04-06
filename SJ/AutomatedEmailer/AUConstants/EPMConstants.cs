using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AUConstants
{
    public class EPMConstants
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

    }


}
