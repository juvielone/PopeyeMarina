using System;

namespace PopeyeMarina.Web.ViewModels
{
    public class BoatHireFormModel
    {
        public int CustomerID { get; set; }

        public int RentalBoatID { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Today;

        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(1);
    }
}