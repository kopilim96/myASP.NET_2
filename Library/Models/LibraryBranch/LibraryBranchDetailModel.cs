using System.Collections.Generic;

namespace Library.Models.LibraryBranch
{
	public class LibraryBranchDetailModel
	{
		public int branchId { get; set; }
		public string branchName { get; set; }
		public string address { get; set; }
		public string openDate { get; set; }
		public string branchPhone { get; set; }
		public string description { get; set; }
		public string imageUrl { get; set; }


		public bool isOpen { get; set; }
		public int numberOfPatron { get; set; }
		public int numberOfAsset { get; set; }
		public decimal totalAssetValue { get; set; }

		public IEnumerable<string> hourOpen { get; set; }
	}
}
