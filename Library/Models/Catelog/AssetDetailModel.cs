using LibraryData.Models;
using System;
using System.Collections.Generic;

namespace Library.Models.Catelog
{
	public class AssetDetailModel
	{
		public int assetId { get; set; }
		public string title { get; set; }
		public string author { get; set; }
		public string type { get; set; }
		public int year { get; set; }
		public string isbn { get; set; }
		public string deweyIndex { get; set; }
		public string status { get; set; }
		public decimal cost { get; set; }
		public string currentBranch { get; set; }
		public string imageUrl { get; set; }
		public string patronName { get; set; }
		public Checkouts latestCheckOut { get; set; }
		public IEnumerable<CheckoutHistory> checkoutHistories { get; set; }
		public IEnumerable<AssetHoldModel> currentHold { get; set; }
	}

	public class AssetHoldModel
	{
		public string patronName { get; set; }
		public DateTime holdPlaced { get; set; }
	}
}
