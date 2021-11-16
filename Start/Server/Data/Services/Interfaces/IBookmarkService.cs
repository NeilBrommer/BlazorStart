using System;
using System.Collections.Generic;
using Start.Server.Models;
using Start.Shared;

namespace Start.Server.Data.Services.Interfaces {
	public interface IBookmarkService {
		public Bookmark? GetBookmark(string userId, int bookmarkId);
		public IList<Bookmark> GetUserBookmarks(string userId);

		public Bookmark? CreateBookmark(string userId, string title, string url,
			string? notes, int bookmarkGroupId);
		public Bookmark? UpdateBookmark(string userId, Bookmark bookmark);
		public bool DeleteBookmark(string userId, int bookmarkId);
	}
}
