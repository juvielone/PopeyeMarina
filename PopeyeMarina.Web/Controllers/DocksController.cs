using Microsoft.AspNetCore.Mvc;
using PopeyeMarina.Core.Data;
using PopeyeMarina.Core.Models;

namespace PopeyeMarina.Web.Controllers
{
    public class DocksController : Controller
    {
        public IActionResult Index()
        {
            var docks = DockData.GetAllDocks();

            return View(docks);
        }

        [HttpPost]
        public IActionResult Add(Dock dock)
        {
            dock.Location = dock.Location?.Trim();

            if (string.IsNullOrWhiteSpace(dock.Location))
            {
                ModelState.AddModelError(
                    "",
                    "Location is required."
                );

                var docks = DockData.GetAllDocks();
                return View("Index", docks);
            }

            try
            {
                DockData.AddDock(dock);

                TempData["SuccessMessage"] = "Dock added successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    "",
                    "Unable to add the dock. Please try again."
                );

                var docks = DockData.GetAllDocks();
                return View("Index", docks);
            }
        }
    }
}