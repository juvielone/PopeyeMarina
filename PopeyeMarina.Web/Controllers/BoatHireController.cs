using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PopeyeMarina.Core.Data;
using PopeyeMarina.Core.Models;
using PopeyeMarina.Web.ViewModels;

namespace PopeyeMarina.Web.Controllers
{
    [Authorize]
    public class BoatHireController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var form = new BoatHireFormModel();

            var pageModel = new BoatHirePageModel
            {
                Customers = CustomerData.GetAllCustomers(),
                AvailableBoats =
                    RentalBoatData.GetAvailableRentalBoats(
                        form.StartDate,
                        form.EndDate),
                Form = form
            };

            return View(pageModel);
        }

        [HttpGet]
        public IActionResult AvailableBoats(DateTime startDate, DateTime endDate)
        {
            if (endDate.Date < startDate.Date)
            {
                return Json(new
                {
                    error = "End date cannot be before start date."
                });
            }

            var boats = RentalBoatData
                .GetAvailableRentalBoats(startDate, endDate)
                .Select(b => new
                {
                    rentalBoatId = b.RentalBoatID,
                    display = $"{b.BoatName} ({b.BoatType})"
                });

            int days = (endDate.Date - startDate.Date).Days;

            return Json(new
            {
                boats,
                durationText =
                    $"Duration: {days} day{(days == 1 ? "" : "s")}"
            });
        }

        [HttpPost]
        public IActionResult AddRentalBoat(string boatName, string boatType)
        {
            if (string.IsNullOrWhiteSpace(boatName) ||
                string.IsNullOrWhiteSpace(boatType))
            {
                return Json(new
                {
                    success = false,
                    error = "Boat name and type are required."
                });
            }

            try
            {
                RentalBoatData.AddRentalBoat(
                    new RentalBoat
                    {
                        BoatName = boatName.Trim(),
                        BoatType = boatType,
                        IsActive = true
                    });

                return Json(new
                {
                    success = true
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }


        [HttpPost]
        public IActionResult CreateHire(
    [Bind(Prefix = "Form")] BoatHireFormModel model)
        {
            // Validation order matches App A's btnCreate.Click

            if (model.CustomerID <= 0)
            {
                ModelState.AddModelError(
                    nameof(model.CustomerID),
                    "Select a customer.");
            }

            if (model.RentalBoatID <= 0)
            {
                ModelState.AddModelError(
                    nameof(model.RentalBoatID),
                    "No boats are available for the selected dates.");
            }

            if (model.EndDate.Date < model.StartDate.Date)
            {
                ModelState.AddModelError(
                    nameof(model.EndDate),
                    "End date cannot be before start date.");
            }

            if (!ModelState.IsValid)
            {
                var pageModel = new BoatHirePageModel
                {
                    Customers = CustomerData.GetAllCustomers(),
                    AvailableBoats =
                        RentalBoatData.GetAvailableRentalBoats(
                            model.StartDate,
                            model.EndDate),
                    Form = model
                };

                return View("Index", pageModel);
            }

            var hire = new BoatHire
            {
                CustomerID = model.CustomerID,
                RentalBoatID = model.RentalBoatID,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            try
            {
                BoatHireData.CreateHire(hire);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    string.Empty,
                    ex.Message);

                var pageModel = new BoatHirePageModel
                {
                    Customers = CustomerData.GetAllCustomers(),
                    AvailableBoats =
                        RentalBoatData.GetAvailableRentalBoats(
                            model.StartDate,
                            model.EndDate),
                    Form = model
                };

                return View("Index", pageModel);
            }

            var selectedBoat = RentalBoatData
                        .GetAllRentalBoats()
                        .FirstOrDefault(b => b.RentalBoatID == model.RentalBoatID);

            var customer = CustomerData
                .GetAllCustomers()
                .FirstOrDefault(c => c.CustomerID == model.CustomerID);

            TempData["Success"] =
            $"Boat hire created successfully for " +
            $"{customer?.CustomerName ?? $"Customer #{model.CustomerID}"} — " +
            $"{selectedBoat?.BoatName ?? $"Rental Boat #{model.RentalBoatID}"} " +
            $"from {model.StartDate.ToShortDateString()} " +
            $"to {model.EndDate.ToShortDateString()}.";

            return RedirectToAction("Index");
        }


    }
}