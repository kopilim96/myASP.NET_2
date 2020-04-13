using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
	public class Checkouts
	{
		public int id { get; set; }
		public LibraryAsset LibraryAsset { get; set; }
		public LibraryCard LibraryCard { get; set; }
		public DateTime since { get; set; }
		public DateTime until { get; set; }
	}
}
