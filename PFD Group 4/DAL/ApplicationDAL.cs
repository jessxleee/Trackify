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
    public class ApplicationDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public ApplicationDAL()
        {
            var buildeer = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = buildeer.Build();
            string strConn = Configuration.GetConnectionString(
                "APTDBConnectionString");

            conn = new SqlConnection(strConn);

        }

        public List<Application> GetAllApplication()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Application ORDER BY DateTimeApplied";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<Application> applicationList = new List<Application>();
            while (reader.Read())
            {
                applicationList.Add(
                    new Application
                    {
                        ApplicationID = reader.GetInt32(0),
                        CustomerID = reader.GetString(1),
                        DateTimeApplied = reader.GetDateTime(2),
                        SelfEmployed = reader.GetString(3)[0],
                        AnnualIncome = reader.GetString(4),
                        ProgressStatus = reader.GetString(5)[0],
                        //ApplicationDocument = reader.GetString(6),
                        ApplicationDocument = !reader.IsDBNull(6) ? reader.GetString(6) : null
            });
            }
            
            reader.Close();
            conn.Close();
            return applicationList;
        }

        public int Add(Application application)
        {
            SqlCommand cmd = conn.CreateCommand();
            //SqlCommand cmd2 = conn.CreateCommand();

            cmd.CommandText = @"INSERT INTO Application (ApplicationID, CustomerID, DateTimeApplied, SelfEmployed, AnnualIncome, ProgressStatus)
                              VALUES (@applicationID, @customerID, @datetimeapplied, @employmentStatus, @annualIncome, @progressStatus)";

            //cmd2.CommandText = @"INSERT INTO Application (ApplicationID, CustomerID, DateTimeApplied, SelfEmployed, AnnualIncome, ProgressStatus, ApplicationDocument)
            //                    VALUES (@applicationID, @customerID, @datetimeapplied, @employmentStatus, @annualIncome, @progressStatus, null)";
            cmd.Parameters.AddWithValue("@applicationID", 5);
            cmd.Parameters.AddWithValue("@customerID", application.CustomerID);
            cmd.Parameters.AddWithValue("@datetimeapplied", application.DateTimeApplied);
            cmd.Parameters.AddWithValue("@employmentStatus", application.SelfEmployed);
            cmd.Parameters.AddWithValue("@annualIncome", application.AnnualIncome);
            cmd.Parameters.AddWithValue("@progressStatus", application.ProgressStatus);
            //cmd.Parameters.AddWithValue("@applicationDocument", application.ApplicationDocument);



            conn.Open();

            cmd.ExecuteNonQuery();
            //cmd2.ExecuteNonQuery();
            //application.ApplicationID = (int)cmd.ExecuteScalar();

            conn.Close();

            return application.ApplicationID;

        }

        public Application GetProgressStatus(string customerID)
        {
            Application application = new Application();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Application WHERE CustomerID = @selectedCustomerID";
            cmd.Parameters.AddWithValue("@selectedCustomerID", customerID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    application.ProgressStatus = !reader.IsDBNull(5) ? reader.GetString(5)[0] : (char)0;
                }
            }
            else
            {
                return null;
            }
            reader.Close();
            conn.Close();
            return application;
        }
        
        public Application GetSpecificApplication(string customerID)
        {
            Application application = new Application();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Application WHERE CustomerID = @selectedCustomerID";
            cmd.Parameters.AddWithValue("@selectedCustomerID", customerID);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while(reader.Read())
                {
                    application.ApplicationID = reader.GetInt32(0);
                    application.CustomerID = customerID;
                    application.DateTimeApplied = reader.GetDateTime(2);
                    application.SelfEmployed = reader.GetString(3)[0];
                    application.AnnualIncome = reader.GetString(4);
                    application.ProgressStatus = reader.GetString(5)[0];
                }
            }
           
            reader.Close();
            conn.Close();
            return application;
        }
        public int Update(char status, string id)
        {
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"UPDATE Application SET ProgressStatus = @status WHERE CustomerID = @selectedid";

            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@selectedid", id);
            //Open a database connection
            conn.Open();

            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();

            return count;
        }
      


    }
}
