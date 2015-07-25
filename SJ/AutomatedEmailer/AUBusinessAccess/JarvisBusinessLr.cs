using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using AUDataAccess;
using NLog;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;

namespace AUBusinessAccess
{
    public class JarvisBusinessLr
    {

          
        AUBusinessAccess.EmailFunctions email = new EmailFunctions();
        AUBusinessAccess.EPMBusinessLr epmlr = new EPMBusinessLr();
        private readonly Logger _nlog = LogManager.GetCurrentClassLogger();
        private AUDataAccess.JarvisDataLr _access;

        public JarvisBusinessLr()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            _access = new JarvisDataLr();
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Exit");
        }

        public DataTable TshirtSalesandQOH(DateTime startdate, DateTime enddate)
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.TshirtSalesandQOH(startdate, enddate);
           
        } 
        
        public DataTable  atm_GetEmailList()
        {
            _nlog.Trace(message:
                this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
                System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.atm_GetEmailList();
           
        }

        public DataTable LogEmail(int EmailSno, string EmailTO, string EmailSubject, string EmailMessage, string result)
        {
            _nlog.Trace(message:
               this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
               System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.LogEmail(EmailSno,EmailTO, EmailSubject,EmailMessage, result);
        }

        public DataTable GetAllCatagoryImageName()
        {
            _nlog.Trace(message:
              this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
              System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetAllCatagoryImageName();
        }

        
        public string SalesReport(string EmailContent, DateTime FromDate, DateTime ToDate, string StoreNumber, string EmailListNo)
        {
            string strreadFile = EmailContent;

            DataTable v1 = epmlr.SalesTransationsGroupedByItem(FromDate, ToDate, Convert.ToInt16(StoreNumber));
            string strIncSales;
            DataTable dtstr10Sales;

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
                atm_GetEmailList()
                    .AsEnumerable()
                    .Where(row => row["Sno"].ToString() == EmailListNo)
                    .CopyToDataTable();


            if (dt.Rows.Count > 0)
            {
                string result = SendAlert(dt.Rows[0]["EmailTo"].ToString(), dt.Rows[0]["EmailSubject"].ToString(), strreadFile);
                LogEmail(Convert.ToInt16(EmailListNo), dt.Rows[0]["EmailTo"].ToString(),
                    dt.Rows[0]["EmailSubject"].ToString(), strreadFile, result);
            }

            ;
            //     = email.TableCreator(v1);
            return strreadFile;
        }
        public string SalesReportWithImage(string EmailContent,DateTime FromDate, DateTime ToDate, string StoreNumber, string EmailListNo)
        {
            string strreadFile = EmailContent;
            DataTable dtImage = new DataTable();
            DataTable catagoryWithImage = GetAllCatagoryImageName();


            DataTable v1 = epmlr.SalesTransationsGroupedByItem(FromDate, ToDate, Convert.ToInt16(StoreNumber));

            v1.Columns.Add("Image");
            foreach (DataRow imageRow in v1.Rows)
            {
                var imageName = catagoryWithImage.Select("Barcode = '" + imageRow["UPC"].ToString() + "'");
                if (imageName.Any())
                {
                    imageRow["Image"] = " <img src=\"http://jarvis.smokinjoe.com/images/" + imageName[0]["Family"] + ".jpg" + "\" style=\"width:42px;height:42px;border:0\">";
                }
            }
            string strIncSales;
            DataTable dtstr10Sales;

            try
            {
                dtstr10Sales =
                    v1;
                dtstr10Sales = SalesColumnDeleteWithImage(dtstr10Sales);
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
                atm_GetEmailList()
                    .AsEnumerable()
                    .Where(row => row["Sno"].ToString() == EmailListNo)
                    .CopyToDataTable();


            if (dt.Rows.Count > 0)
            {
                        string result = SendAlert(dt.Rows[0]["EmailTo"].ToString(), dt.Rows[0]["EmailSubject"].ToString(), strreadFile);
                        LogEmail(Convert.ToInt16(EmailListNo), dt.Rows[0]["EmailTo"].ToString(),
                          dt.Rows[0]["EmailSubject"].ToString(), strreadFile, result);
            }

            ;
            //     = email.TableCreator(v1);
            return strreadFile;
        }
        public string CreateEMailString(string EmailContent, DateTime FromDate, DateTime ToDate, string EmailListNo)
        {


            string strreadFile = EmailContent;

            DataTable v1 = TshirtSalesandQOH(FromDate, ToDate);
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
                atm_GetEmailList()
                    .AsEnumerable()
                    .Where(row => row["Sno"].ToString() == EmailListNo)
                    .CopyToDataTable();


            if (dt.Rows.Count > 0)
            {
                string result = SendAlert(dt.Rows[0]["EmailTo"].ToString(), dt.Rows[0]["EmailSubject"].ToString(), strreadFile);
                LogEmail(Convert.ToInt16(EmailListNo), dt.Rows[0]["EmailTo"].ToString(),
                    dt.Rows[0]["EmailSubject"].ToString(), strreadFile, result);
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
                    client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["EmailID"], ConfigurationManager.AppSettings["EmailPass"]);
                    MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["EmailID"], strTo, strSubject, strMessage);
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

        public DataTable SalesColumnDeleteWithImage(DataTable frTable)
        {
            DataTable dt = frTable;
            ArrayList arl = new ArrayList();

            foreach (DataColumn dc in dt.Columns)
            {
                if (!(dc.ColumnName == "AvgPrice" || dc.ColumnName == "TotalDollars" || dc.ColumnName == "Description" || dc.ColumnName == "Qty" || dc.ColumnName == "Image"))
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

        public DataTable GetProductImage()
        {
            _nlog.Trace(message:
               this.GetType().Namespace + ":" + MethodBase.GetCurrentMethod().DeclaringType.Name + ":" +
               System.Reflection.MethodBase.GetCurrentMethod().Name + "::Entering");
            return _access.GetProductImage();
        }

        //////////////////////////////////////////////////Costco Report
        public string CostcoSaleseport(string EmailContent, DateTime FromDate, DateTime ToDate, string StoreNumber, string EmailListNo)
        {
            string strreadFile = EmailContent;
            DataTable dtImage = new DataTable();
            DataTable catagoryWithImage = GetAllCatagoryImageName();


            DataTable v1 = epmlr.CostcoSalesReport(FromDate, ToDate, Convert.ToInt16(StoreNumber));
            DataTable dtstr10Sales = GetGroupedSalesCostco(v1);

            string strIncSales;
          

            try
            {


                string Sales = v1.AsEnumerable()
                    .Sum(row => row.Field<decimal>("Item_amt"))
                    .ToString("#.##");
                strreadFile = strreadFile.Replace("INCSales", Sales);
                strreadFile = strreadFile.Replace("Store10Sales", email.TableCreator(dtstr10Sales));

            }
            catch (Exception ex)
            {
                strreadFile = strreadFile.Replace("Store10Sales", "No Sales");
            }




            strreadFile = strreadFile.Replace("FDate", String.Format("{0:M/d/yyyy HH:mm:ss}", FromDate));
            strreadFile = strreadFile.Replace("TDate", String.Format("{0:M/d/yyyy HH:mm:ss}", ToDate));

            DataTable dt =
                atm_GetEmailList()
                    .AsEnumerable()
                    .Where(row => row["Sno"].ToString() == EmailListNo)
                    .CopyToDataTable();


            if (dt.Rows.Count > 0)
            {
         string result = SendAlert(dt.Rows[0]["EmailTo"].ToString(), dt.Rows[0]["EmailSubject"].ToString(), strreadFile);
       LogEmail(Convert.ToInt16(EmailListNo), dt.Rows[0]["EmailTo"].ToString(),
        dt.Rows[0]["EmailSubject"].ToString(), strreadFile, result);
            }

            ;
            //     = email.TableCreator(v1);
            return strreadFile;
        }

        private DataTable GetGroupedSalesCostco(DataTable CostcoSales)
        {
            DataTable dtTable = new DataTable();
            dtTable.Columns.Add("Store Number");
            dtTable.Columns.Add("Sales($)");
            dtTable.Columns.Add("Returns($)");
            dtTable.Columns.Add("Item(s) Sold");
            dtTable.Columns.Add("Item(s) Returned");
            dtTable.Columns.Add("Total($)");

            string[] storeNumber = { "1", "2", "3", "4", "6", "7", "9", "10" };
            foreach (var store in storeNumber)
            {
                try
                {
                        var totalSales = CostcoSales.AsEnumerable()
                            .Where(row => row["Str_nbr"].ToString()==store )
                            .Where(row => decimal.Parse(row["Item_qty"].ToString())> 0 )
                            .Sum(row => row.Field<decimal>("Item_amt"))
                            .ToString("#.##");
                        var totalReturns = CostcoSales.AsEnumerable()
                            .Where(row => row["Str_nbr"].ToString()==store )
                            .Where(row => decimal.Parse(row["Item_qty"].ToString()) < 0)
                            .Sum(row => row.Field<decimal>("Item_amt"))
                            .ToString("#.##");
                        var numberSold = CostcoSales.AsEnumerable()
                            .Where(row => row["Str_nbr"].ToString()==store )
                            .Where(row => decimal.Parse(row["Item_qty"].ToString()) > 0)
                            .Sum(row => row.Field<decimal>("Item_qty"))
                            .ToString("#");
                        var numberReturns = CostcoSales.AsEnumerable()
                            .Where(row => row["Str_nbr"].ToString()==store )
                            .Where(row => decimal.Parse(row["Item_qty"].ToString()) < 0)
                            .Sum(row => row.Field<decimal>("Item_qty"))
                            .ToString("#");
                        var total = CostcoSales.AsEnumerable()
                            .Where(row => row["Str_nbr"].ToString() == store)
                            .Sum(row => row.Field<decimal>("Item_amt"))
                            .ToString("#.##");
                        dtTable.Rows.Add(store.ToString(), totalSales, totalReturns, numberSold, numberReturns, total);
                }
                catch (Exception)
                {
                     dtTable.Rows.Add(store.ToString(), "","","","");
                
                }

            }

            try
            {
                dtTable.Rows.Add("Total",
              CostcoSales.AsEnumerable()
                          .Where(row => decimal.Parse(row["Item_qty"].ToString()) > 0)
                          .Sum(row => row.Field<decimal>("Item_amt"))
                          .ToString("#.##"),
              CostcoSales.AsEnumerable()
                          .Where(row => decimal.Parse(row["Item_qty"].ToString()) < 0)
                          .Sum(row => row.Field<decimal>("Item_amt"))
                          .ToString("#.##"),
              CostcoSales.AsEnumerable()
                          .Where(row => decimal.Parse(row["Item_qty"].ToString()) > 0)
                          .Sum(row => row.Field<decimal>("Item_qty"))
                          .ToString("#"),
              CostcoSales.AsEnumerable()
                          .Where(row => decimal.Parse(row["Item_qty"].ToString()) < 0)
                          .Sum(row => row.Field<decimal>("Item_qty"))
                          .ToString("#"),
              CostcoSales.AsEnumerable()
                          .Sum(row => row.Field<decimal>("Item_amt"))
                          .ToString("#.##")
                          );
            }
            catch (Exception)
            {
                
            }
          
            return dtTable;
        }
    }
}
