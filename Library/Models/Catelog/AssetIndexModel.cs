using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.Catelog
{
	public class AssetIndexModel
	{
		public IEnumerable<AssetIndexListingModel> asset { get; set; }
	}
}
