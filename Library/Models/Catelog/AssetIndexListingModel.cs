using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Catelog
{
	//This class is use to set the thing
	//that we want to show instead of showing 
	//all thing 
	public class AssetIndexListingModel
	{
		public int id { get; set; }
		public string imageUrl { get; set; }
		public string title { get; set; }
		public string author { get; set; }
		public string type { get; set; }
		public string deweyIndex { get; set; }
	}
}
