using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using PFD_Group_4.Models;
using System.Data;

namespace PFD_Group_4.DAL
{
    public class CustomerDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        public CustomerDAL()
        {
            var buildeer = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = buildeer.Build();
            string strConn = Configuration.GetConnectionString(
                "APTDBConnectionString");

            conn = new SqlConnection(strConn);

        }

        public List<Customer> GetAllCustomer()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Customer ORDER BY CustomerID";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<Customer> customerList = new List<Customer>();
            while (reader.Read())
            {
                customerList.Add
                    (
                    new Customer
                    {
                        CustomerID = reader.GetString(0),
                        CustomerName = reader.GetString(1),
                        CustomerNRIC = reader.GetString(2),
                        CAddress = reader.GetString(3),
                        CDOB = reader.GetDateTime(4),
                        CNationality = reader.GetString(5)[0],
                        CRace = reader.GetString(6),
                        CGender = reader.GetString(7)[0],
                        CEmailAddress = reader.GetString(8),
                        CMobileNumber = reader.GetString(9),
                        CBankruptStatus = reader.GetString(10)[0],
                        CDepositAccountNo = reader.GetString(11),
                        CInvestAccountNo = !reader.IsDBNull(12) ? reader.GetString(12) : null,
                        CUsername = reader.GetString(13),
                        CPassword = reader.GetString(14),
                    }
                    );
            }

            reader.Close();

