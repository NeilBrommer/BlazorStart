using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Start.Server.Data.Services.Interfaces;
using Start.Server.Models;

namespace Start.Server.Data.Services {
	public class BookmarkService : IBookmarkService {
		private readonly ApplicationDbContext db;

		public BookmarkService(ApplicationDbContext dbContext) {
			this.db = dbContext;
		}

		public async Task<Bookmark?> GetBookmark(string userId, int bookmarkId) {
			if (!BookmarkOwnershipTools.IsBookmarkOwner(this.db, userId, bookmarkId))
				return null;

			return await this.db.Bookmarks
				.SingleOrDefaultAsync(b => b.BookmarkId == bookmarkId);
		}

		public async Task<IList<Bookmark>> GetUserBookmarks(string userId) {
			return await this.db.Bookmarks
				.Where(b => b.BookmarkGroup!.BookmarkContainer!.ApplicationUserId == userId)
				.ToListAsync();
		}

		public async Task<Bookmark?> CreateBookmark(string userId, string title, string url,
			string? notes, int sortOrder, int bookmarkGroupId) {
			if (!BookmarkOwnershipTools.IsBookmarkGroupOwner(this.db, userId, bookmarkGroupId))
				return null;

			Bookmark newBookmark = new(title, url, notes, sortOrder, bookmarkGroupId);

			await db.Bookmarks.AddAsync(newBookmark);
			await db.SaveChangesAsync();

			if (newBookmark.BookmarkId <= 0)
				return null;

			return newBookmark;
		}

		public async Task<Bookmark?> UpdateBookmark(string userId, Bookmark bookmark) {
			Bookmark? existingBookmark = await db.Bookmarks
				.SingleOrDefaultAsync(b => b.BookmarkId == bookmark.BookmarkId);

			if (existingBookmark == null)
				return null;

			if (!BookmarkOwnershipTools.IsBookmarkOwner(this.db, userId, bookmark.BookmarkId))
				return null;

			// Could be moving to a different group
			if (!BookmarkOwnershipTools
				.IsBookmarkGroupOwner(this.db, userId, bookmark.BookmarkGroupId))
				return null;

			if (bookmark.BookmarkGroupId != existingBookmark.BookmarkGroupId) {
				// The bookmark was moved to a different group - shuffle sort order around
				List<Bookmark>? oldGroupBookmarks = await db.Bookmarks
					.Where(b => b.BookmarkGroupId == existingBookmark.BookmarkGroupId)
					.Where(b => b.SortOrder > existingBookmark.SortOrder)
					.ToListAsync();

				oldGroupBookmarks.ForEach(b => b.SortOrder -= 1);

				List<Bookmark>? newGroupBookmarks = await db.Bookmarks
					.Where(b => b.BookmarkGroupId == bookmark.BookmarkGroupId)
					.Where(b => b.SortOrder >= bookmark.SortOrder)
					.ToListAsync();

				newGroupBookmarks.ForEach(b => b.SortOrder += 1);
			}
			else if (bookmark.SortOrder != existingBookmark.SortOrder) {
				// The bookmark has been moved within the same group

				List<Bookmark>? groupBookmarks = await db.Bookmarks
					.Where(b => b.BookmarkGroupId == bookmark.BookmarkGroupId)
					.Where(b => b.BookmarkId != bookmark.BookmarkId)
					.ToListAsync();

				groupBookmarks
					.Where(b => b.SortOrder > existingBookmark.SortOrder)
					.ToList()
					.ForEach(b => b.SortOrder -=1);

				groupBookmarks
					.Where(b => b.SortOrder > bookmark.SortOrder)
					.ToList()
					.ForEach(b => b.SortOrder += 1);
			}

			db.Entry(bookmark).State = EntityState.Modified;
			await db.SaveChangesAsync();

			return bookmark;
		}

		public async Task<bool> DeleteBookmark(string userId, int bookmarkId) {
			Bookmark? bookmark = db.Bookmarks
				.SingleOrDefault(b => b.BookmarkId == bookmarkId);

			if (bookmark == null)
				return false;

			if (!BookmarkOwnershipTools.IsBookmarkOwner(this.db, userId, bookmarkId))
				return false;

			db.Bookmarks.Remove(bookmark);
			await db.SaveChangesAsync();

			return true;
		}
	}
}
