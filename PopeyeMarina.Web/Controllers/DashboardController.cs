using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeMarina.Core.Data;
using PopeyeMarina.Core.Models;
using PopeyeMarina.Web.ViewModels;

namespace PopeyeMarina.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index(
            string activityFilter = "All",
            int customerId = -1)
        {
            Dictionary<int, string> customerNames =
                CustomerData.GetAllCustomers()
                    .ToDictionary(
                        c => c.CustomerID,
                        c => c.CustomerName);

            Dictionary<int, string> rentalBoatNames =
                RentalBoatData.GetAllRentalBoats()
                    .ToDictionary(
                        b => b.RentalBoatID,
                        b => b.BoatName);

            string CustomerName(int id)
            {
                return customerNames.TryGetValue(
                    id,
                    out string? name)
                    ? name
                    : $"Customer #{id}";
            }

            var activity = new List<ActivityRecord>();

            // Slip Leases
            foreach (Lease lease in LeaseData.GetAllLeases())
            {
                activity.Add(
                    new ActivityRecord
                    {
                        CustomerID = lease.CustomerID,
                        CustomerName =
                            CustomerName(lease.CustomerID),
                        ActivityType = "Slip Lease",
                        ItemLabel = $"Slip #{lease.SlipID}",
                        StartDate = lease.StartDate,
                        EndDate = lease.EndDate
                    });
            }

            // Boat Hires
            foreach (BoatHire hire in BoatHireData.GetAllHires())
            {
                string boatName =
                    rentalBoatNames.TryGetValue(
                        hire.RentalBoatID,
                        out string? name)
                        ? name
                        : $"Boat #{hire.RentalBoatID}";

                activity.Add(
                    new ActivityRecord
                    {
                        CustomerID = hire.CustomerID,
                        CustomerName =
                            CustomerName(hire.CustomerID),
                        ActivityType = "Boat Hire",
                        ItemLabel = boatName,
                        StartDate = hire.StartDate,
                        EndDate = hire.EndDate
                    });
            }

            IEnumerable<ActivityRecord> filtered =
                activity.OrderBy(a => a.StartDate);

            if (activityFilter != "All")
            {
                filtered = filtered.Where(
                    a => a.ActivityType == activityFilter);
            }

            if (customerId != -1)
            {
                filtered = filtered.Where(
                    a => a.CustomerID == customerId);
            }

            var pageModel = new DashboardPageModel
            {
                Customers = CustomerData.GetAllCustomers(),
                Activity = filtered.ToList(),
                ActivityFilter = activityFilter,
                SelectedCustomerId = customerId
            };

            return View(pageModel);
        }
    }
}