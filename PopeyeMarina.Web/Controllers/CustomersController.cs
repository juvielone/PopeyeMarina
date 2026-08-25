using Microsoft.AspNetCore.Mvc;
using PopeyeMarina.Core.Data;
using PopeyeMarina.Core.Models;

namespace PopeyeMarina.Web.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            var customers = CustomerData.GetAllCustomers();

            return View(customers);
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            customer.CustomerName = customer.CustomerName?.Trim();
            customer.Address = customer.Address?.Trim();
            customer.PhoneNo = customer.PhoneNo?.Trim();

            if (string.IsNullOrWhiteSpace(customer.CustomerName) ||
                string.IsNullOrWhiteSpace(customer.Address) ||
                string.IsNullOrWhiteSpace(customer.PhoneNo))
            {
                ModelState.AddModelError(
                    "",
                    "Name, Address, and Phone are required."
                );

                var customers = CustomerData.GetAllCustomers();
                return View("Index", customers);
            }

            try
            {
                CustomerData.AddCustomer(customer);

                TempData["SuccessMessage"] = "Customer added successfully.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError(
                    "",
                    "Unable to add the customer. Please try again."
                );

                var customers = CustomerData.GetAllCustomers();
                return View("Index", customers);
            }
        }


    }
}