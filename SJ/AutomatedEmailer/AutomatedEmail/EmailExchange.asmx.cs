using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using AUBusinessAccess;

namespace AutomatedEmail
{

    /// <summary>
    /// Summary description for EmailExchange
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EmailExchange : System.Web.Services.WebService
    {
      AUBusinessAccess.JarvisBusinessLr Lr = new JarvisBusinessLr();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }


        [WebMethod]
        public string GetStoresSalesReport(string fromDate, string toDate, string StoreNumber,string EmailListNo)
        {
            try
            {
                 Lr.SalesReport(readFile("~/" + "SalesReport.html"),Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), StoreNumber, EmailListNo);
                return "Mail Sent";
            }
            catch (Exception)
            {

                throw;
            }
        }


        [WebMethod]
        public string GetStoresSalesReportWithImage(string fromDate, string toDate, string StoreNumber, string EmailListNo)
        {
            try
            {
                Lr.SalesReportWithImage(readFile("~/" + "SalesReport.html"), Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), StoreNumber, EmailListNo);
                return "Mail Sent";
            }
            catch (Exception)
            {

                throw;
            }
        }

        [WebMethod]
        public string EmailNSTShirtReplenishment(string fromDate, string toDate,string EmailListNo)
        {
            try
            {
                Lr.CreateEMailString(readFile("~/" + "EmailTshirtReplenishment.html"), Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), EmailListNo);
                return "Mail Sent";
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        [WebMethod]
        public string CostcoSaleseport(string fromDate, string toDate, string storeNumber,string EmailListNo)
        {
            try
            {
                Lr.CostcoSaleseport(readFile("~/" + "CostoSales.html"), Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), storeNumber, EmailListNo);
                return "Mail Sent";
            }
            catch (Exception)
            {

                throw;
            }

        }

        public string readFile(string FilePath)
        {
            string path = HttpContext.Current.Server.MapPath(FilePath);
            return System.IO.File.ReadAllText(path);
        }

      
    }
}
