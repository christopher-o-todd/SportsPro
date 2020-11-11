using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace SportsPro.Controllers
{
    [Authorize]
    public class TechIncidentController : Controller
    {
        private ISportsProUnitOfWork data { get; set; }

        public TechIncidentController(ISportsProUnitOfWork ctx)
        {
            data = ctx;
        }

        // method to display technician names in a dropdown menu
        public ViewResult Get()
        {
            var incidentOptions = new QueryOptions<Incident> { Includes = "Customer,Product" };

            var technicianOptions = new QueryOptions<Technician>();
            var customerOptions = new QueryOptions<Customer>();
            var productOptions = new QueryOptions<Product>();
            HttpContext.Session.Remove("sessionID");
            Incident activeIncident = new Incident();
            Technician activeTechnician = new Technician();
            var model = new IncidentViewModel
            {

                ActiveIncident = activeIncident,
                ActiveTechnician = activeTechnician,
                Incidents = data.Incidents.List(incidentOptions),
                Technicians = data.Technicians.List(technicianOptions),
                Customers = data.Customers.List(customerOptions),
                Products = data.Products.List(productOptions)
            };

            return View(model);
        }

        // method to list incidents assigned to a selected technician
        [HttpGet]
        public IActionResult List(int technicianId)
        {
            var incidentOptions = new QueryOptions<Incident>
            {
                Includes = "Customer,Product",
                WhereClauses = new WhereClauses<Incident>
                {
                    {t => t.TechnicianID == technicianId },
                    {i => i.DateClosed == null}
                }           
            };
           
            var technicianOptions = new QueryOptions<Technician>();
            var customerOptions = new QueryOptions<Customer>();
            var productOptions = new QueryOptions<Product>();
            int? sessionID = HttpContext.Session.GetInt32("sessionID");
            if (technicianId == 0 && sessionID == null)
            {
                TempData["message"] = "Please select a technician";
                return RedirectToAction("Get");
            }          
            else
            {
                if (technicianId == 0) technicianId = (int)sessionID;
                var model = new IncidentViewModel();
                model.ActiveTechnician = data.Technicians.Get(technicianId);

                model.Incidents = data.Incidents.List(incidentOptions);

                if (model.Incidents.Count == 0)
                    TempData["message"] = $"No open incidents for this technician.";

                return View(model);
            }
        }

        // method to load details of an incident (assigned to a technician) that is to be editted
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Incident activeIncident = data.Incidents.Get(id);
            var model = new IncidentViewModel
            {
                ActiveIncident = activeIncident,
                Incidents = data.Incidents.List(new QueryOptions<Incident>
                {
                    Includes = "Customer,Product",
                    WhereClauses = new WhereClauses<Incident>
                    {
                        {t => t.IncidentID == id }

                    }
                }),
                Technicians = data.Technicians.List(new QueryOptions<Technician>()),
                Customers = data.Customers.List(new QueryOptions<Customer>()),
                Products = data.Products.List(new QueryOptions<Product>()),
                Action = "Edit"
            };

            HttpContext.Session.SetInt32("sessionID", (int)model.ActiveIncident.TechnicianID);
            return View("Edit", model);
        }

        // method to save revised details of an incident assigned to a technician
        [HttpPost]
        public IActionResult Edit(IncidentViewModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["message"] = $"Incident updated successfully.";
                data.Incidents.Update(model.ActiveIncident);
                data.Incidents.Save();
                return RedirectToAction("List", "TechIncident");
            }
            else
            {
                model.ActiveIncident = data.Incidents.Get(model.ActiveIncident.IncidentID);
                return View("Edit", model);
            }
        }

    }

}
