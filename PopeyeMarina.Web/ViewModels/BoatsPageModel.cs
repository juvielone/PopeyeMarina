using PopeyeMarina.Core.Models;

namespace PopeyeMarina.Web.ViewModels
{
    public class BoatsPageModel
    {
        public List<Customer> Customers { get; set; } = new();

        public List<BoatOverviewRow> Overview { get; set; } = new();

        public BoatFormModel Form { get; set; } = new();
    }
}