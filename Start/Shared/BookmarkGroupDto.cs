using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Start.Shared {
	public class BookmarkGroupDto {
		public int BookmarkGroupId { get; set; }
		[StringLength(300)]
		public string Title { get; set; }
		[StringLength(6)]
		public string Color { get; set; }

		public IList<BookmarkDto>? Bookmarks { get; set; }

		public BookmarkGroupDto(string title, string color) {
			this.Title = title;
			this.Color = color;
		}

		public BookmarkGroupDto(int bookmarkGroupId, string title, string color)
			: this(title, color) {
			this.BookmarkGroupId = bookmarkGroupId;
		}

		public BookmarkGroupDto(int bookmarkGroupId, string title, string color,
			IList<BookmarkDto>? bookmarks) : this(bookmarkGroupId, title, color) {
			this.Bookmarks = bookmarks;
		}
	}
}
