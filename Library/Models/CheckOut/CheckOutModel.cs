using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.CheckOut
{
	public class CheckOutModel
	{
		public string libraryCardId { get; set; }
		public string title { get; set; }
		public int assetId { get; set; }
		public string imageUrl { get; set; }
		public int holdCount { get; set; }
		public bool isCheckout { get; set; }
	}
}
