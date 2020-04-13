using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace LibraryData.Models
{
	public abstract class LibraryAsset
	{
		public int id { get; set; }
		public string title { get; set; }
		public int year { get; set; }
		public Status Status { get; set; }
		public decimal cost { get; set; }
		public string imageUrl { get; set; }
		public int numberOfCopies { get; set; }
		public virtual LibraryBranch LibraryBranch { get; set; }
	}
}
