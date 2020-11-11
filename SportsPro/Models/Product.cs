using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsPro.Models
{
	// class that represent an entry in the Products table in database
	public class Product
    {
		public int ProductID { get; set; }

		[Required]
		public string ProductCode { get; set; }

		[Required]
		public string Name { get; set; }

		[Range(0, 1000000)]
		[Column(TypeName = "decimal(8,2)")]
		public decimal YearlyPrice { get; set; }

		public DateTime ReleaseDate { get; set; } = DateTime.Today.Date;

		public ICollection<Registration> Registrations { get; set; }
	}
}
