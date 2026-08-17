using Microsoft.Data.SqlClient;
using PopeyeMarina.Models;
using System;
using System.Data;

namespace PopeyeMarina.Data
{
    public static class DockData
    {
        public static void AddDock(Dock dock)
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = @"
                        INSERT INTO Dock
                        (Location, HasElectricity, HasWater)
                        VALUES
                        (@Location, @HasElectricity, @HasWater)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@Location", SqlDbType.VarChar).Value = dock.Location;
                        command.Parameters.Add("@HasElectricity", SqlDbType.Bit).Value = dock.HasElectricity;
                        command.Parameters.Add("@HasWater", SqlDbType.Bit).Value = dock.HasWater;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to add dock to the database.", ex);
            }
        }

        public static List<Dock> GetAllDocks()
        {
            List<Dock> docks = new List<Dock>();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = @"
                SELECT *
                FROM Dock";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dock dock = new Dock
                            {
                                DockID = reader.GetInt32(
                                    reader.GetOrdinal("DockID")),

                                Location = reader.GetString(
                                    reader.GetOrdinal("Location")),

                                HasElectricity = reader.GetBoolean(
                                    reader.GetOrdinal("HasElectricity")),

                                HasWater = reader.GetBoolean(
                                    reader.GetOrdinal("HasWater"))
                            };

                            docks.Add(dock);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    "Unable to retrieve docks from the database.", ex);
            }

            return docks;
        }
    }
}