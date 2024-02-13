using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using PFD_Group_4.Models;

namespace PFD_Group_4.DAL
{
    public class NotificationDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public NotificationDAL()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("APTDBConnectionString");

            conn = new SqlConnection(strConn);

        }

        public List<Notification> GetAllNotification(int applicationID)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Notification WHERE ApplicationID = @application ORDER BY CreatedDate ASC";
            cmd.Parameters.AddWithValue("@application", applicationID);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<Notification> notificationList = new List<Notification>();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    notificationList.Add(
                        new Notification
                        {
                            NotificationID = reader.GetInt32(0),
                            NotificationHeader = reader.GetString(2),
                            NotificationBody = reader.GetString(3),
                            IsRead = reader.GetBoolean(4),
                            CreatedDate = reader.GetDateTime(5),
                            ApplicationID = applicationID,
                        });
                }
            }
            reader.Close();
            conn.Close();

            return notificationList;
        }

        public int UpdateIsRead(int applicaionId)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE Application SET IsRead=@readValue WHERE ApplicationID =@selectedApplicationID";
            cmd.Parameters.AddWithValue("@readValue", 1);
            cmd.Parameters.AddWithValue("@selectedApplicationID", applicaionId);

            conn.Open();
            conn.Close();

            return applicaionId;
        }
    }
}
