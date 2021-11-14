using System;
using System.Collections.Generic;
using Start.Server.Models;

namespace Start.Server.Data.Services.Interfaces {
	public interface IBookmarkContainerService {
		public (BookmarkStatus, BookmarkContainer?) GetBookmarkContainer(string userId,
			int bookmarkContainerId, bool includeGroups = false, bool includeBookmarks = false);
		public IList<BookmarkContainer> GetUserBookmarkContainers(string userId,
			bool includeGroups = false, bool includeBookmarks = false);

		public (BookmarkStatus, BookmarkContainer?) CreateBookmarkContainer(string userId,
			string title);
		public (BookmarkStatus, BookmarkContainer?) UpdateBookmarkContainer(string userId,
			BookmarkContainer bookmarkContainer);
		public BookmarkStatus DeleteBookmarkContainer(string userId, int bookmarkContainerId);
	}
}
