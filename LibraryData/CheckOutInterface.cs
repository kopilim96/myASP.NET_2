using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
	public interface CheckOutInterface
	{
		IEnumerable<Checkouts> getAll();
		Checkouts getById(int id);
		void add(Checkouts newCheckout);
		void checkOutItem(int assetId, int libraryCardId);
		void checkInItem(int assetId);
		Checkouts getLatestCheckout(int assetId);
		IEnumerable<CheckoutHistory> getCheckOutHistory(int id);

		void placeHold(int assetId, int libraryCardId);
		string getCurrentHoldPatronName(int id);
		string getCurrentCheckOutPatronName(int id);
		DateTime getCurrentHoldPlaced(int id);
		IEnumerable<Holds> getCurrentHold(int id);


		// Asset Book Lost and Found
		void markLost(int assetId);
		void markFound(int assetId);

		bool isCheckOut(int assetId);
	}
}
