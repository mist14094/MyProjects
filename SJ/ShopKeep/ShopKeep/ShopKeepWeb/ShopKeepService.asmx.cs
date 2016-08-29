using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using BusinessLayer;
using OpenQA.Selenium.Chrome;


namespace ShopKeepWeb
{
    /// <summary>
    /// Summary description for ShopKeepService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ShopKeepService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public void ReadFile()
        {
            BusinessLayer.TransactionBL bl= new TransactionBL();
            global::Constants.ConstantsLayer constants = new global::Constants.ConstantsLayer();
            try
            {
                int fromDate =constants.FromData;
                int threadSleepTime = constants.ThreadSleepTime;
                string mainDirectory = constants.MainDirectory;
                string subfolder = DateTime.Now.ToString("yyyyMMddhhmmss");
                string fullPath = mainDirectory + subfolder;
                bool exists = System.IO.Directory.Exists(fullPath);
                if (!exists)
                { System.IO.Directory.CreateDirectory(fullPath); }
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddUserProfilePreference("download.default_directory", fullPath);
               // chromeOptions.
                using (var driver = new ChromeDriver(chromeOptions))
                {
                    driver.Navigate().GoToUrl(constants.GoToUrl);
                    var storeNameField = driver.FindElementById("store_name");
                    var userNameField = driver.FindElementById("login");
                    var userPasswordField = driver.FindElementById("password");
                    var loginButton = driver.FindElementByXPath("//input[@value='Log in']");
                    storeNameField.SendKeys(constants.StoreNameField);
                    userPasswordField.SendKeys(constants.UserPasswordField);
                    userNameField.SendKeys(constants.UserNameField);
                    loginButton.Click();
                    driver.Url =
                    "https://shopkeepapp.com/transactions.csv?start_time=" + DateTime.Now.AddDays(-1 * fromDate).ToString("MMM d yyyy ", CultureInfo.InvariantCulture) + "12:00:00.000%20AM&end_time=" + DateTime.Now.ToString("MMM d yyyy", CultureInfo.InvariantCulture) + "%2011:59:59.999%20PM&&detailed=true&tenders=false";
                    driver.Navigate();
                    Thread.Sleep(threadSleepTime);
                }
                string[] fileEntries = Directory.GetFiles(fullPath);
                foreach (string fileName in fileEntries)
                {
                    File.Move(fileName, fileName.Replace("-", ""));
                    OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + Path.GetDirectoryName(fileName.Replace("-", "")) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"");
                    conn.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM " + Path.GetFileName(fileName.Replace("-", "")).Replace("-", ""), conn);
                    DataSet ds = new DataSet("Temp");
                    adapter.Fill(ds);
                    conn.Close();
                    try
                    {
                        bl.InsertEachData(bl.ConvertTransactionFromDataset(ds));
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                   

                }

              
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

     

    }
}
