using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

/// <summary>
/// Summary description for RFIDMonitor
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class RFIDMonitor : System.Web.Services.WebService {

    public RFIDMonitor () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public static string _TrackerRetail = ConfigurationManager.ConnectionStrings["TrackerConnectionString"].ConnectionString;
  

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public void PingTester()
    {
        Thread t = new Thread(new ThreadStart(PingAllDevices));
        t.Start();
      
    }


    public void PingAllDevices()
    {
        string sessionlog = DateTime.Now.ToString("yMMddhhmmssfff");
        DataSet resultset = new DataSet();
        string query = "SELECT * FROM [TrackerRetail].[dbo].[RFIDDeviceDetails] WHERE IsActive =1";
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
                            ping(resultset.Tables[0].Rows[count]["IPAddress"].ToString(), resultset.Tables[0].Rows[count]["ID"].ToString(), sessionlog);
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
        }

    }

    public void ping(string ipaddress, string id, string sessionlog)
    {
        string address = "192.168.1.17";
        System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
        try
        {
            string ID = id, RoundTrip = "", Status = "";
            try
            {
                System.Net.NetworkInformation.PingReply reply = ping.Send(ipaddress);
                Status = reply.Status.ToString();
                RoundTrip = reply.RoundtripTime.ToString();
            }
            catch (Exception ex)
            {
               
            }

            writevalues(Status, RoundTrip, Convert.ToInt16(ID), sessionlog);
        }
        catch (Exception ex)
        {
            
        }
    }

    public void writevalues(string status, string roundtrip, int id, string sessionlog)
    {
        SqlConnection connection = new SqlConnection(_TrackerRetail);
        try
        {
            SqlCommand command = new SqlCommand("pr_RFIDDeviceLog_Insert", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = id;
            command.Parameters.Add(new SqlParameter("@RoundTrip", SqlDbType.VarChar)).Value = @roundtrip;
            command.Parameters.Add(new SqlParameter("@Status", SqlDbType.VarChar)).Value = @status;
            command.Parameters.Add(new SqlParameter("@sessionlog", SqlDbType.VarChar)).Value = sessionlog;
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
}
