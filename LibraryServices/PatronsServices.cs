using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryServices
{
	public class PatronsServices : PatronsInterface
	{
		private readonly LibraryContext _context;

		public PatronsServices(LibraryContext context) {
			_context = context;
		}
		public void addPatrons(Patron newPatron)
		{
			_context.Add(newPatron);
			_context.SaveChanges();
		}
		public Patron getPatron(int patronId)
		{
			return getAllPatron()
				.FirstOrDefault(p => p.patronId == patronId);
		}
		public IEnumerable<Patron> getAllPatron()
		{
			return _context.Patrons
				.Include(p => p.libraryCard)
				.Include(p => p.LibraryBranch);
		}

		public IEnumerable<Checkouts> getAllCheckout(int patronId)
		{
			var cardId = getPatron(patronId).libraryCard.id;
			return _context.checkOut
				.Include(c => c.LibraryAsset)
				.Include(c => c.LibraryCard)
				.Where(c => c.LibraryCard.id == cardId);
		}

		public IEnumerable<CheckoutHistory> getAllCheckOutHistory(int patronId)
		{
			var cardId = getPatron(patronId).libraryCard.id;
			return _context.checkOutHistory
				.Include(co => co.libraryAsset)
				.Include(co => co.LibraryCard)
				.Where(co => co.LibraryCard.id == cardId)
				.OrderBy(co => co.checkOut);
		}

		public IEnumerable<Holds> getAllHold(int patronId)
		{
			var cardId = getPatron(patronId).libraryCard.id;
			return _context.hold
				.Include(h => h.LibraryAsset)
				.Include(h => h.LibraryCard)
				.Where(h => h.LibraryCard.id == cardId)
				.OrderByDescending(h => h.HoldPlaced);
		}


	}
}