            return customerList;
        }

        public Customer GetCustomerDetails(string customerId)
        {
            Customer customer = new Customer();
        
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Customer WHERE CustomerID = @selectedCustomerId";
            cmd.Parameters.AddWithValue("@selectedCustomerId", customerId);
            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    customer.CustomerID = customerId;
                    customer.CustomerName = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    customer.CustomerNRIC = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    customer.CAddress = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                    customer.CDOB = reader.GetDateTime(4);
                    customer.CNationality = !reader.IsDBNull(5) ? reader.GetString(5)[0] : (char) 0;
                    customer.CRace = !reader.IsDBNull(6) ? reader.GetString(6) : null;
                    customer.CGender = !reader.IsDBNull(7) ? reader.GetString(7)[0] : (char) 0;
                    customer.CEmailAddress = !reader.IsDBNull(8) ? reader.GetString(8) : null;
                    customer.CMobileNumber = !reader.IsDBNull(9) ? reader.GetString(9) : null;
                    customer.CBankruptStatus = !reader.IsDBNull(10) ? reader.GetString(10)[0] : (char)0;
                    customer.CDepositAccountNo = !reader.IsDBNull(11) ? reader.GetString(11) : null;
                    customer.CInvestAccountNo = !reader.IsDBNull(12) ? reader.GetString(12) : null;
                    customer.CUsername = !reader.IsDBNull(13) ? reader.GetString(13) : null;   
                    customer.CPassword = !reader.IsDBNull(14) ? reader.GetString(14) : null;

                }
            }
            reader.Close();
            conn.Close();

            return customer;
        }
        public Customer GetCustomerDetailsWithName(string customerName)
        {
            Customer customer = new Customer();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Customer
                                WHERE CustomerName LIKE '%@selectedCustomerId%'";
            cmd.Parameters.AddWithValue("@selectedCustomerId", customerName);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    customer.CustomerID = !reader.IsDBNull(0) ? reader.GetString(0) : null; ;
                    customer.CustomerName = !reader.IsDBNull(1) ? reader.GetString(1) : null;
                    customer.CustomerNRIC = !reader.IsDBNull(2) ? reader.GetString(2) : null;
                    customer.CAddress = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                    customer.CDOB = reader.GetDateTime(4);
                    customer.CNationality = !reader.IsDBNull(5) ? reader.GetString(5)[0] : (char)0;
                    customer.CRace = !reader.IsDBNull(6) ? reader.GetString(6) : null;
                    customer.CGender = !reader.IsDBNull(7) ? reader.GetString(7)[0] : (char)0;
                    customer.CEmailAddress = !reader.IsDBNull(8) ? reader.GetString(8) : null;
                    customer.CMobileNumber = !reader.IsDBNull(9) ? reader.GetString(9) : null;
                    customer.CBankruptStatus = !reader.IsDBNull(10) ? reader.GetString(10)[0] : (char)0;
                    customer.CDepositAccountNo = !reader.IsDBNull(11) ? reader.GetString(11) : null;
                    customer.CInvestAccountNo = !reader.IsDBNull(12) ? reader.GetString(12) : null;
                    customer.CUsername = !reader.IsDBNull(13) ? reader.GetString(13) : null;
                    customer.CPassword = !reader.IsDBNull(14) ? reader.GetString(14) : null;

                }
            }
            reader.Close();
            conn.Close();

            return customer;
        }

        public List<Customer> GetCustomerwithStatus(string status)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Customer WHERE  = @selectedCustomerId";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            List<Customer> customerList = new List<Customer>();
            while (reader.Read())
            {
                customerList.Add
                    (
                    new Customer
                    {
                        CustomerID = reader.GetString(0),
                        CustomerName = reader.GetString(1),
                        CustomerNRIC = reader.GetString(2),
                        CAddress = reader.GetString(3),
                        CDOB = reader.GetDateTime(4),
                        CNationality = reader.GetString(5)[0],
                        CRace = reader.GetString(6),
                        CGender = reader.GetString(7)[0],
                        CEmailAddress = reader.GetString(8),
                        CMobileNumber = reader.GetString(9),
                        CBankruptStatus = reader.GetString(10)[0],
                        CDepositAccountNo = reader.GetString(11),
                        CInvestAccountNo = !reader.IsDBNull(12) ? reader.GetString(12) : null,
                        CUsername = reader.GetString(13),
                        CPassword = reader.GetString(14),
                    }
                    );
            }

            reader.Close();

            return customerList;
        }

        public List<string> GetCustomerID()
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT CustomerID FROM Customer";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<string> customeridList = new List<string>();
            while (reader.Read())
            {
                customeridList.Add(

                  reader.GetString(0)

                );
            }
            reader.Close();
            conn.Close();
            return customeridList;
        }

        public int UpdateCustomerDetails(Customer customer)
        {
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify an UPDATE SQL statement
            cmd.CommandText = @"UPDATE Customer SET CAddress=@address, 
                                CEmailAddress = @emailAddress, 
                                CMobileNumber = @mobileNumber 
                                WHERE CustomerID = @selectedCustomerID";
            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@address", customer.CAddress);
            cmd.Parameters.AddWithValue("@emailAddress", customer.CEmailAddress);
            cmd.Parameters.AddWithValue("@mobileNumber", customer.CMobileNumber);
            cmd.Parameters.AddWithValue("@selectedCustomerID", customer.CustomerID);
            //Open a database connection
            conn.Open();
            //ExecuteNonQuery is used for UPDATE and DELETE
            int count = cmd.ExecuteNonQuery();
            //Close the database connection
            conn.Close();
            return count;
        }

        /**-------------------------------------------------------------------------**/
        public Customer customerLogin(string username, string password)
        {
            Customer customer = new Customer();

            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();

            //Specify the SELECT SQL statement that
            //retrieves all attributes of a staff record.
            cmd.CommandText = @"SELECT * FROM Customer 
                                WHERE CUsername = @selectedUsername AND CPassword = @selectedPassword";

            //Define the parameter used in SQL statement, value for the
            //parameter is retrieved from the method parameter “staffId”.
            cmd.Parameters.AddWithValue("@selectedUsername", username);
            cmd.Parameters.AddWithValue("@selectedPassword", password);

            //Open a database connection
            conn.Open();

            //Execute SELCT SQL through a DataReader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill staff object with values from the data reader
                    customer.CustomerID = reader.GetString(0);
                    customer.CustomerName = reader.GetString(1);
                    customer.CustomerNRIC = reader.GetString(2);
                    customer.CAddress = reader.GetString(3);
                    customer.CDOB = reader.GetDateTime(4);
                    customer.CNationality = reader.GetString(5)[0];
                    customer.CRace = reader.GetString(6);
                    customer.CGender = reader.GetString(7)[0];
                    customer.CEmailAddress = reader.GetString(8);
                    customer.CMobileNumber = reader.GetString(9);
                    customer.CBankruptStatus = reader.GetString(10)[0];
                    customer.CDepositAccountNo = reader.GetString(11);
                    customer.CInvestAccountNo = !reader.IsDBNull(12) ?
                    reader.GetString(7) : string.Empty;
                    customer.CUsername = reader.GetString(13);
                    customer.CPassword = reader.GetString(14);
                }
            }
            //Close data reader
            reader.Close();

            //Close the database connection
            conn.Close();

            return customer;
        }

    }
}
