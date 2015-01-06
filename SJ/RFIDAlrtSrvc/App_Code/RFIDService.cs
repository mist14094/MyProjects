using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Configuration;
using System.Threading;
using System.Web.Configuration;
using System.Collections;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
/// <summary>
/// Summary description for RFIDService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class RFIDService : System.Web.Services.WebService {

    public RFIDService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public static string _TrackerRetail = ConfigurationManager.ConnectionStrings["TrackerConnectionStringDownTown"].ConnectionString;
  
    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public void SendAlert()
    {

        Thread t = new Thread(new ThreadStart(Tick));
        t.Start();

    }



    [WebMethod]
    public void RetryTransaction()
    {
        Thread t = new Thread(new ThreadStart(GetFailedTransaction));
        t.Start();
    }


    public void GetFailedTransaction()
    {
        DataSet resultset = new DataSet();
        string query = "SELECT *  FROM [TrackerRetail].[dbo].[RFIDAlertLog] where result not like 'yes' and MessageTime between DATEADD(MINUTE,-1,GETDATE()) and getdate()";
        string result = "";
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetail))
            {
                dataAdapter.Fill(resultset, "ServiceProviderName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {
                    for (int count = 0; count <= resultset.Tables[0].Rows.Count; count++)
                    {
                        try
                        {
                            SendAlert(resultset.Tables[0].Rows[count]["ToAddress"].ToString(), resultset.Tables[0].Rows[count]["Subject"].ToString(), resultset.Tables[0].Rows[count]["Body"].ToString());
                            updateRetryAlert(resultset.Tables[0].Rows[count]["SNO"].ToString(), "yes");
                        }
                        catch (Exception ex)
                        {
                            updateRetryAlert(resultset.Tables[0].Rows[count]["SNO"].ToString(), ex.Message + ex.StackTrace);
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
        }

    }
    public void Tick()
    {
        DataTable Alerts = GetAlerts();
        ArrayList arl = new ArrayList();

        var grouped = from table in Alerts.AsEnumerable()
                      group table by new { placeCol = table["LastSeenDevice"] } into groupby
                      select new
                      {
                          Value = groupby.Key.ToString(),
                          ColumnValues = groupby.Count()
                      };
        if (Alerts.Rows.Count > 0)
        {
            for (int count = 0; count < Alerts.Rows.Count; count++)
            {
                string mess = MessageConstructor(Alerts.Rows[count]);
                // if (count == 0)
                {
                    bool checkFlag = true;
                    for (int colcount = 0; colcount < grouped.ToArray().Count(); colcount++)
                    {
                        if (grouped.ToArray()[colcount].Value == "{ placeCol = " + Alerts.Rows[count]["LastSeenDevice"].ToString() + " }")
                        {
                            if (grouped.ToArray()[colcount].ColumnValues > int.Parse(WebConfigurationManager.AppSettings["TotalNumber"]))
                            {
                                checkFlag = false;
                                if (!arl.Contains(Alerts.Rows[count]["LastSeenDevice"].ToString()))
                                {
                                    arl.Add(Alerts.Rows[count]["LastSeenDevice"].ToString());
                                    string sumupmessage = "";
                                    sumupmessage = "Totally " + grouped.ToArray()[colcount].ColumnValues + " items went out by " + Alerts.Rows[count]["LastSeenDevice"].ToString() + " (" + Alerts.Rows[count]["KT_StoreName"].ToString() + ") on " + Alerts.Rows[count]["LastSeenTime"].ToString() + " - Please Check Door Exception Portal for Details";
                                    string toaddress = GetUserName(Alerts.Rows[count]["StoreID"].ToString());
                                    string result = SendAlert(toaddress, "RFIDAlert", sumupmessage);
                                    RFIDMessageLOG(toaddress, "RFIDAlert", sumupmessage, DateTime.Now, int.Parse(Alerts.Rows[count]["SNO"].ToString()), result);
                                    updateAlert(Alerts.Rows[count]["SNO"].ToString());
                                }
                            }
                            break;
                        }
                    }
                    if (checkFlag)
                    {
                        //RFID alert for single values
                        string toaddress = GetUserName(Alerts.Rows[count]["StoreID"].ToString());
                        string result = SendAlert(toaddress, "RFIDAlert", mess);
                        RFIDMessageLOG(toaddress, "RFIDAlert", mess, DateTime.Now, int.Parse(Alerts.Rows[count]["SNO"].ToString()), result);
                        updateAlert(Alerts.Rows[count]["SNO"].ToString());
                    }
                    else
                    {
                        //RFIDAlert bulk send
                        string toaddress = "GroupedSMS";
                        RFIDMessageLOG(toaddress, "RFIDAlert", mess, DateTime.Now, int.Parse(Alerts.Rows[count]["SNO"].ToString()), "yes");
                        updateAlert(Alerts.Rows[count]["SNO"].ToString());

                    }
                }

            }
        }
    }
    public string MessageConstructor(DataRow dr)
    {
        string strmessage = "";
        strmessage = "" + dr["UPC"] + "|" + (dr["DESC"].ToString().Length > 70 ? dr["DESC"].ToString().Substring(0, 70) : dr["DESC"].ToString()) + "|$" + dr["Price"].ToString() + "|" + dr["LastSeenDevice"].ToString() + "(" + dr["KT_StoreName"] + ")|" + dr["LastSeenTime"].ToString().Substring(dr["LastSeenTime"].ToString().IndexOf(" ")) + "|" + dr["RFID"].ToString().Substring(0,1) +"-" + dr["RFID"].ToString().Substring(dr["RFID"].ToString().Length-6) ;
        //strmessage = "UPC : " + dr["UPC"] + "| Desc : " + (dr["DESC"].ToString().Length > 70 ? dr["DESC"].ToString().Substring(0, 70) : dr["DESC"].ToString()) + " | Price : $" + dr["Price"].ToString() + " | Portal : " + dr["LastSeenDevice"].ToString() + " (" + dr["KT_StoreName"] + ") on " + dr["LastSeenTime"];

        if (strmessage.Length > 159)
        {
            strmessage = strmessage.Substring(0, 158);
        }
        return strmessage;
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
                client.Timeout = 10000;
                client.ServicePoint.ConnectionLeaseTimeout = 5000;
                client.ServicePoint.MaxIdleTime = 5000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(WebConfigurationManager.AppSettings["EmailID"], WebConfigurationManager.AppSettings["EmailPass"]);
                MailMessage mm = new MailMessage(WebConfigurationManager.AppSettings["EmailID"], strTo, strSubject, strMessage);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                ServicePointManager.ServerCertificateValidationCallback =
delegate(object s, X509Certificate certificate,
  X509Chain chain, SslPolicyErrors sslPolicyErrors)
{ return true; };
    
                client.Send(mm);
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
    public DataTable GetAlerts()
    {

        DataTable dtRRDetails = new DataTable();
        SqlCommand scmCmdToExecute = new SqlCommand();
        scmCmdToExecute.CommandText = "dbo.[pr_GetRFIDAlertValues]";
        scmCmdToExecute.CommandType = CommandType.StoredProcedure;
        DataTable dtToReturn = new DataTable("Alerts");
        SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);
        SqlConnection sql = new SqlConnection(_TrackerRetail);
        scmCmdToExecute.Connection = sql; ;

        try
        {

            sql.Open();
            sdaAdapter.Fill(dtRRDetails);

        }
        catch (Exception ex)
        {

        }
        finally
        {
            // Close connection.
            sql.Close();
            scmCmdToExecute.Dispose();
            sdaAdapter.Dispose();
        }


        return dtRRDetails;
    }
    public void updateRetryAlert(string sno, string result)
    {
        DataSet resultset = new DataSet();
        string query = "update  [RFIDAlertLog] set numberofretries = ISNULL(numberofretries,0)+1,result='" + result + "' , [RetryTime] = GETDATE() where sno ='" + sno + "'";
        try
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetail))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void updateAlert(string sno)
    {
        DataSet resultset = new DataSet();
        string query = "UPDATE [dbo].[RFIDAlert]   SET  [AlertTime] = getdate(), [AlertFlag] = 2  WHERE sno='" + sno + "'";
        try
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetail))
            {
                dataAdapter.Fill(resultset, "VendorName");
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void RFIDMessageLOG(string strToAddress, string strSubject, string strBody, DateTime dtMessageTime, int SNO, string result)
    {
        SqlConnection connection = new SqlConnection(_TrackerRetail);
        try
        {
            SqlCommand command = new SqlCommand("pr_RFIDAlertLOG_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ToAddress", SqlDbType.VarChar)).Value = strToAddress;
            command.Parameters.Add(new SqlParameter("@Subject", SqlDbType.VarChar)).Value = strSubject;
            command.Parameters.Add(new SqlParameter("@Body", SqlDbType.VarChar)).Value = strBody;
            command.Parameters.Add(new SqlParameter("@MessageTime", SqlDbType.DateTime)).Value = dtMessageTime;
            command.Parameters.Add(new SqlParameter("@LogId", SqlDbType.Int)).Value = SNO;
            command.Parameters.Add(new SqlParameter("@Result", SqlDbType.VarChar)).Value = result;
            connection.Open();
            command.ExecuteNonQuery();


        }
        catch (Exception ex)
        {

        }
        finally
        {
            connection.Close();
        }

    }
    public string GetUserName(string storeID)
    {
        DataSet resultset = new DataSet();
        string query = "select sp +','  from  (SELECT  ( validno +'@'  +serviceprovider +'') as 'sp', VALUE AS 'STORE',SNO   FROM RFIDALERTDIRECTORY  CROSS APPLY DBO.FN_SPLIT(STOREID,',') WHERE IsActive = 1 and VALUE = '" + storeID + "') a where a.sp = a.sp for xml path ('')";
        string result = "";
        try
        {

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(query, _TrackerRetail))
            {
                dataAdapter.Fill(resultset, "ServiceProviderName");
            }

            if (resultset != null)
            {
                if (resultset.Tables.Count > 0)
                {

                    result = resultset.Tables[0].Rows[0][0].ToString();
                    if (result != "")
                    {
                        result = result.Substring(0, result.Length - 1);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        return result;
    }
    
}
