using LibraryData.Models;
using System;
using System.Collections.Generic;

namespace Library.Models.Patrons
{
	public class PatronsDetailModel
	{
		public int id { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public int libraryCardId { get; set; }
		public string address { get; set; }
		public DateTime memberSince { get; set; }
		public string telephone { get; set; }
		public string libraryBranchName { get; set; }
		public decimal overdueFee { get; set; }
		public IEnumerable<Checkouts> assetCheckout { get; set; }
		public IEnumerable<CheckoutHistory> checkoutHistory { get; set; }
		public IEnumerable<Holds> hold { get; set; }
	}
}
