using System;
using System.ComponentModel.DataAnnotations;

namespace Start.Shared {
	public class BookmarkDto {
		public int BookmarkId { get; set; }
		[StringLength(300)]
		public string Title { get; set; }
		[StringLength(2000)]
		public string Url { get; set; }
		[StringLength(5000)]
		public string? Notes { get; set; }

		public BookmarkDto(string title, string url, string? notes) {
			this.Title = title;
			this.Url = url;
			this.Notes = notes;
		}

		public BookmarkDto(int bookmarkId, string title, string url, string? notes)
			: this(title, url, notes) {
			this.BookmarkId = bookmarkId;
		}
	}
}
