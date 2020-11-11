using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportsPro.Models
{
	// class that represent an entry in the Countries table in database
	public class Country
    {
		[Required]
		public string CountryID { get; set; }

		[Required]
		public string Name { get; set; }
	}
}
