using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Models;

namespace Start.Server.Data.Services {
	public class BookmarkContainerService : IBookmarkContainerService {
		private readonly ApplicationDbContext db;

		public BookmarkContainerService(ApplicationDbContext dbContext) {
			this.db = dbContext;
		}

		public (BookmarkStatus, BookmarkContainer?) GetBookmarkContainer(string userId,
			int bookmarkContainerId, bool includeGroups = false, bool includeBookmarks = false) {
			BookmarkContainer? bookmarkContainer = this.db.BookmarkContainers
				.Where(bc => bc.BookmarkContainerId == bookmarkContainerId)
				.If(includeGroups, q => q.Include(bc => bc.BookmarkGroups))
				.If(includeBookmarks, q => q
					.Include(bc => bc.BookmarkGroups)
					.ThenInclude(bg => bg.Bookmarks))
				.SingleOrDefault();

			if (bookmarkContainer == null)
				return (BookmarkStatus.BookmarkDoesNotExist, null);

			if (!BookmarkOwnershipTools
				.IsBookmarkContainerOwner(this.db, userId, bookmarkContainerId))
				return (BookmarkStatus.OwnerDoesNotMatch, null);

			return (BookmarkStatus.OK, bookmarkContainer);
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

		public (BookmarkStatus, BookmarkContainer?) CreateBookmarkContainer(string userId,
			string title) {
			BookmarkContainer newContainer = new(userId, title);
			this.db.BookmarkContainers.Add(newContainer);
			return (BookmarkStatus.OK, newContainer);
		}

		public (BookmarkStatus, BookmarkContainer?) UpdateBookmarkContainer(string userId,
			BookmarkContainer bookmarkContainer) {
			BookmarkContainer? exitingBookmarkContainer = this.db.BookmarkContainers
				.SingleOrDefault(bc => bc.BookmarkContainerId
					== bookmarkContainer.BookmarkContainerId);

			if (exitingBookmarkContainer == null)
				return (BookmarkStatus.BookmarkDoesNotExist, null);

			if (!BookmarkOwnershipTools
				.IsBookmarkContainerOwner(this.db, userId, bookmarkContainer.BookmarkContainerId))
				return (BookmarkStatus.OwnerDoesNotMatch, null);

			this.db.Entry(bookmarkContainer).State = EntityState.Modified;
			this.db.SaveChanges();

			return (BookmarkStatus.OK, bookmarkContainer);
		}

		public BookmarkStatus DeleteBookmarkContainer(string userId, int bookmarkContainerId) {
			BookmarkContainer? bookmarkContainer = this.db.BookmarkContainers
				.Where(bc => bc.BookmarkContainerId == bookmarkContainerId)
				.SingleOrDefault();

			if (bookmarkContainer == null)
				return (BookmarkStatus.BookmarkDoesNotExist);

			if (!BookmarkOwnershipTools.IsBookmarkContainerOwner(this.db, userId, bookmarkContainerId))
				return BookmarkStatus.OwnerDoesNotMatch;

			this.db.BookmarkContainers.Remove(bookmarkContainer);
			this.db.SaveChanges();

			return BookmarkStatus.OK;
		}
	}
}
