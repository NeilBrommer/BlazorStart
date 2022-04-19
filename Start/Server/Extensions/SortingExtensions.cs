using System.Collections.Generic;
using System.Linq;
using Start.Server.Models;

namespace Start.Server.Extensions {
	public static class SortingExtensions {
		public static IEnumerable<BookmarkContainer> SortContainers(
			this IEnumerable<BookmarkContainer> bookmarkContainers) {
			return bookmarkContainers
				.OrderBy(bc => bc.SortOrder)
				.ThenBy(bc => bc.BookmarkContainerId);
		}

		public static IEnumerable<BookmarkGroup> SortGroups(
			this IEnumerable<BookmarkGroup> bookmarkGroups) {
			return bookmarkGroups
				.OrderBy(bg => bg.SortOrder)
				.ThenBy(bg => bg.BookmarkGroupId);
		}

		public static IEnumerable<Bookmark> SortBookmarks(
			this IEnumerable<Bookmark> bookmarks) {
			return bookmarks
				.OrderBy(b => b.SortOrder)
				.ThenBy(b => b.BookmarkId);
		}
	}
}
