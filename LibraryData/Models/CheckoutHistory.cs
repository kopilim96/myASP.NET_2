using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
	public class CheckoutHistory
	{
		public int id { get; set; }
		public LibraryAsset libraryAsset { get; set; }
		public LibraryCard LibraryCard { get; set; }
		public DateTime checkOut { get; set; }
		public DateTime? checkIn { get; set; }
	}
}
