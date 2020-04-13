using LibraryData.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryData
{
	public class LibraryContext : DbContext
	{
		public LibraryContext() { }
		public LibraryContext(DbContextOptions options) : base(options) { }
		public DbSet<Patron> Patrons { get; set; }
		public DbSet<Video> videos { get; set; }
		public DbSet<BranchHour> branchHours { get; set; }
		public DbSet<Book> books { get; set; }
		public DbSet<Checkouts> checkOut { get; set; }
		public DbSet<CheckoutHistory> checkOutHistory { get; set; }
		public DbSet<Holds> hold { get; set; }
		public DbSet<LibraryAsset> libraryAssets { get; set; }
		public DbSet<LibraryBranch> libraryBranches { get; set; }
		public DbSet<LibraryCard> libraryCards { get; set; }
		public DbSet<Status> statuses { get; set; }

	}
}
