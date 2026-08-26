// ViewModels/BoatFormModel.cs
namespace PopeyeMarina.Web.ViewModels
{
    public class BoatFormModel
    {
        // Shared Boat fields — match Boat's base properties exactly
        public string StateRegoNo { get; set; } = string.Empty;
        public decimal BoatLength { get; set; }
        public string Manufacturer { get; set; } = string.Empty;
        public int ModelYear { get; set; }
        public int CustomerID { get; set; }

        // Subtype toggle
        public bool IsSailboat { get; set; } = true; 

        // Sailboat-specific
        public decimal? KeelDepth { get; set; }
        public int? NumberOfSails { get; set; }
        public string? MotorType { get; set; } 

        // Powerboat-specific
        public int? NumberOfEngines { get; set; }
        public string? FuelType { get; set; }
    }
}