using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;
using NLog;
using System.IO;
using System.Web.Configuration;

namespace WebOrderMailService
{
    public class BusinessObject
    {
        internal Logger Nlog = LogManager.GetCurrentClassLogger();
        private ConstantObject _constantObject = new ConstantObject();
        private DataObject _dataObject = new DataObject();

        public DataTable GetAllItemsToBeMailed()
        {
            return _dataObject.GetAllItemsToBeMailed();
        }

        public DataTable GetInventoryOnHand(string sku)
        {
            return _dataObject.GetInventoryOnHand(sku);
        }

        public DataTable UpdateMailID(string Mailid, string mailItemId)
        {
            return _dataObject.UpdateMailID(Mailid, mailItemId);
        }

        public DataTable GetUserNameForStoreId(string storeid)
        {
            return _dataObject.GetUserNameForStoreId(storeid);
        }

        public DataTable SjMailAlertLog(string toAddress, string ccAddress, string bccAddress, string subject,
            string body, string mailType, string Notes)
        {
            return _dataObject.SjMailAlertLog(toAddress, ccAddress, bccAddress, subject, body, mailType,Notes);
        }


        public void SendEmail()
        {
            var itemsToBeEmailed = GetAllItemsToBeMailed();

            foreach (DataRow lineItems in itemsToBeEmailed.Rows)
            {
                if (lineItems["SKU"].ToString() == "")
                {
                   NoSkuSendEmail(lineItems);
                }

                else
                {
                    DataTable dtQoh = GetInventoryOnHand(lineItems["SKU"].ToString());
                    if (dtQoh.Rows.Count > 0)
                    {

                        StockExistSendEmail(lineItems, dtQoh);
                    }
                    else
                    {
                        NoStockExistSendEmail(lineItems);
                    }
                }

                if((lineItems["ShippingMethod"]).ToString()==WebConfigurationManager.AppSettings["StorePickup"].ToString())
                {
                    SendTextMessage(lineItems);
                }
            }
        }



        public string SendTextMessage(DataRow lineItems)
        {

            var strHtml = WebConfigurationManager.AppSettings["TextMessageBody"].ToString();
            try
            {
                var toAddress = WebConfigurationManager.AppSettings["TextToAddress"].ToString();
                var emaillog = SjMailAlertLog(toAddress, "", "",
                    "Store Pickup", strHtml, _constantObject.strMailType, "TextMessage");

                if (emaillog.Rows.Count > 0)
                {
                    UpdateMailID(emaillog.Rows[0][0].ToString(), lineItems["MailItemID"].ToString());
                }
            }
            catch (Exception)
            {

            }
            return strHtml;
        }
        public string NoSkuSendEmail(DataRow lineItems)
        {
            // Response.Write("No SKU" + lineItems["SKU"].ToString() +" - " + lineItems["Name"].ToString() + "<br/>" + Environment.NewLine);
            var strHtml = System.IO.File.ReadAllText(_constantObject.EmailTempl);
            strHtml = strHtml.Replace("prodname", lineItems["Name"].ToString());
            strHtml = strHtml.Replace("tordered", lineItems["qty_Ordered"].ToString());
            strHtml = strHtml.Replace("orderid", lineItems["order_id"].ToString());
            strHtml = strHtml.Replace("productid", lineItems["product_id"].ToString());
            strHtml = strHtml.Replace("createdate", lineItems["created_at"].ToString());
            strHtml = strHtml.Replace("skunumber", lineItems["SKU"].ToString());
            strHtml = strHtml.Replace("desclong", lineItems["desc"].ToString() + "<br/>" + lineItems["Short Desc"].ToString());
            strHtml = strHtml.Replace("urllink", _constantObject.strURL + lineItems["url"].ToString());
            strHtml = strHtml.Replace("imageurl", "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/No_image_available.svg/300px-No_image_available.svg.png");
            strHtml = strHtml.Replace("QOHTABLE", "No Stock!");

            try
            {
                var toAddress = _constantObject.strNoStockDefnMail;
                var emaillog = SjMailAlertLog(toAddress, _constantObject.strCcAddress, _constantObject.strBccAddress,
                    _constantObject.strSubject, strHtml, _constantObject.strMailType, "NoSkuSendEmail");
                if (emaillog.Rows.Count > 0)
                {
                    UpdateMailID(emaillog.Rows[0][0].ToString(), lineItems["MailItemID"].ToString());
                }
            }
            catch (Exception)
            {

            }

            return strHtml;
        }

