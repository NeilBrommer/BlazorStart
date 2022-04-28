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
					.Include(bc => bc.BookmarkGroups!)
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
					.Include(bc => bc.BookmarkGroups!)
					.ThenInclude(bg => bg.Bookmarks))
				.ToListAsync();
		}

		public async Task<BookmarkContainer?> CreateBookmarkContainer(string userId,
			string title, int sortOrder) {
			// No need to worry about ownership here

			// Increase the sorting ID for these items if it's needed to make room for this item
			List<BookmarkContainer>? containers = this.db.BookmarkContainers
				.Where(bc => bc.ApplicationUserId == userId)
				.SortContainers()
				.ToList();

			if (containers == null)
				return null;

			// Fix up sort order just in case
			for (int i = 0; i < containers.Count; i++) {
				containers[i].SortOrder = i;

				// Make room for the new container
				if (i >= sortOrder)
					containers[i].SortOrder++;
			}

			BookmarkContainer newContainer = new(userId, title, sortOrder);
			await this.db.BookmarkContainers.AddAsync(newContainer);
			await this.db.SaveChangesAsync();
			return newContainer;
		}

		public async Task<BookmarkContainer?> UpdateBookmarkContainer(string userId,
			BookmarkContainer bookmarkContainer) {
			BookmarkContainer? existingBookmarkContainer = await this.db.BookmarkContainers
				.SingleOrDefaultAsync(bc =>
					bc.BookmarkContainerId == bookmarkContainer.BookmarkContainerId);

			if (existingBookmarkContainer == null
				|| !BookmarkOwnershipTools
				.IsBookmarkContainerOwner(this.db, userId, bookmarkContainer.BookmarkContainerId))
				return null;

			// If the sort order has changed, then the other containers need to be shuffled around
			if (existingBookmarkContainer.SortOrder < bookmarkContainer.SortOrder) {
				// The container has been moved to a higher sort order
				var affectedContainers = db.BookmarkContainers
					.Where(bc => bc.ApplicationUserId == userId)
					.Where(bc => bc.SortOrder > existingBookmarkContainer.SortOrder)
					.Where(bc => bc.SortOrder <= bookmarkContainer.SortOrder)
					.ToList();

				affectedContainers.ForEach(bc => bc.SortOrder -= 1);
				// Let the save changes below save this
			}
			else if (existingBookmarkContainer.SortOrder > bookmarkContainer.SortOrder) {
				// The container has been moved to a lower sort order
				var affectedContainers = db.BookmarkContainers
					.Where(bc => bc.ApplicationUserId == userId)
					.Where(bc => bc.SortOrder < existingBookmarkContainer.SortOrder)
					.Where(bc => bc.SortOrder >= bookmarkContainer.SortOrder)
					.ToList();

				affectedContainers.ForEach(bc => bc.SortOrder += 1);
				// Let the save changes below save this
			}

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

			List<BookmarkContainer>? containers = this.db.BookmarkContainers
				.Where(bc => bc.ApplicationUserId == userId)
				.SortContainers()
				.ToList();

			if (containers == null)
				// The container *was* deleted, so indicate as such
				return true;

			// Fix up sort order just in case
			for (int i = 0; i < containers.Count; i++) {
				containers[i].SortOrder = i;
			}

			await this.db.SaveChangesAsync();

			return true;
		}
	}
}
