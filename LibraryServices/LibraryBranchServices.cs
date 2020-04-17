using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryServices
{
	public class LibraryBranchServices : LibraryBranchInterface
	{
		private readonly LibraryContext _context;

		public LibraryBranchServices(LibraryContext context)
		{
			_context = context;
		}

		public void add(LibraryBranch newBranch)
		{
			_context.Add(newBranch);
			_context.SaveChanges();
		}

		public LibraryBranch get(int branchId)
		{
			return getAllBranch()
				.FirstOrDefault(b => b.id == branchId);
		}

		public IEnumerable<LibraryBranch> getAllBranch()
		{
			return _context.libraryBranches
				.Include(b => b.LibraryAssets)
				.Include(b => b.patrons);
		}

		public IEnumerable<LibraryAsset> getAsset(int branchId)
		{
			return _context.libraryBranches
				.Include(b => b.LibraryAssets)
				.FirstOrDefault(b => b.id == branchId)
				.LibraryAssets;
		}

		public IEnumerable<string> getBranchHour(int branchId)
		{
			var hours = _context.branchHours
				.Where(h => h.branch.id == branchId);
			return DataHelpers.readableHourMethod(hours);
		}

		public IEnumerable<Patron> getPatrons(int branchId)
		{
			return _context.libraryBranches
				.Include(b => b.patrons)
				.FirstOrDefault(b => b.id == branchId)
				.patrons;
		}

		public bool isBranchOpen(int branchId)
		{
			var currentTime = DateTime.Now.Hour;
			var currentDayofWeek = (int)DateTime.Now.DayOfWeek + 1;
			var hours = _context.branchHours
				.Where(h => h.branch.id == branchId);
			var dayHour = hours.FirstOrDefault(h => h.dayOfWeek == currentDayofWeek);

			return currentTime < dayHour.closeTime && currentTime > dayHour.openTime;
		}
	}
}
