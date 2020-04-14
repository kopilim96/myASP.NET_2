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
			return _context.Patrons
				.Include(p => p.libraryCard)
				.Include(p => p.LibraryBranch)
				.FirstOrDefault(p => p.patronId == patronId);
		}

		public IEnumerable<Checkouts> getAllCheckout(int patronId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CheckoutHistory> getAllCheckOutHistory(int patronId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Holds> getAllHold(int patronId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Patron> getAllPatron(int patronId)
		{
			throw new NotImplementedException();
		}

	}
}
