using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace SportsPro.Controllers
{
    [Authorize (Roles = "Admin")]
    public class CustomerController : Controller
    {
        private ISportsProUnitOfWork data { get; set; }
       

        public CustomerController(ISportsProUnitOfWork unit)
        {
            data = unit;
        }

        [Route("Customers")]

        public IActionResult List()
        {
            var customerOptions = new QueryOptions<Customer>();
            var customers = data.Customers.List(customerOptions);
            return View(customers);
        }

        // action method to load a blank form to add a new customer
        [HttpGet]
        public IActionResult Add()
        {
            HttpContext.Session.SetString("action", "Add");
            var countryOptions = new QueryOptions<Country> { OrderBy = c => c.Name};
            ViewBag.Action = "Add";
            ViewBag.Countries = data.Countries.List(countryOptions);
            return View("AddEdit", new Customer());
        }

        // action method to load selected customer info to edit page
        [HttpGet]
        public IActionResult Edit(int id)
        {
            HttpContext.Session.SetString("action", "Edit");
            var countryOptions = new QueryOptions<Country> { OrderBy = c => c.Name };

            ViewBag.Action = "Edit";
            ViewBag.Countries = data.Countries.List(countryOptions);
            var customer = data.Customers.Get(id);
            return View("AddEdit", customer);
        }

        // action method to save new or revised customer info via post request method
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            string action = HttpContext.Session.GetString("action");
            int duplication;
            if (action == "Add") { duplication = 0; }
            else { duplication = 1; }

            // check if email already in the database
            var validate = new Validate(TempData);
            if (!validate.IsEmailChecked)
            {
                validate.CheckEmail(customer.Email, data.Customers, duplication);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(customer.Email), validate.ErrorMessage);
                }   
            }

            // edit or add only if validation of data on form goes through
            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                    data.Customers.Insert(customer);
                else
                    data.Customers.Update(customer);
                data.Customers.Save();
                validate.ClearEmail();
                TempData["message"] = $"{customer.FullName} {action}ed to customers.";
                return RedirectToAction("List", "Customer");
            }
            else
            {
                return View("Customer", customer);
            }
        }

        // action method to load selected customer data to delete confirmation page 
        [HttpGet]
        public IActionResult Delete(int id)
        {
           
            var customer = data.Customers.Get(id);
            return View(customer);
        }

        // action methoed to accept post request method to delete a customer record
        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            data.Customers.Delete (customer);
            data.Customers.Save();
            return RedirectToAction("List", "Customer");
        }

    }

}


