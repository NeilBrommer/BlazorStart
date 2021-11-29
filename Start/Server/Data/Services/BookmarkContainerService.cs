using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Extensions;
using Start.Server.Models;

namespace Start.Server.Data.Services {
	public class BookmarkContainerService : IBookmarkContainerService {
		private readonly ApplicationDbContext db;

		public BookmarkContainerService(ApplicationDbContext dbContext) {
			this.db = dbContext;
		}

		public async Task<BookmarkContainer?> GetBookmarkContainer(string userId,
			int bookmarkContainerId, bool includeGroups = false, bool includeBookmarks = false) {
			BookmarkContainer? bookmarkContainer = await this.db.BookmarkContainers
				.Where(bc => bc.BookmarkContainerId == bookmarkContainerId)
				.If(includeGroups, q => q.Include(bc => bc.BookmarkGroups))
				.If(includeBookmarks, q => q
					.Include(bc => bc.BookmarkGroups)
					.ThenInclude(bg => bg.Bookmarks))
				.SingleOrDefaultAsync();

			if (bookmarkContainer == null)
				return null;

			if (!BookmarkOwnershipTools
				.IsBookmarkContainerOwner(this.db, userId, bookmarkContainerId))
				return null;

			return bookmarkContainer;
		}

		public async Task<IList<BookmarkContainer>> GetUserBookmarkContainers(string userId,
			bool includeGroups = false, bool includeBookmarks = false) {
			return await this.db.BookmarkContainers
				.Where(bc => bc.ApplicationUserId == userId)
				.If(includeGroups, q => q.Include(bc => bc.BookmarkGroups))
				.If(includeBookmarks, q => q
					.Include(bc => bc.BookmarkGroups)
					.ThenInclude(bg => bg.Bookmarks))
				.ToListAsync();
		}

		public async Task<BookmarkContainer?> CreateBookmarkContainer(string userId,
			string title) {
			// No need to worry about ownership here

			BookmarkContainer newContainer = new(userId, title);
			await this.db.BookmarkContainers.AddAsync(newContainer);
			await this.db.SaveChangesAsync();
			return newContainer;
		}

		public async Task<BookmarkContainer?> UpdateBookmarkContainer(string userId,
			BookmarkContainer bookmarkContainer) {
			BookmarkContainer? exitingBookmarkContainer = await this.db.BookmarkContainers
				.SingleOrDefaultAsync(bc => bc.BookmarkContainerId
					== bookmarkContainer.BookmarkContainerId);

			if (exitingBookmarkContainer == null
				|| !BookmarkOwnershipTools
				.IsBookmarkContainerOwner(this.db, userId, bookmarkContainer.BookmarkContainerId))
				return null;

			this.db.Entry(bookmarkContainer).State = EntityState.Modified;
			await this.db.SaveChangesAsync();

			return bookmarkContainer;
		}

		public async Task<bool> DeleteBookmarkContainer(string userId, int bookmarkContainerId) {
			BookmarkContainer? bookmarkContainer = await this.db.BookmarkContainers
				.Where(bc => bc.BookmarkContainerId == bookmarkContainerId)
				.SingleOrDefaultAsync();

			if (bookmarkContainer == null)
				return false;

			if (!BookmarkOwnershipTools.IsBookmarkContainerOwner(this.db, userId, bookmarkContainerId))
				return false;

			this.db.BookmarkContainers.Remove(bookmarkContainer);
			await this.db.SaveChangesAsync();

			return true;
		}
	}
}
