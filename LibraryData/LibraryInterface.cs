using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryData
{
	public interface LibraryInterface
	{
		IEnumerable<LibraryAsset> getAll();
		LibraryAsset getByID(int id);
		void add(LibraryAsset newAsset);
		string getAuthor(int id);
		string getDeweyIndex(int id);
		string getType(int id);
		string getTitle(int id);
		string getISBN(int id);
		LibraryBranch getCurrentLocation(int id);
	}
}
