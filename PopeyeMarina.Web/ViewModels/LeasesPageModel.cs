using PopeyeMarina.Core.Models;

namespace PopeyeMarina.Web.ViewModels
{
    public class LeasesPageModel
    {
        public List<Customer> Customers { get; set; } = new();

        public List<Slip> VacantSlips { get; set; } = new();

        public LeaseFormModel Form { get; set; } = new();
    }
}