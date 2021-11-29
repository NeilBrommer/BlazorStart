using System.Collections.Generic;
using System.Threading.Tasks;
using Start.Server.Models;

namespace Start.Server.Data.Services.Interfaces {
	public interface IBookmarkContainerService {
		public Task<BookmarkContainer?> GetBookmarkContainer(string userId,
			int bookmarkContainerId, bool includeGroups = false, bool includeBookmarks = false);
		public Task<IList<BookmarkContainer>> GetUserBookmarkContainers(string userId,
			bool includeGroups = false, bool includeBookmarks = false);

		public Task<BookmarkContainer?> CreateBookmarkContainer(string userId,
			string title);
		public Task<BookmarkContainer?> UpdateBookmarkContainer(string userId,
			BookmarkContainer bookmarkContainer);
		public Task<bool> DeleteBookmarkContainer(string userId, int bookmarkContainerId);
	}
}
