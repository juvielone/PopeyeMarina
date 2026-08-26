namespace PopeyeMarina.Web.ViewModels
{
    public class LeaseRow
    {
        public int LeaseID { get; set; }

        public string SlipDisplay { get; set; } = string.Empty;

        public string StateRegoNo { get; set; } = string.Empty;

        public string LeaseType { get; set; } = string.Empty;

        public string StartDate { get; set; } = string.Empty;

        public string EndDate { get; set; } = string.Empty;

        public string Amount { get; set; } = string.Empty;
    }
}