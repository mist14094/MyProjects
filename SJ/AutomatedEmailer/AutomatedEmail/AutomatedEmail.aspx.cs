using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AUBusinessAccess;

namespace AutomatedEmail
{
    public partial class AutomatedEmail : System.Web.UI.Page
    {
        AUBusinessAccess.JarvisBusinessLr jblr = new JarvisBusinessLr();
        AUBusinessAccess.EmailFunctions email = new EmailFunctions();
        protected void Page_Load(object sender, EventArgs e)
        {
           // Response.Write(CreateEMailString(DateTime.Now.AddDays(-3), DateTime.Now));
        }

        private string CreateEMailString(  DateTime FromDate , DateTime ToDate)
        {


            string strreadFile = readFile("/" + "EmailTshirtReplenishment.html");

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


            strreadFile = strreadFile.Replace("FDate", String.Format("{0:d/M/yyyy HH:mm:ss}", FromDate));
            strreadFile = strreadFile.Replace("TDate", String.Format("{0:d/M/yyyy HH:mm:ss}", ToDate));


            ;
            //     = email.TableCreator(v1);
            return strreadFile;
        }

        public DataTable deleteColumn(DataTable frTable,string ColumnName)
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
        public string readFile(string FilePath)
        {
            string path = HttpContext.Current.Server.MapPath(FilePath);
            return System.IO.File.ReadAllText(path);
        }
    }
}
