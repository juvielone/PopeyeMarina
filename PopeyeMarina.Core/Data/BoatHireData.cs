using Microsoft.Data.SqlClient;
using PopeyeMarina.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace PopeyeMarina.Core.Data
{
    public static class BoatHireData
    {
        public static void CreateHire(BoatHire hire)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Reject if the rental boat already has an overlapping hire
                        string checkSql = @"
                            SELECT COUNT(*)
                            FROM BoatHire
                            WHERE RentalBoatID = @RentalBoatID
                            AND StartDate <= @EndDate
                            AND EndDate >= @StartDate";

                        using (SqlCommand command = new SqlCommand(checkSql, connection))
                        {
                            command.Transaction = transaction;
                            command.Parameters.Add("@RentalBoatID", SqlDbType.Int).Value = hire.RentalBoatID;
                            command.Parameters.Add("@StartDate", SqlDbType.Date).Value = hire.StartDate.Date;
                            command.Parameters.Add("@EndDate", SqlDbType.Date).Value = hire.EndDate.Date;

                            int count = Convert.ToInt32(command.ExecuteScalar());

                            if (count > 0)
                            {
                                throw new Exception(
                                    "The selected rental boat is already hired for an overlapping date range.");
                            }
                        }

                        string sql = @"
                            INSERT INTO BoatHire
                            (RentalBoatID, CustomerID, StartDate, EndDate)
                            VALUES
                            (@RentalBoatID, @CustomerID, @StartDate, @EndDate)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Transaction = transaction;
                            command.Parameters.Add("@RentalBoatID", SqlDbType.Int).Value = hire.RentalBoatID;
                            command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = hire.CustomerID;
                            command.Parameters.Add("@StartDate", SqlDbType.Date).Value = hire.StartDate.Date;
                            command.Parameters.Add("@EndDate", SqlDbType.Date).Value = hire.EndDate.Date;

                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static List<BoatHire> GetHiresByCustomer(int customerId)
        {
            List<BoatHire> hires = new List<BoatHire>();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = "SELECT * FROM BoatHire WHERE CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerId;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                hires.Add(ReadHire(reader));
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to retrieve hires for the customer.", ex);
            }

            return hires;
        }

        // Used by the future Activity/Dashboard view; harmless to add now since
        // it touches only the new BoatHire table.
        public static List<BoatHire> GetAllHires()
        {
            List<BoatHire> hires = new List<BoatHire>();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = "SELECT * FROM BoatHire";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            hires.Add(ReadHire(reader));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to retrieve boat hires.", ex);
            }

            return hires;
        }

        public static void DeleteHire(int hireId)
        {
            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = "DELETE FROM BoatHire WHERE HireID = @HireID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@HireID", SqlDbType.Int).Value = hireId;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to delete boat hire.", ex);
            }
        }

        private static BoatHire ReadHire(SqlDataReader reader)
        {
            return new BoatHire
            {
                HireID = reader.GetInt32(reader.GetOrdinal("HireID")),
                RentalBoatID = reader.GetInt32(reader.GetOrdinal("RentalBoatID")),
                CustomerID = reader.GetInt32(reader.GetOrdinal("CustomerID")),
                StartDate = reader.GetDateTime(reader.GetOrdinal("StartDate")),
                EndDate = reader.GetDateTime(reader.GetOrdinal("EndDate")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
            };
        }
    }
}
