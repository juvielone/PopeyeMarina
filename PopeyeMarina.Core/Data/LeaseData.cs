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
    public static class LeaseData
    {
        public static void CreateLease(Lease lease, Slip slip)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Annual leases always have a one-year term
                        if (lease is AnnualLease)
                        {
                            lease.EndDate = lease.StartDate.AddYears(1);
                        }

                        // Check if the slip is already occupied
                        string checkSql = @"
                    SELECT COUNT(*)
                    FROM Lease
                    WHERE SlipID = @SlipID
                    AND StartDate <= ISNULL(@EndDate, '9999-12-31')
                    AND (EndDate IS NULL OR EndDate >= @StartDate)";

                        using (SqlCommand command = new SqlCommand(checkSql, connection))
                        {
                            command.Transaction = transaction;

                            command.Parameters.Add("@SlipID", SqlDbType.Int).Value = lease.SlipID;
                            command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = lease.StartDate;
                            command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value =
                                (object)lease.EndDate ?? DBNull.Value;

                            int count = Convert.ToInt32(command.ExecuteScalar());

                            if (count > 0)
                            {
                                throw new Exception(
                                    "The selected slip is already occupied for the selected lease dates.");
                            }
                        }

                        // Calculate the lease fee
                        lease.Amount = lease.CalculateFee(slip);

                        // Insert base Lease row and return the generated LeaseID
                        string sql = @"
                    INSERT INTO Lease
                    (StartDate, EndDate, Amount, LeaseType, SlipID, StateRegoNo, CustomerID)
                    OUTPUT INSERTED.LeaseID
                    VALUES
                    (@StartDate, @EndDate, @Amount, @LeaseType, @SlipID, @StateRegoNo, @CustomerID)";

                        int leaseID;

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Transaction = transaction;

                            command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value =
                                lease.StartDate;

                            command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value =
                                (object)lease.EndDate ?? DBNull.Value;

                            command.Parameters.Add("@Amount", SqlDbType.Decimal).Value =
                                lease.Amount;

                            command.Parameters.Add("@LeaseType", SqlDbType.VarChar).Value =
                                lease.LeaseType;

                            command.Parameters.Add("@SlipID", SqlDbType.Int).Value =
                                lease.SlipID;

                            command.Parameters.Add("@StateRegoNo", SqlDbType.VarChar).Value =
                                lease.StateRegoNo;

                            command.Parameters.Add("@CustomerID", SqlDbType.Int).Value =
                                lease.CustomerID;

                            object result = command.ExecuteScalar();

                            if (result == null || result == DBNull.Value)
                            {
                                throw new Exception(
                                    "Lease was inserted but no LeaseID was returned.");
                            }

                            leaseID = Convert.ToInt32(result);
                        }

                        // Insert subtype-specific details
                        if (lease is AnnualLease annualLease)
                        {
                            // BalanceDue starts at the full calculated amount
                            annualLease.BalanceDue = annualLease.Amount;

                            string annualSql = @"
                        INSERT INTO AnnualLeaseDetails
                        (LeaseID, PayMonthly, BalanceDue)
                        VALUES
                        (@LeaseID, @PayMonthly, @BalanceDue)";

                            using (SqlCommand command = new SqlCommand(annualSql, connection))
                            {
                                command.Transaction = transaction;

                                command.Parameters.Add("@LeaseID", SqlDbType.Int).Value =
                                    leaseID;

                                command.Parameters.Add("@PayMonthly", SqlDbType.Bit).Value =
                                    annualLease.PayMonthly;

                                command.Parameters.Add("@BalanceDue", SqlDbType.Decimal).Value =
                                    annualLease.BalanceDue;

                                command.ExecuteNonQuery();
                            }
                        }
                        else if (lease is DailyLease dailyLease)
                        {
                            string dailySql = @"
                        INSERT INTO DailyLeaseDetails
                        (LeaseID, NumberOfDays)
                        VALUES
                        (@LeaseID, @NumberOfDays)";

                            using (SqlCommand command = new SqlCommand(dailySql, connection))
                            {
                                command.Transaction = transaction;

                                command.Parameters.Add("@LeaseID", SqlDbType.Int).Value =
                                    leaseID;

                                command.Parameters.Add("@NumberOfDays", SqlDbType.Int).Value =
                                    dailyLease.NumberOfDays;

                                command.ExecuteNonQuery();
                            }
                        }

                        // Commit only after all inserts succeed
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

        public static List<Lease> GetLeasesByCustomer(int customerId)
        {
            List<Lease> leases = new List<Lease>();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = @"
                SELECT *
                FROM Lease
                WHERE CustomerID = @CustomerID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerId;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string leaseType = reader.GetString(
                                    reader.GetOrdinal("LeaseType"));

                                Lease lease;

                                if (leaseType == "Annual")
                                {
                                    lease = new AnnualLease();
                                }
                                else
                                {
                                    lease = new DailyLease();
                                }

                                lease.LeaseID = reader.GetInt32(
                                    reader.GetOrdinal("LeaseID"));

                                lease.StartDate = reader.GetDateTime(
                                    reader.GetOrdinal("StartDate"));

                                int endDateOrdinal = reader.GetOrdinal("EndDate");

                                lease.EndDate = reader.IsDBNull(endDateOrdinal)
                                    ? null
                                    : reader.GetDateTime(endDateOrdinal);

                                lease.Amount = reader.GetDecimal(
                                    reader.GetOrdinal("Amount"));

                                lease.LeaseType = leaseType;

                                lease.SlipID = reader.GetInt32(
                                    reader.GetOrdinal("SlipID"));

                                lease.StateRegoNo = reader.GetString(
                                    reader.GetOrdinal("StateRegoNo"));

                                lease.CustomerID = reader.GetInt32(
                                    reader.GetOrdinal("CustomerID"));

                                leases.Add(lease);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    "Unable to retrieve leases for the customer.",
                    ex);
            }

            return leases;
        }

        // All leases across all customers. Used by the Dashboard's unified
        // activity view. Same read logic as GetLeasesByCustomer, including the
        // null-safe EndDate handling for daily leases with no recorded end date.
        public static List<Lease> GetAllLeases()
        {
            List<Lease> leases = new List<Lease>();

            try
            {
                using (SqlConnection connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();

                    string sql = "SELECT * FROM Lease";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string leaseType = reader.GetString(
                                reader.GetOrdinal("LeaseType"));

                            Lease lease;

                            if (leaseType == "Annual")
                            {
                                lease = new AnnualLease();
                            }
                            else
                            {
                                lease = new DailyLease();
                            }

                            lease.LeaseID = reader.GetInt32(
                                reader.GetOrdinal("LeaseID"));

                            lease.StartDate = reader.GetDateTime(
                                reader.GetOrdinal("StartDate"));

                            int endDateOrdinal = reader.GetOrdinal("EndDate");

                            lease.EndDate = reader.IsDBNull(endDateOrdinal)
                                ? null
                                : reader.GetDateTime(endDateOrdinal);

                            lease.Amount = reader.GetDecimal(
                                reader.GetOrdinal("Amount"));

                            lease.LeaseType = leaseType;

                            lease.SlipID = reader.GetInt32(
                                reader.GetOrdinal("SlipID"));

                            lease.StateRegoNo = reader.GetString(
                                reader.GetOrdinal("StateRegoNo"));

                            lease.CustomerID = reader.GetInt32(
                                reader.GetOrdinal("CustomerID"));

                            leases.Add(lease);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(
                    "Unable to retrieve leases.",
                    ex);
            }

            return leases;
        }

        public static void DeleteLease(int leaseId)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Delete annual details if they exist
                        string annualSql = @"
                    DELETE FROM AnnualLeaseDetails
                    WHERE LeaseID = @LeaseID";

                        using (SqlCommand command = new SqlCommand(annualSql, connection))
                        {
                            command.Transaction = transaction;
                            command.Parameters.Add("@LeaseID", SqlDbType.Int).Value = leaseId;
                            command.ExecuteNonQuery();
                        }

                        // Delete daily details if they exist
                        string dailySql = @"
                    DELETE FROM DailyLeaseDetails
                    WHERE LeaseID = @LeaseID";

                        using (SqlCommand command = new SqlCommand(dailySql, connection))
                        {
                            command.Transaction = transaction;
                            command.Parameters.Add("@LeaseID", SqlDbType.Int).Value = leaseId;
                            command.ExecuteNonQuery();
                        }

                        // Delete base Lease row last
                        string leaseSql = @"
                    DELETE FROM Lease
                    WHERE LeaseID = @LeaseID";

                        using (SqlCommand command = new SqlCommand(leaseSql, connection))
                        {
                            command.Transaction = transaction;
                            command.Parameters.Add("@LeaseID", SqlDbType.Int).Value = leaseId;
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
    }
}