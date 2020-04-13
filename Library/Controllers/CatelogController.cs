using Library.Models.Catelog;
using Library.Models.CheckOut;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
	public class CatelogController : Controller
	{
		private readonly LibraryInterface _asset;
		private readonly CheckOutInterface _checkout;
		public CatelogController(LibraryInterface asset, CheckOutInterface checkout)
		{
			_asset = asset;
			_checkout = checkout;
		}

		public IActionResult Index()
		{
			var assetModels = _asset.getAll();

			var listingResult = assetModels
				.Select(result => new AssetIndexListingModel
				{
					id = result.id,
					imageUrl = result.imageUrl,
					author = _asset.getAuthor(result.id),
					deweyIndex = _asset.getDeweyIndex(result.id),
					title = result.title,
					type = _asset.getType(result.id),
				});
			var model = new AssetIndexModel()
			{
				asset = listingResult
			};
			return View(model);
		}

		public IActionResult Details(int id)
		{
			var asset = _asset.getByID(id);

			var currentHold = _checkout.getCurrentHold(id)
				.Select(a => new AssetHoldModel
				{
					holdPlaced = _checkout.getCurrentHoldPlaced(a.id),
					patronName = _checkout.getCurrentHoldPatronName(a.id)
				});

			var model = new AssetDetailModel
			{
				assetId = id,
				title = asset.title,
				year = asset.year,
				cost = asset.cost,
				status = asset.Status.name,
				imageUrl = asset.imageUrl,
				type = _asset.getType(id),
				author = _asset.getAuthor(id),
				currentBranch = _asset.getCurrentLocation(id).name,
				deweyIndex = _asset.getDeweyIndex(id),
				isbn = _asset.getISBN(id),
				checkoutHistories = _checkout.getCheckOutHistory(id),
				latestCheckOut = _checkout.getLatestCheckout(id),
				patronName = _checkout.getCurrentCheckOutPatronName(id),
				currentHold = currentHold,
			};

			return View(model);
		}

		public IActionResult CheckOut(int id) {

			var asset = _asset.getByID(id);
			var model = new CheckOutModel
			{
				assetId = id,
				imageUrl = asset.imageUrl,
				title = asset.title,
				libraryCardId = "",
				isCheckout = _checkout.isCheckOut(id),
			};

			return View(model);
		}
		public IActionResult CheckIn(int id) {
			_checkout.checkInItem(id);
			return RedirectToAction("Details", new { id = id });
		}

		public IActionResult Hold(int id)
		{

			var asset = _asset.getByID(id);
			var model = new CheckOutModel
			{
				assetId = id,
				imageUrl = asset.imageUrl,
				title = asset.title,
				libraryCardId = "",
				isCheckout = _checkout.isCheckOut(id),
				holdCount = _checkout.getCurrentHold(id).Count(),
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult PlaceCheckOut(int assetId, int libraryCardId) {
			_checkout.checkOutItem(assetId, libraryCardId);
			return RedirectToAction("Details", new { id = assetId});
		}

		/*
		[HttpPost]
		public IActionResult PlaceCheckIn(int assetId, int libraryCardId)
		{
			_checkout.checkInItem(assetId);
			return RedirectToAction("Details", new { id = assetId });
		}
		-------------Didnt use
		*/

		[HttpPost]
		public IActionResult PlaceHold(int assetId, int libraryCardId)
		{
			_checkout.placeHold(assetId, libraryCardId);
			return RedirectToAction("Details", new { id = assetId });
		}

		public IActionResult MarkFound(int assetId) {
			_checkout.markFound(assetId);
			return RedirectToAction("Details", new { id = assetId });
		}

		public IActionResult MarkLost(int assetId)
		{
			_checkout.markLost(assetId);
			return RedirectToAction("Details", new { id = assetId });
		}
	}
}
