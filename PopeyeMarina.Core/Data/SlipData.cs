using Microsoft.Data.SqlClient;
using PopeyeMarina.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopeyeMarina.Core.Data
{
    public static class SlipData
    {
        public static List<Slip> GetVacantSlips()
        {
            List<Slip> slips = new List<Slip>();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = @"
                        SELECT s.*, c.Height, c.DoorType
                        FROM Slip s
                        LEFT JOIN SlipCoveredDetails c
                            ON s.SlipID = c.SlipID
                        WHERE NOT EXISTS
                        (
                            SELECT 1
                            FROM Lease l
                            WHERE l.SlipID = s.SlipID
                            AND (l.EndDate IS NULL OR l.EndDate >= GETDATE())
                        )";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool isCovered = reader.GetBoolean(reader.GetOrdinal("IsCovered"));

                            Slip slip;

                            if (isCovered)
                            {
                                int slipID = reader.GetInt32(reader.GetOrdinal("SlipID"));

                                if (reader.IsDBNull(reader.GetOrdinal("Height")) ||
                                    reader.IsDBNull(reader.GetOrdinal("DoorType")))
                                {
                                    throw new Exception(
                                        $"Slip {slipID} is marked as covered but has no matching SlipCoveredDetails row.");
                                }

                                slip = new CoveredSlip
                                {
                                    Height = reader.GetDecimal(reader.GetOrdinal("Height")),
                                    DoorType = reader.GetString(reader.GetOrdinal("DoorType"))
                                };
                            }
                            else
                            {
                                slip = new Slip();
                            }

                            slip.SlipID = reader.GetInt32(reader.GetOrdinal("SlipID"));
                            slip.Width = reader.GetDecimal(reader.GetOrdinal("Width"));
                            slip.SlipLength = reader.GetDecimal(reader.GetOrdinal("SlipLength"));
                            slip.DockID = reader.GetInt32(reader.GetOrdinal("DockID"));
                            slip.IsCovered = isCovered;

                            slips.Add(slip);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Unable to retrieve vacant slips from the database.", ex);
            }

            return slips;
        }

        public static List<Slip> GetAllSlips()
        {
            List<Slip> slips = new List<Slip>();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = @"
                SELECT s.*, c.Height, c.DoorType
                FROM Slip s
                LEFT JOIN SlipCoveredDetails c
                    ON s.SlipID = c.SlipID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bool isCovered = reader.GetBoolean(
                                reader.GetOrdinal("IsCovered"));

                            Slip slip;

                            if (isCovered)
                            {
                                int slipID = reader.GetInt32(
                                    reader.GetOrdinal("SlipID"));

                                if (reader.IsDBNull(reader.GetOrdinal("Height")) ||
                                    reader.IsDBNull(reader.GetOrdinal("DoorType")))
                                {
                                    throw new Exception(
                                        $"Slip {slipID} is marked as covered but has no matching SlipCoveredDetails row.");
                                }

                                slip = new CoveredSlip
                                {
                                    Height = reader.GetDecimal(
                                        reader.GetOrdinal("Height")),

                                    DoorType = reader.GetString(
                                        reader.GetOrdinal("DoorType"))
                                };
                            }
                            else
                            {
                                slip = new Slip();
                            }

                            // Shared Slip properties
                            slip.SlipID = reader.GetInt32(
                                reader.GetOrdinal("SlipID"));

                            slip.Width = reader.GetDecimal(
                                reader.GetOrdinal("Width"));

                            slip.SlipLength = reader.GetDecimal(
                                reader.GetOrdinal("SlipLength"));

                            slip.DockID = reader.GetInt32(
                                reader.GetOrdinal("DockID"));

                            slip.IsCovered = isCovered;

                            slips.Add(slip);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    "Unable to retrieve slips from the database.", ex);
            }

            return slips;
        }

        public static void AddSlip(Slip slip)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insert base Slip row, returning the new SlipID directly
                        string sql = @"
                    INSERT INTO Slip
                    (Width, SlipLength, DockID, IsCovered)
                    OUTPUT INSERTED.SlipID
                    VALUES
                    (@Width, @SlipLength, @DockID, @IsCovered)";

                        int slipID;

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Transaction = transaction;

                            command.Parameters.Add("@Width", SqlDbType.Decimal).Value = slip.Width;
                            command.Parameters.Add("@SlipLength", SqlDbType.Decimal).Value = slip.SlipLength;
                            command.Parameters.Add("@DockID", SqlDbType.Int).Value = slip.DockID;
                            command.Parameters.Add("@IsCovered", SqlDbType.Bit).Value = slip.IsCovered;

                            object result = command.ExecuteScalar();
                            if (result == null || result == DBNull.Value)
                                throw new Exception("Slip was inserted but no identity value was returned — check that SlipID is configured as IDENTITY on the Slip table.");
                            slipID = Convert.ToInt32(result);
                        }

                        // Only add covered details when the slip is covered
                        if (slip is CoveredSlip coveredSlip)
                        {
                            string coveredSql = @"
                        INSERT INTO SlipCoveredDetails
                        (SlipID, Height, DoorType)
                        VALUES
                        (@SlipID, @Height, @DoorType)";

                            using (SqlCommand command = new SqlCommand(coveredSql, connection))
                            {
                                command.Transaction = transaction;

                                command.Parameters.Add("@SlipID", SqlDbType.Int).Value = slipID;
                                command.Parameters.Add("@Height", SqlDbType.Decimal).Value = coveredSlip.Height;
                                command.Parameters.Add("@DoorType", SqlDbType.VarChar).Value = coveredSlip.DoorType;

                                command.ExecuteNonQuery();
                            }
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

        public static void UpdateSlip(Slip slip)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Update base Slip row
                        string sql = @"
                    UPDATE Slip
                    SET Width = @Width,
                        SlipLength = @SlipLength,
                        DockID = @DockID,
                        IsCovered = @IsCovered
                    WHERE SlipID = @SlipID";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Transaction = transaction;

                            command.Parameters.Add("@Width", SqlDbType.Decimal).Value = slip.Width;
                            command.Parameters.Add("@SlipLength", SqlDbType.Decimal).Value = slip.SlipLength;
                            command.Parameters.Add("@DockID", SqlDbType.Int).Value = slip.DockID;
                            command.Parameters.Add("@IsCovered", SqlDbType.Bit).Value = slip.IsCovered;
                            command.Parameters.Add("@SlipID", SqlDbType.Int).Value = slip.SlipID;

                            command.ExecuteNonQuery();
                        }

                        // Handle covered slip details
                        if (slip is CoveredSlip coveredSlip)
                        {
                            string checkSql = @"
                        SELECT COUNT(*)
                        FROM SlipCoveredDetails
                        WHERE SlipID = @SlipID";

                            int count;

                            using (SqlCommand command = new SqlCommand(checkSql, connection))
                            {
                                command.Transaction = transaction;
                                command.Parameters.Add("@SlipID", SqlDbType.Int).Value = slip.SlipID;

                                count = Convert.ToInt32(command.ExecuteScalar());
                            }

                            if (count > 0)
                            {
                                // Update existing covered-slip details
                                string updateSql = @"
                            UPDATE SlipCoveredDetails
                            SET Height = @Height,
                                DoorType = @DoorType
                            WHERE SlipID = @SlipID";

                                using (SqlCommand command = new SqlCommand(updateSql, connection))
                                {
                                    command.Transaction = transaction;

                                    command.Parameters.Add("@Height", SqlDbType.Decimal).Value = coveredSlip.Height;
                                    command.Parameters.Add("@DoorType", SqlDbType.VarChar).Value = coveredSlip.DoorType;
                                    command.Parameters.Add("@SlipID", SqlDbType.Int).Value = slip.SlipID;

                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                // Add covered-slip details when upgrading to covered
                                string insertSql = @"
                            INSERT INTO SlipCoveredDetails
                            (SlipID, Height, DoorType)
                            VALUES
                            (@SlipID, @Height, @DoorType)";

                                using (SqlCommand command = new SqlCommand(insertSql, connection))
                                {
                                    command.Transaction = transaction;

                                    command.Parameters.Add("@SlipID", SqlDbType.Int).Value = slip.SlipID;
                                    command.Parameters.Add("@Height", SqlDbType.Decimal).Value = coveredSlip.Height;
                                    command.Parameters.Add("@DoorType", SqlDbType.VarChar).Value = coveredSlip.DoorType;

                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                        else
                        {
                            // Remove covered details when changing to a normal slip
                            string deleteSql = @"
                        DELETE FROM SlipCoveredDetails
                        WHERE SlipID = @SlipID";

                            using (SqlCommand command = new SqlCommand(deleteSql, connection))
                            {
                                command.Transaction = transaction;
                                command.Parameters.Add("@SlipID", SqlDbType.Int).Value = slip.SlipID;

                                command.ExecuteNonQuery();
                            }
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
    }
}