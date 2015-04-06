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
        AUBusinessAccess.JarvisBusinessLr jblr = new JarvisBusinessLr();
        AUBusinessAccess.EmailFunctions email = new EmailFunctions();
        AUBusinessAccess.EPMBusinessLr epmlr = new EPMBusinessLr();

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
                SalesReport(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate),StoreNumber, EmailListNo);
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
                CreateEMailString(Convert.ToDateTime(fromDate), Convert.ToDateTime(toDate), EmailListNo);
                return "Mail Sent";
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }

        private string SalesReport(DateTime FromDate, DateTime ToDate,string StoreNumber, string EmailListNo)
        {
            string strreadFile = readFile("~/" + "SalesReport.html");

            DataTable v1 = epmlr.SalesTransationsGroupedByItem(FromDate, ToDate,Convert.ToInt16(StoreNumber));
            string strIncSales;
            DataTable  dtstr10Sales;
            
            try
            {
                dtstr10Sales =
                    v1;
                dtstr10Sales = SalesColumnDelete(dtstr10Sales);
                string Sales = v1.AsEnumerable().Sum(row => row.Field<decimal>("TotalDollars")).ToString("#.##");
                strreadFile = strreadFile.Replace("Store10Sales", email.TableCreator(dtstr10Sales));
                strreadFile = strreadFile.Replace("upc", "UPC");
                strreadFile = strreadFile.Replace("Qty", "Quantity");
                strreadFile = strreadFile.Replace("AvgPrice", "Retail($)");
                strreadFile = strreadFile.Replace("TotalDollars", "Total($)");
                strreadFile = strreadFile.Replace("INCSales", Sales);
            }
            catch (Exception ex)
            {
                strreadFile = strreadFile.Replace("Store10Sales", "No Sales");
            }

           


            strreadFile = strreadFile.Replace("FDate", String.Format("{0:M/d/yyyy HH:mm:ss}", FromDate));
            strreadFile = strreadFile.Replace("TDate", String.Format("{0:M/d/yyyy HH:mm:ss}", ToDate));

            DataTable dt =
                jblr.atm_GetEmailList()
                    .AsEnumerable()
                    .Where(row => row["Sno"].ToString() == EmailListNo)
                    .CopyToDataTable();


            if (dt.Rows.Count > 0)
            {
                string result = SendAlert(dt.Rows[0]["EmailTo"].ToString(), dt.Rows[0]["EmailSubject"].ToString(), strreadFile);
                jblr.LogEmail(Convert.ToInt16(EmailListNo), dt.Rows[0]["EmailTo"].ToString(),
                    dt.Rows[0]["EmailSubject"].ToString(), strreadFile, result);
            }

            ;
            //     = email.TableCreator(v1);
            return strreadFile;
        }

        private string CreateEMailString(DateTime FromDate, DateTime ToDate,string EmailListNo)
        {


            string strreadFile = readFile("~/" + "EmailTshirtReplenishment.html");

            DataTable v1 = jblr.TshirtSalesandQOH(FromDate, ToDate);
            string str6Sales, strRodewaySales, strQualitySales, strIncSales;
            DataTable dtstr6Sales, dtstr10Sales, dtstr21Sales, dtstr22Sales;
            try
            {
                dtstr6Sales =
                    v1.AsEnumerable()
                        .Where(row => Convert.ToInt16(row["GiftShopSales"].ToString()) > 0)
                        .CopyToDataTable();
                dtstr6Sales = deleteColumn(dtstr6Sales, "GiftshopSales");
                strreadFile = strreadFile.Replace("Store6Sales", email.TableCreator(dtstr6Sales));
                strreadFile = strreadFile.Replace("upc", "UPC");
                strreadFile = strreadFile.Replace("GiftshopSales", "Sales");
            }
            catch (Exception ex)
            {
                strreadFile = strreadFile.Replace("Store6Sales", "No Sales");
            }

            try
            {
                dtstr10Sales =
                    v1.AsEnumerable()
                        .Where(row => Convert.ToInt16(row["SalesINC"].ToString()) > 0)
                        .CopyToDataTable();
                dtstr10Sales = deleteColumn(dtstr10Sales, "SalesINC");

                strreadFile = strreadFile.Replace("Store10Sales", email.TableCreator(dtstr10Sales));
                strreadFile = strreadFile.Replace("upc", "UPC");
                strreadFile = strreadFile.Replace("SalesINC", "Sales");
            }
            catch (Exception ex)
            {
                strreadFile = strreadFile.Replace("Store10Sales", "No Sales");
            }

            try
            {
                dtstr21Sales =
                    v1.AsEnumerable()
                        .Where(row => Convert.ToInt16(row["SalesQuality"].ToString()) > 0)
                        .CopyToDataTable();
                dtstr21Sales = deleteColumn(dtstr21Sales, "SalesQuality");

                strreadFile = strreadFile.Replace("Store21Sales", email.TableCreator(dtstr21Sales));
                strreadFile = strreadFile.Replace("upc", "UPC");
                strreadFile = strreadFile.Replace("SalesQuality", "Sales");
            }
            catch (Exception ex)
            {
                strreadFile = strreadFile.Replace("Store21Sales", "No Sales");
            }

            try
            {
                dtstr22Sales =
                    v1.AsEnumerable()
                        .Where(row => Convert.ToInt16(row["SalesRodeway"].ToString()) > 0)
                        .CopyToDataTable();
                dtstr22Sales = deleteColumn(dtstr22Sales, "SalesRodeway");
                strreadFile = strreadFile.Replace("Store22Sales", email.TableCreator(dtstr22Sales));
                strreadFile = strreadFile.Replace("upc", "UPC");
                strreadFile = strreadFile.Replace("SalesRodeway", "Sales");
            }
            catch (Exception ex)
            {
                strreadFile = strreadFile.Replace("Store22Sales", "No Sales");
            }


            strreadFile = strreadFile.Replace("FDate", String.Format("{0:M/d/yyyy HH:mm:ss}", FromDate));
            strreadFile = strreadFile.Replace("TDate", String.Format("{0:M/d/yyyy HH:mm:ss}", ToDate));

            DataTable dt =
                jblr.atm_GetEmailList()
                    .AsEnumerable()
                    .Where(row => row["Sno"].ToString() == EmailListNo)
                    .CopyToDataTable();


            if (dt.Rows.Count > 0)
            {
               string result= SendAlert(dt.Rows[0]["EmailTo"].ToString(), dt.Rows[0]["EmailSubject"].ToString(), strreadFile);
                jblr.LogEmail(Convert.ToInt16(EmailListNo), dt.Rows[0]["EmailTo"].ToString(),
                    dt.Rows[0]["EmailSubject"].ToString(), strreadFile,result);
            }
           
            ;
            //     = email.TableCreator(v1);
            return strreadFile;
        }

        public string SendAlert(string strTo, string strSubject, string strMessage)
        {
            if (strTo.Trim() != "")
            {
                try
                {

                    SmtpClient client = new SmtpClient();
                    client.Port = 587;
                    client.Host = "smtp.office365.com";
                    client.EnableSsl = true;
                    client.Timeout = 300000;
                    client.ServicePoint.ConnectionLeaseTimeout = 300000;
                    client.ServicePoint.MaxIdleTime = 300000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["EmailID"], WebConfigurationManager.AppSettings["EmailPass"]);
                    MailMessage mm = new MailMessage(WebConfigurationManager.AppSettings["EmailID"], strTo, strSubject, strMessage);
                    mm.BodyEncoding = UTF8Encoding.UTF8;
                    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    mm.IsBodyHtml = true;
                    ServicePointManager.ServerCertificateValidationCallback =
    delegate(object s, X509Certificate certificate,
      X509Chain chain, SslPolicyErrors sslPolicyErrors)
    { return true; };

                    client.Send(mm);
                    
                    System.Threading.Thread.Sleep(5000);
                        client.Dispose();
                    return "yes";
                }

                catch (Exception ex)
                {
                    return ex.Message.ToString() + ex.StackTrace;
                }
            }
            else
            {
                return "No Active To Address";
            }

        }
        public string readFile(string FilePath)
        {
            string path = HttpContext.Current.Server.MapPath(FilePath);
            return System.IO.File.ReadAllText(path);
        }
        public DataTable deleteColumn(DataTable frTable, string ColumnName)
        {
            DataTable dt = frTable;
            ArrayList arl = new ArrayList();

            foreach (DataColumn dc in dt.Columns)
            {
                if (!(dc.ColumnName == "upc" || dc.ColumnName == "Desc" || dc.ColumnName == ColumnName))
                {
                    arl.Add(dc.ColumnName);
                }

            }

            foreach (string s in arl)
            {
                dt.Columns.Remove(s);
            }
            return dt;
        }


        public DataTable SalesColumnDelete(DataTable frTable)
        {
            DataTable dt = frTable;
            ArrayList arl = new ArrayList();

            foreach (DataColumn dc in dt.Columns)
            {
                if (!(dc.ColumnName == "AvgPrice" || dc.ColumnName == "TotalDollars" || dc.ColumnName == "Description" || dc.ColumnName == "Qty"))
                {
                    arl.Add(dc.ColumnName);
                }

            }

            foreach (string s in arl)
            {
                dt.Columns.Remove(s);
            }
            return dt;
        }
    }
}