        public string StockExistSendEmail(DataRow lineItems, DataTable qoh)
        {
            // Response.Write("Stock Exist - " + lineItems["SKU"].ToString() + " - " + lineItems["Name"].ToString() + "<br/>" + Environment.NewLine);


            var strHtml = System.IO.File.ReadAllText(_constantObject.EmailTempl);
            strHtml = strHtml.Replace("prodname", lineItems["Name"].ToString());
            strHtml = strHtml.Replace("tordered", lineItems["qty_Ordered"].ToString());
            strHtml = strHtml.Replace("orderid", lineItems["order_id"].ToString());
            strHtml = strHtml.Replace("productid", lineItems["product_id"].ToString());
            strHtml = strHtml.Replace("createdate", lineItems["created_at"].ToString());
            strHtml = strHtml.Replace("skunumber", lineItems["SKU"].ToString());
            strHtml = strHtml.Replace("desclong", lineItems["desc"].ToString() + "<br/>" + lineItems["Short Desc"].ToString());
            strHtml = strHtml.Replace("urllink", _constantObject.strURL + lineItems["url"].ToString());
            strHtml = strHtml.Replace("imageurl", _constantObject.strImageURL + lineItems["image"].ToString());
            strHtml = strHtml.Replace("QOHTABLE", ConvertDataTableToHtml(qoh));

            try
            {
                string storeid = qoh.Select("QOH=max(QOH)")[0]["Store ID"].ToString();
                var toAddress = GetUserNameForStoreId(storeid);
                var emaillog = SjMailAlertLog(toAddress.Rows[0][0].ToString(), _constantObject.strCcAddress, _constantObject.strBccAddress,
                    _constantObject.strSubject, strHtml, _constantObject.strMailType, "StockExistSendEmail");

                if (emaillog.Rows.Count > 0)
                {
                    UpdateMailID(emaillog.Rows[0][0].ToString(), lineItems["MailItemID"].ToString());
                }
            }
            catch (Exception)
            {

            }
            return strHtml;
        }
        public string NoStockExistSendEmail(DataRow lineItems)
        {
            // Response.Write("No Stock Exist - " + lineItems["SKU"].ToString() + " - " + lineItems["Name"].ToString() + "<br/>" + Environment.NewLine);
           ;
            var strHtml = System.IO.File.ReadAllText(_constantObject.EmailTempl);
            strHtml = strHtml.Replace("prodname", lineItems["Name"].ToString());
            strHtml = strHtml.Replace("tordered", lineItems["qty_Ordered"].ToString());
            strHtml = strHtml.Replace("orderid", lineItems["order_id"].ToString());
            strHtml = strHtml.Replace("productid", lineItems["product_id"].ToString());
            strHtml = strHtml.Replace("createdate", lineItems["created_at"].ToString());
            strHtml = strHtml.Replace("skunumber", lineItems["SKU"].ToString());
            strHtml = strHtml.Replace("desclong", lineItems["desc"].ToString() + "<br/>" + lineItems["Short Desc"].ToString());
            strHtml = strHtml.Replace("urllink", _constantObject.strURL + lineItems["url"].ToString());
            strHtml = strHtml.Replace("imageurl", _constantObject.strImageURL + lineItems["image"].ToString());
            strHtml = strHtml.Replace("QOHTABLE", "No Stock!");

            try
            {

                var toAddress = _constantObject.strNoStockExistMail;
                var emaillog = SjMailAlertLog(toAddress, _constantObject.strCcAddress, _constantObject.strBccAddress,
                    _constantObject.strSubject, strHtml, _constantObject.strMailType, "NoStockExistSendEmail");
                if (emaillog.Rows.Count > 0)
                {
                    UpdateMailID(emaillog.Rows[0][0].ToString(), lineItems["MailItemID"].ToString());
                }
            }
            catch (Exception)
            {

            }


            return strHtml;
        }

        public static string ConvertDataTableToHtml(DataTable dt)
        {
            string html = "<table>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

    }
}