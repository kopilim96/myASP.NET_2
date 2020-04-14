using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
	public interface PatronsInterface
	{
		//add Patrons
		void addPatrons(Patron newPatron);

		//GetAllProterties from Patron
		Patron getPatron(int patronId);

		//Get collection of Patron, Checkout, CheckoutHistory, Hold
		IEnumerable<Patron> getAllPatron(int patronId);
		IEnumerable<Checkouts> getAllCheckout(int patronId);
		IEnumerable<CheckoutHistory> getAllCheckOutHistory(int patronId);
		IEnumerable<Holds> getAllHold(int patronId);
	}
}
