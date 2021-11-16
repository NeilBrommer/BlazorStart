using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Models;

namespace Start.Server.Data.Services {
	public class BookmarkService : IBookmarkService {
		private readonly ApplicationDbContext db;

		public BookmarkService(ApplicationDbContext dbContext) {
			this.db = dbContext;
		}

		public Bookmark? GetBookmark(string userId, int bookmarkId) {
			if (!BookmarkOwnershipTools.IsBookmarkOwner(this.db, userId, bookmarkId))
				return null;

			return this.db.Bookmarks
				.SingleOrDefault(b => b.BookmarkId == bookmarkId);
		}

		public IList<Bookmark> GetUserBookmarks(string userId) {
			return this.db.Bookmarks
				.Where(b => b.BookmarkGroup!.BookmarkContainer!.ApplicationUserId == userId)
				.ToList();
		}

		public Bookmark? CreateBookmark(string userId, string title, string url, string? notes,
			int bookmarkGroupId) {
			if (!BookmarkOwnershipTools.IsBookmarkGroupOwner(this.db, userId, bookmarkGroupId))
				return null;

			Bookmark newBookmark = new(title, url, bookmarkGroupId);

			db.Bookmarks.Add(newBookmark);
			db.SaveChanges();

			if (newBookmark.BookmarkId <= 0)
				return null;

			return newBookmark;
		}

		public Bookmark? UpdateBookmark(string userId, Bookmark bookmark) {
			Bookmark? existingBookmark = db.Bookmarks
				.SingleOrDefault(b => b.BookmarkId == bookmark.BookmarkId);

			if (!BookmarkOwnershipTools.IsBookmarkOwner(this.db, userId, bookmark.BookmarkId))
				return null;

			// Could be moving to a different group
			if (!BookmarkOwnershipTools
				.IsBookmarkGroupOwner(this.db, userId, bookmark.BookmarkGroupId))
				return null;

			db.Entry(bookmark).State = EntityState.Modified;
			db.SaveChanges();

			return bookmark;
		}

		public bool DeleteBookmark(string userId, int bookmarkId) {
			Bookmark? bookmark = db.Bookmarks
				.SingleOrDefault(b => b.BookmarkId == bookmarkId);

			if (bookmark == null)
				return false;

			if (!BookmarkOwnershipTools.IsBookmarkOwner(this.db, userId, bookmarkId))
				return false;

			db.Bookmarks.Remove(bookmark);
			db.SaveChanges();

			return true;
		}
	}
}
