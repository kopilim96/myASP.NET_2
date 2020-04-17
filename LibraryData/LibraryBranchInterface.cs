using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
	public interface LibraryBranchInterface
	{
		IEnumerable<LibraryBranch> getAllBranch();
		IEnumerable<Patron> getPatrons(int branchId);
		IEnumerable<LibraryAsset> getAsset(int branchId);
		IEnumerable<string> getBranchHour(int branchId);

		void add(LibraryBranch newBranch);
		bool isBranchOpen(int branchId);

		LibraryBranch get(int branchId);
	}
}
