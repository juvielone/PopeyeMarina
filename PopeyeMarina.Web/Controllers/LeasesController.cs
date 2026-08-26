using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeMarina.Core.Data;
using PopeyeMarina.Core.Models;
using PopeyeMarina.Web.ViewModels;
using System.Linq;

namespace PopeyeMarina.Web.Controllers
{
    [Authorize]
    public class LeasesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var pageModel = new LeasesPageModel
            {
                Customers = CustomerData.GetAllCustomers(),
                VacantSlips = SlipData.GetVacantSlips(),
                Form = new LeaseFormModel()
            };

            return View(pageModel);
        }


        [HttpGet]
        public IActionResult BoatsForCustomer(int customerId)
        {
            List<Boat> boats = BoatData.GetBoatsByCustomer(customerId);

            var result = boats.Select(b => new
            {
                stateRegoNo = b.StateRegoNo,
                display = $"{b.StateRegoNo} — {b.Manufacturer} ({b.BoatType})"
            });

            return Json(result);
        }

        [HttpPost]
        public IActionResult Save([Bind(Prefix = "Form")] LeaseFormModel model)
        {
            // Validation order — matches App A's btnGenerate_Click

            if (model.CustomerID <= 0)
            {
                ModelState.AddModelError(
                    nameof(model.CustomerID),
                    "Select a customer.");
            }

            if (string.IsNullOrWhiteSpace(model.StateRegoNo))
            {
                ModelState.AddModelError(
                    nameof(model.StateRegoNo),
                    "Select a vessel.");
            }

            if (model.SlipID <= 0)
            {
                ModelState.AddModelError(
                    nameof(model.SlipID),
                    "Select a slip.");
            }

            // Daily leases only need an end-date validation.
            if (!model.IsAnnual &&
                model.EndDate <= model.StartDate)
            {
                ModelState.AddModelError(
                    nameof(model.EndDate),
                    "The end date must be after the start date.");
            }

            if (!ModelState.IsValid)
            {
                var pageModel = new LeasesPageModel
                {
                    Customers = CustomerData.GetAllCustomers(),
                    VacantSlips = SlipData.GetVacantSlips(),
                    Form = model
                };

                return View("Index", pageModel);
            }

            Lease lease;

            if (model.IsAnnual)
            {
                lease = new AnnualLease
                {
                    PayMonthly = model.PayMonthly
                };
            }
            else
            {
                lease = new DailyLease
                {
                    NumberOfDays = (model.EndDate - model.StartDate).Days,
                    EndDate = model.EndDate
                };
            }

            lease.CustomerID = model.CustomerID;
            lease.StateRegoNo = model.StateRegoNo;
            lease.SlipID = model.SlipID;
            lease.StartDate = model.StartDate;
            lease.CustomerID = model.CustomerID;
            lease.StateRegoNo = model.StateRegoNo;
            lease.SlipID = model.SlipID;
            lease.StartDate = model.StartDate;
            lease.LeaseType = model.IsAnnual
                ? "Annual"
                : "Daily";

            var selectedSlip = SlipData.GetVacantSlips()
      .FirstOrDefault(s => s.SlipID == model.SlipID);

            if (selectedSlip == null)
            {
                ModelState.AddModelError(
                    nameof(model.SlipID),
                    "The selected slip is no longer available.");

                var pageModel = new LeasesPageModel
                {
                    Customers = CustomerData.GetAllCustomers(),
                    VacantSlips = SlipData.GetVacantSlips(),
                    Form = model
                };

                return View("Index", pageModel);
            }

            try
            {
                LeaseData.CreateLease(lease, selectedSlip);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                var pageModel = new LeasesPageModel
                {
                    Customers = CustomerData.GetAllCustomers(),
                    VacantSlips = SlipData.GetVacantSlips(),
                    Form = model
                };

                return View("Index", pageModel);
            }

            TempData["Success"] =
                $"Lease created successfully. Amount: {lease.Amount:C}";

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Search(int? customerId)
        {
            var pageModel = new LeaseSearchPageModel
            {
                Customers = CustomerData.GetAllCustomers(),
                SelectedCustomerId = customerId
            };

            if (customerId is int id)
            {
                pageModel.Results = LeaseData.GetLeasesByCustomer(id)
                    .Select(l => new LeaseRow
                    {
                        LeaseID = l.LeaseID,
                        SlipDisplay = $"Slip {l.SlipID}",
                        StateRegoNo = l.StateRegoNo,
                        LeaseType = l.LeaseType,
                        StartDate = l.StartDate.ToShortDateString(),
                        EndDate = l.EndDate?.ToShortDateString() ?? "—",
                        Amount = l.Amount.ToString("C")
                    })
                    .ToList();
            }

            return View(pageModel);
        }

        [HttpPost]
        public IActionResult Delete(int leaseId, int customerId)
        {
            try
            {
                LeaseData.DeleteLease(leaseId);
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    "Unable to delete lease. " + ex.Message;
            }

            return RedirectToAction(
                "Search",
                new { customerId });
        }
    }
}