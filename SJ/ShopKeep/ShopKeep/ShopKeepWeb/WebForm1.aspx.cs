using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenQA.Selenium.Chrome;

namespace ShopKeepWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ReadFile();
           
            
        }

        public void ReadFile()
        {
            try
            {
                Response.Write("Main1");
                string MainDirectory = @"C:\Users\vramalingam\Desktop\Download\";
                string subfolder = DateTime.Now.ToString("yyyyMMddhhmmss");
                string FullPath = MainDirectory + subfolder;
                bool exists = System.IO.Directory.Exists(FullPath);
                if (!exists)
                { System.IO.Directory.CreateDirectory(FullPath); }
                Response.Write("Chrome1");
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddUserProfilePreference("download.default_directory", FullPath);
               

                    
                using (var driver = new ChromeDriver(chromeOptions))
                {
                    driver.Navigate().GoToUrl("https://shopkeepapp.com/login");
                    var storeNameField = driver.FindElementById("store_name");
                    var userNameField = driver.FindElementById("login");
                    var userPasswordField = driver.FindElementById("password");
                    var loginButton = driver.FindElementByXPath("//input[@value='Log in']");
                    storeNameField.SendKeys("niagarafallsadventurepark");
                    userPasswordField.SendKeys("Smokin2293!");
                    userNameField.SendKeys("Athomas@smokinjoe.com");
                    loginButton.Click();
                    driver.Url =
                    "https://shopkeepapp.com/transactions.csv?start_time=" + DateTime.Now.AddDays(-30).ToString("MMM+d+yyyy", CultureInfo.InvariantCulture) + "12:00:00.000%20AM&end_time=" + DateTime.Now.ToString("MMM+d+yyyy", CultureInfo.InvariantCulture) + "%2011:59:59.999%20PM&&detailed=true&tenders=false";
                    driver.Navigate();
                    Thread.Sleep(10000);
                }
                string[] fileEntries = Directory.GetFiles(FullPath);
                foreach (string fileName in fileEntries)
                {
                    File.Move(fileName, fileName.Replace("-", ""));
                    OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + Path.GetDirectoryName(fileName.Replace("-", "")) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"");
                    conn.Open();
                    OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM " + Path.GetFileName(fileName.Replace("-", "")).Replace("-", ""), conn);
                    DataSet ds = new DataSet("Temp");
                    adapter.Fill(ds);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                
            }

        }

        public void ReadCSV()
        {
            string MainDirectory = @"C:\Users\vramalingam\Desktop\Download\";
            MainDirectory = @"C:\Users\vramalingam\Desktop\Download\20160518034826";
            string[] fileEntries = Directory.GetFiles(MainDirectory);
            foreach (string fileName in fileEntries)
            {
                File.Move(fileName, fileName.Replace("-", ""));
                OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + Path.GetDirectoryName(fileName.Replace("-", "")) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"");
                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM " + Path.GetFileName(fileName.Replace("-", "")).Replace("-", ""), conn);
                DataSet ds = new DataSet("Temp");
                adapter.Fill(ds);
             //   var d2 = tsTransaction.ConvertTransactionFromDataset(ds);
                conn.Close();
            }
        }
    }
}