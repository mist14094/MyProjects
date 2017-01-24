using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using AdBsnsLayer;
using Microsoft.AspNet.SignalR;
using System.Configuration;
namespace AdWeb
{
    public class RealtimeDataHub : Hub
    {
        public void GetUsers()
        {
            List<User> _lst = new List<User>();
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AdventurePark"].ConnectionString))
            {
                String query = "SELECT *   FROM [AdventurePark].[dbo].[UserDetails]";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Notification = null;
                    DataTable dt = new DataTable();
                    SqlDependency dependency = new SqlDependency(command);

                    dependency.OnChange += dependency_OnChange;

                    if (connection.State == ConnectionState.Closed) connection.Open();

                    SqlDependency.Start(connection.ConnectionString);
                    var reader = command.ExecuteReader();
                    dt.Load(reader);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _lst.Add(new User
                            {
                                EmailId = dt.Rows[i]["EmailID"].ToString(),
                                FirstName = dt.Rows[i]["FirstName"].ToString(),
                                LastName = dt.Rows[i]["LastName"].ToString(),

                            });
                        }
                    }
                }
            }
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<RealtimeDataHub>();
            this.Clients.All.displayUsers(_lst);
        }

        void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                RealtimeDataHub _dataHub = new RealtimeDataHub();
                _dataHub.GetUsers();
            }
        }
    }
}