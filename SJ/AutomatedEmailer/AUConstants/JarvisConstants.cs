using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AUConstants
{
    public class JarvisConstants
    {
        public static string DefaultString = ConfigurationManager.ConnectionStrings["Jarvis"].ConnectionString;
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

        public string atm_NativeStichesTshirtSale = "atm_NativeStichesTshirtSale";

        public string atm_GetEmailList =
            "SELECT [Sno]      ,[EmailName]      ,[EmailSubject]      ,[EmailTo]      ,[IsValid]  FROM [Jarvis].[dbo].[atm_EmailList]";

        public string LogEmail = "atm_SP_EmailLog";

    }


}
