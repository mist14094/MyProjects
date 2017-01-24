using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AdWeb
{
    public class StatisticsHub : Hub
    {


        private string FirstName;
        private string LastName;
        private string Address;
        private string City;

        [HubMethodName("sendNotifications")]
        public string SendNotifications()
        {
            using (
                var connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["AdventurePark"].ConnectionString))
            {
                string query =
                    "SELECT *   FROM [AdventurePark].[dbo].[UserDetails]";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Notification = null;
                    DataTable dt = new DataTable();
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    var reader = command.ExecuteReader();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        FirstName = (dt.Rows[0]["NewMessageCount"].ToString());
                        LastName = (dt.Rows[0]["NewCircleRequestCount"].ToString());
                        Address = (dt.Rows[0]["NewJobNotificationsCount"].ToString());
                        City = (dt.Rows[0]["NewNotificationsCount"].ToString());
                    }
                }
            }
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<StatisticsHub>();
            return context.Clients.All.RecieveNotification(FirstName, LastName, Address, City);
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                StatisticsHub nHub = new StatisticsHub();
                nHub.SendNotifications();
            }
        }
    }
}