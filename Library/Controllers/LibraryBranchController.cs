using Library.Models.LibraryBranch;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Controllers
{
	public class LibraryBranchController : Controller
	{
		private readonly LibraryBranchInterface _branch;

		public LibraryBranchController(LibraryBranchInterface branch)
		{
			_branch = branch;
		}

		public IActionResult Index()
		{
			var branch1 = _branch.getAllBranch()
				.Select(branch => new LibraryBranchDetailModel
				{
					branchId = branch.id,
					branchName = branch.name,
					branchPhone = branch.telephone,
					address = branch.address,
					imageUrl = branch.imageUrl,
					isOpen = _branch.isBranchOpen(branch.id),
					numberOfAsset = _branch.getAsset(branch.id).Count(),
					numberOfPatron = _branch.getPatrons(branch.id).Count(),
				});

			//Store to the Collection
			var model = new LibraryBranchIndexModel()
			{
				branch = branch1
			};

			return View(model);
		}

		public IActionResult LibraryBranchDetails(int id) {
			var branch = _branch.get(id);
			var model = new LibraryBranchDetailModel
			{
				branchId = branch.id,
				branchName = branch.name,
				address = branch.address,
				description = branch.description,
				branchPhone = branch.telephone,
				openDate = branch.openDate.ToString("yyyy-MM-dd"),
				numberOfAsset = _branch.getAsset(branch.id).Count(),
				numberOfPatron = _branch.getPatrons(branch.id).Count(),
				totalAssetValue = _branch.getAsset(branch.id).Sum(a => a.cost),
				imageUrl = branch.imageUrl,
				hourOpen = _branch.getBranchHour(id),
			};

			return View(model);
		}
	}
}
