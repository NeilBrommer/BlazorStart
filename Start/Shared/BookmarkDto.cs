using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Start.Shared {
	public class BookmarkDto {
		public int BookmarkId { get; set; }
		[StringLength(300)]
		public string Title { get; set; }
		[StringLength(2000)]
		public string Url { get; set; }
		[StringLength(5000)]
		public string? Notes { get; set; }
		public int SortOrder { get; set; }

		public int BookmarkGroupId { get; set; }

		public BookmarkDto(string title, string url, string? notes, int sortOrder,
			int bookmarkGroupId) {
			this.Title = title;
			this.Url = url;
			this.Notes = notes;
			this.SortOrder = sortOrder;
			this.BookmarkGroupId = bookmarkGroupId;
		}

		[JsonConstructor]
		public BookmarkDto(int bookmarkId, string title, string url, string? notes, int sortOrder,
			int bookmarkGroupId)
			: this(title, url, notes, sortOrder, bookmarkGroupId) {
			this.BookmarkId = bookmarkId;
		}
	}
}
