using Microsoft.AspNetCore.Mvc;
using PopeyeMarina.Core.Data;
using PopeyeMarina.Core.Models;
using PopeyeMarina.Web.Models;
using PopeyeMarina.Web.ViewModels;
namespace PopeyeMarina.Web.Controllers
{
    public class SlipsController : Controller
    {
        public IActionResult Index()
        {
            var pageModel = new SlipsPageModel
            {
                Slips = SlipData.GetAllSlips(),
                Docks = DockData.GetAllDocks(),
                Form = new SlipFormModel()
            };
            return View(pageModel);
        }

        [HttpPost]
        public IActionResult Save([Bind(Prefix = "Form")] SlipFormModel model)
        {
            // --- Required-field validation, same order as SlipsControl.btnSave_Click ---

            if (model.Width <= 0)
            {
                ModelState.AddModelError(nameof(model.Width), "Enter a valid slip width greater than 0.");
            }

            if (model.SlipLength <= 0)
            {
                ModelState.AddModelError(nameof(model.SlipLength), "Enter a valid slip length greater than 0.");
            }

            if (model.DockID <= 0)
            {
                ModelState.AddModelError(nameof(model.DockID), "Select a dock for this slip.");
            }

            if (model.IsCovered)
            {
                if (model.Height is null || model.Height <= 0)
                {
                    ModelState.AddModelError(nameof(model.Height), "Enter a valid covered-slip height greater than 0.");
                }

                if (string.IsNullOrWhiteSpace(model.DoorType))
                {
                    ModelState.AddModelError(nameof(model.DoorType), "Enter a door type for the covered slip.");
                }
            }

            if (!ModelState.IsValid)
            {
                var pageModel = new SlipsPageModel
                {
                    Slips = SlipData.GetAllSlips(),
                    Docks = DockData.GetAllDocks(),
                    Form = model
                };

                return View("Index", pageModel);
            }
            // --- Persistence (step 5) ---

            Slip slip = model.IsCovered
                ? new CoveredSlip { Height = model.Height!.Value, DoorType = model.DoorType!.Trim() }
                : new Slip();

            slip.Width = model.Width;
            slip.SlipLength = model.SlipLength;
            slip.DockID = model.DockID;
            slip.IsCovered = model.IsCovered;

            try
            {
                if (model.SlipID is null)
                {
                    SlipData.AddSlip(slip);
                }
                else
                {
                    slip.SlipID = model.SlipID.Value;
                    SlipData.UpdateSlip(slip);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Unable to save slip. " + ex.Message);
                var pageModel = new SlipsPageModel
                {
                    Slips = SlipData.GetAllSlips(),
                    Docks = DockData.GetAllDocks(),
                    Form = model
                };

                return View("Index", pageModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Slip? slip = SlipData.GetAllSlips().FirstOrDefault(s => s.SlipID == id);

            if (slip == null)
            {
                return NotFound();
            }

            var model = new SlipFormModel
            {
                SlipID = slip.SlipID,
                Width = slip.Width,
                SlipLength = slip.SlipLength,
                DockID = slip.DockID,
                IsCovered = slip.IsCovered
            };

            if (slip is CoveredSlip coveredSlip)
            {
                model.Height = coveredSlip.Height;
                model.DoorType = coveredSlip.DoorType;
            }

            ViewBag.Docks = DockData.GetAllDocks();
            return View("Index", model);
        }




    }
}