using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryServices
{
	public class LibraryAssetService : LibraryInterface
	{
		private readonly LibraryContext _context;

		public LibraryAssetService(LibraryContext context)
		{
			_context = context;
		}
		public void add(LibraryAsset newAsset)
		{
			//Add to newAsset and save the changes made
			_context.Add(newAsset);
			_context.SaveChanges();
		}

		public IEnumerable<LibraryAsset> getAll()
		{
			//When Calling getAll IEnumberable LibraryAsset
			//It return all the status and the library Branch

			return _context.libraryAssets
				.Include(a => a.Status)
				.Include(a => a.LibraryBranch);


		}

		public LibraryAsset getByID(int id)
		{
			//This method call back getAll() from above and 
			//get use of its method return
			//If there is ntg -> return Null or so called Default
			//Otherwise it will return id 
			return getAll()
				.FirstOrDefault(asset => asset.id == id);
		}

		public LibraryBranch getCurrentLocation(int id)
		{
			//same goes here but here we will need 
			//to get the location from the branch
			return getByID(id)
				.LibraryBranch;
		}

		public string getDeweyIndex(int id)
		{
			if (_context.books.Any(book => book.id == id))
			{
				return _context.books
					.FirstOrDefault(book => book.id == id).deweyIndex;
			}
			else
				return "";
		}

		public string getISBN(int id)
		{
			if (_context.books.Any(book => book.id == id))
			{
				return _context.books
					.FirstOrDefault(book => book.id == id).isbn;
			}
			else
				return "";
		}

		public string getTitle(int id)
		{
			return _context.libraryAssets
				.FirstOrDefault(a => a.id == id).title;
		}

		public string getType(int id)
		{
			//Check the type if it is a book
			var book = _context.libraryAssets.OfType<Book>()
				.Where(b => b.id == id);

			return book.Any() ? "Book" : "Video";
		}
		public string getAuthor(int id)
		{
			//Since we have to ensure the author is from Video 
			//or it is from Book
			//So we made the variable and with the condition

			var isBook = _context.libraryAssets.OfType<Book>()
				.Where(asset => asset.id == id).Any();
			var isVideo = _context.libraryAssets.OfType<Video>()
				.Where(asset => asset.id == id).Any();

			return isBook ?
				_context.books.FirstOrDefault(book => book.id == id).author :
				_context.videos.FirstOrDefault(video => video.id == id).director
				?? "I am not sure what the fuck this is ... so I return Unknow";

			// A ?? B  
			//If it is not null return A 
			//Else it return B which is comment in this case
		}
	}
}
