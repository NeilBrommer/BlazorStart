using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Microsoft.EntityFrameworkCore;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Models;

namespace Start.Server.Data.Services {
	public class BookmarkService : IBookmarkService {
		private readonly ApplicationDbContext db;

		public BookmarkService(ApplicationDbContext dbContext) {
			this.db = dbContext;
		}

		public (BookmarkStatus, Bookmark?) GetBookmark(string userId, int bookmarkId) {
			if (!BookmarkOwnershipTools.IsBookmarkOwner(this.db, userId, bookmarkId))
				return (BookmarkStatus.OwnerDoesNotMatch, null);

			Bookmark? bookmark = this.db.Bookmarks
				.SingleOrDefault(b => b.BookmarkId == bookmarkId);

			if (bookmark == null)
				return (BookmarkStatus.BookmarkDoesNotExist, null);

			return (BookmarkStatus.OK, bookmark);
		}

		public IList<Bookmark> GetUserBookmarks(string userId) {
			return this.db.Bookmarks
				.Where(b => b.BookmarkGroup!.BookmarkContainer!.ApplicationUserId == userId)
				.ToList();
		}

		public (BookmarkStatus, Bookmark?) CreateBookmark(string userId, string title, string url, string? notes,
			int bookmarkGroupId) {
			if (!BookmarkOwnershipTools.IsBookmarkGroupOwner(this.db, userId, bookmarkGroupId))
				return (BookmarkStatus.OwnerDoesNotMatch, null);

			Bookmark newBookmark = new(title, url, bookmarkGroupId);

			db.Bookmarks.Add(newBookmark);
			db.SaveChanges();

			return (BookmarkStatus.OK, newBookmark);
		}

		public (BookmarkStatus, Bookmark?) UpdateBookmark(string userId, Bookmark bookmark) {
			Bookmark? existingBookmark = db.Bookmarks
				.SingleOrDefault(b => b.BookmarkId == bookmark.BookmarkId);

			if (existingBookmark == null)
				return (BookmarkStatus.BookmarkDoesNotExist, null);

			if (!BookmarkOwnershipTools.IsBookmarkOwner(this.db, userId, bookmark.BookmarkId))
				return (BookmarkStatus.OwnerDoesNotMatch, null);

			if (!BookmarkOwnershipTools
				.IsBookmarkGroupOwner(this.db, userId, bookmark.BookmarkGroupId))
				return (BookmarkStatus.OwnerDoesNotMatch, null);

			db.Entry(bookmark).State = EntityState.Modified;
			db.SaveChanges();

			return (BookmarkStatus.OK, bookmark);
		}

		public BookmarkStatus DeleteBookmark(string userId, int bookmarkId) {
			Bookmark? bookmark = db.Bookmarks
				.SingleOrDefault(b => b.BookmarkId == bookmarkId);

			if (bookmark == null)
				return BookmarkStatus.BookmarkDoesNotExist;

			if (!BookmarkOwnershipTools.IsBookmarkOwner(this.db, userId, bookmarkId))
				return BookmarkStatus.OwnerDoesNotMatch;

			db.Bookmarks.Remove(bookmark);
			db.SaveChanges();

			return BookmarkStatus.OK;
		}
	}
}
