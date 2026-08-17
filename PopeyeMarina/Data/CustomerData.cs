using Microsoft.Data.SqlClient;
using PopeyeMarina.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopeyeMarina.Data
{
    public static class CustomerData
    {
        public static List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = "SELECT * FROM Customer";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                                CustomerName = reader.GetString(reader.GetOrdinal("CustomerName")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                PhoneNo = reader.GetString(reader.GetOrdinal("PhoneNo"))
                            };

                            customers.Add(customer);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to retrieve customers from the database.", ex);
            }

            return customers;
        }

        public static void AddCustomer(Customer customer)
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = @"
                INSERT INTO Customer
                (CustomerName, Address, PhoneNo)
                VALUES
                (@CustomerName, @Address, @PhoneNo)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = customer.CustomerName;
                        command.Parameters.Add("@Address", SqlDbType.VarChar).Value = customer.Address;
                        command.Parameters.Add("@PhoneNo", SqlDbType.VarChar).Value = customer.PhoneNo;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to add customer to the database.", ex);
            }
        }
    }
}
