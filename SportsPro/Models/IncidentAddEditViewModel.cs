using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
	// ViewModel to pass data to the Views/Incident/AddEdit.cshtml file
    public class IncidentAddEditViewModel
    {
		public List<Customer> Customers { get; set; }
		public Customer ActiveCust { get; set; }
		public string FilterString { get; set; }
		public Incident ActiveIncident { get; set; }
		public Technician ActiveTechnician { get; set; }
		public string Action { get; set; }
		public List<Product> Products { get; set; }
		
		private List<Incident> incidents;
		public List<Incident> Incidents
		{
			get => incidents;
			set
			{
				incidents = value;
			}
		}

		
		private List<Technician> technicians;
		public List<Technician> Technicians 
		{
			get => technicians;
			set 
			{
				technicians = value;
			}
		}

		public string CheckActiveIncident(int i) => i == ActiveIncident.IncidentID ? "active" : "";
		public string CheckActiveTechnician(int t) => t == ActiveTechnician.TechnicianID ? "active" : "";
		
	}
}