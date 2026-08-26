namespace PopeyeMarina.Web.Models
{
    public class SlipFormModel
    {
        public int? SlipID { get; set; }          // null = add mode, set = edit mode
        public decimal Width { get; set; }
        public decimal SlipLength { get; set; }
        public int DockID { get; set; }
        public bool IsCovered { get; set; }
        public decimal? Height { get; set; }
        public string? DoorType { get; set; }
    }
}