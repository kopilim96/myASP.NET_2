using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryServices
{
	public class CheckOutServices : CheckOutInterface
	{
		private LibraryContext _context;
		public CheckOutServices(LibraryContext context)
		{
			_context = context;
		}
		public void add(Checkouts newCheckout)
		{
			_context.Add(newCheckout);
			_context.SaveChanges();
		}

		public IEnumerable<Checkouts> getAll()
		{
			return _context.checkOut;
		}

		public Checkouts getById(int id)
		{
			return getAll()
				.FirstOrDefault(checkoutId => checkoutId.id == id);
		}

		public IEnumerable<CheckoutHistory> getCheckOutHistory(int id)
		{
			//When getting the CheckOut History 
			//It will display 2gether with the 
			//Library Asset which is Books
			//And with the LibraryCard

			return _context.checkOutHistory
				.Include(his => his.libraryAsset)
				.Include(his => his.LibraryCard)
				.Where(his => his.libraryAsset.id == id);
		}

		public IEnumerable<Holds> getCurrentHold(int id)
		{
			//Same as method above
			//It retrived the current Hold data

			return _context.hold
				.Include(hold => hold.LibraryAsset)
				.Where(hold => hold.LibraryAsset.id == id);
		}

		public Checkouts getLatestCheckout(int assetId)
		{
			//Get the Latest checkout 
			return _context.checkOut
				.Where(asset => asset.id == assetId)
				.OrderByDescending(date => date.since)
				.FirstOrDefault();
		}

		private void updateAsset(int assetId, string keyWord)
		{
			var item = _context.libraryAssets
				.FirstOrDefault(a => a.id == assetId);

			_context.Update(item); //Mark this item for update

			item.Status = _context.statuses
				.FirstOrDefault(status => status.name == keyWord);
		}

		private void removeCheckout(int assetId)
		{
			var checkout = _context.checkOut
				.FirstOrDefault(c => c.LibraryAsset.id == assetId);

			if (checkout != null)
			{
				_context.Remove(checkout);
			}
		}

		private void removeCheckOutHis(int assetId, DateTime timeNow)
		{
			var history = _context.checkOutHistory
				.FirstOrDefault(h => h.libraryAsset.id == assetId
				&& h.checkIn == null);

			if (history != null)
			{
				_context.Update(history);
				history.checkIn = timeNow;
			}
		}

		public void markFound(int assetId)
		{
			var timeNow = DateTime.Now;
			updateAsset(assetId, "Available");

			//After Item have found, remove all the existing checkout on this item
			//Poeple borrow this asset and make lost but now its found
			//so the checkout for this asset is no longer exist

			removeCheckout(assetId);

			//if the checkout is not null 
			//we remove it or so called 
			//remove the whole table


			//close existing checkout History
			removeCheckOutHis(assetId, timeNow);


			_context.SaveChanges();

		}

		public void markLost(int assetId)
		{
			updateAsset(assetId, "Lost");

			_context.SaveChanges();
		}


		public void checkInItem(int assetId)
		{
			var timeNow = DateTime.Now;
			var item = _context.libraryAssets
				.FirstOrDefault(asset => asset.id == assetId);

			//Remove any existing checkout on this item
			removeCheckout(assetId);

			//close any existing checkout history
			removeCheckOutHis(assetId, timeNow);

			//Remove the onHold by the patrons
			//if there are onHold -> then checkout the item
			//if not. update the status to available

			var currentHold = _context.hold
				.Include(h => h.LibraryAsset)
				.Include(h => h.LibraryCard)
				.Where(h => h.LibraryAsset.id == assetId);

			if (currentHold.Any())
			{
				checkoutToEarliestHold(assetId, currentHold);
				return;
			}

			updateAsset(assetId, "Available");

			_context.SaveChanges();

		}

		private void checkoutToEarliestHold(int assetId, IQueryable<Holds> currentHold)
		{
			var earlyHold = currentHold
				.OrderBy(h => h.HoldPlaced)
				.FirstOrDefault();

			var card = earlyHold.LibraryCard;

			_context.Remove(earlyHold);
			_context.SaveChanges();
			checkOutItem(assetId, card.id);
		}

		public void checkOutItem(int assetId, int libraryCardId)
		{
			if (isCheckOut(assetId))
			{
				//If item has been checkout 
				//Here can do smtg to notify user
				return;
			}
			var item = _context.libraryAssets
				.FirstOrDefault(a => a.id == assetId);

			updateAsset(assetId, "Checked Out");

			var libraryCard = _context.libraryCards
				.Include(c => c.checkOut)
				.FirstOrDefault(c => c.id == libraryCardId);

			var timeNow = DateTime.Now;

			var checkOut = new Checkouts
			{
				LibraryAsset = item,
				LibraryCard = libraryCard,
				since = timeNow,
				until = getDefaultCheckoutTime(timeNow),
			};

			_context.Add(checkOut);

			var checkOutHis = new CheckoutHistory
			{
				libraryAsset = item,
				LibraryCard = libraryCard,
				checkOut = timeNow,
				//checkIn = 
				//Check in would be null for this i supposed
				//since item just got checked out and 
				//we dont know when user will return 
			};

			_context.Add(checkOutHis);
			_context.SaveChanges();

		}

		private DateTime getDefaultCheckoutTime(DateTime timeNow)
		{
			return timeNow.AddDays(14);
		}

		public bool isCheckOut(int assetId)
		{
			//check if the item has been checkout or not
			var isCheckOut = _context.checkOut
				.Where(co => co.LibraryAsset.id == assetId)
				.Any();

			return isCheckOut;
		}

		public void placeHold(int assetId, int libraryCardId)
		{
			var timeNow = DateTime.Now;
			var asset = _context.libraryAssets
				.Include(a => a.Status)
				.FirstOrDefault(a => a.id == assetId);
			var card = _context.libraryCards
				.FirstOrDefault(c => c.id == libraryCardId);

			if (asset.Status.name == "Available")
			{
				updateAsset(assetId, "On Hold");
			}

			var hold = new Holds
			{
				HoldPlaced = timeNow,
				LibraryAsset = asset,
				LibraryCard = card,
			};

			_context.Add(hold);
			_context.SaveChanges();
		}

		public string getCurrentHoldPatronName(int holdId)
		{
			var hold = _context.hold
				.Include(h => h.LibraryAsset)
				.Include(h => h.LibraryCard)
				.FirstOrDefault(h => h.id == holdId);

			var cardId = hold?.LibraryCard.id;
			var patron = _context.Patrons
				.Include(p => p.libraryCard)
				.FirstOrDefault(p => p.libraryCard.id == cardId);

			//? -> is where the value is null
			//It might be there is no hold in the case
			//so if there is no holding, then it will return null as "?" work

			return patron?.firstName + " " + patron?.lastName;
		}

		public DateTime getCurrentHoldPlaced(int holdId)
		{
			return
				_context.hold
				.Include(h => h.LibraryAsset)
				.Include(h => h.LibraryCard)
				.FirstOrDefault(h => h.id == holdId)
				.HoldPlaced;
		}

		public string getCurrentCheckOutPatronName(int assetId)
		{
			var checkout = _context.checkOut
				.Include(c => c.LibraryAsset)
				.Include(c => c.LibraryCard)
				.FirstOrDefault(c => c.LibraryAsset.id == assetId);
			if (checkout == null) {
				//return "Not Checked Out";
				return "";
			}
			var cardId = checkout.LibraryCard.id;
			var patron = _context.Patrons
				.Include(p => p.libraryCard)
				.FirstOrDefault(p => p.libraryCard.id == cardId);
			return patron.firstName + " " + patron.lastName;
		}

	}
}
