using System.Collections.Generic;
using System.Threading.Tasks;
using Start.Server.Models;

namespace Start.Server.Data.Services.Interfaces {
	public interface IBookmarkGroupService {
		public Task<BookmarkGroup?> GetBookmarkGroup(string userId,
			int bookmarkGroupId, bool includeBookmarks = false);
		public Task<IList<BookmarkGroup>> GetUserBookmarkGroups(string userId,
			bool includeBookmarks = false);

		public Task<BookmarkGroup?> CreateBookmarkGroup(string userId, string title,
			string color, int sortOrder, int bookmarkContainerId);
		public Task<BookmarkGroup?> UpdateBookmarkGroup(string userId,
			BookmarkGroup bookmarkGroup);
		public Task<bool> DeleteBookmarkGroup(string userId, int bookmarkGroupId);
	}
}
