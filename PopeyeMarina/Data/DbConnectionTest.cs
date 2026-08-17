using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopeyeMarina.Data
{
    public static class DbConnectionTest
    {
        public static int GetCustomerCount()
        {
            string connectionString =
                @"Server=DESKTOP-AK2FHA4\MSSQLSERVER01;Database=PopeyeMarinaDB;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = "SELECT COUNT(*) FROM Customer";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    int count = (int)command.ExecuteScalar();

                    return count;
                }
            }
        }
    }
}
