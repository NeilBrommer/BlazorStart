using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Start.Shared {
	public class BookmarkContainerDto {
		public int BookmarkContainerId { get; set; }
		[Required]
		[StringLength(300)]
		public string Title { get; set; }

		public IList<BookmarkGroupDto>? BookmarkGroups { get; set; }

		public BookmarkContainerDto(string title) {
			this.Title = title;
		}

		public BookmarkContainerDto(int bookmarkContainerId, string title) : this(title) {
			this.BookmarkContainerId = bookmarkContainerId;
		}

		[JsonConstructor]
		public BookmarkContainerDto(int bookmarkContainerId, string title,
			IList<BookmarkGroupDto>? bookmarkGroups) : this(bookmarkContainerId, title) {
			this.BookmarkGroups = bookmarkGroups;
		}
	}
}
