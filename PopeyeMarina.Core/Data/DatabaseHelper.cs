using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopeyeMarina.Core.Data
{
    public static class DatabaseHelper
    {
        private static readonly IConfiguration Configuration =
             new ConfigurationBuilder()
                 .SetBasePath(AppContext.BaseDirectory)
                 .AddJsonFile(
                     "appsettings.json",
                     optional: true,
                     reloadOnChange: false)
                 .AddEnvironmentVariables()
                 .Build();

        public static SqlConnection GetConnection()
        {
            string connectionString =
                Configuration.GetConnectionString("PopeyeMarinaDB")
                ?? throw new InvalidOperationException(
                    "Connection string 'PopeyeMarinaDB' was not found in appsettings.json.");

            return new SqlConnection(connectionString);
        }
    }
}
