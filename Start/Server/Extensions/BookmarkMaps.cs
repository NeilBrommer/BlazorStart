﻿using System.Linq;
using Start.Server.Models;
using Start.Shared;

namespace Start.Server.Extensions {
	public static class BookmarkMaps {
		public static BookmarkDto MapToDto(this Bookmark bookmark) {
			return new BookmarkDto(bookmark.BookmarkId, bookmark.Title, bookmark.Url,
				bookmark.Notes, bookmark.SortOrder, bookmark.BookmarkGroupId);
		}

		public static BookmarkGroupDto MapToDto(this BookmarkGroup bookmarkGroup) {
			return new BookmarkGroupDto(bookmarkGroup.BookmarkGroupId, bookmarkGroup.Title,
				bookmarkGroup.Color, bookmarkGroup.SortOrder, bookmarkGroup.BookmarkContainerId,
				bookmarkGroup.Bookmarks?.Select(b => b.MapToDto()).ToList());
		}

		public static BookmarkContainerDto MapToDto(this BookmarkContainer bookmarkContainer) {
			return new BookmarkContainerDto(bookmarkContainer.BookmarkContainerId,
				bookmarkContainer.Title, bookmarkContainer.SortOrder,
				bookmarkContainer.BookmarkGroups?.Select(bg => bg.MapToDto()).ToList());
		}
	}
}
