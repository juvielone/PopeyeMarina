using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopeyeMarina.Data
{
    public static class DatabaseHelper
    {
        private const string ConnectionString =
            @"Server=localhost\SQLEXPRESS;Database=PopeyeMarinaDB;Integrated Security=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
