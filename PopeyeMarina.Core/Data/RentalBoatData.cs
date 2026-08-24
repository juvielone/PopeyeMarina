using Microsoft.Data.SqlClient;
using PopeyeMarina.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace PopeyeMarina.Core.Data
{
    public static class RentalBoatData
    {
        public static void AddRentalBoat(RentalBoat boat)
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = @"
                        INSERT INTO RentalBoat
                        (BoatName, BoatType, IsActive)
                        VALUES
                        (@BoatName, @BoatType, @IsActive)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@BoatName", SqlDbType.VarChar).Value = boat.BoatName;
                        command.Parameters.Add("@BoatType", SqlDbType.VarChar).Value = boat.BoatType;
                        command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = boat.IsActive;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to add rental boat to the database.", ex);
            }
        }

        // All rental boats, active or not. Used for reporting/lookup purposes
        // (e.g. resolving a boat name for a historical hire even if the boat
        // has since been deactivated). Use GetAvailableRentalBoats for the
        // hire workflow instead — that one correctly restricts to active,
        // unbooked boats.
        public static List<RentalBoat> GetAllRentalBoats()
        {
            List<RentalBoat> boats = new List<RentalBoat>();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = "SELECT * FROM RentalBoat";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            boats.Add(ReadRentalBoat(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to retrieve rental boats from the database.", ex);
            }

            return boats;
        }

        // Active rental boats with no BoatHire row overlapping the given date range.
        // Used to populate the boat picker when creating a new hire.
        public static List<RentalBoat> GetAvailableRentalBoats(DateTime startDate, DateTime endDate)
        {
            List<RentalBoat> boats = new List<RentalBoat>();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = @"
                        SELECT rb.*
                        FROM RentalBoat rb
                        WHERE rb.IsActive = 1
                        AND NOT EXISTS (
                            SELECT 1
                            FROM BoatHire bh
                            WHERE bh.RentalBoatID = rb.RentalBoatID
                            AND bh.StartDate <= @EndDate
                            AND bh.EndDate >= @StartDate
                        )";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate.Date;
                        command.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate.Date;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                boats.Add(ReadRentalBoat(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to retrieve available rental boats.", ex);
            }

            return boats;
        }

        private static RentalBoat ReadRentalBoat(SqlDataReader reader)
        {
            return new RentalBoat
            {
                RentalBoatID = reader.GetInt32(reader.GetOrdinal("RentalBoatID")),
                BoatName = reader.GetString(reader.GetOrdinal("BoatName")),
                BoatType = reader.GetString(reader.GetOrdinal("BoatType")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
            };
        }
    }
}