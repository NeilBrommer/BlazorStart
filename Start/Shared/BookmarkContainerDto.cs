using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Start.Shared {
	public class BookmarkContainerDto {
		public int BookmarkContainerId { get; set; }
		[Required]
		[StringLength(300)]
		public string Title { get; set; }
		public int SortOrder { get; set; }

		public IList<BookmarkGroupDto>? BookmarkGroups { get; set; }

		public BookmarkContainerDto(string title, int sortOrder) {
			this.Title = title;
			this.SortOrder = sortOrder;
		}

		public BookmarkContainerDto(int bookmarkContainerId, string title, int sortOrder)
			: this(title, sortOrder) {
			this.BookmarkContainerId = bookmarkContainerId;
		}

		[JsonConstructor]
		public BookmarkContainerDto(int bookmarkContainerId, string title, int sortOrder,
			IList<BookmarkGroupDto>? bookmarkGroups) : this(bookmarkContainerId, title, sortOrder) {
			this.BookmarkGroups = bookmarkGroups;
		}
	}
}
