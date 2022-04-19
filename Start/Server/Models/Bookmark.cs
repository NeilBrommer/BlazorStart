using System.ComponentModel.DataAnnotations;

namespace Start.Server.Models {
	/// <summary>A bookmark with display text and a URL to link to</summary>
	public class Bookmark {
		/// <summary>A unique ID for the bookmark</summary>
		[Key]
		public int BookmarkId { get; set; }

		/// <summary>The text to display for the bookmark</summary>
		[MaxLength(300)]
		public string Title { get; set; }
		/// <summary>The URL the bookmark links to</summary>
		[MaxLength(2000)] // De facto max length of URLs
		public string Url { get; set; }
		/// <summary>Arbitrary notes about the bookmark</summary>
		[MaxLength(5000)]
		public string? Notes { get; set; }
		/// <summary>Used for sorting lists of bookmarks</summary>
		public int SortOrder { get; set; }

		/// <summary>The unique ID for the group the bookmark is in</summary>
		public int BookmarkGroupId { get; set; }
		/// <summary>The group the bookmark is in</summary>
		public BookmarkGroup? BookmarkGroup { get; set; }

		public Bookmark(string title, string url, string? notes, int sortOrder, int bookmarkGroupId) {
			this.Title = title;
			this.Url = url;
			this.Notes = notes;
			this.SortOrder = sortOrder;
			this.BookmarkGroupId = bookmarkGroupId;
		}

		public Bookmark(int bookmarkId, string title, string url, string? notes, int sortOrder,
			int bookmarkGroupId) : this(title, url, notes, sortOrder, bookmarkGroupId) {
			this.BookmarkId = bookmarkId;
		}
	}
}
