using System.Collections.Generic;
using System.Linq;
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

		public BookmarkContainer? GetBookmarkContainer(string userId,
			int bookmarkContainerId, bool includeGroups = false, bool includeBookmarks = false) {
			BookmarkContainer? bookmarkContainer = this.db.BookmarkContainers
				.Where(bc => bc.BookmarkContainerId == bookmarkContainerId)
				.If(includeGroups, q => q.Include(bc => bc.BookmarkGroups))
				.If(includeBookmarks, q => q
					.Include(bc => bc.BookmarkGroups)
					.ThenInclude(bg => bg.Bookmarks))
				.SingleOrDefault();

			if (bookmarkContainer == null)
				return null;

			if (!BookmarkOwnershipTools
				.IsBookmarkContainerOwner(this.db, userId, bookmarkContainerId))
				return null;

			return bookmarkContainer;
		}

		public IList<BookmarkContainer> GetUserBookmarkContainers(string userId,
			bool includeGroups = false, bool includeBookmarks = false) {
			return this.db.BookmarkContainers
				.Where(bc => bc.ApplicationUserId == userId)
				.If(includeGroups, q => q.Include(bc => bc.BookmarkGroups))
				.If(includeBookmarks, q => q
					.Include(bc => bc.BookmarkGroups)
					.ThenInclude(bg => bg.Bookmarks))
				.ToList();
		}

		public BookmarkContainer? CreateBookmarkContainer(string userId,
			string title) {
			// No need to worry about ownership here

			BookmarkContainer newContainer = new(userId, title);
			this.db.BookmarkContainers.Add(newContainer);
			return newContainer;
		}

		public BookmarkContainer? UpdateBookmarkContainer(string userId,
			BookmarkContainer bookmarkContainer) {
			BookmarkContainer? exitingBookmarkContainer = this.db.BookmarkContainers
				.SingleOrDefault(bc => bc.BookmarkContainerId
					== bookmarkContainer.BookmarkContainerId);

			if (exitingBookmarkContainer == null
				|| !BookmarkOwnershipTools
				.IsBookmarkContainerOwner(this.db, userId, bookmarkContainer.BookmarkContainerId))
				return null;

			this.db.Entry(bookmarkContainer).State = EntityState.Modified;
			this.db.SaveChanges();

			return bookmarkContainer;
		}

		public bool DeleteBookmarkContainer(string userId, int bookmarkContainerId) {
			BookmarkContainer? bookmarkContainer = this.db.BookmarkContainers
				.Where(bc => bc.BookmarkContainerId == bookmarkContainerId)
				.SingleOrDefault();

			if (bookmarkContainer == null)
				return false;

			if (!BookmarkOwnershipTools.IsBookmarkContainerOwner(this.db, userId, bookmarkContainerId))
				return false;

			this.db.BookmarkContainers.Remove(bookmarkContainer);
			this.db.SaveChanges();

			return true;
		}
	}
}
