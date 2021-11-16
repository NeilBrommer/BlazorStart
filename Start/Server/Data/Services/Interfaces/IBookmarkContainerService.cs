using System;
using System.Collections.Generic;
using Start.Server.Models;
using Start.Shared;

namespace Start.Server.Data.Services.Interfaces {
	public interface IBookmarkContainerService {
		public BookmarkContainer? GetBookmarkContainer(string userId,
			int bookmarkContainerId, bool includeGroups = false, bool includeBookmarks = false);
		public IList<BookmarkContainer> GetUserBookmarkContainers(string userId,
			bool includeGroups = false, bool includeBookmarks = false);

		public BookmarkContainer? CreateBookmarkContainer(string userId,
			string title);
		public BookmarkContainer? UpdateBookmarkContainer(string userId,
			BookmarkContainer bookmarkContainer);
		public bool DeleteBookmarkContainer(string userId, int bookmarkContainerId);
	}
}
