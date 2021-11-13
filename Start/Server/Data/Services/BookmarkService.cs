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
			if (!IsBookmarkOwner(userId, bookmarkId))
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

		public Bookmark CreateBookmark(string userId, string title, string url, string? notes,
			int bookmarkGroupId) {
			if (!this.IsBookmarkGroupOwner(userId, bookmarkGroupId))
				throw new SecurityException(
					"The provided user ID doesn't match the bookmark group owner ID");

			Bookmark newBookmark = new(title, url, bookmarkGroupId);

			db.Bookmarks.Add(newBookmark);
			db.SaveChanges();

			return newBookmark;
		}

		public (BookmarkStatus, Bookmark?) UpdateBookmark(string userId, Bookmark bookmark) {
			Bookmark? existingBookmark = db.Bookmarks
				.SingleOrDefault(b => b.BookmarkId == bookmark.BookmarkId);

			if (existingBookmark == null)
				return (BookmarkStatus.BookmarkDoesNotExist, null);

			if (!IsBookmarkOwner(userId, bookmark.BookmarkId))
				return (BookmarkStatus.OwnerDoesNotMatch, null);

			if (!IsBookmarkGroupOwner(userId, bookmark.BookmarkGroupId))
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

			if (!IsBookmarkOwner(userId, bookmarkId))
				return BookmarkStatus.OwnerDoesNotMatch;

			db.Bookmarks.Remove(bookmark);
			db.SaveChanges();

			return BookmarkStatus.OK;
		}

		private bool IsBookmarkOwner(string userId, int bookmarkId) {
			string? bookmarkOwnerId = this.db.Bookmarks
				.Where(b => b.BookmarkId == bookmarkId)
				.Select(b => b.BookmarkGroup!.BookmarkContainer!.ApplicationUserId)
				.SingleOrDefault();

			return userId == bookmarkOwnerId;
		}

		private bool IsBookmarkGroupOwner(string userId, int bookmarkGroupId) {
			string? groupOwnerId = this.db.BookmarkGroups
				.Where(bg => bg.BookmarkGroupId == bookmarkGroupId)
				.Select(bg => bg.BookmarkContainer!.ApplicationUserId)
				.SingleOrDefault();

			return userId == groupOwnerId;
		}
	}
}
