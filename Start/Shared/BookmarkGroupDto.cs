using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Start.Shared {
	public class BookmarkGroupDto {
		public int BookmarkGroupId { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Title is required")]
		[StringLength(300)]
		public string Title { get; set; }
		[Required(AllowEmptyStrings = false, ErrorMessage = "Color is required")]
		[StringLength(7)]
		public string Color { get; set; }
		public int BookmarkContainerId { get; set; }

		public IList<BookmarkDto>? Bookmarks { get; set; }

		public BookmarkGroupDto(string title, string color, int bookmarkContainerId) {
			this.Title = title;
			this.Color = color;
			this.BookmarkContainerId = bookmarkContainerId;
		}

		public BookmarkGroupDto(int bookmarkGroupId, string title, string color,
			int bookmarkContainerId)
			: this(title, color, bookmarkContainerId) {
			this.BookmarkGroupId = bookmarkGroupId;
		}

		[JsonConstructor]
		public BookmarkGroupDto(int bookmarkGroupId, string title, string color,
			int bookmarkContainerId, IList<BookmarkDto>? bookmarks)
			: this(bookmarkGroupId, title, color, bookmarkContainerId) {
			this.Bookmarks = bookmarks;
		}
	}
}
