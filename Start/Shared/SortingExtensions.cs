using System.Collections.Generic;
using System.Linq;

namespace Start.Shared {
	public static class SortingExtensions {
		public static IEnumerable<BookmarkContainerDto> SortContainers(
			this IEnumerable<BookmarkContainerDto> bookmarkContainers) {
			return bookmarkContainers
				.OrderBy(bc => bc.SortOrder)
				.ThenBy(bc => bc.BookmarkContainerId);
		}

		public static IEnumerable<BookmarkGroupDto> SortGroups(
			this IEnumerable<BookmarkGroupDto> bookmarkGroups) {
			return bookmarkGroups
				.OrderBy(bg => bg.SortOrder)
				.ThenBy(bg => bg.BookmarkGroupId);
		}

		public static IEnumerable<BookmarkDto> SortBookmarks(
			this IEnumerable<BookmarkDto> bookmarks) {
			return bookmarks
				.OrderBy(b => b.SortOrder)
				.ThenBy(b => b.BookmarkId);
		}
	}
}
