using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
	public class LibraryBranch
	{
		public int id { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public string telephone { get; set; }
		public string description { get; set; }
		public DateTime openDate { get; set; }
		public virtual IEnumerable<Patron> patrons { get; set; }
		public virtual IEnumerable<LibraryAsset> LibraryAssets { get; set; }
		public string imageUrl { get; set; }

	}
}
