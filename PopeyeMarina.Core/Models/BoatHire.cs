using System;

namespace PopeyeMarina.Core.Models
{
    // Represents a customer hiring a RentalBoat for a date range.
    // Deliberately unrelated to Lease/AnnualLease/DailyLease, which represent
    // slip leasing for registered vessels.
    public class BoatHire
    {
        public int HireID { get; set; }
        public int RentalBoatID { get; set; }
        public int CustomerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }

        // Computed, not stored — avoids the value drifting out of sync as dates pass.
        public int DurationDays => Math.Max(0, (EndDate.Date - StartDate.Date).Days);

        public string Status
        {
            get
            {
                DateTime today = DateTime.Today;

                if (today < StartDate.Date)
                {
                    return "Upcoming";
                }

                if (today >= StartDate.Date && today < EndDate.Date)
                {
                    return "Active";
                }

                return "Completed";
            }
        }
    }
}
