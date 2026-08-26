using Microsoft.AspNetCore.Mvc;
using PopeyeMarina.Core.Data;
using PopeyeMarina.Core.Models;
using PopeyeMarina.Web.ViewModels;

namespace PopeyeMarina.Web.Controllers
{
    public class BoatsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            List<Customer> customers = CustomerData.GetAllCustomers();

            var pageModel = new BoatsPageModel
            {
                Customers = customers,
                Overview = RebuildOverview(),
                Form = new BoatFormModel()
            };

            return View(pageModel);
        }

        private List<BoatOverviewRow> RebuildOverview()
        {
            List<Customer> customers = CustomerData.GetAllCustomers();
            List<Boat> boats = BoatData.GetAllBoats();
            List<Lease> leases = LeaseData.GetAllLeases();

            Dictionary<int, string> customerNames =
                customers.ToDictionary(
                    c => c.CustomerID,
                    c => c.CustomerName);

            HashSet<string> takenRegoNumbers = leases
                .Where(l => l.EndDate == null || l.EndDate >= DateTime.Today)
                .Select(l => l.StateRegoNo)
                .ToHashSet();

            return boats
                .Select(b => new BoatOverviewRow
                {
                    StateRegoNo = b.StateRegoNo,
                    Manufacturer = b.Manufacturer,
                    BoatType = b.BoatType,

                    CustomerName =
                        customerNames.TryGetValue(
                            b.CustomerID,
                            out string? name)
                            ? name
                            : $"Customer #{b.CustomerID}",

                    IsTaken = takenRegoNumbers.Contains(b.StateRegoNo)
                })
                .ToList();
        }


        // BoatsController.cs
        [HttpPost]
        public IActionResult Add([Bind(Prefix = "Form")] BoatFormModel model)
        {
            // --- Validation, same order as RecordsControl's btnAdd.Click ---

            if (string.IsNullOrWhiteSpace(model.StateRegoNo) || string.IsNullOrWhiteSpace(model.Manufacturer))
            {
                ModelState.AddModelError(string.Empty, "State rego number and manufacturer are required.");
            }

            if (model.BoatLength <= 0)
            {
                ModelState.AddModelError(nameof(model.BoatLength), "Enter a valid boat length greater than 0.");
            }

            if (model.ModelYear <= 0)
            {
                ModelState.AddModelError(nameof(model.ModelYear), "Enter a valid model year.");
            }

            if (model.CustomerID <= 0)
            {
                ModelState.AddModelError(nameof(model.CustomerID), "Select the boat's owner.");
            }

            if (model.IsSailboat)
            {
                if (model.NumberOfSails is null || model.NumberOfSails <= 0)
                {
                    ModelState.AddModelError(nameof(model.NumberOfSails), "Enter a valid number of sails.");
                }

                if (model.KeelDepth is null || model.KeelDepth <= 0)
                {
                    ModelState.AddModelError(nameof(model.KeelDepth), "Enter a valid keel depth greater than 0.");
                }
                // MotorType is optional — no check, matches App A
            }
            else
            {
                if (model.NumberOfEngines is null || model.NumberOfEngines <= 0)
                {
                    ModelState.AddModelError(nameof(model.NumberOfEngines), "Enter a valid number of engines.");
                }

                if (string.IsNullOrWhiteSpace(model.FuelType))
                {
                    ModelState.AddModelError(nameof(model.FuelType), "Enter the fuel type.");
                }
            }

            if (!ModelState.IsValid)
            {
                return View("Index", RebuildPageModel(model));
            }

            // --- Construct and save ---

            Boat boat = model.IsSailboat
                ? new Sailboat
                {
                    KeelDepth = model.KeelDepth!.Value,
                    NumberOfSails = model.NumberOfSails!.Value,
                    MotorType = string.IsNullOrWhiteSpace(model.MotorType) ? null : model.MotorType.Trim()
                }
                : new Powerboat
                {
                    NumberOfEngines = model.NumberOfEngines!.Value,
                    FuelType = model.FuelType!.Trim()
                };

            boat.StateRegoNo = model.StateRegoNo.Trim();
            boat.BoatLength = model.BoatLength;
            boat.Manufacturer = model.Manufacturer.Trim();
            boat.ModelYear = model.ModelYear;
            boat.BoatType = model.IsSailboat ? "Sailboat" : "Powerboat";
            boat.CustomerID = model.CustomerID;

            try
            {
                BoatData.AddBoat(boat);
            }
            catch (Exception ex)
            {
                // AddBoat already converts SQL 2627/2601 into a friendly duplicate-rego message
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Index", RebuildPageModel(model));
            }

            TempData["Success"] = "Boat added successfully.";
            return RedirectToAction("Index");
        }

        // Shared by Index() and Add()'s failure paths — avoids duplicating the
        // customers/overview composition logic in two places.
        private BoatsPageModel RebuildPageModel(BoatFormModel form)
        {
            List<Customer> customers = CustomerData.GetAllCustomers();
            List<Boat> boats = BoatData.GetAllBoats();
            List<Lease> leases = LeaseData.GetAllLeases();

            Dictionary<int, string> customerNames = customers.ToDictionary(c => c.CustomerID, c => c.CustomerName);

            HashSet<string> takenRegoNumbers = leases
                .Where(l => l.EndDate == null || l.EndDate >= DateTime.Today)
                .Select(l => l.StateRegoNo)
                .ToHashSet();

            List<BoatOverviewRow> overview = boats.Select(b => new BoatOverviewRow
            {
                StateRegoNo = b.StateRegoNo,
                Manufacturer = b.Manufacturer,
                BoatType = b.BoatType,
                CustomerName = customerNames.TryGetValue(b.CustomerID, out string? name) ? name : $"Customer #{b.CustomerID}",
                IsTaken = takenRegoNumbers.Contains(b.StateRegoNo)
            }).ToList();

            return new BoatsPageModel { Customers = customers, Overview = overview, Form = form };
        }
    }
}
