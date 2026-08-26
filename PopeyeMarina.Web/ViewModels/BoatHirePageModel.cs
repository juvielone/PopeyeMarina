using PopeyeMarina.Core.Models;

namespace PopeyeMarina.Web.ViewModels
{
    public class BoatHirePageModel
    {
        public List<Customer> Customers { get; set; } = new();

        public List<RentalBoat> AvailableBoats { get; set; } = new();

        public BoatHireFormModel Form { get; set; } = new();
    }
}