namespace PopeyeMarina.Core.Models
{
    // Represents a marina-owned boat available for hire.
    // Deliberately unrelated to Boat/Sailboat/Powerboat, which represent
    // customer-owned registered vessels.
    public class RentalBoat
    {
        public int RentalBoatID { get; set; }
        public string BoatName { get; set; } = string.Empty;
        public string BoatType { get; set; } = string.Empty; // "Sailboat" or "Powerboat"
        public bool IsActive { get; set; } = true;

        public override string ToString() => BoatName;
    }
}
