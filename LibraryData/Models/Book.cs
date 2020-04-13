using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
	public class Book : LibraryAsset
	{
		public string isbn { get; set; }
		public string author { get; set; }
		public string deweyIndex { get; set; }
	}
}
