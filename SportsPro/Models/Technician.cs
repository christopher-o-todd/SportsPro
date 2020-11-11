using System;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
	// class that represent an entry in the Technicians table in database
	public class Technician
    {
		public int TechnicianID { get; set; }	   

		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Phone { get; set; }
	}
}
