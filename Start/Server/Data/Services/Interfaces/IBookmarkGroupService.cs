using System;
using System.Collections.Generic;
using Start.Server.Models;
using Start.Shared;

namespace Start.Server.Data.Services.Interfaces {
	public interface IBookmarkGroupService {
		public BookmarkGroup? GetBookmarkGroup(string userId,
			int bookmarkGroupId, bool includeBookmarks = false);
		public IList<BookmarkGroup> GetUserBookmarkGroups(string userId,
			bool includeBookmarks = false);

		public BookmarkGroup? CreateBookmarkGroup(string userId, string title,
			string color, int bookmarkContainerId);
		public BookmarkGroup? UpdateBookmarkGroup(string userId,
			BookmarkGroup bookmarkGroup);
		public bool DeleteBookmarkGroup(string userId, int bookmarkGroupId);
	}
}
