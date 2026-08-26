using PopeyeMarina.Core.Models;

namespace PopeyeMarina.Web.ViewModels
{
    public class LeaseSearchPageModel
    {
        public List<Customer> Customers { get; set; } = new();

        public int? SelectedCustomerId { get; set; }

        public List<LeaseRow> Results { get; set; } = new();
    }
}