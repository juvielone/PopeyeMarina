using System;

namespace PopeyeMarina.Web.ViewModels
{
    public class LeaseFormModel
    {
        public int CustomerID { get; set; }

        public string StateRegoNo { get; set; } = string.Empty;

        public int SlipID { get; set; }

        public bool IsAnnual { get; set; } = true;

        public DateTime StartDate { get; set; } = DateTime.Today;

        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);

        public bool PayMonthly { get; set; }
    }
}