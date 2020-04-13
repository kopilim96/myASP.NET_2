using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
	public class LibraryCard
	{
		public int id { get; set; }
		public decimal fees { get; set; }
		public DateTime created { get; set; }
		public virtual IEnumerable<Checkouts> checkOut { get; set; }

	}
}
