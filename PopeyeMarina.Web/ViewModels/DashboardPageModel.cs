using PopeyeMarina.Core.Models;

namespace PopeyeMarina.Web.ViewModels
{
    public class DashboardPageModel
    {
        public List<Customer> Customers { get; set; } = new();

        public List<ActivityRecord> Activity { get; set; } = new();

        // "All" | "Slip Lease" | "Boat Hire"
        public string ActivityFilter { get; set; } = "All";

        // -1 = All customers
        public int SelectedCustomerId { get; set; } = -1;
    }
}