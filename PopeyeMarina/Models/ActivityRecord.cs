using System;

namespace PopeyeMarina.Models
{
    // Not backed by a database table. Exists only to feed the Dashboard's
    // unified activity grid. Lease and BoatHire remain the real, separate
    // business entities — this is a display-only projection of both.
    public class ActivityRecord
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string ActivityType { get; set; } = string.Empty; // "Slip Lease" or "Boat Hire"
        public string ItemLabel { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string Status
        {
            get
            {
                DateTime today = DateTime.Today;

                if (today < StartDate.Date)
                {
                    return "Upcoming";
                }

                // No recorded end date (existing daily leases can be null) —
                // treat as ongoing rather than excluding or crashing.
                if (EndDate == null)
                {
                    return "Active";
                }

                if (today < EndDate.Value.Date)
                {
                    return "Active";
                }

                return "Completed";
            }
        }
    }
}
