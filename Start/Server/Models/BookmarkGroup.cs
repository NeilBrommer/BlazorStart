using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Start.Server.Models {
	/// <summary>A group of <see cref="Bookmark"/>s</summary>
	public class BookmarkGroup {
		/// <summary>A unique ID for the group</summary>
		[Key]
		public int BookmarkGroupId { get; set; }

		/// <summary>The title of the group</summary>
		[MaxLength(300)]
		public string Title { get; set; }
		/// <summary>A hex color for the group</summary>
		[MaxLength(6)]
		public string Color { get; set; }

		/// <summary>The unique ID of the container this group is in</summary>
		public int BookmarkContainerId { get; set; }
		/// <summary>The container this group is in</summary>
		public BookmarkContainer? BookmarkContainer { get; set; }

		/// <summary>The bookmarks in this group</summary>
		public List<Bookmark>? Bookmarks { get; set; }

		public BookmarkGroup(string title, string color, int bookmarkContainerId) {
			this.Title = title;
			this.Color = color;
			this.BookmarkContainerId = bookmarkContainerId;
		}

		public BookmarkGroup(int bookmarkGroupId, string title, string color,
			int bookmarkContainerId) : this(title, color, bookmarkContainerId) {
			this.BookmarkGroupId = bookmarkGroupId;
		}
	}
}
