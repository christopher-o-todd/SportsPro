﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
	// ViewModel to pass data to the Views/Registration/ GetCustomer, List, Delete .cshtml files
    public class RegistrationViewModel
    {
		public List<Customer> Customers { get; set; }

		public Customer ActiveCustomer { get; set; }

		public List<Registration> Registrations { get; set; }

		public List<Product> Products { get; set; }
		
		public int CustomerID { get; set; }

		public int ProductID { get; set; }

		public Product ActiveProduct { get; set; }



	}
}