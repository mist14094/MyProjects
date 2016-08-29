using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    public class ConstantsLayer
    {
        public static string DefaultString = ConfigurationManager.ConnectionStrings["DefaultString"].ConnectionString;

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

      //  public string EMailId = ConfigurationManager.AppSettings["EmailID"].ToString();

        public const string ImportSalesData = "ImportSalesData";
        public string MainDirectory = ConfigurationManager.AppSettings["MainDirectory"].ToString();
        public string GoToUrl = ConfigurationManager.AppSettings["GoToUrl"].ToString();
        public string StoreNameField = ConfigurationManager.AppSettings["storeNameField"].ToString();
        public string UserPasswordField = ConfigurationManager.AppSettings["userPasswordField"].ToString();
        public string UserNameField = ConfigurationManager.AppSettings["userNameField"].ToString();
        public int FromData = int.Parse(ConfigurationManager.AppSettings["FromData"].ToString());
        public int ThreadSleepTime = int.Parse(ConfigurationManager.AppSettings["ThreadSleepTime"].ToString());



    }
}
