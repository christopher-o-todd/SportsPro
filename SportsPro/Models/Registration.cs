using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
	// a linking entity class that connect a customer to a product in their corresponding tables. This facilitates a many-to-many relationship
    public class Registration
    {
		public int ProductID { get; set; }
		public int CustomerID { get; set; }

		public List<Product> Products { get; set; }

		public Product Product { get; set; }
		
		public Customer Customer { get; set; }


	}
}
