using Microsoft.Data.SqlClient;
using PopeyeMarina.Models;
using System;
using System.Data;

namespace PopeyeMarina.Data
{
    public static class BoatData
    {
        public static void AddBoat(Boat boat)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert base Boat row
                        string sql = @"
                            INSERT INTO Boat
                            (StateRegoNo, BoatLength, Manufacturer, ModelYear, BoatType, CustomerID)
                            VALUES
                            (@StateRegoNo, @BoatLength, @Manufacturer, @ModelYear, @BoatType, @CustomerID)";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Transaction = transaction;

                            command.Parameters.Add("@StateRegoNo", SqlDbType.VarChar).Value = boat.StateRegoNo;
                            command.Parameters.Add("@BoatLength", SqlDbType.Decimal).Value = boat.BoatLength;
                            command.Parameters.Add("@Manufacturer", SqlDbType.VarChar).Value = boat.Manufacturer;
                            command.Parameters.Add("@ModelYear", SqlDbType.Int).Value = boat.ModelYear;
                            command.Parameters.Add("@BoatType", SqlDbType.VarChar).Value = boat.BoatType;
                            command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = boat.CustomerID;

                            command.ExecuteNonQuery();
                        }

                        // Insert subtype details
                        if (boat is Sailboat sailboat)
                        {
                            string sailboatSql = @"
                                INSERT INTO SailboatDetails
                                (StateRegoNo, KeelDepth, NumberOfSails, MotorType)
                                VALUES
                                (@StateRegoNo, @KeelDepth, @NumberOfSails, @MotorType)";

                            using (SqlCommand command = new SqlCommand(sailboatSql, connection))
                            {
                                command.Transaction = transaction;

                                command.Parameters.Add("@StateRegoNo", SqlDbType.VarChar).Value = sailboat.StateRegoNo;
                                command.Parameters.Add("@KeelDepth", SqlDbType.Decimal).Value = sailboat.KeelDepth;
                                command.Parameters.Add("@NumberOfSails", SqlDbType.Int).Value = sailboat.NumberOfSails;
                                command.Parameters.Add("@MotorType", SqlDbType.VarChar).Value =
                                    (object?)sailboat.MotorType ?? DBNull.Value;

                                command.ExecuteNonQuery();
                            }
                        }
                        else if (boat is Powerboat powerboat)
                        {
                            string powerboatSql = @"
                                INSERT INTO PowerboatDetails
                                (StateRegoNo, NumberOfEngines, FuelType)
                                VALUES
                                (@StateRegoNo, @NumberOfEngines, @FuelType)";

                            using (SqlCommand command = new SqlCommand(powerboatSql, connection))
                            {
                                command.Transaction = transaction;

                                command.Parameters.Add("@StateRegoNo", SqlDbType.VarChar).Value = powerboat.StateRegoNo;
                                command.Parameters.Add("@NumberOfEngines", SqlDbType.Int).Value = powerboat.NumberOfEngines;
                                command.Parameters.Add("@FuelType", SqlDbType.VarChar).Value = powerboat.FuelType;

                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
                    {
                        transaction.Rollback();
                        throw new Exception(
                            $"A boat with registration '{boat.StateRegoNo}' already exists.");
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static List<Boat> GetBoatsByCustomer(int customerId)
        {
            List<Boat> boats = new List<Boat>();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = @"
                SELECT *
                FROM Boat
                WHERE CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerId;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string boatType = reader.GetString(
                                    reader.GetOrdinal("BoatType"));

                                Boat boat;

                                if (boatType == "Sailboat")
                                {
                                    boat = new Sailboat();
                                }
                                else
                                {
                                    boat = new Powerboat();
                                }

                                boat.StateRegoNo = reader.GetString(
                                    reader.GetOrdinal("StateRegoNo"));

                                boat.BoatLength = reader.GetDecimal(
                                    reader.GetOrdinal("BoatLength"));

                                boat.Manufacturer = reader.GetString(
                                    reader.GetOrdinal("Manufacturer"));

                                boat.ModelYear = reader.GetInt32(
                                    reader.GetOrdinal("ModelYear"));

                                boat.BoatType = boatType;

                                boat.CustomerID = reader.GetInt32(
                                    reader.GetOrdinal("CustomerID"));

                                boats.Add(boat);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    "Unable to retrieve boats for the customer.",
                    ex);
            }

            return boats;
        }
    }
}