using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Extensions;
using Start.Server.Models;

namespace Start.Server.Data.Services {
	public class BookmarkGroupService : IBookmarkGroupService {
		private readonly ApplicationDbContext db;

		public BookmarkGroupService(ApplicationDbContext dbContext) {
			this.db = dbContext;
		}

		public (BookmarkStatus, BookmarkGroup?) GetBookmarkGroup(string userId,
			int bookmarkGroupId, bool includeBookmarks = false) {
			BookmarkGroup? group = db.BookmarkGroups
				.Where(bg => bg.BookmarkGroupId == bookmarkGroupId)
				.If(includeBookmarks, q => q.Include(bg => bg.Bookmarks))
				.SingleOrDefault();

			if (group == null)
				return (BookmarkStatus.BookmarkDoesNotExist, null);

			if (!BookmarkOwnershipTools.IsBookmarkGroupOwner(db, userId, bookmarkGroupId))
				return (BookmarkStatus.OwnerDoesNotMatch, null);

			return (BookmarkStatus.OK, group);
		}

		public IList<BookmarkGroup> GetUserBookmarkGroups(string userId,
			bool includeBookmarkGroups = false) {
			return this.db.BookmarkGroups
				.Where(bg => bg.BookmarkContainer!.ApplicationUserId == userId)
				.If(includeBookmarkGroups, q => q.Include(bg => bg.Bookmarks))
				.ToList();
		}

		public (BookmarkStatus, BookmarkGroup?) CreateBookmarkGroup(string userId, string title,
			string color, int bookmarkContainerId) {
			if (!BookmarkOwnershipTools
				.IsBookmarkContainerOwner(this.db, userId, bookmarkContainerId))
				return (BookmarkStatus.OwnerDoesNotMatch, null);

			BookmarkGroup newBookmarkGroup = new(title, color, bookmarkContainerId);
			this.db.BookmarkGroups.Add(newBookmarkGroup);
			this.db.SaveChanges();

			return (BookmarkStatus.OK, newBookmarkGroup);
		}

		public (BookmarkStatus, BookmarkGroup?) UpdateBookmarkGroup(string userId,
			BookmarkGroup bookmarkGroup) {
			BookmarkGroup? existingGroup = this.db.BookmarkGroups
				.SingleOrDefault(bg => bg.BookmarkGroupId == bookmarkGroup.BookmarkGroupId);

			if (existingGroup == null)
				return (BookmarkStatus.BookmarkDoesNotExist, null);

			if (!BookmarkOwnershipTools
				.IsBookmarkGroupOwner(this.db, userId, bookmarkGroup.BookmarkGroupId))
				return (BookmarkStatus.OwnerDoesNotMatch, null);

			this.db.Entry(bookmarkGroup).State = EntityState.Modified;
			this.db.SaveChanges();

			return (BookmarkStatus.OK, bookmarkGroup);
		}

		public BookmarkStatus DeleteBookmarkGroup(string userId, int bookmarkGroupId) {
			BookmarkGroup? bookmarkGroup = this.db.BookmarkGroups
				.SingleOrDefault(bg => bg.BookmarkGroupId == bookmarkGroupId);

			if (bookmarkGroup == null)
				return BookmarkStatus.BookmarkDoesNotExist;

			if (!BookmarkOwnershipTools.IsBookmarkGroupOwner(this.db, userId, bookmarkGroupId))
				return BookmarkStatus.OwnerDoesNotMatch;

			this.db.BookmarkGroups.Remove(bookmarkGroup);
			this.db.SaveChanges();

			return BookmarkStatus.OK;
		}
	}
}
