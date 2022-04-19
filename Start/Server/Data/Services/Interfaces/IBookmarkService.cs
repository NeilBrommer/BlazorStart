using System.Collections.Generic;
using System.Threading.Tasks;
using Start.Server.Models;

namespace Start.Server.Data.Services.Interfaces {
	public interface IBookmarkService {
		public Task<Bookmark?> GetBookmark(string userId, int bookmarkId);
		public Task<IList<Bookmark>> GetUserBookmarks(string userId);

		public Task<Bookmark?> CreateBookmark(string userId, string title, string url,
			string? notes, int sortOrder, int bookmarkGroupId);
		public Task<Bookmark?> UpdateBookmark(string userId, Bookmark bookmark);
		public Task<bool> DeleteBookmark(string userId, int bookmarkId);
	}
}
