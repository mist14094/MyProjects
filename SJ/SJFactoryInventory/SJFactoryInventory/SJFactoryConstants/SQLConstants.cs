using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace SJFactoryConstants
{
    
    public class SQLConstants
    {
        public static string DefaultString = ConfigurationManager.ConnectionStrings["SJFactoryInventoryControl"].ConnectionString;
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

        public string CheckLogin = "CheckLogin";
        public string InsertUserLog = "InsertUserLog";
        public string InsertInternalLog = "InsertInternalLog";
        public string GetProducts = "SELECT *  FROM [SJFactoryInventoryControl].[dbo].[Products]";
        public string GetLocations = "SELECT *  FROM [SJFactoryInventoryControl].[dbo].[Location]";
        public string CreateNewLabel = "CreateNewLabel";
        public string GetAllToteLabels = "GetAllToteLabels";
        public string GetToteLabelsDetails = "GetToteLabelsDetails";

    }
}
