using System;
using System.Linq;

namespace Start.Server.Data.Services {
	public static class BookmarkOwnershipTools {
		public static bool IsBookmarkOwner(ApplicationDbContext db, string userId, int bookmarkId) {
			string? bookmarkOwnerId = db.Bookmarks
				.Where(b => b.BookmarkId == bookmarkId)
				.Select(b => b.BookmarkGroup!.BookmarkContainer!.ApplicationUserId)
				.SingleOrDefault();

			return userId == bookmarkOwnerId;
		}

		public static bool IsBookmarkGroupOwner(ApplicationDbContext db, string userId,
			int bookmarkGroupId) {
			string? groupOwnerId = db.BookmarkGroups
				.Where(bg => bg.BookmarkGroupId == bookmarkGroupId)
				.Select(bg => bg.BookmarkContainer!.ApplicationUserId)
				.SingleOrDefault();

			return userId == groupOwnerId;
		}

		public static bool IsBookmarkContainerOwner(ApplicationDbContext db, string userId,
			int bookmarkContainerId) {
			string? containerOwnerId = db.BookmarkContainers
				.Where(bc => bc.BookmarkContainerId == bookmarkContainerId)
				.Select(bc => bc.ApplicationUserId)
				.SingleOrDefault();

			return userId == containerOwnerId;
		}
	}
}
