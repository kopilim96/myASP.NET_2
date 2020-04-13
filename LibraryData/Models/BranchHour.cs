using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData.Models
{
	public class BranchHour
	{
		public int id { get; set; }
		public LibraryBranch branch { get; set; }
		public int dayOfWeek { get; set; }
		public int openTime { get; set; }
		public int closeTime { get; set; }

	}
}
