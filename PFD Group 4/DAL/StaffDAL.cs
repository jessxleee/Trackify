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
    public class StaffDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public StaffDAL()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString(
                "APTDBConnectionString");

            conn = new SqlConnection(strConn);

        }
        public List<Staff> GetAllStaff()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Staff ORDER BY StaffID";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<Staff> staffList = new List<Staff>();
            while (reader.Read())
            {
                staffList.Add
                    (
                    new Staff
                    {
                        StaffID = reader.GetString(0),
                        SName = reader.GetString(1),
                        SGender = reader.GetString(2)[0],
                        SEmailAddr = reader.GetString(3),
                        SMobileNumber = reader.GetString(4),
                        SDateJoin = reader.GetDateTime(5),
                        SAppt = reader.GetString(6),
                        SUsername = reader.GetString(7),
                        SPassword = reader.GetString(8),
                    }
                    );
            }

            reader.Close();
            return staffList;
        }
    }
}
