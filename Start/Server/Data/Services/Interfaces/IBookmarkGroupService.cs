using System;
using System.Collections.Generic;
using Start.Server.Models;
using Start.Shared;

namespace Start.Server.Data.Services.Interfaces {
	public interface IBookmarkGroupService {
		public (BookmarkStatus, BookmarkGroup?) GetBookmarkGroup(string userId,
			int bookmarkGroupId, bool includeBookmarks = false);
		public IList<BookmarkGroup> GetUserBookmarkGroups(string userId,
			bool includeBookmarks = false);

		public (BookmarkStatus, BookmarkGroup?) CreateBookmarkGroup(string userId, string title,
			string color, int bookmarkContainerId);
		public (BookmarkStatus, BookmarkGroup?) UpdateBookmarkGroup(string userId,
			BookmarkGroup bookmarkGroup);
		public BookmarkStatus DeleteBookmarkGroup(string userId, int bookmarkGroupId);
	}
}
